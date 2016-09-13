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
        /// Lädt alle Buchungen aus der Datenbank zu einer bestimmten Reise
        /// </summary>
        /// <param name="reise_id"></param>
        /// <returns></returns>
        //[Authorize(Roles = "Mitarbeiter")]
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
                    ReiseID=aktbuchung.Reisedatum.Reise.ID,
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
                Session["Mailtext"]=MailTextErzeugen(model) as string;
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
                if (!Regex.IsMatch(model.Nummer, "^[A-Z]{2}$"))
                {
                    if (ZahlungsVerwaltung.PruefeLuhn(model.Nummer))
                    {
                        erfolgreich = ZahlungSpeichern(model);
                        //Aufruf Buchungsbestätigung für den Kunden
                        string text = Session["Mailtext"] as string;
                        bool gesendet = EmailVerwaltung.BuchungBestaetigen(User.Identity.Name, text);
                    }
                }
                else
                {
                    erfolgreich = ZahlungSpeichern(model);
                    //Aufruf Buchungsbestätigung für den Kunden
                    string text = Session["Mailtext"] as string;
                    bool gesendet = EmailVerwaltung.BuchungBestaetigen(User.Identity.Name, text);
                }
                

                //Könnte man noch einbauen:
                // Wenn gesendet false ergibt, Nachfrage ob Email korrekt war etc...

            }
            if (!erfolgreich)
            {
                ZahlungModel zahlung = new ZahlungModel()
                {
                    Vorname = model.Vorname,
                    Nachname = model.Nachname,
                    Nummer = model.Nummer
                };
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
            text += @"</ul></div>";
            text += @"<p class='logoschrift'>Wir wünschen Ihnen viel Freude in Ihrem Urlaub und hoffen, dass Sie auch das nächste mal bei uns buchen</p></body></html>";
            return text;
        }

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
                List<int> BuchungIDs = Session["Buchungen"] as List<int>;
                int zeilen = ZahlungsVerwaltung.ZuordnungZahlungBuchung(BuchungIDs, neueID);

                if (zeilen == BuchungIDs.Count)
                {
                    erfolgreich = true;
                }
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
        /// Liefert für die Mitarbeiter die Übersicht über alle stornierten Buchungen
        /// </summary>
        /// <returns></returns>
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
                if (buchung.Geburtsdatum.AddYears(13).Date<=DateTime.Now.Date)
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
    }
}