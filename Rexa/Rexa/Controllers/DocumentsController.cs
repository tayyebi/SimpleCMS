using System;
using System.Linq;
using DbController;
using System.Web.Mvc;
using Rexa.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebInterface.Controllers
{
    public class DocumentsController : Controller
    {
        public ActionResult Download(Guid Id)
        {

            var file = new DbController.Model_File().Select().Where(y => y.Id == Id).FirstOrDefault();

            Response.ContentType = file.Type;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + file.Name);
            //Response.ContentLenght = file.Lenght;
            Response.StatusCode = 200;
            return File(file.Content.ToArray(), file.Type);
        }

        public ActionResult Post(Guid id)
        {
            var tt = new DbController.Model_Post().Select().Where(x => x.Id == id).FirstOrDefault();
            ViewBag.Title = tt.Title;
            return View(tt);
        }

        public ActionResult Group(Guid id)
        {
            var group = new DbController.Model_Group().Select().Where(x => x.OtherProps.Id == id).FirstOrDefault().OtherProps;
            ViewBag.Title = group.Name;
            ViewBag.Id = group.Id.ToString();
            var posts =new Model_Post().Select().Where(x => x.GroupId == id).Select(x => new v_Post { Abstract = x.Abstract, AdminUsername = x.AdminUsername, Date = x.Date, Title = x.Title, Id = x.Id }).OrderByDescending(x => x.Date).Take(30);
            return View(posts);
        }
    }
}