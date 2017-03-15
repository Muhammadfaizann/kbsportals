using IdentityManager.AspNetIdentity;
using KBS.Portals.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KBS.Portals.Web.Services
{
    public class ApplicationIdentityManagerService : AspNetIdentityManagerService<ApplicationUser, string, IdentityRole, string>
    {
        public ApplicationIdentityManagerService(ApplicationUserManager userManager, ApplicationRoleManager roleManager) : base(userManager, roleManager)
        {
            
        }
    }
}