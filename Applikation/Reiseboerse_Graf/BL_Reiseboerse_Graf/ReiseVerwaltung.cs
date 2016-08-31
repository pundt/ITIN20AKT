using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL_Reiseboerse_Graf
{
    public class ReiseVerwaltung
    {
        /// <summary>
        /// Lädt alle Reisen aus der Datenbank
        /// </summary>
        /// <returns>eine Liste von Reisen oder bei einem Fehler null</returns>
        public static List<Reise> LadeAlleReisen()
        {
            Debug.WriteLine("ReiseVerwaltung - ladeAlleReisen");
            Debug.Indent();
            using (var context = new reisebueroEntities())
            {
                try
                {  
                    List<Reise> reisen = context.AlleReisen
                        .Include("AlleReisedaten")
                        .ToList();
                    return reisen;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden aller Reisen");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                    return null;
                }
                
            }
        }

        /// <summary>
        /// Lädt alle Reisen, die den Filterkriterien entsprechen
        /// </summary>
        /// <param name="kontinent_id">ID des ausgewählten Kontinents aus der Dropdownbox</param>
        /// <param name="land_id">ID des ausgewählten Landes aus der Dropdownbox</param>
        /// <param name="stadt_id">ID der ausgewählten Stadt aus der Dropdownbox</param>
        /// <param name="kategorien_id">Alle IDs der ausgewählten Kategorien (Checkboxen)</param>
        /// <returns>eine Liste von Reisen</returns>
        public static List<Reise> LadeReisenGefiltert(int kontinent_id, int land_id, int stadt_id, List<int> kategorien_id)
        {

            return null;
        }

        /// <summary>
        /// Ermittelt die Restplätze einer Reise aus der DB
        /// </summary>
        /// <returns>die Anzahl der Restplätze oder -1 wenn ein Fehler auftritt</returns>      
        public static int Restplätze(int reise_id)
        {
            Debug.WriteLine("ReiseVerwaltung - Restplätze");
            Debug.Indent();
            int restplätze=-1;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    List<Reisedurchfuehrung> liste=context.AlleReisedurchfuehrungen.Where(x => x.Buchung == null).ToList();
                    restplätze = liste.Count;

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Ermitteln der Restplätze");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }

            Debug.Unindent();
            return restplätze;
        }

        /// <summary>
        /// Sucht nach einer spezifischen Reise mittels eines Suchtexts
        /// </summary>
        /// <param name="suchtext">String Variable beinhalted den gesuchten Begriff</param>
        /// <returns> Liste vom typ Reise</returns>
        public static List<Reise> SucheReise(string suchtext)
        {
            Debug.WriteLine("ReiseVerwaltung - Suche Reise");
            Debug.Indent();
            List<Reise> liste = new List<Reise>();
            using (var context=new reisebueroEntities())
            {
                try
                {
                    liste = context.AlleReisen.Include("AlleReisedaten").Include("Unterkunft.Verpflegung").Include("Ort.Land").ToList();
                    if (liste != null&&liste.Count>0)
                    {
                        liste = liste.Where(x => x.Beschreibung.Contains(suchtext)).ToList();
                    }
                  
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Suchen der Reise");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }

            Debug.Unindent();
            return liste;

        }

        






    }
}
