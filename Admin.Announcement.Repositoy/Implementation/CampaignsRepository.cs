using Admin.Announcement.Core.Helpers;
using Admin.Announcement.Models;
using Admin.Announcement.Models.Entities;
using Admin.Announcement.Repositoy.Interface;
using MongoDB.Driver;
using CampaignEntity = Admin.Announcement.Models.Entities.Campaign;

namespace Admin.Announcement.Repositoy.Implementation
{
    public class CampaignsRepository : ICampaignsRepository
    {
        private readonly IMongoCollection<CampaignEntity> _campaignEntityCollection;


        public CampaignsRepository(ISolutionsMongoClient mongoClient)
        {
            _campaignEntityCollection = mongoClient.Collection<CampaignEntity>();
        }

        public async Task<IEnumerable<CampaignEntity>> GetCampaigns(bool? isActive, CampaignSection? campaignType)
        {
            var queryBuilder = Builders<CampaignEntity>.Filter;
            var query = queryBuilder.Eq(d => d.IsActive, isActive);
            switch (campaignType)
            {
                case CampaignSection.Future:
                    query &= queryBuilder.Gte(d => d.StartDateTime, DateTime.UtcNow.Date.AddDays(1));
                    break;
                case CampaignSection.Historical:
                    query &= queryBuilder.Lt(d => d.EndDateTime, DateTime.UtcNow.Date);
                    break;
                default:
                    query &= queryBuilder.Lt(d => d.StartDateTime, DateTime.UtcNow.Date.AddDays(1));
                    query &= queryBuilder.Gte(d => d.EndDateTime, DateTime.UtcNow.Date);
                    break;
            }
            var result = await _campaignEntityCollection.FindAsync(query);
            return result.ToEnumerable();
        }
        public async Task<IEnumerable<LocalizedMessage>> GetLocalizedMessageForWidget(string region, string languageCode, string audience)
        {
            var currentDate = Common.GetCurrentDateTime();
            var queryBuilder = Builders<CampaignEntity>.Filter;
            var languageCodeFilter = Builders<LocalizedMessage>.Filter;
            var query = queryBuilder.Eq(d => d.IsActive, true);
            query &= queryBuilder.AnyIn(d => d.Regions, new[] { region });
            query &= queryBuilder.AnyIn(d => d.Audiences, new[] { audience });
            query &= queryBuilder.ElemMatch(d => d.LocalizedMessages,
                languageCodeFilter.Eq(d => d.LanguageCode, languageCode));
            query &= queryBuilder.Lte(d => d.StartDateTime, currentDate);
            query &= queryBuilder.Gte(d => d.EndDateTime, currentDate);

            var queryResult = await _campaignEntityCollection.FindAsync(query);
            return queryResult.ToEnumerable()
                .Select(r => r.LocalizedMessages.FirstOrDefault(lm => !string.IsNullOrEmpty(lm.LanguageCode)));
        }

        public async Task<CampaignEntity> SaveCampaign(CampaignEntity campaign)
        {
            campaign.CreatedOn = DateTime.Now;
            campaign.ModifiedOn = null;
            await _campaignEntityCollection.InsertOneAsync(campaign);
            return string.IsNullOrWhiteSpace(campaign.Id) ? null : campaign;
        }

        public async Task<bool> UpdateCampaign(CampaignEntity campaign)
        {
            var filterBuilder = Builders<CampaignEntity>.Filter;
            FilterDefinition<CampaignEntity> filterDefinition = filterBuilder.Eq(rn => rn.Id, campaign.Id);
            var queryResponse = await _campaignEntityCollection.FindAsync(filterDefinition);
            var existingDocument = queryResponse.FirstOrDefault();
            if (existingDocument == null)
                return false;
            campaign.CreatedOn = existingDocument.CreatedOn;
            campaign.ModifiedOn = DateTime.Now;
            var options = new ReplaceOptions
            {
                IsUpsert = false
            };
            var result = await _campaignEntityCollection.ReplaceOneAsync(filterDefinition, campaign, options);
            return result.IsAcknowledged
                && result.IsModifiedCountAvailable
                && result.MatchedCount > 0;
        }

        public async Task<bool> ArchiveCampaign(string id, string userName, CancellationToken token)
        {
            var result = await _campaignEntityCollection.FindOneAndUpdateAsync(
                Builders<CampaignEntity>.Filter.Eq(c => c.Id, id),
                Builders<CampaignEntity>.Update.Set(c => c.IsActive, false)
                .Set(c => c.ModifiedBy, userName)
                .Set(c => c.ModifiedOn, DateTime.Now), cancellationToken: token);
            return result != null;
        }
    }
}
