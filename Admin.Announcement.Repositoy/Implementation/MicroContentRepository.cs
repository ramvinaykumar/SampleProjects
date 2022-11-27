using Admin.Announcement.Models.CmsKeys;
using Admin.Announcement.Models.Configuration;
using Admin.Announcement.Repositoy.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Announcement.Repositoy.Implementation
{
    public class MicroContentRepository : IMicroContentService
    {
        private readonly IContentClient _cmsServiceClient;
        private IOptionsMonitor<ApplicationConfiguration> _configSpring;

        public MicroContentRepository(IContentClient cmsServiceClient, IOptionsMonitor<ApplicationConfiguration> configSpring)
        {
            _cmsServiceClient = cmsServiceClient;
            _configSpring = configSpring;
        }

        public async Task<string> GetTranslationAsync(string cmsKey, string defaultValue, string country, string language, string region, CancellationToken token)
        {
            var cmsDictionary = await GetCMSCollection(country, language, region, token);
            var result = string.Empty;
            if (cmsDictionary != null && cmsDictionary.ContainsKey(cmsKey))
                result = cmsDictionary[cmsKey];
            return string.IsNullOrEmpty(result) ? defaultValue : result;
        }

        public string GetTranslation(string cmsKey, string defaultValue, string country, string language, string region)
        {
            var cmsDictionary = GetCMSCollection(country, language, region, CancellationToken.None).GetAwaiter().GetResult();
            var result = string.Empty;
            if (cmsDictionary != null && cmsDictionary.ContainsKey(cmsKey))
                result = cmsDictionary[cmsKey];
            return string.IsNullOrEmpty(result) ? defaultValue : result;
        }

        public async Task<Dictionary<string, string>> GetCMSCollection(string country, string language, string region, CancellationToken token)
        {
            var collectionName = _configSpring.CurrentValue.CmsCollectionName;
            var microContentRequest = new MicroContentRequest()
            {
                CollectionName = collectionName,
                Country = country,
                Language = language,
                Region = region
            };
            return await GetCmsCollectionFromProvider(microContentRequest, token);
        }

        public async Task<Dictionary<string, string>> GetCmsCollectionFromProvider(MicroContentRequest microContentRequest, CancellationToken cancellationToken)
        {
            var cmsDictionary = new Dictionary<string, string>();
            microContentRequest.CollectionName = _configSpring.CurrentValue.CmsCollectionName;
            var cmsResponse = await _cmsServiceClient.GetContentAsync(microContentRequest, cancellationToken);
            if (cmsResponse != null && cmsResponse.queryResponse != null && cmsResponse.queryResponse.Items != null && cmsResponse.queryResponse.Items.Count > 0)
            {
                Dictionary<string, string> cmsCollectionDictionary = new Dictionary<string, string>();
                foreach (var cmsItem in cmsResponse.queryResponse.Items)
                {
                    var key = cmsItem.Key;
                    if (!cmsCollectionDictionary.ContainsKey(key))
                    {
                        var value = cmsItem.Value?.MicroContentValue;
                        cmsCollectionDictionary.Add(key, value);
                    }
                }
                cmsDictionary = cmsCollectionDictionary;
            }
            return cmsDictionary;
        }
    }

}
