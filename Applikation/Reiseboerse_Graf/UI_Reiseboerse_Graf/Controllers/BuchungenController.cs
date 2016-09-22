using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_Reiseboerse_Graf.Models;
using BL_Reiseboerse_Graf;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

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
        /// Lädt alle Buchungen aus der Datenbank zu einem bestimmten BuchungsDatums.
        /// </summary>
        /// <param name="reisedatum_id"></param>
        /// <returns>Eine View mit allen Daten laut der Filterung</returns>
        //[PruefeBenutzer]
        //[HttpGet]
        //public ActionResult LadeAlleBuchungenDatum(int reisedatum_id)
        //{
        //    Debug.WriteLine("Buchungen - Lade Alle Buchungen Datum - GET");
        //    Debug.Indent();
        //    List<Buchung> BL_Liste = BuchungsVerwaltung.LadeAlleBuchungen(reisedatum_id);
        //    List<BuchungAnzeigenModel> UI_Liste = new List<BuchungAnzeigenModel>();
        //    foreach (Buchung buchung in BL_Liste)
        //    {
        //        UI_Liste.Add(new BuchungAnzeigenModel()
        //        {
        //            ReiseID = buchung.Reisedatum.Reise.ID,
        //            Startdatum = buchung.Reisedatum.Startdatum,
        //            Enddatum = buchung.Reisedatum.Enddatum,
        //            Reisetitel = buchung.Reisedatum.Reise.Titel,
        //            Reisedatum_ID = buchung.Reisedatum.ID
        //        });
        //    }
        //    Debug.Unindent();
        //    return View();
        //}
        /// <summary>
        /// Lädt alle Buchungen aus der Datenbank zu einer bestimmten Reise
        /// </summary>
        /// <param name="reise_id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LadeAlleBuchungenReise(int reise_id)
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
            List<BuchungAnzeigenModel> UI_Liste = new List<BuchungAnzeigenModel>();
            List<Buchung> BL_Buchung = BuchungsVerwaltung.LadeAlleEinzelBuchungenBenutzer(id);
            foreach (var buchung in BL_Buchung)
            {
                UI_Liste.Add(new BuchungAnzeigenModel()
                {
                    Reisetitel = buchung.Reisedatum.Reise.Titel,
                    Startdatum = buchung.Reisedatum.Startdatum,
                    Enddatum=buchung.Reisedatum.Enddatum,
                    ReiseID = buchung.Reisedatum.Reise.ID,
                    Benutzer_ID = id,
                    Reisedatum_ID=buchung.Reisedatum.ID
                });
            }
            List<BuchungAnzeigenModel> neueListe = new List<BuchungAnzeigenModel>();
            List<int> reisedaten = new List<int>();
            neueListe.Add(UI_Liste[0]);
            reisedaten.Add(UI_Liste[0].Reisedatum_ID);
            for (int i = 0; i < UI_Liste.Count; i++)
            {
                if (!reisedaten.Contains(UI_Liste[i].Reisedatum_ID))
                {
                    reisedaten.Add(UI_Liste[i].Reisedatum_ID);
                    neueListe.Add(UI_Liste.Where(x => x.Reisedatum_ID == UI_Liste[i].Reisedatum_ID).FirstOrDefault());
                }
            }
            Debug.Unindent();
            return PartialView(neueListe);
        }

        /// <summary>
        /// Zeigt die Details zu der Buchung eines Benutzers (alle Reisenden),
        /// die er dann einzeln stornieren kann
        /// </summary>
        /// <param name="id">die ID der Buchung (Reisedatum)</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult ZeigeDetails(int id)
        {
            Debug.WriteLine("Buchungen - ZeigeDetails - GET");
            Debug.Indent();
            List<Buchung> BL_Liste = BuchungsVerwaltung.LadeAlleEinzelBuchungenBenutzer(id);
            List<BuchungenModel> UI_BuchungListe = new List<BuchungenModel>();
            foreach (var buchung in BL_Liste)
            {
                if (buchung.Reisedatum.ID==id)
                {
                    BuchungenModel model = new BuchungenModel()
                    {
                        ID = buchung.ID,
                        Vorname = buchung.Vorname,
                        Nachname = buchung.Nachname,
                        ReisePassNummer = buchung.Passnummer,
                        Reisedatum_ID = buchung.ID
                    };
                    model.Stornierbar = BuchungsVerwaltung.Stornierbar(model.ID);
                    UI_BuchungListe.Add(model);
                }
            }
            
            Debug.Unindent();
            return View(UI_BuchungListe);
        }

        /// <summary>
        /// Lädt alle Buchungen für den Mitarbeiter die nach reisedatum_id und benutzer_id sortiert sind
        /// </summary>
        /// <param name="reiseDatum_id">int32</param>
        /// <param name="benutzer_id">in32</param>
        /// <returns>Eine View mit allen Daten der gefilterten Buchungen</returns>
        [PruefeBenutzer]
        [HttpGet]
        public ActionResult LadeAlleBuchungenMitarbeiter(int reiseDatum_id, int benutzer_id)
        {
            Debug.WriteLine("Buchungen - LadeAlleBuchungenMitarbeiter - GET");
            Debug.Indent();
            List<Buchung> BL_ListeMitarbeiter = BuchungsVerwaltung.LadeAlleBuchungenZuReiseDatumUndBenutzer(reiseDatum_id,benutzer_id);
            List<BuchungAnzeigenModel> UI_Liste = new List<BuchungAnzeigenModel>();
            foreach (var aktbuchung in BL_ListeMitarbeiter)
            {
                UI_Liste.Add(new BuchungAnzeigenModel()
                {
                    ReiseID = aktbuchung.Reisedatum.Reise.ID,
                    Startdatum = aktbuchung.Reisedatum.Startdatum,
                    Enddatum = aktbuchung.Reisedatum.Enddatum,
                    Reisetitel = aktbuchung.Reisedatum.Reise.Titel 
                });
            }
            Debug.Unindent();

            return View();

        }


        /// <summary>
        /// Fügt alle notwendigen Eingabefelder für Buchungsdaten in eine View
        /// </summary>
        /// <param name="anzahl">die ausgewählte Anzahl der Buchungen bei der View Anzeigen</param>
        /// <returns>die View zum Eingeben der Daten</returns>
        [Authorize]
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
        [Authorize]
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
                model.ReiseID = reise.ID;
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
                        Passnummer = item.ReisePassNummer
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
                Session["Mailtext"] = MailTextErzeugen(model) as string;
            }
            return View("ZeigeGesamt", model);
        }

        /// <summary>
        /// Erstellt ein neues ZahlungsModel zur Eingabe der Zahlungsdaten
        /// müsste im Produktivbetrieb eigentlich SSL verschlüsselt sein (https)
        /// </summary>
        /// <returns>View zur Eingabe der Zahlungsdaten</returns>
        [Authorize]
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
        /// <returns>falls ModelState.IsValid falsch zurück liefert, gibt es die View zurück</returns>
        [Authorize]
        [HttpPost]
        public ActionResult Zahlung(ZahlungModel model)
        {
            Debug.WriteLine("Buchung - Zahlung - POST");
            Debug.Indent();

            bool erfolgreich = false;

            if (ModelState.IsValid)
            {
                if (!Regex.IsMatch(model.Nummer, "^[A-Z]{2}"))
                {
                    if (ZahlungsVerwaltung.PruefeLuhn(model.Nummer))
                    {
                        erfolgreich = ZahlungSpeichern(model);
                        //Aufruf Buchungsbestätigung für den Kunden
                        string text = Session["Mailtext"] as string;
                        //bool gesendet = EmailVerwaltung.BuchungBestaetigen(User.Identity.Name, text);
                    }
                }
                else
                {
                    erfolgreich = ZahlungSpeichern(model);
                    //Aufruf Buchungsbestätigung für den Kunden
                    string text = Session["Mailtext"] as string;
                    //bool gesendet = EmailVerwaltung.BuchungBestaetigen(User.Identity.Name, text);
                }


                //Könnte man noch einbauen:
                // Wenn gesendet false ergibt, Nachfrage ob Email korrekt war etc...

            }
            if (!erfolgreich)
            {
                return RedirectToAction("Zahlung");
            }
            Debug.Unindent();
            return View("Bestaetigung");
        }

        /// <summary>
        /// Erstellt für jede Person in AnzahlModel Eingabefelder für die Daten, die für die Buchung erforderlich sind
        /// </summary>
        /// <param name="anzahlmodel">Die Anzahl der gewünschten Buchungen und zusätzl. Daten</param>
        /// <returns>View zum Eingeben der Daten</returns>
        [Authorize]
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

        /// <summary>
        /// Erzeugt den Mailtext der als Buchungsbestätigung an den Benutzer gesendet wird
        /// </summary>
        /// <param name="model">Das Buchungsmodel, mit den Daten, die in den Mailtext vorbereitet</param>
        /// <returns>den Mailtext als string</returns>
        private string MailTextErzeugen(BuchungGesamtModel model)
        {
            string text = @"<!DOCTYPE html><html lang = en xmlns = http://www.w3.org/1999/xhtml>
                            <head><meta charset = utf-8/><title></title></head><body><style>
                            .logoschrift {
                                font - family: 'Bradley Hand ITC';
                                font - weight: bold;
                                color: #005ead;
                                font - size: 1.6em;
                                text - align: right;
                                margin - top: 4 %;
                            }
                            </style>";
            text += @"<h3 class='logoschrift'>Reisebüro Graf</h3><h4 class='logoschrift'>Vielen Dank für Ihre Buchung</h4>
                <article><section>";
            text = string.Format("{0} Ihre gebuchte Reise: {1} von {2} bis {3} <br/> Gesamtpreis: € {4}", text, model.Reisetitel, model.Startdatum.ToShortDateString(), model.Enddatum.ToShortDateString(), model.Gesamtpreis);

            text += "<h4>Hier nochmal die Übersicht über Ihre Daten</h4><div><ul>";
            foreach (var buchung in model.BuchungErwachsen)
            {
                DateTime geburtsdatum = buchung.Geburtsdatum;
                int alter = DateTime.Now.Year - geburtsdatum.Year;
                if (geburtsdatum.Month > DateTime.Now.Month)
                {
                    alter = alter + 1;
                }
                string name = string.Format("{0} {1}", buchung.Vorname, buchung.Nachname);
                text += string.Format("<li>{0} Alter: {1}</li>", name, alter);
            }
            foreach (var buchung in model.BuchungKind)
            {
                DateTime geburtsdatum = buchung.Geburtsdatum;
                int alter = DateTime.Now.Year - geburtsdatum.Year;
                if (geburtsdatum.Month > DateTime.Now.Month)
                {
                    alter = alter + 1;
                }
                string name = string.Format("{0} {1}", buchung.Vorname, buchung.Nachname);
                text += string.Format("<li>{0} Alter: {1}</li>", name, alter);
            }
            text += @"</ul></div>";
            text += @"<p class='logoschrift'>Wir wünschen Ihnen viel Freude in Ihrem Urlaub und hoffen, dass Sie auch das nächste mal bei uns buchen</p></body></html>";
            return text;
        }


        /// <summary>
        /// Ausgelagerte Methode zum Zahlung speichern damit man sowohl für Kreditkarte als auch für IBAN 
        /// diese Methode aufrufen
        /// </summary>
        /// <param name="model">das ausgefüllte ZahlungModel</param>
        /// <returns>true wenn erfolgreich in DB gespeichert sonst false</returns>
        private bool ZahlungSpeichern(ZahlungModel model)
        {
            Debug.WriteLine("Buchungen - Zahlung Speichern");
            Debug.Indent();

            bool erfolgreich = false;

            try
            {
                Zahlung zahlung = new Zahlung()
                {
                    Vorname = model.Vorname,
                    Nachname = model.Nachname,
                    Nummer = model.Nummer
                };

                int neueID = ZahlungsVerwaltung.NeueZahlungSpeichern(zahlung, model.Zahlungsart_ID);
                zahlung.ID = neueID;
                List<int> BuchungIDs = Session["Buchungen"] as List<int>;
                int zeilen = ZahlungsVerwaltung.ZuordnungZahlungBuchung(BuchungIDs, zahlung.ID);
                erfolgreich = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler beim Speichern der Zahlung");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return erfolgreich;
        }

        /// <summary>
        /// Anzeige aller Buchungen (für den Mitarbeiter)
        /// </summary>
        /// <returns></returns>
        [PruefeBenutzer]
        [HttpGet]
        public ActionResult Verwalten(int id)
        {
            Debug.WriteLine("Buchung - Verwalten - GET");
            Debug.Indent();
            List<BuchungVerwaltenModel> UI_Buchungen = new List<BuchungVerwaltenModel>();
            List<Buchung> BL_Buchungen = BuchungsVerwaltung.LadeAlleBuchungen();
            foreach (var buchung in BL_Buchungen)
            {
                UI_Buchungen.Add(new BuchungVerwaltenModel()
                {
                    ID=buchung.ID,
                    BenutzerName = buchung.Benutzer.Email,
                    Geburtsdatum = buchung.Geburtsdatum,
                    Nachname = buchung.Nachname,
                    Vorname = buchung.Vorname,
                    Enddatum = buchung.Reisedatum.Enddatum,
                    Startdatum = buchung.Reisedatum.Startdatum,
                    Passnummer = buchung.Passnummer,
                    Reisedatum_ID = buchung.Reisedatum.ID,
                    Reisetitel = buchung.Reisedatum.Reise.Titel
                });
            }
            if (id!=0)
            {
                UI_Buchungen = UI_Buchungen.Where(x => x.Reisedatum_ID == id).ToList();
            }
            Debug.Unindent();
            return View(UI_Buchungen);
        }

        /// <summary>
        /// Liefert für die Mitarbeiter die Übersicht über alle stornierten Buchungen
        /// von dort kann der Mitarbeiter die Stornierung rückgängig machen
        /// </summary>
        /// <returns>View mit Liste von StornoAufträgen</returns>
        [PruefeBenutzer]
        [HttpGet]
        public ActionResult StornoVerwalten()
        {
            Debug.WriteLine("Buchungen - StornoVerwalten - GET");
            Debug.Indent();
            List<Buchung> BL_Buchungen = BuchungsVerwaltung.LadeAlleStorniertenBuchungen();
            List<StornoModel> UI_StornoauftragListe = new List<StornoModel>();
            foreach (var buchung in BL_Buchungen)
            {
                StornoModel storno = new StornoModel()
                {
                    Id = buchung.ID,
                    Name = string.Format("{0} {1}", buchung.Vorname, buchung.Nachname),
                    Geburtsdatum = buchung.Geburtsdatum,
                    GebuchtAm = buchung.ErstelltAm,
                    Passnummer = buchung.Passnummer
                };
                if (buchung.Geburtsdatum.AddYears(13).Date <= DateTime.Now.Date)
                {
                    storno.Preis = buchung.Reisedatum.Reise.Preis_Erwachsener;
                }
                else
                {
                    storno.Preis = buchung.Reisedatum.Reise.Preis_Kind;
                }
                UI_StornoauftragListe.Add(storno);
            }
            Debug.Unindent();
            return View(UI_StornoauftragListe);
        }

        /// <summary>
        /// Der Mitarbeiter kann die Stornierung rückgängig machen
        /// </summary>
        /// <param name="id">die ID der stornierten Buchung</param>
        /// <returns>leitet den Mitarbeiter weiter zu Storno verwalten Oberfläche</returns>
        [PruefeBenutzer]
        [HttpGet]
        public ActionResult StornoEntfernen(int id)
        {
            Debug.WriteLine("Buchungen - StornoVerwalten - GET");
            Debug.Indent();
            bool erfolgreich = BuchungsVerwaltung.StornierungAufheben(id);
            return RedirectToAction("StornoVerwalten");

        }

        /// <summary>
        /// Stornieren einer Buchung (sowohl für Mitarbeiter als auch für Kunde)
        /// eigentlich kein guter Stil Httpget besser wäre POST damit man nicht einfach irgendwelche Reisen
        /// über die URL löschen kann
        /// </summary>
        /// <param name="id">die ID der Reise</param>
        /// <returns>Weiterleitung zu StornoVerwalten wenn Mitarbeiter sonst zur Profilseite wenn Kunde</returns>
        [HttpGet]
        public ActionResult Stornieren(int id)
        {
            Debug.WriteLine("Buchungen - Stornieren - GET");
            Debug.Indent();
            bool erfolgreich = false; 
            if(!Tools.BistDuMitarbeiter(User.Identity.Name))
            {
                BuchungsVerwaltung.Stornieren(id);
                return RedirectToAction("Aktualisieren", "Benutzer");
            }
            return RedirectToAction("StornoVerwalten");

        }



    }
}