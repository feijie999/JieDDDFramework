using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JieDDDFramework.Module.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace JieDDDFramework.Module.Identity.Services
{
    public class IdentityLoginService<TApplicationUser> : ILoginService<TApplicationUser> where TApplicationUser : ApplicationUser
    {
        private UserManager<TApplicationUser> _userManager;
        private SignInManager<TApplicationUser> _signInManager;

        public IdentityLoginService(UserManager<TApplicationUser> userManager,SignInManager<TApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<TApplicationUser> FindByUsername(string account)
        {
            TApplicationUser user = null;
            if (Regex.IsMatch(account, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", RegexOptions.IgnoreCase))
            {
                user = await _userManager.FindByEmailAsync(account);
            }
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(account);
            }
            return user;
        }

        public Task SignIn(TApplicationUser user)
        {
            return _signInManager.SignInAsync(user,true);
        }

        public Task<bool> ValidateCredentials(TApplicationUser user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }
    }
}
