using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Admin.Announcement.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class Language
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement()]
        public string Code { get; set; }
        [BsonElement()]
        public string DisplayText { get; set; }
    }
}
