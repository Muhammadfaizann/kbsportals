using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

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
                    ClientName = "Client Credentials Flow Client",
                    Enabled = true,
                    ClientId = "clientcredentials.client",
                    Flow = Flows.ClientCredentials,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256()),
                    },

                    AllowedScopes = new List<string>
                    {
                        "KBS.Portals.Calculator"
                    },

                    Claims = new List<Claim>
                    {
                        new Claim("location", "datacenter")
                    }
                },
                new Client
                {
                    ClientName = "Client Credentials Flow Client",
                    Enabled = true,
                    ClientId = "idmgr",
                    Flow = Flows.ClientCredentials,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256()),
                    },

                    AllowedScopes = new List<string>
                    {
                        "KBS.Portals.Calculator"
                    },

                    Claims = new List<Claim>
                    {
                        new Claim("location", "datacenter")
                    }
                }
            };
        }
    }
}