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
        /// Erhält das Model, schreibt die Daten in die Datenbank
        /// </summary>
        /// <param name="bm">RegistrierungsModel Datentyp</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BenutzerErstellen(RegistrierungsModel rm)
        {
            try
            {
                using (testdbEntities context = new testdbEntities())
                {
                    List<Benutzer> bl = context.AlleBenutzer.ToList();

                    Benutzer b = new Benutzer()
                    {
                        Email = rm.Email,
                        Geschlecht = rm.Geschlecht,
                        Nachname = rm.Nachname,
                        Vorname = rm.Vorname,
                        Passwort = rm.Passwort   
                };
                    context.AlleBenutzer.Add(b);
                    //foreach (var item in bl)
                    //{
                    //    rm.Email = item.email
                    //}
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View();

        }
        /// <summary>
        /// Benutzer view zum eingeben der notwendigen daten.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BenutzerRegistrieren()
        {
           
            

    

            return View();
        
        }

    }
}