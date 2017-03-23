using System.Web.Mvc;
using System.Web.Routing;
using IdentityManager;
using IdentityManager.Configuration;
using IdentityServer3.Core.Configuration;
using KBS.Portals.Web.Models;
using KBS.Portals.Web.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(KBS.Portals.Web.Startup))]
namespace KBS.Portals.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            RouteTable.Routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                LoginPath = new PathString("/Home/Login")
            });

            app.Map("/idm", idm =>
            {
                var identityManagerServiceFactory = new IdentityManagerServiceFactory
                {
                    IdentityManagerService =
                        new IdentityManager.Configuration.Registration
                            <IIdentityManagerService, ApplicationIdentityManagerService>()
                };
                identityManagerServiceFactory.Register(
                    new IdentityManager.Configuration.Registration<ApplicationUserManager>());
                identityManagerServiceFactory.Register(
                    new IdentityManager.Configuration.Registration<ApplicationRoleManager>());
                identityManagerServiceFactory.Register(
                    new IdentityManager.Configuration.Registration<ApplicationRoleStore>());
                identityManagerServiceFactory.Register(
                    new IdentityManager.Configuration.Registration<ApplicationUserStore>());
                identityManagerServiceFactory.Register(
                    new IdentityManager.Configuration.Registration<ApplicationDbContext>());

                idm.UseIdentityManager(new IdentityManagerOptions
                {
                    Factory = identityManagerServiceFactory,
                    SecurityConfiguration = new HostSecurityConfiguration
                    {
                        HostAuthenticationType = "Cookies",
                        NameClaimType = Constants.ClaimTypes.Name,
                        RoleClaimType = Constants.ClaimTypes.Role,
                        AdminRoleName = "Admin"
                    }
                });
            });

            var factory = new IdentityServerServiceFactory()
                .UseInMemoryClients(Clients.Get())
                .UseInMemoryScopes(Scopes.Get())
                .UseInMemoryUsers(Users.Get());
            var options = new IdentityServerOptions
            {
                Factory = factory,
            };
            app.Map("/core", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(options);
            });
        }
    }
}