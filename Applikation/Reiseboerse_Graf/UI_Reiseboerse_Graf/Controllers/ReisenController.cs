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
            model.Filter = FilterBefuellen();

            if (Globals.IST_TESTSYSTEM)
            {
                #region test
                try
                {
                    Debug.WriteLine("Testsystem");
                    model.Reisen = ReiseAnzeigeListeTest();

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
                try
                {
                    Debug.WriteLine("Daten aus Datenbank");
                    List<Reise> BL_ReiseListe = ReiseVerwaltung.LadeAlleReisen();
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
                        reiseModel.Bewertung = BewertungVerwaltung.LadeBewertungReise(reiseModel.ID);
                        foreach (var datum in ReiseVerwaltung.LadeReiseZeitpunkte(reiseModel.ID))
                        {
                            ReisedatumModel reisedatum = new ReisedatumModel()
                            {
                                Anmeldefrist = datum.Anmeldefrist,
                                Beginndatum = datum.Startdatum,
                                Enddatum = datum.Enddatum,
                                ID = datum.ID,
                                Restplätze = ReiseVerwaltung.Restplätze(datum.ID)
                            };
                            if (reisedatum.Anmeldefrist >= DateTime.Now && reisedatum.Restplätze >= 1)
                            {
                                reiseModel.Reisedaten.Add(reisedatum);
                            }
                        }
                        if (reiseModel.Reisedaten.Count >= 1)
                        {
                            model.Reisen.Add(reiseModel);
                        }
                    }
                }

                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden aller Reisen");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
                #endregion db
            }
            Debug.Unindent();
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
            model.Reisen = new List<ReiseModel>();
            Debug.WriteLine("Reisen - Laden - POST");
            Debug.Indent();
            if (Globals.IST_TESTSYSTEM)
            {
                #region Testsystem
                Debug.WriteLine("Testsystem");
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
                #endregion Testsystem
            }
            else
            {
                #region DB
                Debug.WriteLine("Daten aus Datenbank");
                try
                {
                    List<Reise> BL_FilterListe = new List<Reise>();
                    BL_FilterListe = ReiseVerwaltung.LadeReisenGefiltert(filterModel.Land_id, filterModel.Ort_ID, filterModel.HotelKategorie, filterModel.Verpflegungs_ID, filterModel.PreisMin, filterModel.PreisMax, filterModel.Startdatum, filterModel.Enddatum);
                    foreach (var reise in BL_FilterListe)
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
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Filtern der Reisen");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
                return View(model);
                #endregion DB
            }
        }

        /// <summary>
        /// Zeigt alle Details der Reise (Beschreibung, Hotelbeschreibung, Reisedatum)
        /// </summary>
        /// <param name="id">Id des Reisedatums (Reise zu entsprechendem Zeitpunkt)</param>
        /// <returns>ein ReiseAnzeigenModel oder NULL bei Fehler</returns>
        [HttpGet]
        public ActionResult Anzeigen(int id)
        {
            Debug.WriteLine("Reisedetails- Anzeigen - GET");
            Debug.Indent();
            ReiseAnzeigenModel model = new ReiseAnzeigenModel();
            if (Globals.IST_TESTSYSTEM)
            {
                #region Testsystem
                List<ReisedetailModel> liste = ReiseDetailListeTest();
                model.Reisedetail = liste.Find(x => x.ID == id);
                #endregion Testsystem
            }
            else
            {
                try
                {
                    Debug.WriteLine("Daten aus der Datenbank");
                    Reise BL_Reise = ReiseVerwaltung.SucheReiseZuDatum(id);
                    Reisedatum BL_Datum = ReiseVerwaltung.SucheReisedatum(id);
                    model.Reisedatum = new ReisedatumModel()
                    {
                        Anmeldefrist = BL_Datum.Anmeldefrist,
                        Beginndatum = BL_Datum.Startdatum,
                        Enddatum = BL_Datum.Enddatum,
                        Restplätze = ReiseVerwaltung.Restplätze(id),
                        ID = id
                    };
                    model.Reisedetail = new ReisedetailModel()
                    {
                        ID = BL_Reise.ID,
                        Beschreibung = BL_Reise.Beschreibung,
                        Hotelkategorie = BL_Reise.Unterkunft.Kategorie,
                        Verpflegung = BL_Reise.Unterkunft.Verpflegung.Bezeichnung,
                        Land = BL_Reise.Ort.Land.Bezeichnung,
                        Ort = BL_Reise.Ort.Bezeichnung,
                        Unterkunft = BL_Reise.Unterkunft.Bezeichnung,
                        Preis_Erwachsene = BL_Reise.Preis_Erwachsener,
                        Preis_Kind = BL_Reise.Preis_Kind,
                        Reisedatum_ID = id,
                        Titel = BL_Reise.Titel,
                        Unterkunft_ID = BL_Reise.Unterkunft.ID
                    };

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Anzeigen der Reise");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return View(model);
        }

        /// <summary>
        /// erzeugt die Partialview zum Einbinden auf die jeweilige Reisedetailseite
        /// </summary>
        /// <param name="id">ID der Unterkunft</param>
        /// <returns>Teilansicht mit dem Unterkunft-Model</returns>
        [ChildActionOnly]
        public ActionResult UnterkunftAnzeigen(int id)
        {
            Debug.WriteLine("Reisen - UnterkunftAnzeigen - ChildActionOnly - Get");
            Debug.Indent();

            Unterkunft unterkunft = new Unterkunft();
            unterkunft = ReiseVerwaltung.LadeUnterkunftZuReise(id);

            UnterkunftdetailModel model = new UnterkunftdetailModel();

            VerpflegungModel vm = new VerpflegungModel();

            try
            {
                vm.Id = unterkunft.Verpflegung.ID;
                vm.Bezeichnung = unterkunft.Verpflegung.Bezeichnung;

                model.Beschreibung = unterkunft.Beschreibung;
                model.Bezeichnung = unterkunft.Bezeichnung;
                model.ID = unterkunft.ID;
                model.Kategorie = unterkunft.Kategorie;
                model.Verpflegung = vm;
                model.Verpflegung_ID = vm.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler bei Unterkunft Anzeigen!");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }
            Debug.Unindent();
            return PartialView(model);
        }

        /// <summary>
        /// Speichern der Bewertung
        /// </summary>
        /// <param name="bewertung">den Wert der Bewertung (1-5)</param>
        /// <param name="reiseID">die bewertete Reise</param>
        /// <returns>gibt nichts zurück (Empty Result)</returns>
        [HttpPost]
        public EmptyResult Bewerten(int bewertung, int reiseID)
        {
            Debug.WriteLine("Reise - Bewerten - POST");
            BewertungVerwaltung.BewertungSpeichern(reiseID, bewertung);
            return new EmptyResult();
        }



        [PruefeBenutzer]
        [HttpGet]
        public ActionResult ReiseHinzufuegen()
        {
            List<Land> alleLaender = BL_Reiseboerse_Graf.BenutzerVerwaltung.AlleLaender();
            ReiseAnlegenModel UI_Reise = new ReiseAnlegenModel();
            UI_Reise.NeueUnterkunft = new UnterkunftdetailModel();
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
                    Id = aktVerpflegung.ID
                });
            }

            return View(UI_Reise);
        }

        /// <summary>
        /// Fügt eine neue Reise hinzu
        /// </summary>
        /// <param name="neueReise">Model mit den entsprechenden Daten</param>
        /// <returns></returns>
        [PruefeBenutzer]
        [HttpPost]
        public ActionResult ReiseHinzufuegen(ReiseAnlegenModel neueReise, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4, HttpPostedFileBase file5, HttpPostedFileBase file6, HttpPostedFileBase file7, HttpPostedFileBase file8, HttpPostedFileBase file9, HttpPostedFileBase file10)
        {
            Unterkunft neueUnterkunft = null;
            int e = 0;
            int i = 0;
            int bild_id = 0;
            int[] Reisebild_id = new int[5];
            int[] Unterkunftbild_id = new int[5];
            Debug.Indent();
            Debug.WriteLine("Reise - ReiseHinzufügen - Post");
            if (ModelState.IsValid)
            {

                if (file1 != null && file1.ContentLength > 0)
                {
                    bild_id = BL_Reiseboerse_Graf.BildVerwaltung.BildSpeichern(file1);
                    if (bild_id > 0)
                    {
                        Debug.WriteLine("Bildspeicher erfolgreich");
                        Reisebild_id[i] = bild_id;
                        i++;
                        bild_id = 0;
                    }
                }

                if (file2 != null && file2.ContentLength > 0)
                {
                    bild_id = BL_Reiseboerse_Graf.BildVerwaltung.BildSpeichern(file1);
                    if (bild_id > 0)
                    {
                        Debug.WriteLine("Bildspeicher erfolgreich");
                        Reisebild_id[i] = bild_id;
                        i++;
                        bild_id = 0;
                    }
                }
                if (file3 != null && file3.ContentLength > 0)
                {
                    bild_id = BL_Reiseboerse_Graf.BildVerwaltung.BildSpeichern(file1);
                    if (bild_id > 0)
                    {
                        Debug.WriteLine("Bildspeicher erfolgreich");
                        Reisebild_id[i] = bild_id;
                        i++;
                        bild_id = 0;
                    }
                }
                if (file4 != null && file4.ContentLength > 0)
                {
                    bild_id = BL_Reiseboerse_Graf.BildVerwaltung.BildSpeichern(file1);
                    if (bild_id > 0)
                    {
                        Debug.WriteLine("Bildspeicher erfolgreich");
                        Reisebild_id[i] = bild_id;
                        i++;
                        bild_id = 0;
                    }
                }
                if (file5 != null && file5.ContentLength > 0)
                {
                    bild_id = BL_Reiseboerse_Graf.BildVerwaltung.BildSpeichern(file1);
                    if (bild_id > 0)
                    {
                        Debug.WriteLine("Bildspeicher erfolgreich");
                        Reisebild_id[i] = bild_id;
                        i++;
                        bild_id = 0;
                    }
                }
                if (file6 != null && file6.ContentLength > 0)
                {
                    bild_id = BL_Reiseboerse_Graf.BildVerwaltung.BildSpeichern(file2);
                    if (bild_id > 0)
                    {
                        Unterkunftbild_id[e] = bild_id;
                        Debug.WriteLine("Bildspeicher erfolgreich");
                        e++;
                        bild_id = 0;
                    }
                }
                if (file7 != null && file7.ContentLength > 0)
                {
                    bild_id = BL_Reiseboerse_Graf.BildVerwaltung.BildSpeichern(file2);
                    if (bild_id > 0)
                    {
                        Unterkunftbild_id[e] = bild_id;
                        Debug.WriteLine("Bildspeicher erfolgreich");
                        e++;
                        bild_id = 0;
                    }
                }
                if (file8 != null && file8.ContentLength > 0)
                {
                    bild_id = BL_Reiseboerse_Graf.BildVerwaltung.BildSpeichern(file2);
                    if (bild_id > 0)
                    {
                        Unterkunftbild_id[e] = bild_id;
                        Debug.WriteLine("Bildspeicher erfolgreich");
                        e++;
                        bild_id = 0;
                    }
                }
                if (file9 != null && file9.ContentLength > 0)
                {
                    bild_id = BL_Reiseboerse_Graf.BildVerwaltung.BildSpeichern(file2);
                    if (bild_id > 0)
                    {
                        Unterkunftbild_id[e] = bild_id;
                        Debug.WriteLine("Bildspeicher erfolgreich");
                        e++;
                        bild_id = 0;
                    }
                }
                if (file10 != null && file10.ContentLength > 0)
                {
                    bild_id = BL_Reiseboerse_Graf.BildVerwaltung.BildSpeichern(file2);
                    if (bild_id > 0)
                    {
                        Unterkunftbild_id[e] = bild_id;
                        Debug.WriteLine("Bildspeicher erfolgreich");
                        e++;
                        bild_id = 0;
                    }
                }

                else
                {
                    Debug.WriteLine("kein Bild vorhanden");
                }
                if (neueReise.NeuerOrt != null)
                {

                    neueReise.Ort_id = BL_Reiseboerse_Graf.LaenderVerwaltung.SpeicherNeuenOrt(neueReise.NeuerOrt, neueReise.Land_id);
                    if (neueReise.Ort_id > 0)
                    {
                        Debug.WriteLine("Ortspeichern erfolgreich");
                    }
                    else
                    {
                        Debug.WriteLine("Ortspeichern fehlgeschlagen");
                    }
                }
                if (neueReise.NeuesLand != null)
                {

                    neueReise.Land_id = BL_Reiseboerse_Graf.LaenderVerwaltung.SpeicherNeuesLand(neueReise.NeuesLand);
                    if (neueReise.Land_id > 0)
                    {
                        Debug.WriteLine("landspeichern erfolgreich");
                    }
                    else
                    {
                        Debug.WriteLine("Landspeichern fehlgeschlagen");
                    }
                }
                if (UnterkunftdetailModel.PruefeUnterkunft(neueReise.NeueUnterkunft))// man muss kontrollieren ob in der Unterkunft Werte sind, anscheinend ist es immer NULL
                {
                    Verpflegung verpflegung = BL_Reiseboerse_Graf.ReiseVerwaltung.SucheVerpflegung(neueReise.Verpflegung_id);
                    neueUnterkunft = new Unterkunft()
                    {
                        Bezeichnung = neueReise.NeueUnterkunft.Bezeichnung,
                        Beschreibung = neueReise.NeueUnterkunft.Beschreibung,
                        Kategorie = neueReise.NeueUnterkunft.Kategorie,
                        Verpflegung = verpflegung,
                        
                    };
                    neueUnterkunft.ID = LaenderVerwaltung.SpeichereNeueUnterkunft(neueUnterkunft.Bezeichnung, neueUnterkunft.Beschreibung, neueUnterkunft.Kategorie, neueUnterkunft.Verpflegung);

                }




                if (neueReise.Unterkunft_id > 0)
                {
                    Debug.WriteLine("Unterkunftspeichern erfolgreich");
                }
                else
                {
                    Debug.WriteLine("Unterkunftspeichern fehlgeschlagen");

                }
                Reise BlReise = new Reise();
                BlReise.Titel = neueReise.Titel;
                BlReise.Beschreibung = neueReise.Beschreibung;
                BlReise.Preis_Erwachsener = neueReise.PreisErw;
                BlReise.Preis_Kind = neueReise.PreisKind;
                BlReise.Ort = BL_Reiseboerse_Graf.LaenderVerwaltung.SucheOrt(neueReise.Ort_id);
                BlReise.Unterkunft = neueUnterkunft;
                neueReise.Id = ReiseVerwaltung.SpeicherReise(BlReise);
                if (neueReise.Id > 0)
                {
                    Debug.WriteLine("Reise anlegen erfolgreich");
                    return RedirectToAction("");
                }
                try
                {
                    if (BildVerwaltung.BildZuReiseSpeichern(neueReise.Id, Reisebild_id) > 0)
                    {
                        Debug.WriteLine("BildZuReiseSpeichern - erfolgreich");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("BildZuReiseSpeichern - fehlgeschlagen");
                    Debug.WriteLine(ex.Message);
                }
                try
                {
                    if (BildVerwaltung.BildZuUnterkunft(neueReise.Unterkunft_id, Unterkunftbild_id) > 0)
                    {
                        Debug.WriteLine("UnterkunftBildSpeichern - erfolgreich");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("UnterkunftBildSpeichern - fehlgeschlagen");
                    Debug.WriteLine(ex.Message);
                }

            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// ReiseStart- und Enddatum sowie Anzahl der Reisen wird gesetzt.
        /// </summary>
        /// <param name="Reiseid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReiseAnzahlErstellen(int? Reiseid)
        {
            ReisedurchfuehrenModel DatumUndAnzahl = new ReisedurchfuehrenModel();
            DatumUndAnzahl.Reise_id = Reiseid;

            return View(DatumUndAnzahl);
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
                Filter = FilterBefuellen(),
                TextSuche = TextSuche
            };

            return View("Laden", viewmodel);
        }

        /// <summary>
        /// Bekommt eine Liste von Reisen aus BL und mappt diese auf das ReisenverwaltenModel
        /// der Mitarbeiter kann dort Reisen ändern oder löschen
        /// </summary>
        /// <returns>die View mit der Reisenliste</returns>
        [PruefeBenutzer]
        [HttpGet]
        public ActionResult Verwalten()
        {
            Debug.WriteLine("Reisen - Verwalten - GET");
            Debug.Indent();
            List<Reise> BL_Reisen = ReiseVerwaltung.LadeAlleReisen();
            List<ReiseVerwaltenModel> UI_Reisen = new List<ReiseVerwaltenModel>();

            foreach (var reise in BL_Reisen)
            {
                UI_Reisen.Add(new ReiseVerwaltenModel()
                {
                    ID = reise.ID,
                    Reisetitel = reise.Titel
                });
            }
            UI_Reisen = UI_Reisen.OrderBy(x => x.ID).ToList();
            Debug.Unindent();
            return View(UI_Reisen);
        }

        /// <summary>
        /// Alle Reisedaten einer Reise anzeigen
        /// </summary>
        /// <param name="id">ID der Reise</param>
        /// <returns></returns>
        [PruefeBenutzer]
        [HttpGet]
        public ActionResult Reisedaten(int id)
        {
            Debug.WriteLine("Reisen - Reisedaten - GET");
            Debug.Indent();
            List<ReisedatumModel> UI_Liste = new List<ReisedatumModel>();
            List<Reisedatum> BL_Liste = ReiseVerwaltung.LadeReiseZeitpunkte(id);
            foreach (var datum in BL_Liste)
            {
                UI_Liste.Add(new ReisedatumModel()
                {
                    Anmeldefrist = datum.Anmeldefrist,
                    Beginndatum = datum.Startdatum,
                    Enddatum = datum.Enddatum,
                    Restplätze = ReiseVerwaltung.Restplätze(datum.ID),
                    ID = datum.ID
                });
            }
            Debug.Unindent();
            return View(UI_Liste);
        }

        [PruefeBenutzer]
        [HttpGet]
        public ActionResult Entfernen(int id)
        {
            Debug.WriteLine("Reisen - Entfernen - GET");
            Debug.Indent();



            Debug.Unindent();
            return View();
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
        /// Befüllen des FilterModels mit Testdaten oder Daten aus der DB je nach Globals.ISTTESTSYSTEM
        /// </summary>
        /// <returns>das Filtermodel</returns>
        private FilterModel FilterBefuellen()
        {
            FilterModel model = new FilterModel();
            model.Startdatum = DateTime.Now;
            model.Enddatum = DateTime.Now.AddYears(1);
            if (Globals.IST_TESTSYSTEM)
            {
                #region Testsystem
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
                #endregion Testsystem
            }
            else
            {
                #region DB
                #region Filter Land
                model.Land = new List<LandModel>();
                model.Land.Add(new LandModel
                {
                    landName = "Alle Länder",
                    land_ID = 0
                });
                foreach (var land in BenutzerVerwaltung.AlleLaender())
                {
                    model.Land.Add(new LandModel
                    {
                        landName = land.Bezeichnung,
                        land_ID = land.ID
                    });
                }
                #endregion Filter Land
                #region Filter Ort
                model.Ort = new List<OrtModel>();
                model.Ort.Add(new OrtModel
                {
                    Bezeichnung = "Alle Orte",
                    Id = 0
                });
                foreach (var ort in LaenderVerwaltung.AlleOrte())
                {
                    model.Ort.Add(new OrtModel
                    {
                        Bezeichnung = ort.Bezeichnung,
                        Id = ort.ID
                    });
                }
                #endregion Filter Ort
                #region Filter Verpflegungen
                model.Verpflegung = new List<VerpflegungModel>();
                model.Verpflegung.Add(new VerpflegungModel()
                {
                    Bezeichnung = "Alle Verpflegungen",
                    Id = 0
                });
                foreach (var verpflegung in LaenderVerwaltung.alleVerpflegung())
                {
                    model.Verpflegung.Add(new VerpflegungModel
                    {
                        Bezeichnung = verpflegung.Bezeichnung,
                        Id = verpflegung.ID
                    });
                }
                #endregion Filter Verpflegungen
                #endregion DB
            }
            return model;
        }

        /// <summary>
        /// Befüllen des FilterModels für Laden POST (wenn bereits Daten ausgewählt wurden)
        /// </summary>
        /// <param name="filtermodel">Übergebenen Daten im FilterModel</param>
        /// <returns>Ein FilterModel mit Werten für DropDown</returns>
        private FilterModel FilterAnzeigeTest(FilterModel filtermodel)
        {
            FilterModel model = FilterBefuellen();
            model.Startdatum = filtermodel.Startdatum;
            model.Enddatum = filtermodel.Enddatum;
            model.PreisMax = filtermodel.PreisMax;
            model.PreisMin = filtermodel.PreisMin;
            model.HotelKategorie = filtermodel.HotelKategorie;
            return model;
        }
    }
}