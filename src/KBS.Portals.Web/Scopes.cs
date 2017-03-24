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
                StandardScopes.AllClaims
            };
        }
    }
}