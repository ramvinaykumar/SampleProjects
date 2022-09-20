using MongoToElastic.Models.Enums;
using MongoToElastic.Repository;

namespace MongoToElastic.Helpers
{
    public class ChangeInfrastructure : IChangeInfrastructure
    {
        private static readonly Dictionary<string, IDictionary<string, string>> infrConfiguaration = new Dictionary<string, IDictionary<string, string>>();
        private readonly IConfiguration _configuration;
        private readonly IEnvironment _environment;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public ChangeInfrastructure(IConfiguration configuration, IEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        /// <summary>
        /// Update Services
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public bool UpdateServices(Enviornment env)
        {
            string enviornment = _configuration.GetValue<string>("ENVIRONMENT");
            if (!string.IsNullOrEmpty(enviornment) && (enviornment.ToLower() != env.ToString().ToLower()))
            {
                //If data already fetched
                if (infrConfiguaration.Any(x => x.Key == env.ToString().ToLower()))
                {
                    return true;
                }
                InfrastructureConfigurationSource infrastructureConfigurationSource = new InfrastructureConfigurationSource()
                {
                    Setup = (Action<InfrastructureConfigurationProvider>)(provider =>
                    {
                        provider.UseSecure = false;
                    })
                };
                InfrastructureConfigurationProvider infrastructureConfigurationProvider = new InfrastructureConfigurationProvider(infrastructureConfigurationSource);
                infrastructureConfigurationProvider.GetData(_configuration.GetValue<string>(env.ToString().ToLower()));
                infrConfiguaration.Add(env.ToString().ToLower(), infrastructureConfigurationProvider.nConfiguration);
                return true;
            }
            return false;

        }

        /// <summary>
        /// Get Service URL
        /// </summary>
        /// <param name="path"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public string GetServiceURL(string path, Enviornment env) => infrConfiguaration[env.ToString().ToLower()][path];

        /// <summary>
        /// Get Mongo Data From Infra
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetMongoDataFromInfra(string path) => _environment["Mongo"][path];

        /// <summary>
        /// Get ES Data From Infra
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetESDataFromInfra() => _environment["ElasticSearch"]["MDC"];

        public Enviornment GetAndSetEnvironment(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            Enviornment env = ExtensionMethods.ParseEnum<Enviornment>(_configuration.GetValue<string>("ENVIRONMENT"));
            if (httpContext.Request != null)
            {
                var Env = httpContext.Request.Query["ENV"];
                if (Env.Count > 0)
                {
                    env = ExtensionMethods.ParseEnum<Enviornment>(Env[0]);
                }
            }
            return env;
        }
    }
}
