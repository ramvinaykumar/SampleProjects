using System;

namespace Application.Inventory.EFDBF.API.Models
{
    /// <summary>
    /// Model class for user information
    /// </summary>
    public partial class UserInfo
    {
        /// <summary>
        /// UserId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Record created date
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
