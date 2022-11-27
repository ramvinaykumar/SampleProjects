using MongoDB.Bson.Serialization.Attributes;

namespace Admin.Announcement.Models.Entities
{
    public class LocalizedMessage
    {
        [BsonElement()]
        public string LanguageCode { get; set; }
        [BsonElement()]
        public string Message { get; set; }
    }
}
