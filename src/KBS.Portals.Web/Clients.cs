using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System.Web.Configuration;

namespace KBS.Portals.Web
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "KBS Calculator",
                    ClientId = "KBS.Calculator",
                    Flow = Flows.ResourceOwner,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("3ZufOIEYhWT#!duEN8mB".Sha256())
                    },

                    AllowedScopes = new List<Scope>
                    {
                        StandardScopes.OpenId,
                        StandardScopes.AllClaims
                    }.Select(x => x.Name).ToList(),
                    AccessTokenLifetime = 2678400 // 31 days
                },
                new Client
                {
                    ClientName = "KBS Portal Admin",
                    ClientId = "idmgr",
                    Flow = Flows.Implicit,
                    Enabled = true,
                    RedirectUris = new List<string>
                    {
                        WebConfigurationManager.AppSettings["IdentityManagerUri"],
                    },
                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        "idmgr"
                    }
                }
            };
        }
    }
}