using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.JWT.Authentication.Repository;

namespace WebAPI.JWT.Authentication.Helpers
{
    /// <summary>
    /// Jwt Middleware class 
    /// </summary>
    public class JwtMiddleware
    {
        /// <summary>
        /// Readonly variable for RequestDelegate
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Readonly variable for AppSettings
        /// </summary>
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="next">RequestDelegate next</param>
        /// <param name="appSettings">AppSettings appSettings</param>
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context">HttpContext context</param>
        /// <param name="userService">IUserService userService</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, userService, token);

            await _next(context);
        }

        /// <summary>
        /// Attach User To Context
        /// </summary>
        /// <param name="context">HttpContext context</param>
        /// <param name="userService">IUserService userService</param>
        /// <param name="token">string token</param>
        private void attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetById(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
