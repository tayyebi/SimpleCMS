using DbController;
using Rexa.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebInterface.Controllers
{

    [Secure(IsAdmin = false)]
    public class FilesController : Controller
    {
        // GET: Files
        public ActionResult Index()
        {
            var files = new DbController.Model_File().Select().Select(m => new v_File { Type = m.Type, Name = m.Name, Id = m.Id }).OrderByDescending(m => m.Name).ToList();

            return View(files);
        }

        // GET: Files/Create
        public ActionResult Upload()
        {
            return View();
        }

        // POST: Files/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(string Name, HttpPostedFileBase Parvandeh)
        {
   
                var _Bytes = new byte[Parvandeh.ContentLength];
                Parvandeh.InputStream.Read(_Bytes, 0, (int)Parvandeh.ContentLength);

                new DbController.Model_File().Insert(Name, Parvandeh.ContentType, _Bytes);
                return RedirectToAction("Index");

        }

        // GET: Files/Edit/5
        public ActionResult Update(Guid id)
        {
            var file = new DbController.Model_File().Select().Where(x => x.Id == id).Select(m => new v_File { Id = m.Id, Name = m.Name, Lenght = m.Lenght, Type = m.Type }).FirstOrDefault();
            return View(file);
        }

        // POST: Files/Edit/5
        [HttpPost]
        public ActionResult Update(Guid id, string Name, HttpPostedFile Parvandeh)
        {
            try
            {
                if (Parvandeh == null)
                {
                    new DbController.Model_File().Update(id, Parvandeh.ContentType, Name, null);
                }
                else
                {
                    var _Bytes = new byte[Parvandeh.ContentLength];
                    Parvandeh.InputStream.Read(_Bytes, 0, (int)Parvandeh.ContentLength);

                    new DbController.Model_File().Update(id, Parvandeh.ContentType, Name, _Bytes);

                }
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "خطا در به روز رسانی";
                var file = new DbController.Model_File().Select().Where(x => x.Id == id).Select(m => new v_File { Id = m.Id, Name = m.Name, Lenght = m.Lenght, Type = m.Type }).FirstOrDefault();
                return View(file);
            }
        }

        // GET: Files/Delete/5
        public ActionResult Drop(Guid? id)
        {

            var file = new DbController.Model_File().Select().Where(x => x.Id == id).Select(m => new v_File { Id = m.Id, Name = m.Name, Lenght = m.Lenght, Type = m.Type }).FirstOrDefault();
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost]
        public ActionResult Drop(Guid id, FormCollection collection)
        {
            try
            {
                new DbController.Model_File().Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "خطا در حذف فایل";
                var file = new DbController.Model_File().Select().Where(x => x.Id == id).Select(m => new v_File { Id = m.Id, Name = m.Name, Lenght = m.Lenght, Type = m.Type }).FirstOrDefault();
                return View(file);
            }
        }
    }
}
