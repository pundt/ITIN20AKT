using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
