using Admin.Announcement.Models.CmsKeys;

namespace Admin.Announcement.Repositoy.Interface
{
    public interface IContentClient
    {
        Task<CMSResponse> GetContentAsync(MicroContentRequest microContentRequest, CancellationToken token);
    }   
}
