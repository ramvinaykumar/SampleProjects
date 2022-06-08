using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.API.Models
{
    /// <summary>
    /// Employee Model
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Employee ID
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Employee Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Employee Department
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Employee Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Employee City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Employee Country
        /// </summary>
        public string Country { get; set; }
    }
}
