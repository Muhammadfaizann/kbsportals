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
    }
}