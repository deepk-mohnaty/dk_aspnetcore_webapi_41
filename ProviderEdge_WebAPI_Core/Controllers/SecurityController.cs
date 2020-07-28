using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProviderEdge_V3_Core.GblService.ViewModels;
using ProviderEdge_V3_Core.GblService.Service;
using ProviderEdge_V3_Core.GblService.InterFace;
using ProviderEdge_V3_Core.Common.CommonEntities;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ProviderEdge_WebAPI_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private ISecurityService _obISecurityService;
        private ApplicationSettings _configAppSettings;
        public SecurityController(ISecurityService objSecurityService, IOptions<ApplicationSettings> configAppSettings)
        {
            _obISecurityService = objSecurityService;
            _configAppSettings = configAppSettings.Value;
        }

        [HttpGet("GetLogin")]
        public async Task<IActionResult> GetLogin()
        {
            LoginViewModelRes objLoginVMRes = new LoginViewModelRes();
            await Task.Delay(3000);

            return Ok(objLoginVMRes);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel objLoginVM)
        {
            LoginViewModelRes objLoginVMRes = new LoginViewModelRes();

             objLoginVMRes = await _obISecurityService.AuthenticateUser(objLoginVM);

            if (objLoginVMRes.Status == "Y")
            {
                objLoginVMRes.SecurityToken= GenerateJWTToken(objLoginVMRes);
            }

            return Ok(objLoginVMRes);
        }

        private string GenerateJWTToken(LoginViewModelRes objLoginViewModelRes)
        {
            string securityToken = string.Empty;
            JwtSecurityTokenHandler objJwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configAppSettings.SecurityKey);

            var objTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", "1"),
                    new Claim("loginid", objLoginViewModelRes.UserLoginId),
                    new Claim("username", objLoginViewModelRes.UserName),
                    new Claim("role", objLoginViewModelRes.RoleName)
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                

            };

           var newsecurityToken= objJwtSecurityTokenHandler.CreateToken(objTokenDescriptor);
            securityToken = objJwtSecurityTokenHandler.WriteToken(newsecurityToken);

            return securityToken;
        }


    }
}