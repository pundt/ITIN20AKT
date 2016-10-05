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
    /// <summary>
    /// Im HomeController werden die Startseite des Reisebüros und die Startseite der Verwaltung verarbeitet
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Startseite des Projektes, hier wird auch die Ansicht mit Bildern aus der Datenbank gefüllt
        /// </summary>
        /// <returns>Die Startseite mit Bildern</returns>
        public ActionResult Index()
        {
            Debug.WriteLine("Home - Index - GET");
            Debug.Indent();
            List<int> liste = new List<int>();
            liste=BildVerwaltung.LadeAlleBildIDs();

            Debug.Unindent();
            return View(liste);
        }

        /// <summary>
        /// Liefert die Oberfläche für die Verwaltung zurück (nur aufrufbar für den Mitarbeiter)
        /// </summary>
        /// <returns>die View</returns>
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