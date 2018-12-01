using System;
using IdentityServer4.Configuration;

namespace JieDDDFramework.Module.Identity
{
    public static class IdentityServerExtensions
    {
       public static IdentityServerBuilder AddDefualtIdentityServerConfig(this IdentityServerBuilder builder)
        {
            return builder;
        }
    }
}
