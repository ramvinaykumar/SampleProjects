using System.Text.Json.Serialization;

namespace WebAPI.JWT.Authentication.Models
{
    /// <summary>
    /// Model class for User
    /// </summary>
    public class User
    {
        /// <summary>
        /// UserId
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; }
    }
}
