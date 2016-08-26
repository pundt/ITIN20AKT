using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_Reiseboerse_Graf.Models;
using BL_Reiseboerse_Graf;

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
        [HttpGet]
        public ActionResult LadeAlleBuchungen(int reise_id)
        {
            return View();
        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult LadeAlleBuchungenBenutzer(int id)
        {
            List<Buchung> BL_Liste=BuchungsVerwaltung.LadeAlleEinzelBuchungenBenutzer(id);
            List<BuchungAnzeigenModel> UI_Liste = new List<BuchungAnzeigenModel>();
            foreach (var aktbuchung in BL_Liste)
            {
                UI_Liste.Add(new BuchungAnzeigenModel()
                {
                    Startdatum = aktbuchung.Reisedurchfuehrung.Reisedatum.Startdatum,
                    Enddatum = aktbuchung.Reisedurchfuehrung.Reisedatum.Enddatum,
                    Reisetitel = aktbuchung.Reisedurchfuehrung.Reisedatum.Reise.Titel
                });
            }
            return PartialView(UI_Liste);
        }



        /// <summary>
        /// Fügt alle notwendigen Eingabefelder für Buchungsdaten in eine View
        /// </summary>
        /// <param name="anzahl">die ausgewählte Anzahl der Buchungen bei der View Anzeigen</param>
        /// <returns>die View zum Eingeben der Daten</returns>
        [HttpGet]
        public ActionResult Hinzufuegen(BuchungHinzufuegenModel model)
        {
            return View(model);

        }

        /// <summary>
        /// Fügt Buchungen hinzu anhand des übergebenen Models
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult Hinzufuegen(List<BuchungenModel> liste)
        {
            if (ModelState.IsValid)
            {

            }
            else
            {

            }
            return null;



        }

        public ActionResult Buchen(BuchungAnzahlModel anzahlmodel)
        {
            Debug.WriteLine("Buchungen - Hinzufuegen - POST");
            Debug.Indent();

            BuchungHinzufuegenModel model = new BuchungHinzufuegenModel()
            {
                AnzahlModel = anzahlmodel,
                BuchungenErwachsen = new List<BuchungenModel>(),
                BuchungenKind = new List<BuchungenModel>()

            };
            int aktReisedurchfuehrung_ID = BuchungsVerwaltung.Ermittle_aktID(anzahlmodel.Reise_ID, anzahlmodel.Beginndatum);
            for (int i = 0; i < model.AnzahlModel.Anzahl_Erwachsene; i++)
            {
                aktReisedurchfuehrung_ID = aktReisedurchfuehrung_ID + i;
                BuchungenModel bm = new BuchungenModel()
                {
                    Reisedurchfuehrung_ID = aktReisedurchfuehrung_ID
                };

                model.BuchungenErwachsen.Add(bm);

            }
            for (int i = 0; i < model.AnzahlModel.Anzahl_Kinder; i++)
            {
                aktReisedurchfuehrung_ID = aktReisedurchfuehrung_ID + i;
                BuchungenModel bm = new BuchungenModel()
                {
                    Reisedurchfuehrung_ID = aktReisedurchfuehrung_ID
                };

                model.BuchungenKind.Add(bm);
            }
            Debug.Unindent();
            return View("Hinzufuegen", model);
        }
    }
}