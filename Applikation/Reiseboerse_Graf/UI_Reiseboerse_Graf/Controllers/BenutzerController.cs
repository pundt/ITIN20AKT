using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_Reiseboerse_Graf.Models;
using BL_Reiseboerse_Graf;
using System.Diagnostics;
using System.Web.Security;

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
        public PartialViewResult Login()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Login(LoginModel lm)
        {
            if (ModelState.IsValid)
            {
                if (lm.AngemeldetBleiben)
                {
                    FormsAuthentication.SetAuthCookie(lm.Email, true);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(lm.Email, false);
                }
            }

            return View("~/Views/Home/Index.cshtml");
        }
        [HttpPost]
        public ActionResult Logout()
        {
            
            return View("~/Views/Home/Index.cshtml");
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
        public ActionResult BenutzerAnlegen(KundenAnlegenModel bm)
        {

            //Benutzer neuerBenutzer = new Benutzer();
            //neuerBenutzer.Id = bm.ID;
            //neuerBenutzer.Vorname = bm.Vorname;
            //neuerBenutzer.Nachname = bm.Nachname;
            //neuerBenutzer.Passwort = bm.Passwort;
            //neuerBenutzer.Geschlecht = bm.Geschlecht;

            if (Globals.IST_TESTSYSTEM)
            {
                if (ModelState.IsValid)
                {
                    Debug.WriteLine("Erfolgreich");
                    return RedirectToAction("Laden", "Reisen");
                }
                else
                {
                    return View(bm);
                }
            }
            else
            {
                // benutzer in DB speichern
            }        
        }
        [HttpGet]
        public ActionResult BenutzerAnlegen()
        {
            KundenAnlegenModel modell = new KundenAnlegenModel()
            {
                Land = new List<LandModel>()

            };
            for (int i = 0; i < 3; i++)
            {
                modell.Land.Add(new LandModel()
                {
                    landName = "Land" + i,
                    land_ID = i + 1
                });
            }
            return View(modell);
        }
    }
}