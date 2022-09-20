using MongoDB.Bson.Serialization.Attributes;
using MongoToElastic.Models.Enums;
using Nest;
using Newtonsoft.Json;

namespace MongoToElastic.Models
{
    [BsonIgnoreExtraElements]
    public class VariantToGroups
    {
        [PropertyName("id"), JsonProperty("id")]
        [BsonElement("id")]
        public string Id { get; set; }

        [PropertyName("region"), JsonProperty("region")]
        [BsonElement("region")]
        public Region Region { get; set; }

        [PropertyName("state"), JsonProperty("state")]
        [BsonElement("state")]
        public VariantState State { get; set; }

        [PropertyName("groups"), JsonProperty("groups")]
        [BsonElement("groups")]
        public IEnumerable<long> Groups { get; set; }

        [PropertyName("pageSize"), JsonProperty("pageSize")]
        [BsonElement("pageSize")]
        public int PageSize { get; set; }
        [PropertyName("pageNumber"), JsonProperty("pageNumber")]

        [BsonElement("pageNumber")]
        public int PageNumber { get; set; }
    }
}
