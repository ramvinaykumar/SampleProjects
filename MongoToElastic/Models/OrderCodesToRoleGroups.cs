using MongoDB.Bson.Serialization.Attributes;
using MongoToElastic.Models.Enums;
using Nest;
using Newtonsoft.Json;

namespace MongoToElastic.Models
{
    [BsonIgnoreExtraElements]
    public class OrderCodesToRoleGroups
    {
        [PropertyName("orderCodeId"), JsonProperty("orderCodeId")]
        [BsonElement("OrderCodeId")]
        public string OrderCodeId { get; set; }

        [PropertyName("region"), JsonProperty("region")]
        [BsonElement("Region")]
        public string Region { get; set; }
        
        [PropertyName("state"), JsonProperty("state")]
        [BsonElement("State")]
        public OrderCodeState State { get; set; }
        
        [PropertyName("roleGroups"), JsonProperty("roleGroups")]
        [BsonElement("Rolegroups")]
        public IEnumerable<long> RoleGroups { get; set; }
    }
}
