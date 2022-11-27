using Admin.Announcement.Models;
using Admin.Announcement.Models.Entities;
using CampaignEntity = Admin.Announcement.Models.Entities.Campaign;

namespace Admin.Announcement.Repositoy.Interface
{
    public interface ICampaignsRepository
    {
        Task<IEnumerable<CampaignEntity>> GetCampaigns(bool? isActive, CampaignSection? campaignType);
        Task<IEnumerable<LocalizedMessage>> GetLocalizedMessageForWidget(string region, string languageCode, string audience);
        Task<CampaignEntity> SaveCampaign(CampaignEntity campaign);
        Task<bool> UpdateCampaign(CampaignEntity campaign);
        Task<bool> ArchiveCampaign(string id, string userName, CancellationToken token);
    }
}
