using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_Reiseboerse_Graf.Models;

namespace UI_Reiseboerse_Graf.Controllers
{
    public class BuchungenController : Controller
    {
        // GET: Buchungen
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Lädt alle Buchungen aus der Datenbank zu einer bestimmten Reise
        /// </summary>
        /// <param name="reise_id"></param>
        /// <returns></returns>
        public ActionResult LadeAlleBuchungen(int reise_id)
        {

            return View();
        }



        /// <summary>
        /// Fügt alle notwendigen Eingabefelder für Buchungsdaten in eine View
        /// </summary>
        /// <param name="anzahl">die ausgewählte Anzahl der Buchungen bei der View Anzeigen</param>
        /// <returns>die View zum Eingeben der Daten</returns>
        [HttpPost]
        public ActionResult Hinzufuegen(BuchungAnzahlModel anzahl)
        {
            BuchungHinzufuegenModel model = new BuchungHinzufuegenModel()
            {
                AnzahlModel = anzahl,
                Buchungen = new List<BuchungenModel>()

            };
            for (int i = 0; i < model.AnzahlModel.Anzahl; i++)
            {
                BuchungenModel bm = new BuchungenModel();
                model.Buchungen.Add(bm);
            }
            return View(model);
        }

        /// <summary>
        /// Fügt eine Buchung hinzu anhand des übergebenen Models
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult Buchen(List<BuchungenModel> liste)
        {
            
            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Laden", "Login");
            }
            else
            {
               
            }
            return null;



        }
    }
}