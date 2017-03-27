using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Web.Configuration;

[assembly: OwinStartup(typeof(KBS.Portals.Web.Startup))]
namespace KBS.Portals.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>(); // for authorizing the identitymanager user

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "oidc",
                Authority = "https://dev.local/core/",
                ClientId = "idmgr",
                RedirectUri = WebConfigurationManager.AppSettings["IdentityManagerUri"],
                ResponseType = "id_token",
                Scope = "openid idmgr",
                SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType
            });

            IdentityManagerConfig.SetUpIdentityManager(app);
            IdentityServerConfig.SetUpIdentityServer(app);
        }
    }
}