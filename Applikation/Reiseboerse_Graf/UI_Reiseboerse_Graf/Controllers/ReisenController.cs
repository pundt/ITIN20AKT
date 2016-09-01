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
        [HttpGet]
        public ActionResult Laden()
        {
            Debug.WriteLine("ReisenController - Laden - Get");
            Debug.Indent();
            ReiseLadenModel model = new ReiseLadenModel();

            if (Globals.IST_TESTSYSTEM)
            {
                #region test
                try
                {
                    Debug.WriteLine("Testsystem");
                    model.Reisen = ReiseAnzeigeListeTest();
                    model.Filter = FilterAnzeigeTest();

                    Debug.WriteLine($"{model.Reisen.Count()} Reisen geladen");
                }

                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden aller Reisen");
                    Debug.WriteLine(ex.Message);
                }
                #endregion test
            }
            else
            {
                #region db
                List<Reise> BL_ReiseListe = ReiseVerwaltung.LadeAlleReisen();
                List<List<ReisedatumModel>> reisedaten = new List<List<ReisedatumModel>>();
                model.Reisen = new List<ReiseModel>();
                foreach (var reise in BL_ReiseListe)
                {
                    ReiseModel reiseModel = new ReiseModel()
                    {
                        ID = reise.ID,
                        Titel = reise.Titel,
                        Hotelkategorie = reise.Unterkunft.Kategorie,
                        Land = reise.Ort.Land.Bezeichnung,
                        Land_id = reise.Ort.Land.ID,
                        Ort = reise.Ort.Bezeichnung,
                        Ort_id = reise.Ort.ID,
                        Unterkunft = reise.Unterkunft.Bezeichnung,
                        Preis = reise.Preis_Erwachsener,
                        Verpflegung = reise.Unterkunft.Verpflegung.Bezeichnung,
                        Verpflegungs_id = reise.Unterkunft.Verpflegung.ID
                    };
                    reiseModel.Reisedaten = new List<ReisedatumModel>();
                    foreach (var datum in ReiseVerwaltung.LadeReiseZeitpunkte(reiseModel.ID))
                    {
                        reiseModel.Reisedaten.Add(new ReisedatumModel()
                        {
                            Anmeldefrist = datum.Anmeldefrist,
                            Beginndatum = datum.Startdatum,
                            Enddatum = datum.Enddatum,
                            Restplätze = ReiseVerwaltung.Restplätze(datum.ID)
                        });
                    }

                    model.Reisen.Add(reiseModel);

                    
                    

                }

                List<LandModel> laender = new List<LandModel>();
                foreach (var land in BenutzerVerwaltung.AlleLaender())
                {
                    laender.Add(new LandModel
                    {
                        landName = land.Bezeichnung,
                        land_ID = land.ID
                    });
                }
                List<OrtModel> orte = new List<OrtModel>();
                foreach (var ort in LaenderVerwaltung.AlleOrte())
                {
                    orte.Add(new OrtModel
                    {
                        Bezeichnung = ort.Bezeichnung,
                        Id = ort.ID
                    });
                }
                List<VerpflegungModel> verpflegungen = new List<VerpflegungModel>();
                foreach (var verpflegung in LaenderVerwaltung.alleVerpflegung())
                {
                    verpflegungen.Add(new VerpflegungModel
                    {
                        Bezeichnung = verpflegung.Bezeichnung,
                        Id = verpflegung.ID
                    });
                }
                model.Filter = new FilterModel()
                {
                    Startdatum = DateTime.Now,
                    Enddatum = DateTime.Now.AddYears(1),
                    Land = laender,
                    Ort = orte,
                    Verpflegung = verpflegungen
                };
                #endregion db
            }
            return View(model);
        }

        /// <summary>
        /// Die Liste aller Reisen nach dem Filtermodel filtern und an die View zurückgeben
        /// </summary>
        /// <param name="filterModel">Filtermodel mit Daten</param>
        /// <returns>Die View </returns>
        [HttpPost]
        public ActionResult Laden(FilterModel filterModel)
        {
            ReiseLadenModel model = new ReiseLadenModel();
            model.Filter = FilterAnzeigeTest(filterModel);

            if (Globals.IST_TESTSYSTEM)
            {
                // holt sich die FakeReisen von ReiseAnzeigeListeTest
                model.Reisen = ReiseAnzeigeListeTest();

                // kontrolliert Validierung
                //if (ModelState.IsValid)
                //{

                if (filterModel.Land_id != 0)
                    model.Reisen = model.Reisen.Where(x => x.Land_id == filterModel.Land_id).ToList();


                if (filterModel.Ort_ID != 0)
                    model.Reisen = model.Reisen.Where(x => x.Ort_id == filterModel.Ort_ID).ToList();


                if (filterModel.HotelKategorie != 0)
                    model.Reisen = model.Reisen.Where(x => x.Hotelkategorie == filterModel.HotelKategorie).ToList();

                if (filterModel.PreisMin != 0)
                    model.Reisen = model.Reisen.Where(x => x.Preis >= filterModel.PreisMin).ToList();

                if (filterModel.PreisMax != 0)
                    model.Reisen = model.Reisen.Where(x => x.Preis <= filterModel.PreisMax).ToList();

                if (filterModel.Verpflegungs_ID != 0)
                    model.Reisen = model.Reisen.Where(x => x.Verpflegungs_id == filterModel.Verpflegungs_ID).ToList();

                //model.Reisen = model.Reisen.ToList();

                //}
                //return RedirectToAction("Laden", gefilterteReisen);
                ///Wenn ich zur Action Laden gehe bekomm ich alle Reisen, aber zur View Laden
                /// mit gefilterten Reisen als Model landet in einer Endlosschleife ...
                return View(model);
            }
            else
            {
                return null;
                // Datenbankverbindung List auslesen
            }
        }

        /// <summary>
        /// Zeigt alle Details der Reise (Beschreibung, Hotelbeschreibung)
        /// </summary>
        /// <param name="reise_ID">ID der anzuzeigenden Reise</param>
        /// <returns></returns>
        [HttpGet]
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
        /// erzeugt die Partialview zum Einbinden auf die jeweilige Reisedetailseite
        /// </summary>
        /// <param name="id">ID der Unterkunft</param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult UnterkunftAnzeigen(int id)
        {
            UnterkunftdetailModel um = new UnterkunftdetailModel()
            {
                ID = id,
                Bezeichnung = "Hotel XYZ",
                Beschreibung = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.",
                Kategorie = 3,
                Verpflegung = new VerpflegungModel()
                {
                    Id = 3,
                    Bezeichnung = "Halbpension"
                }
            };
            return PartialView(um);
        }
        [HttpGet]
        public ActionResult ReiseHinzufuegen()
        {
            List<Land> alleLaender = BL_Reiseboerse_Graf.BenutzerVerwaltung.AlleLaender();
            ReiseAnlegenModel UI_Reise = new ReiseAnlegenModel();
            UI_Reise.Reiseland = new List<LandModel>();

            foreach (Land aktLand in alleLaender)
            {
                UI_Reise.Reiseland.Add(new LandModel()
                {
                    landName = aktLand.Bezeichnung,
                    land_ID = aktLand.ID
                });
            }

            List<Ort> alleOrte = BL_Reiseboerse_Graf.LaenderVerwaltung.AlleOrte();
            UI_Reise.ReiseOrt = new List<OrtModel>();
            foreach (Ort aktOrt in alleOrte)
            {
                UI_Reise.ReiseOrt.Add(new OrtModel()
                {
                    Bezeichnung = aktOrt.Bezeichnung,
                    Id = aktOrt.ID

                });
            }
            List<Unterkunft> alleUnterkuenfte = BL_Reiseboerse_Graf.LaenderVerwaltung.AlleUnterkuenfte();
            UI_Reise.Unterkunft = new List<UnterkunftdetailModel>();           
            foreach (Unterkunft aktUnterkunft in alleUnterkuenfte)
            {
                UI_Reise.Unterkunft.Add(new UnterkunftdetailModel()
                {
                    ID = aktUnterkunft.ID,
                    Beschreibung = aktUnterkunft.Beschreibung,
                    Bezeichnung = aktUnterkunft.Bezeichnung,
                    Kategorie = aktUnterkunft.Kategorie,
                    Verpflegung_ID = aktUnterkunft.Verpflegung.ID                                
                });
      
            }
            List<Verpflegung> alleVerpflegung = BL_Reiseboerse_Graf.LaenderVerwaltung.alleVerpflegung();
            UI_Reise.Verpflegung = new List<VerpflegungModel>();
            foreach (Verpflegung aktVerpflegung in alleVerpflegung)
            {
                UI_Reise.Verpflegung.Add(new VerpflegungModel()
                {
                    Bezeichnung = aktVerpflegung.Bezeichnung,
                    Id= aktVerpflegung.ID
                });
            }      

            return View(UI_Reise);
        }

        /// <summary>
        /// Fügt eine neue Reise hinzu
        /// </summary>
        /// <param name="neueReise">Model mit den entsprechenden Daten</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReiseHinzufuegen(ReiseAnlegenModel neueReise, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (neueReise.NeuerOrt!=null)
                {

                }
            }
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
        /// TextSuche nach Beschreibungstext der Reise
        /// </summary>
        /// <param name="model">Suchtext</param>
        /// <returns>Liste von Reisen bei Fehler oder ungültigen Suchtext Null</returns>
        [HttpPost]
        public ActionResult Suchen(string TextSuche)
        {
            List<Reise> liste = new List<Reise>();
            List<Reisedatum> reisedaten = new List<Reisedatum>();
            List<ReiseModel> viewliste = new List<ReiseModel>();
            ReiseModel rm = new ReiseModel();
            if (!string.IsNullOrEmpty(TextSuche))
            {
                liste = ReiseVerwaltung.SucheReise(TextSuche);
            }
            foreach (var reise in liste)
            {
                reisedaten = reise.AlleReisedaten.ToList();
                rm.Hotelkategorie = reise.Unterkunft.Kategorie;
                rm.ID = reise.ID;
                rm.Land = reise.Ort.Land.Bezeichnung;
                rm.Ort = reise.Ort.Bezeichnung;
                rm.Land_id = reise.Ort.Land.ID;
                rm.Ort_id = reise.Ort.ID;
                rm.Preis = reise.Preis_Erwachsener;
                rm.Titel = reise.Titel;
                rm.Unterkunft = reise.Unterkunft.Bezeichnung;
                rm.Verpflegung = reise.Unterkunft.Verpflegung.Bezeichnung;
                rm.Verpflegungs_id = reise.Unterkunft.Verpflegung.ID;
                viewliste.Add(rm);
            }
            ReiseLadenModel viewmodel = new ReiseLadenModel()
            {
                Reisen = viewliste,
                Filter = FilterAnzeigeTest(),
                TextSuche = TextSuche
            };

            return View("Laden", viewmodel);
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
                if (i % 5 == 0)
                {
                    ReiseModel reise = new ReiseModel()
                    {
                        ID = i,
                        Preis = 599,
                        Titel = "Hamburg im Advent",
                        Ort = "Hamburg",
                        Ort_id = 2,
                        Hotelkategorie = 4,
                        Land = "Deutschland",
                        Land_id = 2,
                        Unterkunft = "Hafentower Hotel",
                        Verpflegung = "Halbpension",
                        Verpflegungs_id = 2
                    };
                    liste.Add(reise);
                }
                else if (i % 3 == 0)
                {
                    ReiseModel reise = new ReiseModel()
                    {
                        ID = i,
                        Preis = 399,
                        Titel = "Städtereise Wien",
                        Ort = "Wien",
                        Ort_id = 1,
                        Hotelkategorie = 4,
                        Land = "Österreich",
                        Land_id = 1,
                        Unterkunft = "Schlosshotel Burckhardt",
                        Verpflegung = "Vollpension",
                        Verpflegungs_id = 2
                    };

                    liste.Add(reise);
                }
                {
                    ReiseModel reise = new ReiseModel()
                    {
                        ID = i,
                        Preis = 895,
                        Titel = "Kultururlaub Antike",
                        Ort = "Rom",
                        Ort_id = 3,
                        Hotelkategorie = 3,
                        Land = "Italien",
                        Land_id = 3,
                        Unterkunft = "Pension Dolce Vita ",
                        Verpflegung = "Halbpension",
                        Verpflegungs_id = 1
                    };
                    liste.Add(reise);
                }

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
                    ID = i,
                    Preis = 599 + i * 3,
                    Titel = "TestReise",
                    Ort = "Irgendwohin",
                    Beschreibung = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum.",
                    Preis_Erwachsene = i * 156,
                    Preis_Kind = i * 133,
                    Unterkunft = "Hotel XYZ",
                    Unterkunft_ID = i,
                    Verpflegung = "Verpflegung nach Wunsch"
                };
                liste.Add(reise);
            }
            return liste;
        }

        /// <summary>
        /// Befüllen des FilterModels mit Testdaten
        /// </summary>
        /// <returns></returns>
        private FilterModel FilterAnzeigeTest()
        {
            FilterModel model = new FilterModel();

            #region Land
            model.Land = new List<LandModel>();

            model.Land.Add(new LandModel()
            {
                land_ID = 0,
                landName = "Alle"
            });

            LandModel lm1 = new LandModel()
            {
                land_ID = 1,
                landName = "Österreich"
            };
            LandModel lm2 = new LandModel()
            {
                land_ID = 2,
                landName = "Deutschland"
            };
            LandModel lm3 = new LandModel()
            {
                land_ID = 3,
                landName = "Italien"
            };
            model.Land.Add(lm1);
            model.Land.Add(lm2);
            model.Land.Add(lm3);
            #endregion

            #region Ort
            model.Ort = new List<OrtModel>();

            model.Ort.Add(new OrtModel()
            {
                Id = 0,
                Bezeichnung = "Alle"
            });

            OrtModel om1 = new OrtModel()
            {
                Id = 1,
                Bezeichnung = "Wien"
            };
            OrtModel om2 = new OrtModel()
            {
                Id = 2,
                Bezeichnung = "Hamburg"
            };
            OrtModel om3 = new OrtModel()
            {
                Id = 3,
                Bezeichnung = "Rom"
            };

            model.Ort.Add(om1);
            model.Ort.Add(om2);
            model.Ort.Add(om3);
            #endregion

            #region Verpflegung
            model.Verpflegung = new List<VerpflegungModel>();

            model.Verpflegung.Add(new VerpflegungModel()
            {
                Id = 0,
                Bezeichnung = "Alle"
            });

            VerpflegungModel vm1 = new VerpflegungModel()
            {
                Id = 1,
                Bezeichnung = "Halbpension"
            };
            VerpflegungModel vm2 = new VerpflegungModel()
            {
                Id = 2,
                Bezeichnung = "Vollpension"
            };
            VerpflegungModel vm3 = new VerpflegungModel()
            {
                Id = 3,
                Bezeichnung = "All Inclusive"
            };

            model.Verpflegung.Add(vm1);
            model.Verpflegung.Add(vm2);
            model.Verpflegung.Add(vm3);

            #endregion

            model.Startdatum = DateTime.Now;
            model.Enddatum = DateTime.Now.AddYears(1);

            return model;
        }

        /// <summary>
        /// Befüllen des FilterModels für Laden POST (wenn bereits Daten ausgewählt wurden)
        /// </summary>
        /// <param name="filtermodel">Übergebenen Daten im FilterModel</param>
        /// <returns>Ein FilterModel mit Werten für DropDown</returns>
        private FilterModel FilterAnzeigeTest(FilterModel filtermodel)
        {
            FilterModel model = FilterAnzeigeTest();
            model.Startdatum = filtermodel.Startdatum;
            model.Enddatum = filtermodel.Enddatum;
            model.PreisMax = filtermodel.PreisMax;
            model.PreisMin = filtermodel.PreisMin;
            model.HotelKategorie = filtermodel.HotelKategorie;
            return model;
        }
    }
}