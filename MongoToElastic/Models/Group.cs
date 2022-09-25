using MongoDB.Bson.Serialization.Attributes;
using Nest;
using Newtonsoft.Json;

namespace MongoToElastic.Models
{
    [BsonIgnoreExtraElements]
    public class Group
    {
        [PropertyName("id"), JsonProperty("id")]
        [BsonElement("id")]
        public long Id { get; set; }
        [PropertyName("name"), JsonProperty("name")]
        [BsonElement("name")]
        public string Name { get; set; }
        [PropertyName("description"), JsonProperty("description")]
        [BsonElement("description")]
        public string Description { get; set; }
    }
}
