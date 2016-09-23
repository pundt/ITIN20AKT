using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_Reiseboerse_Graf.Models;
using BL_Reiseboerse_Graf;
using System.Diagnostics;
using System.Web.Security;
using System.Reflection;

namespace UI_Reiseboerse_Graf.Controllers
{
    /// <summary>
    /// Attribut zum Prüfen ob der aktuell angemeldete Benutzer berechtigt ist (also ein MA ist)
    /// </summary>
    public class PruefeBenutzer : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string username = filterContext.HttpContext.User.Identity.Name;

            if (!Tools.BistDuMitarbeiter(username))
            {
                filterContext.Result = new RedirectResult("~/Reisen/Laden");
            }
        }
    }

    public class BenutzerController : Controller
    {
        //[ChildActionOnly]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel lm)
        {

            if (BenutzerVerwaltung.Anmelden(lm.Email, lm.Passwort))
            {
                if (lm.AngemeldetBleiben)
                {
                    FormsAuthentication.SetAuthCookie(lm.Email, true);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(lm.Email, false);
                }
                if (Tools.BistDuMitarbeiter(lm.Email))
                {
                    return RedirectToAction("Verwaltung", "Home");
                }
                //wenn der User nicht von Reisen/laden kommt leite ihn dahin weiter woher er kam
                if (!Request.UrlReferrer.AbsoluteUri.Contains("Reisen/Laden"))
                {
                    return Redirect(Request.UrlReferrer.AbsoluteUri);
                }
            }

            return RedirectToAction("Laden", "Reisen");
        }
        [Authorize]
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Registrieren eines Benutzers (Erstellen eines Kundenmodel und Befüllen der Dropdownlisten)
        /// </summary>
        /// <returns>View mit dem Kundenmodel</returns>
        [HttpGet]
        public ActionResult BenutzerAnlegen()
        {
            Debug.WriteLine("Benutzer - Benutzer Anlegen - GET".ToUpper());
            Debug.Indent();

            reisebueroEntities context = new reisebueroEntities();

            KundenModel model = new KundenModel();
            model.GeburtsDatum = DateTime.Now;

            using (context)
            {
                try
                {
                    List<Land> laender = BenutzerVerwaltung.AlleLaender();
                    List<Ort> orte = LaenderVerwaltung.AlleOrte();
                    List<LandModel> lmListe = new List<LandModel>();
                    List<OrtModel> ortListe = new List<OrtModel>();

                    /// Hier werden die Laender der lmList hinzugefügt um
                    /// die Dropdown-Liste in der View zu füllen
                    foreach (Land l in laender)
                    {
                        lmListe.Add(new LandModel() { landName = l.Bezeichnung, land_ID = l.ID });
                    }

                    foreach (Ort ort in orte)
                    {
                        ortListe.Add(new OrtModel() { Id = ort.ID, Bezeichnung = ort.Bezeichnung });
                    }
                    model.Land = lmListe;
                    model.Ort = ortListe;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Holen der Daten!");
                    Debug.WriteLine(ex.Message);
                    Debug.Unindent();
                    Debugger.Break();
                }
            }

            Debug.Unindent();
            return View(model);
        }


        [HttpPost]
        public ActionResult BenutzerAnlegen(KundenAnlegenModel bm)
        {
            Debug.WriteLine("Benutzer - Benutzer Anlegen - POST".ToUpper());
            Debug.Indent();

            reisebueroEntities context = new reisebueroEntities();

            List<LandModel> lmList = new List<LandModel>();
            List<Land> laender = new List<Land>();
            List<Benutzer> benutzerList = new List<Benutzer>();
            Benutzer b = new Benutzer();
            Land l = new Land();

            using (context)
            {
                try
                {
                    laender = BenutzerVerwaltung.AlleLaender();
                    benutzerList = BenutzerVerwaltung.AlleBenutzer();

                    if (ModelState.IsValid)
                    {
                        if (LaenderVerwaltung.SucheAdresse(bm.Adresse) == null)
                        {
                            b.Adresse = new Adresse()
                            {
                                Adressdaten = bm.Adresse,
                                Ort = LaenderVerwaltung.SucheOrt(bm.Ort_ID)
                            };
                        }
                        else
                        {
                            b.Adresse = LaenderVerwaltung.SucheAdresse(bm.Adresse);
                        }
                        b.Email = bm.Email;
                        b.Geschlecht = bm.Geschlecht;
                        b.Passwort = Tools.PasswortZuByteArray(bm.Passwort);
                        b.Telefon = bm.Telefon;
                        b.Vorname = bm.Vorname;
                        b.Nachname = bm.Nachname;
                        b.Land = LaenderVerwaltung.SucheLand(bm.Land_ID);
                        b.Geburtsdatum = bm.GeburtsDatum;

                        context.AlleBenutzer.Add(b);

                        context.SaveChanges();
                    }
                    else
                    {
                        /// Wenn das Model nicht valide ist, wird eine neue Landliste generiert,
                        /// da dieses bei erneutem Aufruf sonst verloren geht
                        foreach (Land ld in laender)
                        {
                            lmList.Add(new LandModel() { landName = ld.Bezeichnung, land_ID = ld.ID });
                        }

                        bm.Land = lmList;

                        return View(bm);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Anlegen eines Benutzers!");
                    Debug.WriteLine(ex.Message);
                    Debug.Unindent();
                    Debugger.Break();
                }
            }

            Debug.Unindent();
            return View("Bestaetigung");
        }



        /// <summary>
        /// Die Profilseite des Kunden wo er seine Daten ändern kann
        /// </summary>
        /// <returns>die View mit dem KundenModel des aktuellen Benutzers</returns>
        [Authorize]
        [HttpGet]
        public ActionResult Aktualisieren()
        {
            Debug.WriteLine("Benutzer - Aktualisieren - GET".ToUpper());
            Debug.Indent();
            KundenModel model = new KundenModel();

            using (var context = new reisebueroEntities())
            {
                List<Benutzer> benutzerListe = BenutzerVerwaltung.AlleBenutzer();

                List<Land> laender = BenutzerVerwaltung.AlleLaender();
                List<LandModel> lmListe = new List<LandModel>();
                try
                {
                    foreach (Land l in laender)
                    {
                        lmListe.Add(new LandModel() { landName = l.Bezeichnung, land_ID = l.ID });
                    }

                    //Hier werden die Benutzer der DB auf das Kundenmodel gemappt
                    foreach (Benutzer b in benutzerListe)
                    {
                        if (b.Email == User.Identity.Name)
                        {
                            model.Adresse = b.Adresse.Adressdaten;
                            model.Email = b.Email;
                            model.GeburtsDatum = b.Geburtsdatum;
                            model.Geschlecht = b.Geschlecht;
                            model.ID = b.ID;
                            model.Land = lmListe;
                            model.Land_ID = b.Land.ID;
                            model.Nachname = b.Nachname;
                            model.Passwort = "";
                            model.PasswortWiederholung = "";
                            model.Telefon = b.Telefon;
                            model.Titel = b.Titel;
                            model.Vorname = b.Vorname;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Holen der Daten eines Benutzers!");
                    Debug.WriteLine(ex.Message);
                    Debug.Unindent();
                    Debugger.Break();
                }
            }

            return View(model);
        }

        /// <summary>
        /// Speichern der geänderten Kundendaten des Benutzers
        /// </summary>
        /// <param name="model">das übergebene KudenModel</param>
        /// <returns>leitet zurück auf Reisen/Laden</returns>
        [Authorize]
        [HttpPost]
        public ActionResult Aktualisieren(KundenModel model)
        {
            Land l = new Land();
            //ModelState.IsValid funktioniert nicht da es nur KundenAnlegenModel gibt,
            //mit Land &Remote Validierung email --> liefert false, deswegen hier nicht angewendet
            Benutzer benutzer = new Benutzer()
            {
                Email = User.Identity.Name,
                Geburtsdatum = model.GeburtsDatum,
                ID = model.ID,
                Land = LaenderVerwaltung.SucheLand(model.Land_ID),
                Geschlecht = model.Geschlecht,
                Nachname = model.Nachname,
                Vorname = model.Vorname,
                Passwort = Tools.PasswortZuByteArray(model.Passwort),
                Telefon = model.Telefon,
                Titel = model.Titel
            };

            if (LaenderVerwaltung.SucheAdresse(model.Adresse) == null)
            {
                benutzer.Adresse = new Adresse()
                {
                    Adressdaten = model.Adresse,
                    Ort = LaenderVerwaltung.SucheOrt(model.Ort_ID)
                };
            }
            else
            {
                benutzer.Adresse = LaenderVerwaltung.SucheAdresse(model.Adresse);
            }


            if (BenutzerVerwaltung.Aktualisieren(benutzer) == 1)
            {
                return RedirectToAction("Aktualisieren");
            }
            else
            {
                model.Land = new List<LandModel>();
                List<Land> BL_Liste = BenutzerVerwaltung.AlleLaender();
                foreach (var land in BL_Liste)
                {
                    model.Land.Add(new LandModel()
                    {
                        landName = land.Bezeichnung,
                        land_ID = land.ID
                    });
                }
                return View(model);
            }
        }


        /// <summary>
        /// Generiert Dummydaten zum Anmelden
        /// </summary>
        /// <returns>List von Kundenmodel</returns>
        private List<KundenModel> DummyKundenAnlegen()
        {
            List<KundenModel> kunden = new List<KundenModel>();

            bool geschlechtAnlegen = true;

            for (int i = 1; i <= 30; i++)
            {
                if (i % 2 == 0)
                {
                    geschlechtAnlegen = false;
                }

                kunden.Add(new KundenModel
                {
                    ID = i,
                    Email = "maxmuster@gmx" + i + ".at",
                    GeburtsDatum = new DateTime(1980 + i, 1, 1 + i),
                    Geschlecht = geschlechtAnlegen,
                    Vorname = "Maxi",
                    Nachname = "Muster",
                    Land = new List<LandModel>()
                    {
                        new LandModel { land_ID = 1, landName = "Österreich" },
                        new LandModel { land_ID = 2, landName = "Deutschland" },
                        new LandModel { land_ID = 3, landName = "Italien" },
                    },
                    Passwort = "123" + i + "user!",
                    PasswortWiederholung = "123" + i + "user!",
                    Telefon = "067612345" + i,
                    Titel = "",
                    Adresse = "Musterstrasse 1" + i,
                    Land_ID = i + 1
                });
            }

            return kunden;
        }
    }
}