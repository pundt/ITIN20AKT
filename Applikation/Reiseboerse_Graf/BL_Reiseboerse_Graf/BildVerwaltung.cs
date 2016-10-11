﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Geschäftslogik inklusive Datenbankverbindung
/// </summary>
namespace BL_Reiseboerse_Graf
{
    /// <summary>
    /// Die Verwaltung der Bilder aus der Datenbank
    /// </summary>
    public class BildVerwaltung
    {
        /// <summary>
        /// Lade alle IDs aller Bilder
        /// </summary>
        /// <returns>Liste von int</returns>
        public static List<int> LadeAlleBildIDs()
        {
            Debug.WriteLine("BildVerwaltung - LadeAlleBildIDs");
            Debug.Indent();
            List<int> liste = new List<int>();
            using (var context = new reisebueroEntities())
            {
                try
                {
                    foreach (var bild in context.AlleBilder)
                    {
                        liste.Add(bild.ID);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden aller Bild IDs");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return liste;
        }


        /// <summary>
        /// Liest aus der Datebank ein Bild aus
        /// </summary>
        /// <param name="id">Id des Bildes</param>
        /// <returns>Bild Objekt wenn ID gueltig bei ungueltiger ID oder Fehler NULL</returns>
        public static Bild LadeBild(int id)
        {
            Debug.WriteLine("BildVerwaltung - LadeBild");
            Debug.Indent();
            Bild gesuchtesBild = null;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    gesuchtesBild = context.AlleBilder.Where(x => x.ID == id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler bei Bild Laden");
                    Debug.WriteLine(ex.Message);
                }
            }
            Debug.Unindent();
            return gesuchtesBild;
        }

        /// <summary>
        /// Sucht die alle Bild_IDs zu der Reise
        /// </summary>
        /// <param name="id">ID der Reise</param>
        /// <returns>die Liste aller passender Bild IDs</returns>
        public static List<int> LadeBildID(int id)
        {
            Debug.WriteLine("BildVerwaltung - LadeBildID");
            Debug.Indent();
            List<int> id_bilder = new List<int>();
            using (var context = new reisebueroEntities())
            {
                try
                {
                    List<Reise_Bild> reisebilder = context.AlleReise_Bilder.Where(x => x.Reise.ID == id).ToList();
                    foreach (var reisebild in reisebilder)
                    {
                        id_bilder.Add(reisebild.Bild.ID);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden der Bild ID");
                    Debug.WriteLine(ex.Message);
                }
            }
            Debug.Unindent();
            return id_bilder;
        }

        /// <summary>
        /// Sucht alle passende Bild ID zu der Unterkunft
        /// </summary>
        /// <param name="id">ID der Unterkunft</param>
        /// <returns>die Liste aller passender Bild IDs</returns>
        public static List<int> LadeUnterkunftBildID(int id)
        {
            Debug.WriteLine("BildVerwaltung - LadeUnterkunftBildID");
            Debug.Indent();
            List<int> id_bilder = new List<int>();
            using (var context = new reisebueroEntities())
            {
                try
                {
                    List<Unterkunft_Bild> unterkunftbilder = context.AlleUnterkunft_Bilder.Where(x => x.Unterkunft.ID == id).ToList();
                    foreach (var unterkunftbild in unterkunftbilder)
                    {
                        id_bilder.Add(unterkunftbild.Bild.ID);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden der Bild ID einer Unterkunft");
                    Debug.WriteLine(ex.Message);
                }
            }
            Debug.Unindent();
            return id_bilder;
        }


        /// <summary>
        /// Bildfile wird übergeben, Bilddaten werden ausgelesen und in Datenbank gespeichert
        /// </summary>
        /// <param name="file">die hochgeladene Bilddatei</param>
        /// <returns>wenn speichern erfolgreich "true"</returns>
        public static int BildSpeichern(HttpPostedFileBase file)
        {

            Debug.Write("BildVerwaltung - BildSpeichern");
            Debug.Indent();

            Bild neuesBild = new Bild();

            reisebueroEntities context = new reisebueroEntities();

            if (file != null && file.ContentLength > 0)
            {

                using (var reader = new System.IO.BinaryReader(file.InputStream))
                {
                    neuesBild.Bilddaten = reader.ReadBytes(file.ContentLength);

                    Debug.Write("Bilddaten lesen erfolgreich");
                }
            }
            else
            {
                Debug.Write("kein Bild mittgeschickt");
            }


            context.AlleBilder.Add(neuesBild);
            context.SaveChanges();
            int bild_id = neuesBild.ID;

            return bild_id;
        }
        /// <summary>
        /// Speichert die Zuordnung der Reise zu dem Bild in der Zwischentabelle Reise_Bild
        /// mit den Übergabewerten von Reise_id und Bild_id
        /// </summary>
        /// <param name="reiseid">die ID der Reise</param>
        /// <param name="bild_id">die ID des Bildes</param>
        /// <returns>bei Erfolg die ID des Eintrags in die Zwischentabelle Reise_Bild</returns>
        public static int BildZuReiseSpeichern(int reiseid, int bild_id)
        {

            Reise_Bild reisebild = new Reise_Bild();

            using (reisebueroEntities context = new reisebueroEntities())
            {


                try
                {
                    Reise reise = context.AlleReisen.Where(x => x.ID == reiseid).FirstOrDefault();
                    Bild bild = context.AlleBilder.Where(x => x.ID == bild_id).FirstOrDefault();
                    if (reise == null)
                    {
                        Debug.WriteLine("Keine Reise gefunden");
                    }
                    if (bild == null)
                    {
                        Debug.WriteLine("Kein Bild gefunden");
                    }
                    if (reise.ID > 0 && bild.ID > 0)
                    {

                        reisebild.Reise = reise;
                        reisebild.Bild = bild;
                        context.AlleReise_Bilder.Add(reisebild);
                        context.SaveChanges();

                        Debug.WriteLine("ReiseBild speichern erfolgreich");
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ReiseBild speichern fehlgeschlagen");
                    Debug.WriteLine(ex.Message);
                }
            }
            return reisebild.ID;
        }
        /// <summary>
        /// Speichert die Zuordnung der Unterkunft zu dem Bild in der Zwischentabelle Unterkunft_Bild
        /// mit den Übergabewerten von Unterkunft_id und Bild_id
        /// </summary>
        /// <param name="bild_id">die ID der Reise</param>
        /// <param name="unterkunft_id">die ID der Unterkunft</param>
        /// <returns>bei Erfolg die ID des Eintrags in die Zwischentabelle Unterkunft_Bild</returns>
        public static int BildZuUnterkunftSpeichern(int bild_id, int unterkunft_id)
        {

            Unterkunft_Bild unterkunftBild = new Unterkunft_Bild();

            using (reisebueroEntities context = new reisebueroEntities())
            {

                try
                {
                    Bild bild = context.AlleBilder.Where(x => x.ID == bild_id).FirstOrDefault(); ;
                    Unterkunft unterkunft = context.AlleUnterkuenfte.Where(x => x.ID == unterkunft_id).FirstOrDefault();
                    if (unterkunft == null)
                    {
                        Debug.WriteLine("Keine Unterkunft gefunden");
                    }
                    if (bild == null)
                    {
                        Debug.WriteLine("Kein Bild gefunden");
                    }
                    if (unterkunft.ID > 0 && bild.ID > 0)
                    {

                        unterkunftBild.Unterkunft = unterkunft;
                        unterkunftBild.Bild = bild;

                        context.AlleUnterkunft_Bilder.Add(unterkunftBild);
                        context.SaveChanges();

                        Debug.WriteLine("UnterkunftBild speichern erfolgreich");

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("UnterkunftBild speichern fehlgeschlagen");
                    Debug.WriteLine(ex.Message);
                }
            }
            return unterkunftBild.ID;
        }

    }
}
