using System;
using System.Threading.Tasks;
using IdentityServer3.AspNetIdentity;
using KBS.Portals.Web.Models;
using Microsoft.AspNet.Identity;
using System.Web.Configuration;

namespace KBS.Portals.Web.Services
{
    public class ApplicationUserService : AspNetIdentityUserService<ApplicationUser, string>
    {
        public ApplicationUserService(UserManager<ApplicationUser, string> userManager) : base(userManager)
        {
        }

        protected override async Task<ApplicationUser> FindUserAsync(string username)
        {
            // check for admin user and create if it does not exits
            await CheckForAdminUser();
            return await base.FindUserAsync(username);
        }

        private async Task CheckForAdminUser()
        {
            var adminUser = userManager.FindByName("admin");
            if (adminUser != null)
            {
                var result = userManager.AddToRole(adminUser.Id, "Admin");
            }
        }
    }
}