using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminDashboard.Controllers
{
    public class HomeController : Controller
    {
        [Attributes.Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}