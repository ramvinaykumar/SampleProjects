using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.API.Models
{
    /// <summary>
    /// Model class for Book
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Primary Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Book name
        /// </summary>
        [BsonElement("Name")]
        public string BookName { get; set; }

        /// <summary>
        /// Book price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Book category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Book author
        /// </summary>
        public string Author { get; set; }
    }
}
