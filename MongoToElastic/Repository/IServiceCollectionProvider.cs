namespace MongoToElastic.Repository
{
    public interface IServiceCollectionProvider
    {
        IServiceCollection ServiceCollection { get; }
    }
}
