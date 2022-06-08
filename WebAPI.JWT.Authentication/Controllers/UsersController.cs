using Microsoft.AspNetCore.Mvc;
using WebAPI.JWT.Authentication.Helpers;
using WebAPI.JWT.Authentication.Models.Request;
using WebAPI.JWT.Authentication.Repository;

namespace WebAPI.JWT.Authentication.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        /// <summary>
        /// Parameterized Controller
        /// </summary>
        /// <param name="userService">IUserService userService</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("allusers")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
