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

            Debug.Unindent();
            return View(liste);
        }
        [PruefeBenutzer]
        [HttpGet]
        public ActionResult Verwaltung()
        {
            Debug.WriteLine("Home - Verwaltung - GET");
            Debug.Indent();
            Debug.Unindent();
            return View();
        }

        
    }
}