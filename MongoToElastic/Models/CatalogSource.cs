using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Nest;
using Newtonsoft.Json;

namespace MongoToElastic.Models
{
    [BsonIgnoreExtraElements]
    public class CatalogSource
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("CustomerSet")]
        [PropertyName("customerset"), JsonProperty("customerset")]
        public string CustomerSet { get; set; }

        [BsonElement("CustomerSetMode")]
        [PropertyName("customersetmode"), JsonProperty("customersetmode")]
        public int CustomerSetMode { get; set; }

        [BsonElement("CustomerSetModeTest")]
        [PropertyName("customersetmodetest"), JsonProperty("customersetmodetest")]
        public int CustomerSetModeTest { get; set; }

        [BsonElement("Region")]
        [PropertyName("region"), JsonProperty("region")]
        public string Region { get; set; }

        [BsonElement("Country")]
        [PropertyName("country"), JsonProperty("country")]
        public string Country { get; set; }

        [BsonElement("Segment")]
        [PropertyName("segment"), JsonProperty("segment")]
        public string Segment { get; set; }

        [BsonElement("Language")]
        [PropertyName("language"), JsonProperty("language")]
        public string Language { get; set; }

        [BsonElement("CurrencyCode")]
        [PropertyName("currencycode"), JsonProperty("currencycode")]
        public string CurrencyCode { get; set; }
    }
}
