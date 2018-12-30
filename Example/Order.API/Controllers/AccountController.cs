using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.Models;
using IdentityModel.Client;
using JieDDDFramework.Module.Identity;
using JieDDDFramework.Web;
using JieDDDFramework.Web.ModelValidate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly JwtSettings _jwtSettings;

        public AccountController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <remarks>
        ///     {
        ///        "email": "demouser@xx.com",
        ///        "password": "123456"
        ///}
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody, Validator] LoginViewModel model)
        {
            var discoveryResponse = await DiscoveryClient.GetAsync(_jwtSettings.Issuer);
            var tokenClient = new TokenClient(discoveryResponse.TokenEndpoint, _jwtSettings.ClientId, _jwtSettings.SecretKey);
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(model.Email, model.Password);
            return Success(new { tokenResponse.IsError,tokenResponse.AccessToken});
        }
    }
}
