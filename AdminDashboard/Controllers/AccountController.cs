using AdminDashboard.Models;
using AdminDashboard.Models.Controllers;
using System.Web.Mvc;

namespace AdminDashboard.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if(ModelState.IsValid)
            {
                var identityServices = new IdentityServices();

                var result = identityServices.Login(login);

                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password.");
                }
            }
            return View(login);
        }

        
        public ActionResult Logout()
        {
            var identityServices = new IdentityServices();
            identityServices.Logout();
            return View("Login");
        }
    }
}