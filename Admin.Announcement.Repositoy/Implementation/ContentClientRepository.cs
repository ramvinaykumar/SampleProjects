using Admin.Announcement.Models.CmsKeys;
using Admin.Announcement.Models.Configuration;
using Admin.Announcement.Repositoy.Interface;
using Microsoft.Extensions.Options;

namespace Admin.Announcement.Repositoy.Implementation
{
    public class ContentClientRepository : HttpClient, IContentClient

    {
        private IOptionsMonitor<ApplicationConfiguration> _configSpring;

        public ContentClientRepository(HttpClient httpClient, IOptionsMonitor<ApplicationConfiguration> configSpring) : base(httpClient)
        {
            _configSpring = configSpring;
        }

        public async Task<CMSResponse> GetContentAsync(MicroContentRequest microContentRequest, CancellationToken token)
        {
            var endPoint = new Uri(_configSpring.CurrentValue.CmsKeyEndPoint);
            return await Post<CMSResponse, MicroContentRequest>(endPoint, microContentRequest, token);
        }
    }
}
