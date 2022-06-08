using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebAPI.JWT.Authentication.Helpers;
using WebAPI.JWT.Authentication.Models;
using WebAPI.JWT.Authentication.Models.Request;
using WebAPI.JWT.Authentication.Models.Response;
using WebAPI.JWT.Authentication.Repository;

namespace WebAPI.JWT.Authentication.Services
{
    /// <summary>
    /// Service class for user
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// User list with static data
        /// </summary>
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        /// <summary>
        /// Readonly variable
        /// </summary>
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="appSettings">AppSettings appSettings</param>
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="model">AuthenticateRequest model</param>
        /// <returns>Returns AuthenticateResponse</returns>
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        /// <summary>
        /// Get all the user data
        /// </summary>
        /// <returns>Return users list</returns>
        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        /// <summary>
        /// Get user data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Return user data</returns>
        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        // helper methods
        /// <summary>
        /// Generate JWT toke
        /// </summary>
        /// <param name="user">User user</param>
        /// <returns>Returns the token as string</returns>
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}