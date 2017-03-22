using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebInterface.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Menu()
        {
            return PartialView();
            // return null;
        }
        public ActionResult Admin()
        {
            return View();
        }
        public class ErrorItem
        {
            public int Code { get; set; }
            public string Message { get; set; }
        }
        [Route("~/Error/{id}")]
        public ActionResult Error(int Id = 0)
        {

            List<ErrorItem> ErrorList = new List<ErrorItem>();
            ErrorList.Add(new ErrorItem { Code = 403, Message = "درخواست نامعتبر" });
            ErrorList.Add(new ErrorItem { Code = 404, Message = "یافت نشد" });
            ErrorList.Add(new ErrorItem { Code = 401, Message = "هویت احراز نشد" });

            ErrorItem output = ErrorList.Where(x => x.Code == 403).First();

            switch (Id)
            {
                case 0:
                    break;
                default:
                    var er = ErrorList.Where(x => x.Code == Id).FirstOrDefault();
                    if (er != null)
                        output = er;
                    break;
            }

            ViewData["Message"] = output.Message;
            ViewData["Title"] = output.Code;

            Response.StatusCode = output.Code;
            return View();
        }

    }
}
