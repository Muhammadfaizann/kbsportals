using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityManager;
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

        public async Task InitialiseAdmin()
        {
            var admin = await GetUserAsync("admin");
            if (admin.Result == null)
            {
                await CreateRoleAsync(new PropertyValue[] { new PropertyValue { Type = Constants.ClaimTypes.Name, Value = "Admin" } });
                await CreateUserAsync(new PropertyValue[] { new PropertyValue { Type = Constants.ClaimTypes.Username, Value = "admin" }, new PropertyValue { Type = Constants.ClaimTypes.Password, Value = "OK*3Smdm1tLq" } });
                var userAdmin = await userManager.FindByNameAsync("admin");
                await userManager.AddToRoleAsync(userAdmin.Id, "Admin");
            }
        }
    }
}