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
    public class ReisenController : Controller
    {
        /// <summary>
        /// Reisenstartseite mit allen Reisen
        /// </summary>
        /// <returns>Den View Index</returns>
        public ActionResult Laden()
        {
            Debug.WriteLine("ReisenController - Laden - Get");
            Debug.Indent();
            List<ReiseModel> liste = new List<ReiseModel>();
            if (Globals.IST_TESTSYSTEM)
            {
                try
                {
                    Debug.WriteLine("Testsystem");
                    for (int i = 0; i < 50; i++)
                    {
                        ReiseModel reise = new ReiseModel()
                        {
                            Anmeldefrist = new DateTime(2016, 08, 30),
                            Beginndatum = new DateTime(2016, 10, 01),
                            Enddatum = new DateTime(2016, 10, 30),
                            Preis = 599 + i * 3,
                            Titel = "Wandern in der Wachau " + i,
                            Beschreibung = "Das ist eine ganz tolle Reise in ein wundervolles Weingebiet in Österreich. Ganz besonders toll im Herbst",
                            Ort = "Spitz" + i,
                            Hotel = "Schlosshotel Burckhardt " + i,
                            Verpflegung="Halbpension"+i%2,
                            Hotel_ID = i,
                            Restplätze = i %5
                        };
                        liste.Add(reise);
                    }
                    Debug.WriteLine($"{liste.Count} Reisen geladen");
                }

                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden aller Reisen");
                    Debug.WriteLine(ex.Message);
                }

            }
            else
            {
                /// die Liste aus der DB mit der Methode aus BL aufrufen und auf die Liste von ReiseModel 
                /// mappen
            }
            return View(liste);
        }

        /// <summary>
        /// Filtert die Reisen nach dem übergebenen FilterModel 
        /// und ruft die BL_Reiseverwaltung.LadeReisenGefiltert auf
        /// </summary>
        /// <returns>den View Index</returns>
        public ActionResult LadenGefiltert(FilterModel filterung)
        {
            return View();
        }

        /// <summary>
        /// Zeigt alle Details der Reise (Beschreibung, Hotelbeschreibung)
        /// </summary>
        /// <param name="reise_ID">ID der anzuzeigenden Reise</param>
        /// <returns></returns>
        public ActionResult Anzeigen(int reise_ID)
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
            return RedirectToAction("Index");
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