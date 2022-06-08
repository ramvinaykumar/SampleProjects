using System.Collections.Generic;
using WebAPI.JWT.Authentication.Models;
using WebAPI.JWT.Authentication.Models.Request;
using WebAPI.JWT.Authentication.Models.Response;

namespace WebAPI.JWT.Authentication.Repository
{
    /// <summary>
    /// Interface for user services
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetById(int id);
    }
}
