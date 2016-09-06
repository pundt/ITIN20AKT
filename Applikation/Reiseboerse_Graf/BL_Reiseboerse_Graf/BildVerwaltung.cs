using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BL_Reiseboerse_Graf
{
    public class BildVerwaltung
    {
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
        /// Sucht die passende Bild ID zu der Reise
        /// </summary>
        /// <param name="id">ID der Reise</param>
        /// <returns></returns>
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
        /// Sucht die passende Bild ID zu der Unterkunft
        /// </summary>
        /// <param name="id">ID der Unterkunft</param>
        /// <returns></returns>
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
        /// Bildfile wird übergeben, Bilddaten werden ausgelesen und in Tabelle gespeichert
        /// </summary>
        /// <param name="file"></param>
        /// <returns>wenn speichern erfolgreich "true"</returns>
        public static bool BildSpeichern(HttpPostedFileBase file)
        {

            Debug.Write("BildVerwaltung - BildSpeichern");
            Debug.Indent();

            Bild neuesBild = new Bild();            
            
            bool erfolgreich = false;
            reisebueroEntities context = new reisebueroEntities();
            
            if (file != null && file.ContentLength>0)
            {
                using (var reader = new System.IO.BinaryReader(file.InputStream))
                {
                    neuesBild.Bilddaten = reader.ReadBytes(file.ContentLength);
                    erfolgreich = true;                    
                    Debug.Write("Bilddaten lesen erfolgreich");
                }
            }
            else
            {
                Debug.Write("kein Bild mittgeschickt");
            }     
        
            context.AlleBilder.Add(neuesBild);
            context.SaveChanges();

            return erfolgreich;
        }
    }
}
