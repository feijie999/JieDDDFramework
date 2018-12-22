using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JieDDDFramework.Data.EntityFramework.Migrate;
using JieDDDFramework.Module.Identity.Data;
using JieDDDFramework.Module.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Init
{
    public class IdentityUserDbContextSeed : IDbContextSeed<IdentityUserDbContext>
    {
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();
        public async Task SeedAsync(IdentityUserDbContext context)
        {
#if DEBUG
            var users = context.Users.ToList();
            context.Users.RemoveRange(users);
            await context.SaveChangesAsync();

#endif
            if (!context.Users.Any())
            {
                context.Users.AddRange(GetDefaultUser());

                await context.SaveChangesAsync();
            }
        }

        private IEnumerable<ApplicationUser> GetDefaultUser()
        {
            var user =
                new ApplicationUser()
                {
                    City = "CQ",
                    Country = "CN",
                    Email = "demouser@xx.com",
                    NormalizedEmail = "demouser@xx.com".ToUpper(),
                    Id = Guid.NewGuid().ToString(),
                    Name = "DemoUser",
                    PhoneNumber = "1234567890",
                    UserName = "demouser@xx.com",
                    NormalizedUserName = "demouser@xx.com".ToUpper(),
                    State = "WA",
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                };

            user.PasswordHash = _passwordHasher.HashPassword(user, "123456");

            return new List<ApplicationUser>()
            {
                user
            };
        }
    }
}
