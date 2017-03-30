using IdentityManager;
using IdentityManager.Configuration;
using KBS.Portals.Web.Models;
using KBS.Portals.Web.Services;
using Owin;

namespace KBS.Portals.Web
{
    public class IdentityManagerConfig
    {
        public static void SetUpIdentityManager(IAppBuilder app)
        {
            app.Map("/idm", idm =>
            {
                var identityManagerServiceFactory = new IdentityManagerServiceFactory
                {
                    IdentityManagerService =
                        new Registration
                            <IIdentityManagerService, ApplicationIdentityManagerService>()
                };
                identityManagerServiceFactory.Register(new Registration<ApplicationUserManager>());
                identityManagerServiceFactory.Register(new Registration<ApplicationRoleManager>());
                identityManagerServiceFactory.Register(new Registration<ApplicationRoleStore>());
                identityManagerServiceFactory.Register(new Registration<ApplicationUserStore>());
                identityManagerServiceFactory.Register(new Registration<ApplicationDbContext>());
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
        }
    }
}