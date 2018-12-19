using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.Models;
using IdentityServer4.Services;
using JieDDDFramework.Module.Identity.Models;
using JieDDDFramework.Web;
using JieDDDFramework.Web.ModelValidate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IIdentityServerInteractionService interaction)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
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
            if (await _userManager.CheckPasswordAsync(user,model.Password))
            {
                await _signInManager.SignInAsync(user, model.RememberMe);
                if (_interaction.IsValidReturnUrl(model.ReturnUrl))
                {
                    return Success(model.ReturnUrl);
                }
            }
            else
            {
                return Fail("登陆失败");
            }
          
            return Success();
        }
    }
}
