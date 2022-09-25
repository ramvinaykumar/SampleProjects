using MongoToElastic.Repository;

namespace MongoToElastic.Helpers
{
    public sealed class ServiceCollectionProvider : IServiceCollectionProvider
    {
        public ServiceCollectionProvider(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public IServiceCollection ServiceCollection { get; }
    }
}
