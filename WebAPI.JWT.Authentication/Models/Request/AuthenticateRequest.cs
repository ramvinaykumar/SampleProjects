using System.ComponentModel.DataAnnotations;

namespace WebAPI.JWT.Authentication.Models.Request
{
    /// <summary>
    /// Model class for authentication request
    /// </summary>
    public class AuthenticateRequest
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
