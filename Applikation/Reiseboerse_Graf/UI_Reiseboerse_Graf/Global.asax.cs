using BL_Reiseboerse_Graf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UI_Reiseboerse_Graf
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// wird bei JEDEM Aufruf (bevor der Request eigentlich verarbeitet wird)
        /// gestartet
        //internal protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    string userName = Context.User.Identity.Name;

        //    if (Tools.BistDuMitarbeiter(userName))
        //    {
        //        BenutzerVerwaltung.BenutzerRolle = "Mitarbeiter";
        //    }
        //}
    }
}
