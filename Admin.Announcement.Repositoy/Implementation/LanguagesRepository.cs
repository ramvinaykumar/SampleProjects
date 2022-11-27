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
    public class LanguagesRepository : ILanguagesRepository
    {
        private readonly IMongoCollection<Language> _languageCollection;

        public LanguagesRepository(ISolutionsMongoClient mongoClient)
        {
            _languageCollection = mongoClient.Collection<Language>();
        }

        public async Task<IEnumerable<Language>> GetLanguages(CancellationToken cancellationToken)
        {
            return await _languageCollection.Find(FilterDefinition<Language>.Empty).ToListAsync(cancellationToken);
        }
    }
}
