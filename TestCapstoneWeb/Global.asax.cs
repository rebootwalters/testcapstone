using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TestCapstoneWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ApplicationConfig.RegisterAplicationVariables();


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

           
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            if (ex is System.Threading.ThreadAbortException)
                return; // Redirects may cause this exception..
            Lumberjack.Logger.Log(ex);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
       {
            string UserName = Session["AUTHUserName"] as string;
            string Sessroles = Session["AUTHRoles"] as string;
            if (string.IsNullOrEmpty(UserName))
            {
                return;
            }
            GenericIdentity i = new GenericIdentity(UserName, "MyCustomType");
            if (Sessroles == null)  {Sessroles = "";}
            string[] roles = Sessroles.Split(' ');
            GenericPrincipal p = new GenericPrincipal(i, roles);
            HttpContext.Current.User = p;
        }
    }
}
