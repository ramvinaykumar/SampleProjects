using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Admin.Announcement.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class CountryLanguage
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement()]
        public string Region { get; set; }
        [BsonElement()]
        public string Country { get; set; }
        [BsonElement()]
        public string CountryCode { get; set; }
        [BsonElement()]
        public List<LanguageList> Languages { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class LanguageList
    {
        [BsonElement()]
        public string Language { get; set; }
        [BsonElement()]
        public string LanguageCode { get; set; }
    }
}
