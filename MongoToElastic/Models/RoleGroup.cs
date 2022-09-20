using MongoDB.Bson.Serialization.Attributes;
using Nest;
using Newtonsoft.Json;

namespace MongoToElastic.Models
{
    [BsonIgnoreExtraElements]
    public class RoleGroup
    {
        [PropertyName("roleId"), JsonProperty("roleId")]
        [BsonElement("RoleId")]
        public long RoleId { get; set; }

        [PropertyName("name"), JsonProperty("name")]
        [BsonElement("Name")]
        public string Name { get; set; }

        [PropertyName("description"), JsonProperty("description")]
        [BsonElement("Description")]
        public string Description { get; set; }

        [PropertyName("region"), JsonProperty("region")]
        [BsonElement("Region")]
        public string Region { get; set; }
    }
}
