using Admin.Announcement.Models.Entities;

namespace Admin.Announcement.Repositoy.Interface
{
    public interface ICountryLanguageRepository
    {
        Task<IEnumerable<CountryLanguage>> GetCountryLanguage(CancellationToken cancellationToken);
    }
}
