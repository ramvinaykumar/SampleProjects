using Admin.Announcement.Models.Entities;
using Admin.Announcement.Repositoy.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Announcement.Repositoy.Implementation
{
    public class CountryLanguageRepository : ICountryLanguageRepository
    {
        private readonly IMongoCollection<CountryLanguage> _countryLanguageEntityCollection;

        public CountryLanguageRepository(ISolutionsMongoClient mongoClient)
        {
            _countryLanguageEntityCollection = mongoClient.Collection<CountryLanguage>();
        }
        public async Task<IEnumerable<CountryLanguage>> GetCountryLanguage(CancellationToken cancellationToken)
        {
            return await _countryLanguageEntityCollection.Find(FilterDefinition<CountryLanguage>.Empty).ToListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
