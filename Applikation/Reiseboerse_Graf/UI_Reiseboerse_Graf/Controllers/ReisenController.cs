using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_Reiseboerse_Graf.Models;

namespace UI_Reiseboerse_Graf.Controllers
{
    public class ReisenController : Controller
    {
        /// <summary>
        /// Reisenstartseite mit allen Reisen
        /// </summary>
        /// <returns>Den View Index</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Filtert die Reisen nach dem übergebenen FilterModel 
        /// und ruft die BL_Reiseverwaltung.LadeReisenGefiltert auf
        /// </summary>
        /// <returns>den View Index</returns>
        public ActionResult ReisenFilter(FilterModel filterung)
        {
            return View();
        }

        public ActionResult Hinzufuegen(UI_ReiseModel neueReise)
        {

        }
    }
}