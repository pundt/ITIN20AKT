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

        /// <summary>
        /// Fügt eine neue Reise hinzu
        /// </summary>
        /// <param name="neueReise">Model mit den entsprechenden Daten</param>
        /// <returns></returns>
        public ActionResult Hinzufuegen(ReiseModel neueReise)
        {
            return View();
        }

        /// <summary>
        /// Löscht das übergebene ReiseModel
        /// </summary>
        /// <param name="reise">Das ReiseModel das gelöscht werden soll</param>
        /// <returns>Redirect to Index (Reise)</returns>
        public ActionResult Loeschen(ReiseModel reise)
        {

            return RedirectToAction("Index");
        }
    }
}