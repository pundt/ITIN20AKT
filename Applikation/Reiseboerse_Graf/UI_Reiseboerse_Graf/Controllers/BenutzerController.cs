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
                        b.Adresse.Adressdaten = bm.Adresse;
                        b.Email = bm.Email;
                        b.Geschlecht = bm.Geschlecht;
                        b.Passwort = Tools.PasswortZuByteArray(bm.Passwort);
                        b.Telefon = bm.Telefon;
                        b.Vorname = bm.Vorname;
                        b.Nachname = bm.Nachname;
                        b.Land.ID = bm.Land_ID;

                        lmList = bm.Land;
                        foreach (LandModel lm in lmList)
                        {
                            if (lm.land_ID == bm.Land_ID)
                            {
                                l.Bezeichnung = lm.landName;
                                l.ID = lm.land_ID;
                            }
                        }

                        b.Land = l;

                        benutzerList.Add(b);

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
            return View();
        }

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
                    List<LandModel> lmListe = new List<LandModel>();

                    /// Hier werden die Laender der lmList hinzugefügt um
                    /// die Dropdown-Liste in der View zu füllen
                    foreach (Land l in laender)
                    {
                        lmListe.Add(new LandModel() { landName = l.Bezeichnung, land_ID = l.ID });
                    }

                    model.Land = lmListe;
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

        [Authorize]
        [HttpPost]
        public ActionResult Aktualisieren(KundenModel model)
        {
            List<Benutzer> benutzerListe = BenutzerVerwaltung.AlleBenutzer();
            Land l = new Land();

            foreach (Benutzer b in benutzerListe)
            {
                if (b.ID == model.ID)
                {
                    using (var context = new reisebueroEntities())
                    {
                        try
                        {
                            Debug.WriteLine("Benutzer - Aktualisieren - POST".ToUpper());
                            Debug.Indent();

                            /// Das ausgewählte Land wird hier umgemappt für die DB
                            #region Landmapping
                            foreach (LandModel lm in model.Land)
                            {
                                if (lm.land_ID == b.Land.ID)
                                {
                                    l.ID = lm.land_ID;
                                    l.Bezeichnung = lm.landName;
                                }
                            }
                            #endregion

                            b.Adresse.Adressdaten = model.Adresse;
                            b.Email = model.Email;
                            b.Geburtsdatum = model.GeburtsDatum;
                            b.Geschlecht = model.Geschlecht;
                            b.ID = model.ID;
                            b.Land = l;
                            b.Nachname = model.Nachname;
                            b.Vorname = model.Vorname;
                            b.Passwort = Tools.PasswortZuByteArray(model.Passwort);
                            b.Telefon = model.Telefon;
                            b.Titel = model.Titel;

                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Fehler beim Aktualisieren des Benutzers!");
                            Debug.WriteLine(ex.Message);
                            Debug.Unindent();
                            Debugger.Break();
                        }
                    }
                }
            }

            return RedirectToAction("Laden", "Reisen");
        }

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
                    Plz = "101" + i,
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