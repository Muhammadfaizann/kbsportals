using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(KBS.Portals.Web.Startup))]
namespace KBS.Portals.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var factory = new IdentityServerServiceFactory()
                .UseInMemoryClients(Clients.Get())
                .UseInMemoryScopes(Scopes.Get());

            // TODO: Once we have Autofac in place, update this line
            factory.UserService = new Registration<IUserService>(x => new UserService());

            var options = new IdentityServerOptions
            {
                //SigningCertificate = Certificate.Load(),
                Factory = factory,
            };

            app.Map("/core", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(options);
            });
        }
    }
}