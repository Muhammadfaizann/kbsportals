﻿using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using KBS.Portals.Web.Models;
using KBS.Portals.Web.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;

namespace KBS.Portals.Web
{
    public class IdentityServerConfig
    {
        public static void SetUpIdentityServer(IAppBuilder app)
        {
            var factory = new IdentityServerServiceFactory()
                .UseInMemoryClients(Clients.Get())
                .UseInMemoryScopes(Scopes.Get());

            factory.UserService =
                new Registration<IUserService>(typeof (ApplicationUserService));
            factory.Register(new Registration<UserManager<ApplicationUser, string>>());
            factory.Register(new Registration<ApplicationUserStore>());
            // this is registered as a factory
            factory.Register(new Registration<ApplicationDbContext>(resolver => new ApplicationDbContext()));
            factory.Register(
                new Registration<IUserStore<ApplicationUser, string>>(
                    resolver => resolver.Resolve<ApplicationUserStore>()));

            var options = new IdentityServerOptions
            {
                Factory = factory,
                SigningCertificate = Certificate.Get()
            };
            app.Map("/core", idsrvApp => { idsrvApp.UseIdentityServer(options); });
        }
    }
}