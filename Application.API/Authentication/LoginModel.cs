using System.ComponentModel.DataAnnotations;

namespace Application.API.Authentication
{
    /// <summary>
    /// Model class for login
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
