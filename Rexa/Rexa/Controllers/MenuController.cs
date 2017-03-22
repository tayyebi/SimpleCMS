using Rexa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebInterface.Controllers
{
    [Secure(IsAdmin = true)]
    public class MenuController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index(Guid? id = null)
        {
            ViewData["Id"] = id.ToString();
            var items = new DbController.Model_Group().Select().Where(x => x.OtherProps.ParentId == id).Select(x => new v_Group { AdminUsername = x.OtherProps.AdminUsername, Id = x.OtherProps.Id, Name = x.OtherProps.Name, ParentId = x.OtherProps.ParentId, Posts = x.OtherProps.Posts, Publish = x.OtherProps.Publish, Serial = x.OtherProps.Serial, SubGroups = x.OtherProps.SubGroups });
            try
            {
                ViewBag.Title = new DbController.Model_Group().Select().Where(y=>y.OtherProps.Id == id).Select(x => new { Title =  x.OtherProps.Name }).FirstOrDefault().Title;
            }
            catch
            {
                ViewBag.Title = "منو";
            }
            try
            {
                ViewData["ParentId"] = new DbController.Model_Group().Select().Where(y => y.OtherProps.Id == id).Select(x => new { ParentId = x.OtherProps.ParentId }).FirstOrDefault().ParentId;
            }
            catch
            {
            }
            ViewData["newParentId"] = id;
            return View(items);
        }
        public ActionResult New(Guid? Id = null)
        {
            ViewData["newParentId"] = Id;
            return View();
        }
        [HttpPost]
        public ActionResult New(v_Group props)
        { 
            if (!new DbController.Model_Group().Insert(props))
            {
                ViewData["Message"] = "خطا در ایجاد گروه";
                return View();

            }
            else
            {
                return RedirectToRoute(new { id = props.ParentId, action = "Index", controller = "Menu" });

            }
        }

        public ActionResult Update(Guid Id)
        {
            v_Group group = new DbController.Model_Group().Select().Where(x => x.OtherProps.Id == Id).Select(x => new v_Group { AdminUsername = x.OtherProps.AdminUsername, Id = x.OtherProps.Id, Name = x.OtherProps.Name, ParentId = x.OtherProps.ParentId, Posts = x.OtherProps.Posts, Publish = x.OtherProps.Publish, Serial = x.OtherProps.Serial, SubGroups = x.OtherProps.SubGroups }).FirstOrDefault();
            return View(group);
        }
        [HttpPost]
        public ActionResult Update(Guid Id, v_Group props)
        {
            props.Id = Id;
            props.AdminUsername = Session["Username"].ToString();
            if (!new DbController.Model_Group().Update(props))
            {
                ViewData["Message"] = "خطا در به روز رسانی پست";
                return View();

            }
            else
            {
                return RedirectToRoute(new { id = props.ParentId, action = "Index", controller = "Menu" });

            }
        }

        public ActionResult Delete(Guid? Id = null)
        {
            v_Group group = new DbController.Model_Group().Select().Where(x => x.OtherProps.Id == Id).Select(y => new v_Group { Name = y.OtherProps.Name, Id = y.OtherProps.Id }).FirstOrDefault();
            ViewData["Message"] = "آیا از حذف این گروه اطمینان دارید؟";
            return View(group);
        }
        [HttpPost]
        public ActionResult Delete(Guid Id)
        {
            Guid? ParentId = new DbController.Model_Group().Select().Where(x => x.OtherProps.Id == Id).FirstOrDefault().OtherProps.ParentId;
            if (!new DbController.Model_Group().Delete(Id))
            {
                ViewData["Message"] = "گروه حذف نشد";
                return View();
            }
            else
            {
                return RedirectToRoute(new { id = ParentId, action = "Index", controller = "Menu" });
            }

        }
    }
}
