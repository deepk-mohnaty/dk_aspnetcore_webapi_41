using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace ProviderEdge_WebAPI_Core.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DkAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
       
        public void OnAuthorization(AuthorizationFilterContext authContext)
        {
            bool isAuthorized = false;
            if (authContext.HttpContext.User != null && authContext.HttpContext.User.Identity != null)
            {
                if (authContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    isAuthorized = true;
                }
            }

            if(!isAuthorized)
            {
                authContext.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
