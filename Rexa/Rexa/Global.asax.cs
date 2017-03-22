using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Rexa
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_Error()
        {
            var le = Server.GetLastError();
            try
            {
                File.WriteAllText(Server.MapPath("~/App_Logs") + "/" + ((HttpException)le).GetHttpCode() + " " + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt", "DateTime: " + DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString() + "\nHttpStatusCode: " + ((HttpException)le).GetHttpCode() + "\nSource: " + le.Source + "\nMessage: " + le.Message + "\nHashCode: " + le.GetHashCode());
            }
            catch { }
            Response.Redirect("~/Error/" + ((HttpException)le).GetHttpCode());
        }
    }
}
