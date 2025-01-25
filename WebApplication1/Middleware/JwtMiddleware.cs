using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Modals;

namespace WebApplication1.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationSettings _appSetting;

        public JwtMiddleware(RequestDelegate next, IOptions<ApplicationSettings> appSetting)
        {
            _next = next;
            _appSetting = appSetting.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var responseSubject = ValidateJwtToken(token, _appSetting.Secret);

            if (responseSubject != null)
            {

                context.Items["userId"] = responseSubject;
            }

            //context.Items["User"] = userService.GetById(responseSubject.UserId);

            await _next(context);
        }
        public int ValidateJwtToken(string token, string Secret)
        {
            if (token == null)
                return 0;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            try
            {
                var responseSubject = 0;

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
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value.ToString());

                responseSubject = userId;

                // return user id from JWT token if validation successful
                return responseSubject;
            }
            catch
            {
                // return null if validation fails
                return 0;
            }
        }
    }
}