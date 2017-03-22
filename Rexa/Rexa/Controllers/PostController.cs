using Rexa.Models;
using System;
using System.Linq;
using System.Web.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebInterface.Controllers
{
    [Secure(IsAdmin = false)]
    public class PostController : Controller
    {
        // GET: /<controller>/
        public ActionResult New(Guid Id) // Id is group id
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult New(Guid Id, v_Post props)
        {
            props.AdminUsername = Session["Username"].ToString();
            props.GroupId =  Id;
            props.Date = DateTime.Now;
            if (!new DbController.Model_Post().Insert(props))
            { 
                ViewData["Message"] = "خطا در ارسال پست جدید";
                return View();

            }
            else
            {
                return RedirectToRoute(new { id = Id, action = "Group", controller = "Documents" });

            }
        }

        public ActionResult Update(Guid Id) // Id is post id
        {
            v_Post Post = new DbController.Model_Post().Select().Where(x => x.Id == Id).Select(y => new v_Post { Title = y.Title, Id = y.Id  , Abstract = y.Abstract, GroupId = y.GroupId , Body = y.Body }).FirstOrDefault();
            return View(Post);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Guid Id, v_Post props)
        {
            props.AdminUsername = Session["Username"].ToString();
            props.Date = DateTime.Now;
            if (!new DbController.Model_Post().Update(props))
            {
                ViewData["Message"] = "خطا در به روز رسانی پست";
                return View();

            }
            else
            {
                return RedirectToRoute(new { id = props.GroupId, action = "Group", controller = "Documents" });

            }
        }

        public ActionResult Delete(Guid? Id = null)
        {
            v_Post Post = new DbController.Model_Post().Select().Where(x => x.Id == Id).Select(y => new v_Post { Title = y.Title, Id = y.Id }).FirstOrDefault();
            ViewData["Message"] = "آیا از حذف این پست اطمینان دارید؟";
            return View(Post);
        }
        [HttpPost]
        public ActionResult Delete(Guid Id)
        {
            Guid? GroupId = new DbController.Model_Post().Select().Where(x => x.Id == Id).FirstOrDefault().GroupId;
            if (!new DbController.Model_Post().Delete(Id))
            {
                ViewData["Message"] = "پست حذف نشد";
                return View();
            }
            else
            {
                return RedirectToRoute(new { id = GroupId, action = "Group", controller = "Documents" });
            }

        }
    }
}
