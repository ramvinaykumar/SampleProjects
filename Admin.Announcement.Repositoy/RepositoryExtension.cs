using Admin.Announcement.Repositoy.Implementation;
using Admin.Announcement.Repositoy.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Admin.Announcement.Repositoy
{
    public static class RepositoryExtension
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ICampaignsRepository, CampaignsRepository>();
            services.AddSingleton<ILanguagesRepository, LanguagesRepository>();
            services.AddSingleton<ICountryLanguageRepository, CountryLanguageRepository>();
            services.AddSingleton<IContentClient, ContentClientRepository>();
            services.AddSingleton<IMicroContentService, MicroContentRepository>();
        }

        public static void RegisterMongoDbContext(this IServiceCollection services)
        {
            services.AddMongoClient((options, env) =>
            {
                options.ConnectionString = env["Mongo"]["Solution"];
                options.ConfigureMongoUrl = x =>
                {
                    x.ReadPreference = ReadPreference.Primary;
                };
                options.AddMap<Models.Entities.Campaign>("DSCCampaign");
                options.AddMap<Models.Entities.Language>("Language");
                options.AddMap<Models.Entities.CountryLanguage>("CountryLanguage");
            });
        }
    }
}