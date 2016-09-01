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
        /// <param name="land_id">ID des ausgewählten Landes aus der Dropdownbox</param>
        /// <param name="ort_id">ID des ausgewählten Ortes aus der Dropdownbox</param>
        /// <param name="kategorie_id">Alle IDs der ausgewählten Kategorien (Checkboxen)</param>
        /// <param name="startdatum">Das gewählte Startdatum</param>
        /// <param name="enddatum">Das gewählte Enddatum</param>
        /// <returns>eine Liste von Reisen</returns>
        public static List<Reise> LadeReisenGefiltert(int land_id, int ort_id, int kategorie_id, DateTime startdatum, DateTime enddatum)
        {
            Debug.WriteLine("Reiseverwaltung - Lade Reisen gefiltert");
            Debug.Indent();

            List<Reise> reisenGefiltert = new List<Reise>();

            using (var context = new reisebueroEntities())
            {
                reisenGefiltert = LadeAlleReisen();

                try
                {
                    if (land_id != 0)
                    {
                        reisenGefiltert = reisenGefiltert.Where(x => x.Ort.Land.ID == land_id).ToList();
                    }
                    if (ort_id != 0)
                    {
                        reisenGefiltert = reisenGefiltert.Where(x => x.Ort.ID == ort_id).ToList();
                    }
                    if (kategorie_id != 0)
                    {
                        reisenGefiltert = reisenGefiltert.Where(x => x.Unterkunft.Kategorie >= kategorie_id).ToList();
                    }
                    if (startdatum >= DateTime.Now)
                    {   
                        reisenGefiltert = reisenGefiltert.Where(x => x.AlleReisedaten.Any(y => y.Startdatum >= startdatum)).ToList();
                    }
                    if (enddatum > startdatum)
                    {
                        reisenGefiltert = reisenGefiltert.Where(x => x.AlleReisedaten.Any(y => y.Enddatum > startdatum)).ToList();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Filtern der Reise!");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }

            Debug.Unindent();
            return reisenGefiltert;
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
