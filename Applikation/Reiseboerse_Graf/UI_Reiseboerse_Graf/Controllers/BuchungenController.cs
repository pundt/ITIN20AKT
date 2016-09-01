﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_Reiseboerse_Graf.Models;
using BL_Reiseboerse_Graf;
using System.Collections.Specialized;

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
            Debug.WriteLine("Buchungen - Lade Alle Buchungen - GET");
            Debug.Indent();
            Debug.Unindent();
            return View();
        }

        /// <summary>
        /// Lädt alle Reisen die der Benutzer bereits gebucht hat, die dann anschließend bewertet werden können
        /// </summary>
        /// <param name="id">ID des Benutzers</param>
        /// <returns>eine PartialView die in das Benutzer Aktualisieren View gerendert wird</returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult LadeAlleBuchungenBenutzer(int id)
        {
            Debug.WriteLine("Buchungen - LadeAlleBuchungenBenutzer - GET");
            Debug.Indent();
            List<Buchung> BL_Liste = BuchungsVerwaltung.LadeAlleReiseBuchungenBenutzer(id);
            List<BuchungAnzeigenModel> UI_Liste = new List<BuchungAnzeigenModel>();
            foreach (var aktbuchung in BL_Liste)
            {
                UI_Liste.Add(new BuchungAnzeigenModel()
                {
                    Startdatum = aktbuchung.Reisedatum.Startdatum,
                    Enddatum = aktbuchung.Reisedatum.Enddatum,
                    Reisetitel = aktbuchung.Reisedatum.Reise.Titel
                });
            }
            Debug.Unindent();
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
            Debug.WriteLine("Buchungen - Hinzufuegen - GET");
            Debug.Indent();
            Debug.Unindent();
            return View(model);
        }

        /// <summary>
        /// Fügt Buchungen anhand der Formulardaten (Request) hinzu
        /// </summary>
        /// <returns>Kontrollansicht der Daten</returns>
        [HttpPost]
        public ActionResult Hinzufuegen()
        {
            Debug.WriteLine("Buchungen - Hinzufuegen - POST");
            Debug.Indent();
            NameValueCollection col = new NameValueCollection();
            BuchungGesamtModel model = new BuchungGesamtModel();
            try
            {
                col = Request.Form;

                List<BuchungenModel> buchungenErw = new List<BuchungenModel>();
                List<BuchungenModel> buchungenKind = new List<BuchungenModel>();

                int anzahlErw = int.Parse(col["AnzahlModel.Anzahl_Erwachsene"]);
                int anzahlKind = int.Parse(col["AnzahlModel.Anzahl_Kinder"]);

                decimal preisErw = decimal.Parse(col["AnzahlModel.Preis_Erwachsene"]);
                decimal preisKind = decimal.Parse(col["AnzahlModel.Preis_Kind"]);

                model.Gesamtpreis = (anzahlErw * preisErw) + (anzahlKind * preisKind);

                for (int i = 0; i < anzahlErw; i++)
                {
                    buchungenErw.Add(new BuchungenModel()
                    {
                        Vorname = col[string.Format("BuchungenErwachsen[{0}].Vorname", i)],
                        Nachname = col[string.Format("BuchungenErwachsen[{0}].Nachname", i)],
                        Geburtsdatum = Convert.ToDateTime(col[string.Format("BuchungenErwachsen[{0}].Geburtsdatum", i)]),
                        ReisePassNummer = col[string.Format("BuchungenErwachsen[{0}].ReisePassNummer", i)],
                        Reisedatum_ID = int.Parse(col[string.Format("BuchungenErwachsen[{0}].Reisedatum_ID", i)])
                    });
                }
                for (int i = 0; i < anzahlKind; i++)
                {
                    buchungenKind.Add(new BuchungenModel()
                    {
                        Vorname = col[string.Format("BuchungenKind[{0}].Vorname", i)],
                        Nachname = col[string.Format("BuchungenKind[{0}].Nachname", i)],
                        Geburtsdatum = Convert.ToDateTime(col[string.Format("BuchungenKind[{0}].Geburtsdatum", i)]),
                        ReisePassNummer = col[string.Format("BuchungenKind[{0}].ReisePassNummer", i)],
                        Reisedatum_ID = int.Parse(col[string.Format("BuchungenKind[{0}].Reisedatum_ID", i)])
                    });
                }
                model.BuchungErwachsen = buchungenErw;
                model.BuchungKind = buchungenKind;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler beim Auslesen der Formulardaten");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
                Debug.Unindent();

            }

            if (ModelState.IsValid)
            {
                bool gespeichert = false;

                foreach (var item in model.BuchungErwachsen)
                {
                    Buchung neueBuchung = new Buchung()
                    {
                        Vorname = item.Vorname,
                        Nachname = item.Nachname,
                        Geburtsdatum = item.Geburtsdatum,
                        Passnummer = item.ReisePassNummer,
                        ErstelltAm = DateTime.Now
                    };

                    gespeichert = BuchungsVerwaltung.NeueBuchungSpeichern(neueBuchung, User.Identity.Name);
                    if (!gespeichert)
                    {
                        Debug.WriteLine("Fehler!");
                    }
                }
                foreach (var item in model.BuchungKind)
                {
                    Buchung neueBuchung = new Buchung()
                    {
                        Vorname = item.Vorname,
                        Nachname = item.Nachname,
                        Geburtsdatum = item.Geburtsdatum,
                        Passnummer = item.ReisePassNummer
                    };
                    gespeichert = BuchungsVerwaltung.NeueBuchungSpeichern(neueBuchung, User.Identity.Name);
                    if (!gespeichert)
                    {
                        Debug.WriteLine("Fehler!");
                    }
                }
            }
            return View("ZeigeGesamt", model);
        }

        /// <summary>
        /// Speichert die Reisedurchführung_IDs und gibt die View zur Eingabe der Zahlungsdaten zurück
        /// </summary>
        /// <param name="model">beinhaltet alle BuchungenModel</param>
        /// <returns>View zur Eingabe der Zahlungsdaten</returns>
        [HttpGet]
        public ActionResult Zahlung(BuchungGesamtModel model)
        {
            Debug.WriteLine("Buchungen - Zahlung - GET");
            ZahlungModel zahlung = new ZahlungModel();
            zahlung.Reisedatum_ID = model.BuchungErwachsen[0].Reisedatum_ID;
            return View(zahlung);
        }

        /// <summary>
        /// Nimmt das übergebene ZahlungModel entgegen und übergibt es an die BL
        /// </summary>
        /// <param name="model">das ZahlungModel</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Zahlung(ZahlungModel model)
        {
            Debug.WriteLine("Buchung - Zahlung - POST");
            Debug.Indent();

            if (ModelState.IsValid)
            {
                Zahlung zahlung = new Zahlung()
                {
                    Vorname = model.Vorname,
                    Nachname = model.Nachname,
                    Nummer = model.Nummer
                };
                zahlung.Zahlungsart.ID = model.Zahlungsart_ID;

                int neueID = ZahlungsVerwaltung.NeueZahlungSpeichern(zahlung);
                ZahlungsVerwaltung.ZuordnungZahlungBuchung(model.Reisedatum_ID, neueID);
            }
            Debug.Unindent();
            return null;
        }

        /// <summary>
        /// Erstellt für jede Person in AnzahlModel Eingabefelder für die Daten, die für die Buchung erforderlich sind
        /// </summary>
        /// <param name="anzahlmodel">Die Anzahl der gewünschten Buchungen und zusätzl. Daten</param>
        /// <returns>View zum Eingeben der Daten</returns>
        [HttpPost]
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
            //for (int i = 0; i < model.AnzahlModel.Anzahl_Erwachsene; i++)
            //{
            //    aktReisedurchfuehrung_ID = aktReisedurchfuehrung_ID + 1;
            //    BuchungenModel bm = new BuchungenModel()
            //    {
            //        Reisedurchfuehrung_ID = aktReisedurchfuehrung_ID
            //    };

            //    model.BuchungenErwachsen.Add(bm);

            //}
            //for (int i = 0; i < model.AnzahlModel.Anzahl_Kinder; i++)
            //{
            //    aktReisedurchfuehrung_ID = aktReisedurchfuehrung_ID + 1;
            //    BuchungenModel bm = new BuchungenModel()
            //    {
            //        Reisedurchfuehrung_ID = aktReisedurchfuehrung_ID
            //    };

            //    model.BuchungenKind.Add(bm);
            //}
            Debug.Unindent();
            return View("Hinzufuegen", model);
        }
    }
}