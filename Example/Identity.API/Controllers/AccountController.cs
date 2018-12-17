using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.Models;
using JieDDDFramework.Module.Identity.Models;
using JieDDDFramework.Web.ModelValidate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]/[action]")]
    //[ApiController]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <remarks>
        ///     {
        ///         "email":"demo@xx.com",
        ///         "password":"123456"
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody, Validator]LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            return Ok();
        }
    }
}
