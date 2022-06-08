using System.ComponentModel.DataAnnotations;

namespace Application.API.Authentication
{
    /// <summary>
    /// Model class for user registration
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
