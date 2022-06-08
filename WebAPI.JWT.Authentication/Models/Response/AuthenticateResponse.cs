namespace WebAPI.JWT.Authentication.Models.Response
{
    /// <summary>
    /// Model class for authentication response
    /// </summary>
    public class AuthenticateResponse
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
        /// Authentication token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="user">User user</param>
        /// <param name="token">string token</param>
        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Token = token;
        }
    }
}
