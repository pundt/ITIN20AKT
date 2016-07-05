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
        [HttpPost]
        public ActionResult BenutzerErstellen(BenutzerModel bm)
        {
            List<Benutzer> bl = new List<Benutzer>();
            //neuer benutzer b der mit den daten aus dem benutzermodel aus der view gefüttert 
            //wird und dann in eine liste hinzugefügt wird
            Benutzer b = new Benutzer()
            {
                Email = bm.Email,
                Geschlecht = bm.Geschlecht,
                Nachname = bm.Nachname,
                Vorname = bm.Vorname,
                Passwort = bm.Passwort
            };
            bl.Add(b);
            // dummy daten erstellen 30 testbenutzer die abgefragt werden können
            if (Globals.IST_TESTSYSTEM)
            {
                //for (int i = 0; i < 30; i++)
                //{
                //    Benutzer TestBenutzer = new Benutzer()
                //    {
                //        b.Id = i,
                //        b.Email = "daniel" + i + "@gmx.at",
                //        b.Geschlecht = false,
                //        b.Nachname = "Nachname" + i,
                //        b.Vorname = "Vorname" + i,
                //        b.Passwort = "123" + i
                //    };
                //    bl.Add(TestBenutzer);
                //}
            }
            else
            {


            }
            return RedirectToAction("Laden","Reisen");

    }



}
}