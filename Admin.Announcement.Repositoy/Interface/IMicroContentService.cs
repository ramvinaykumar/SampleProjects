using Admin.Announcement.Models.CmsKeys;

namespace Admin.Announcement.Repositoy.Interface
{
    public interface IMicroContentService
    {
        Task<Dictionary<string, string>> GetCMSCollection(string country, string language, string region, CancellationToken token);
        Task<Dictionary<string, string>> GetCmsCollectionFromProvider(MicroContentRequest microContentRequest, CancellationToken cancellationToken);
        Task<string> GetTranslationAsync(string cmsKey, string defaultValue, string country, string language, string region, CancellationToken token);
        string GetTranslation(string cmsKey, string defaultValue, string country, string language, string region);
    }
}
