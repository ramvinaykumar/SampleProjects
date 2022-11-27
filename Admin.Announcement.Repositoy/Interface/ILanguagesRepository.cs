using Admin.Announcement.Models.Entities;

namespace Admin.Announcement.Repositoy.Interface
{
    public interface ILanguagesRepository
    {
        Task<IEnumerable<Language>> GetLanguages(CancellationToken cancellationToken);
    }
}
