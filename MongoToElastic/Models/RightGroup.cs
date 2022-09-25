using MongoDB.Bson.Serialization.Attributes;
using Nest;
using Newtonsoft.Json;

namespace MongoToElastic.Models
{
    [BsonIgnoreExtraElements]
    public class RightGroup
    {
        [PropertyName("id"), JsonProperty("id")]
        [BsonElement("id")]
        public string Id { get; set; }

        [PropertyName("name"), JsonProperty("name")]
        [BsonElement("name")]
        public string Name { get; set; }

        [PropertyName("groups"), JsonProperty("groups")]
        [BsonElement("groups")]
        public IEnumerable<long> Groups { get; set; }
    }
}
