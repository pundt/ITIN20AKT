using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL_Reiseboerse_Graf;
using System.Diagnostics;
using System.Web.Security;

namespace UI_Reiseboerse_Graf.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Debug.WriteLine("Home - Index - GET");
            Debug.Indent();
            List<int> liste = new List<int>();
            liste=BildVerwaltung.LadeAlleBildIDs();

            Roles.AddUserToRole("marco@itfox.at", "Mitarbeiter");
            Roles.AddUserToRole("claudia@itfox.at", "Mitarbeiter");

            Debug.Unindent();
            return View(liste);
        }

        
    }
}