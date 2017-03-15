using System;
using IdentityManager;
using IdentityManager.Configuration;
using IdentityServer3.Core.Configuration;
using KBS.Portals.Web.Models;
using KBS.Portals.Web.Services;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(KBS.Portals.Web.Startup))]
namespace KBS.Portals.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
                var factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get())
                    .UseInMemoryUsers(Users.Get());

                var options = new IdentityServerOptions
                {
                    //SigningCertificate = Certificate.Load(),
                    Factory = factory,
                };

                app.Map("/core", idsrvApp =>
                {
                    idsrvApp.UseIdentityServer(options);
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
                        Factory = identityManagerServiceFactory
                    });
                });
            }
            catch (Exception exc)
            {
                
            }
        }
    }
}