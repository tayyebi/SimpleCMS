using Rexa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebInterface.Controllers
{
    public class UsersController : Controller
    {
        // GET: /<controller>/
        [Secure(IsAdmin = true)]
        public ActionResult Index()
        {
            var items = new DbController.Model_User().Select();
            return View(items);
        }
        [Secure(IsAdmin = true)]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        [Secure(IsAdmin = true)]
        public ActionResult New(v_User props, string Password, string Confirm)
        {
            if (Password != Confirm)
            {
                ViewData["Message"] = "کلمه عبور و تکرار آن همخوانی ندارند";
                return View();
            }
            else
            {
                if (!new DbController.Model_User().Insert(props, Password))
                {
                    ViewData["Message"] = "خطا در تعریف کاربر جدید";
                    return View();
                }
                else
                {
                    return RedirectToRoute(new { action = "Index", controller = "Users" });
                }
            }
        }
        [Secure(IsAdmin = false)]
        public ActionResult Update(string Id)
        {
            if (Convert.ToBoolean(Session["IsAdmin"].ToString()) || (!Convert.ToBoolean(Session["IsAdmin"].ToString()) && Session["Username"].ToString() == Id))
            {
                v_User user = new DbController.Model_User().Select().Where(x => x.Username == Id).FirstOrDefault();
                return View(user);
            }
            else
            {
                Response.StatusCode = 401;
                return new EmptyResult();
            }
        }
        [HttpPost]
        [Secure(IsAdmin = false)]
        public ActionResult Update(string Id, v_User props) // Id is prevUsername
        {
            if (Convert.ToBoolean(Session["IsAdmin"].ToString()) || (!Convert.ToBoolean(Session["IsAdmin"].ToString()) && Session["Username"].ToString() == Id))
            {

                if (!new DbController.Model_User().Update(Id, props))
                {
                    v_User user = new DbController.Model_User().Select().Where(x => x.Username == Id).FirstOrDefault();
                    ViewData["Message"] = "خطا در به روز رسانی اطلاعات کاربر عزیزمان";
                    return View(user);
                }
                else
                {
                    return RedirectToRoute(new { action = "Admin", controller = "Home" });
                }
            }
            else
            {
                Response.StatusCode = 401;
                return new EmptyResult();
            }
        }
        [Secure(IsAdmin = false)]
        public ActionResult Password(string Id)
        {
            if (Convert.ToBoolean(Session["IsAdmin"].ToString()) || (!Convert.ToBoolean(Session["IsAdmin"].ToString()) && Session["Username"].ToString() == Id))
            {

                v_User user = new DbController.Model_User().Select().Where(x => x.Username == Id).FirstOrDefault();
                return View(user);
            }
            else
            {
                Response.StatusCode = 401;
                return new EmptyResult();
            }
        }
        [HttpPost]
        [Secure(IsAdmin = false)]
        public ActionResult Password(string Id, string prevPass, string Password, string Confirm) // Id is prevUsername
        {
            if (Convert.ToBoolean(Session["IsAdmin"].ToString()) || (!Convert.ToBoolean(Session["IsAdmin"].ToString()) && Session["Username"].ToString() == Id))
            {

                v_User user = new DbController.Model_User().Select().Where(x => x.Username == Id).FirstOrDefault();
                if (Password == Confirm)
                {
                    if (new DbController.Model_User().Login(Id, prevPass).Success)
                    {
                        if (!new DbController.Model_User().Password(Id, prevPass, Password))
                        {
                            ViewData["Message"] = "نشد. چرا نشد؟ باید میشد. دوباره امتحان کنید.";
                            return View(user);
                        }
                        else
                        {
                            return RedirectToRoute(new { action = "Update", controller = "Users", id = Id });
                        }
                    }
                    else
                    {
                        ViewData["Message"] = "توی وارد کردن رمز قبلی دقت کنید";
                        return View(user);
                    }
                }
                else
                {
                    ViewData["Message"] = "رمز شما با تکرارش همخوانی نداره";
                    return View(user);
                }
            }
            else
            {
                Response.StatusCode = 401;
                return new EmptyResult();
            }
        }
        [Secure(IsAdmin = true)]
        public ActionResult Delete(string Id)
        {
            v_User user = new DbController.Model_User().Select().Where(x => x.Username == Id).FirstOrDefault();
            ViewData["Message"] = "آیا از حذف داده های مربوط به این کاربر اطمینان دارید؟";
            return View(user);
        }
        [HttpPost]
        [Secure(IsAdmin = true)]
        public ActionResult Delete(string Id, string Fullname)
        {
            if (!new DbController.Model_User().Delete(Id))
            {
                ViewData["Message"] = "داده های " + Fullname + " حذف شد";
                return View();
            }
            else
            {
                return RedirectToRoute(new {  action = "Index", controller = "Users" });
            }

        }
    }
}
