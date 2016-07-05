using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_Reiseboerse_Graf.Models;
using BL_Reiseboerse_Graf;

namespace UI_Reiseboerse_Graf.Controllers
{
    public class BenutzerController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GebuchteReisen()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string passwort)
        {
            BenutzerModel bm = new BenutzerModel();
            email = bm.Email;
            passwort = bm.Passwort;

            return View("~/Views/Home/Index.cshtml");
        }
        [HttpPost]
        public ActionResult Logout()
        {
            /// Session variable von derzeitigem benutzer wird auf null gesetzt.
            return View();
        }
        /// <summary>
        /// Erhält das Model, sendet daten an Bl zur weitergabe in die datenbank
        /// </summary>
        /// <param name="bm">BenutzerModel Datentyp</param>
        /// <returns></returns>
        [HttpGet]
        public List<Benutzer> BenutzerAnzeigen()
        {
            List<Benutzer> AlleBenutzer = new List<Benutzer>();

            //neuer benutzer b der mit den daten aus dem benutzermodel aus der view gefüttert 
            //wird und dann in eine liste hinzugefügt wird
            //Benutzer benutzer = new Benutzer()
            //{
            //    Email = benutzer.Email,
            //    Geschlecht = benutzer.Geschlecht,
            //    Nachname = benutzer.Nachname,
            //    Vorname = benutzer.Vorname,
            //    Passwort = benutzer.Passwort
            //};
            //AlleBenutzer.Add(benutzer);

            if (Globals.IST_TESTSYSTEM)
            {
                // dummy daten erstellen 30 testbenutzer die abgefragt werden können
                for (int i = 0; i < 30; i++)
                {
                    Benutzer neuerBenutzer = new Benutzer()
                    {
                        Id = i,
                        Email = "Max" + i + "@gmx.at",
                        Geschlecht = false,
                        Nachname = "Nachname" + i,
                        Vorname = "Vorname" + i,
                        Passwort = "12345678" + i
                    };
                    if (neuerBenutzer.Geschlecht== false)
                    {
                        neuerBenutzer.Geschlecht = true;
                    }
                    else
                    {
                        neuerBenutzer.Geschlecht = false;
                    }
                    AlleBenutzer.Add(neuerBenutzer);
                }
            }
            else
            {
            }
            return AlleBenutzer;

        }
        [HttpPost]
        public ActionResult BenutzerAnlegen(BenutzerModel bm, List<Benutzer> AlleBenutzer)
        {

            //Benutzer neuerBenutzer = new Benutzer();
            //neuerBenutzer.Id = bm.ID;
            //neuerBenutzer.Vorname = bm.Vorname;
            //neuerBenutzer.Nachname = bm.Nachname;
            //neuerBenutzer.Passwort = bm.Passwort;
            //neuerBenutzer.Geschlecht = bm.Geschlecht;
            //foreach (var item in AlleBenutzer)
            //{
            //    item.
            //}


            return RedirectToAction("Laden","Reisen");

        }


    }
}