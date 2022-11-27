using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Admin.Announcement.Models.Entities
{
    [BsonIgnoreExtraElements]
    public class Campaign : Audit
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement()]
        public string Name { get; set; }
        [BsonElement()]
        public List<string> Regions { get; set; }
        [BsonElement]
        public List<string> Audiences { get; set; }
        [BsonElement()]
        public List<LocalizedMessage> LocalizedMessages { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime StartDateTime { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime EndDateTime { get; set; }
        [BsonElement()]
        public bool IsActive { get; set; }
    }
}
