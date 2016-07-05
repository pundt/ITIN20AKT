﻿using System;
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
                    liste = ReiseAnzeigeListeTest();
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
        /// Zeigt alle Details der Reise (Beschreibung, Hotelbeschreibung)
        /// </summary>
        /// <param name="reise_ID">ID der anzuzeigenden Reise</param>
        /// <returns></returns>
        public ActionResult Anzeigen(int id)
        {
            Debug.WriteLine("Reisedetails- Anzeigen - GET");
            Debug.Indent();
            ReisedetailModel rm = new ReisedetailModel();
            if (Globals.IST_TESTSYSTEM)
            {
                Debug.Indent();
                List<ReisedetailModel> liste = ReiseDetailListeTest();
                //die entsprechende Reise aus der Liste finden (anhand der ID)
                rm = liste.Find(x => x.ID == id);
            }
            else
            {
                //ReiseDetails aus DB auslesen
            }
            Debug.Unindent();
            return View(rm);
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

        /// <summary>
        /// Befüllen mit Daten im Testsytem für Reise anzeigen 
        /// </summary>
        /// <returns></returns>
        private List<ReiseModel> ReiseAnzeigeListeTest()
        {
            List<ReiseModel> liste = new List<ReiseModel>();
            for (int i = 0; i < 50; i++)
            {
                ReiseModel reise = new ReiseModel()
                {
                    ID=i,
                    Anmeldefrist = new DateTime(2016, 08, 30),
                    Beginndatum = new DateTime(2016, 10, 01),
                    Enddatum = new DateTime(2016, 10, 30),
                    Preis = 599 + i * 3,
                    Titel = "Wandern in der Wachau " + i,
                    Ort = "Spitz" + i,
                    Unterkunft = "Schlosshotel Burckhardt " + i,
                    Verpflegung = "Halbpension" + i % 2,
                    Restplätze = i % 5
                };
                liste.Add(reise);
            }
            return liste;
        }

        /// <summary>
        /// Befüllen mit Daten im Testsystem für die Reisedetails
        /// </summary>
        /// <returns></returns>
        private List<ReisedetailModel> ReiseDetailListeTest()
        {
            List<ReisedetailModel> liste = new List<ReisedetailModel>();
            for (int i = 0; i < 50; i++)
            {
                ReisedetailModel reise = new ReisedetailModel()
                {
                    ID=i,
                    Anmeldefrist = new DateTime(2016, 08, 30),
                    Beginndatum = new DateTime(2016, 10, 01),
                    Enddatum = new DateTime(2016, 10, 30),
                    Preis = 599 + i * 3,
                    Titel = "Wandern in der Wachau " + i,
                    Ort = "Spitz" + i,
                    Beschreibung= "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.",
                    Preis_Erwachsene=i*156,
                    Preis_Kind=i*133,
                    Unterkunft = "Schlosshotel Burckhardt " + i,
                    Unterkunft_ID=i,
                    Verpflegung = "Halbpension" + i % 2,
                    Restplätze = i % 5
                };
                liste.Add(reise);
            }
            return liste;
        }


    }
}