using System;
using System.Web.Mvc;

namespace WebInterface.Controllers
{
    class SecureAttribute : ActionFilterAttribute, IActionFilter
    {
        public bool IsAdmin { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool CanAccess = false;
            try
            {
                if (filterContext.HttpContext.Session["Username"] != null)
                {
                    if (IsAdmin && !Convert.ToBoolean(filterContext.HttpContext.Session["IsAdmin"].ToString()))
                        CanAccess = false;
                    else
                        CanAccess = true;
                }
            }
            catch
            {
                CanAccess = false;
            }
            if (!CanAccess)
                filterContext.Result = new HttpStatusCodeResult(401);
        }
    }

}