using System;
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
            model.BuchungIDs = new List<int>();
            int reisedatum_ID = 0;
            try
            {
                //holt sich die Daten aus dem Payload des Formulars
                col = Request.Form;

                List<BuchungErwachsenModel> buchungenErw = new List<BuchungErwachsenModel>();
                List<BuchungKindModel> buchungenKind = new List<BuchungKindModel>();

                reisedatum_ID = int.Parse(col["AnzahlModel.Reisedatum_ID"]);
                int anzahlErw = int.Parse(col["AnzahlModel.Anzahl_Erwachsene"]);
                int anzahlKind = int.Parse(col["AnzahlModel.Anzahl_Kinder"]);

                decimal preisErw = decimal.Parse(col["AnzahlModel.Preis_Erwachsene"]);
                decimal preisKind = decimal.Parse(col["AnzahlModel.Preis_Kind"]);

                model.Gesamtpreis = (anzahlErw * preisErw) + (anzahlKind * preisKind);

                for (int i = 0; i < anzahlErw; i++)
                {
                    buchungenErw.Add(new BuchungErwachsenModel()
                    {
                        Vorname = col[string.Format("BuchungenErwachsen[{0}].Vorname", i)],
                        Nachname = col[string.Format("BuchungenErwachsen[{0}].Nachname", i)],
                        Geburtsdatum = Convert.ToDateTime(col[string.Format("BuchungenErwachsen[{0}].Geburtsdatum", i)]),
                        ReisePassNummer = col[string.Format("BuchungenErwachsen[{0}].ReisePassNummer", i)],
                        Reisedatum_ID = reisedatum_ID
                    });
                }
                for (int i = 0; i < anzahlKind; i++)
                {
                    buchungenKind.Add(new BuchungKindModel()
                    {
                        Vorname = col[string.Format("BuchungenKind[{0}].Vorname", i)],
                        Nachname = col[string.Format("BuchungenKind[{0}].Nachname", i)],
                        Geburtsdatum = Convert.ToDateTime(col[string.Format("BuchungenKind[{0}].Geburtsdatum", i)]),
                        ReisePassNummer = col[string.Format("BuchungenKind[{0}].ReisePassNummer", i)],
                        Reisedatum_ID = reisedatum_ID
                    });
                }
                model.BuchungErwachsen = buchungenErw;
                model.BuchungKind = buchungenKind;
                Reise reise = ReiseVerwaltung.SucheReiseZuDatum(reisedatum_ID);
                model.Reisetitel = reise.Titel;
                Reisedatum datum = ReiseVerwaltung.SucheReisedatum(reisedatum_ID);
                model.Startdatum = datum.Startdatum;
                model.Enddatum = datum.Enddatum;

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

                    int neueID = BuchungsVerwaltung.NeueBuchungSpeichern(neueBuchung, reisedatum_ID, User.Identity.Name);

                    model.BuchungIDs.Add(neueID);
                    if (neueID == -1)
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
                    int neueID = BuchungsVerwaltung.NeueBuchungSpeichern(neueBuchung, reisedatum_ID, User.Identity.Name);
                    model.BuchungIDs.Add(neueID);
                    if (neueID == -1)
                    {
                        Debug.WriteLine("Fehler!");
                    }
                }
                Session["Buchungen"] = model.BuchungIDs as List<int>;
            }
            return View("ZeigeGesamt", model);
        }

        /// <summary>
        /// Speichert die Reisedurchführung_IDs und gibt die View zur Eingabe der Zahlungsdaten zurück
        /// </summary>
        /// <param name="model">beinhaltet alle BuchungenModel</param>
        /// <returns>View zur Eingabe der Zahlungsdaten</returns>
        [HttpGet]
        public ActionResult Zahlung()
        {
            Debug.WriteLine("Buchungen - Zahlung - GET");
            ZahlungModel zahlung = new ZahlungModel();
            zahlung.Zahlungsarten = new List<ZahlungsartModel>();
            foreach (var zahlungsart in ZahlungsVerwaltung.LadeAlleZahlungsArten())
            {
                zahlung.Zahlungsarten.Add(new ZahlungsartModel()
                {
                    Bezeichnung = zahlungsart.Bezeichnung,
                    ID = zahlungsart.ID
                });
            }
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

                int neueID = ZahlungsVerwaltung.NeueZahlungSpeichern(zahlung, model.Zahlungsart_ID);
                List<int> BuchungIDs = Session["Buchungen"] as List<int>;
                ZahlungsVerwaltung.ZuordnungZahlungBuchung(BuchungIDs, neueID);
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
        public ActionResult Buchen()
        {
            Debug.WriteLine("Buchungen - Hinzufuegen - POST");
            Debug.Indent();
            BuchungHinzufuegenModel model = new BuchungHinzufuegenModel();
            NameValueCollection col = new NameValueCollection();
            try
            {
                col = Request.Form;

                string preisE = col["Reisedetail.Preis_Erwachsene"];
                string preisK = col["Reisedetail.Preis_Kind"];
                //preisE = preisE.Replace(',', '.');
                //preisK = preisK.Replace(',', '.');             

                BuchungAnzahlModel anzahl = new BuchungAnzahlModel();
                anzahl.Preis_Erwachsene = decimal.Parse(preisE);
                anzahl.Preis_Kind = decimal.Parse(preisK);
                anzahl.Reisetitel = col["Reisedetail.Titel"];
                anzahl.Anzahl_Erwachsene = int.Parse(col["Anzahl_Erwachsene"]);
                anzahl.Anzahl_Kinder = int.Parse(col["Anzahl_Kinder"]);
                anzahl.Reisedatum_ID = int.Parse(col["Reisedatum.ID"]);
                model.AnzahlModel = anzahl;
                model.BuchungenErwachsen = new List<BuchungErwachsenModel>();
                model.BuchungenKind = new List<BuchungKindModel>();
                for (int i = 0; i < anzahl.Anzahl_Erwachsene; i++)
                {
                    model.BuchungenErwachsen.Add(new BuchungErwachsenModel());
                }
                for (int i = 0; i < anzahl.Anzahl_Kinder; i++)
                {
                    model.BuchungenKind.Add(new BuchungKindModel());
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler bei Buchung - Buchen - POST (Auslesen der Formulardaten)");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }
            Debug.Unindent();
            return View("Hinzufuegen", model);
        }
    }
}