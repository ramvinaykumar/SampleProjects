using Nest;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace MongoToElastic.Helpers
{
    [DataContract]
    public class Service
    {
        public string this[string key]
        {
            get
            {
                if (Credentials.TryGetValue(key, out var value))
                {
                    return value.Value<string>();
                }

                return null;
            }
        }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public string Label { get; set; }

        public List<string> Tags { get; set; }

        [DataMember(Name = "credentials")]
        public ServiceDictionary<string, JToken> Credentials { get; set; }

        public string SysLog_Drain_Url { get; set; }

        public string Volume_Mounts { get; set; }

        public Service()
        {
            Credentials = new ServiceDictionary<string, JToken>();
        }

        public JToken Get(string key)
        {
            return Credentials[key];
        }
    }
}
