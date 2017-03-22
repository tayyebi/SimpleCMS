using DbController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebInterface.Controllers
{
    public class LogController : Controller
    {
        // GET: /<controller>/
        public ActionResult In()
        {
            ViewData["Message"] = "نام کاربری و کلمه ی عبور خود را وارد نمائید سپس بر روی ورود کلیک نمائید";
            return View();
        }
        [HttpPost]
        public ActionResult In(string Username, string Password)
        {
            var login = new Model_User().Login(Username, Password);
            if (Convert.ToBoolean(login.Success))
            {
                Session["Username"] = Username;
                Session["Fullname"] = login.Fullname;
                Session["IsAdmin"] = login.IsAdmin.ToString();

                return RedirectToRoute(new { controller = "Home", action = "Admin" });
            }
            ViewData["Message"] = "نام کاربری یا کلمه عبور معتبر نیست";
            return View();
        }
        [Secure(IsAdmin = false)]
        public ActionResult Out()
        {
            Session.Remove("Fullname");
            Session.Remove("IsAdmin");
            Session.Remove("Username");
            return RedirectToRoute(new { controller = "Log", Action = "In" });
        }
    }
}
