using MongoDB.Bson.Serialization.Attributes;
using Nest;
using Newtonsoft.Json;

namespace MongoToElastic.Models
{
    [BsonIgnoreExtraElements]
    public class GiiProductAccessAdminOrderCode
    {
        [PropertyName("orderCodeId"), JsonProperty("orderCodeId")]
        [BsonElement("OrderCodeId")]
        public string OrderCodeId { get; set; }
        [PropertyName("region"), JsonProperty("region")]
        [BsonElement("Region")]
        public string Region { get; set; }
    }
}
