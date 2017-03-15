using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace KBS.Portals.Web
{
    public class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "1",
                    Username = "kbs",
                    Password = "kbs",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.Name, "KBS User"),
                        new Claim(Constants.ClaimTypes.Email, "kbs.user@kbs.ie"),
                        new Claim(Constants.ClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(Constants.ClaimTypes.Role, "Admin")
                    }
                }
            };
        }
    }
}