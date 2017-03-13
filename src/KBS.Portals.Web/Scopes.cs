using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace KBS.Portals.Web
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new[]
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email,
                StandardScopes.Address,
                StandardScopes.OfflineAccess,
                StandardScopes.RolesAlwaysInclude,
                StandardScopes.AllClaims,
                new Scope
                {
                    Name = "KBS.Portals.Calculator",
                    DisplayName = "KBS Calculator",
                    Type = ScopeType.Resource,
                    Emphasize = false,
                    ScopeSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    }
                }
            };
        }
    }
}