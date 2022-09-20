using MongoToElastic.Helpers;

namespace MongoToElastic.Repository
{
    public interface IEnvironment
    {
        Service this[string key] { get; }

        string ApplicationName { get; }

        string ApplicationVersion { get; }

        string SpaceName { get; }

        string InstanceId { get; }

        string GlobalTrafficManager { get; }

        string LocalTrafficManager { get; }

        void Reload();

        void Reload(bool force = false);
    }
}
