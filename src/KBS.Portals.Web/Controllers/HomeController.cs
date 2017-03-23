using System.Security.Claims;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using IdentityManager;

namespace KBS.Portals.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string returlUrl)
        {
            if (username == WebConfigurationManager.AppSettings["username"] && password == WebConfigurationManager.AppSettings["password"])
            {
                var claims = new[]
                {
                    new Claim(Constants.ClaimTypes.Name, "AdminUser"),
                    new Claim(Constants.ClaimTypes.Role, "Admin"),
                };
                var id = new ClaimsIdentity(claims, "Cookies");
                Request.GetOwinContext().Authentication.SignIn(id);
                return Redirect(returlUrl);
            }
            return View();
        }
    }
}