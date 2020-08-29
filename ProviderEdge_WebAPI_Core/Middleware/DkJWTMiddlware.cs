using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using ProviderEdge_V3_Core.GblService.Service;
using ProviderEdge_V3_Core.GblService.InterFace;
using ProviderEdge_V3_Core.Common.CommonEntities;
using Microsoft.Extensions.DependencyInjection;

namespace ProviderEdge_WebAPI_Core.Middleware
{
    public class DkJWTMiddlware
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationSettings _objAppsetings;
       
        public DkJWTMiddlware(RequestDelegate next, IOptions<ApplicationSettings> objAppSettings)
        {
            _next = next;
            _objAppsetings = objAppSettings.Value;
        }
        public async Task Invoke(HttpContext context, ISecurityService objSecurityService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if(token != null)
            {
                //attach user to context;
                AttachUserToContext(context, objSecurityService, token);
            }
            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, ISecurityService objSecurittyService, string SecurityToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_objAppsetings.SecurityKey);

               var objVal=  tokenHandler.ValidateToken(SecurityToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                 }, out SecurityToken validatedToken);

                 context.User = objVal;

                var jwtSecurityToken = (JwtSecurityToken)validatedToken;
                var onlineid =jwtSecurityToken.Claims.First(x => x.Type == "id").Value;

                UserOnlineContext objUserOnlineContext = new UserOnlineContext()
                {
                    OnlineId=Convert.ToInt32(jwtSecurityToken.Claims.First(x => x.Type == "id").Value),
                    UserLoginId =jwtSecurityToken.Claims.First(x => x.Type == "loginid").Value,
                    RoleName = jwtSecurityToken.Claims.First(x => x.Type == "role").Value,
                    UserName = jwtSecurityToken.Claims.First(x => x.Type == "username").Value,
                };

                //retrive session object and assign to the item
                context.Items["ONLINEOBJ"] = objUserOnlineContext;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

    }
}
