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
            Debug.WriteLine("ReiseVerwaltung - LadeAlleReisen");
            Debug.Indent();
            List<Reise> reisen = new List<Reise>();
            using (var context = new reisebueroEntities())
            {
                try
                {  
                    reisen = context.AlleReisen.Include("AlleReisedaten")
                        .Include("Unterkunft.Verpflegung").Include("Ort.Land")
                        .ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden aller Reisen");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }     
            }
            Debug.Unindent();
            return reisen;
        }

        /// <summary>
        /// Lädt zu einer Reise alle Zeitpunkte dieser Reise (also alle Reisedaten)
        /// </summary>
        /// <param name="reise_id">die ID der Reise</param>
        /// <returns>eine Liste von Reisedaten oder NULL bei Fehler</returns>
        public static List<Reisedatum> LadeReiseZeitpunkte(int reise_id)
        {
            Debug.WriteLine("ReiseVerwaltung - LadeReiseZeitpunkte");
            Debug.Indent();
            List<Reisedatum> liste = new List<Reisedatum>();
            using (var context = new reisebueroEntities())
            {
                try
                {
                    liste=context.AlleReisedaten.Where(x => x.Reise.ID == reise_id).ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden aller Reisedaten einer Reise");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }

            Debug.Unindent();
            return liste;
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
        public static List<Reise> LadeReisenGefiltert(int land_id, int ort_id, int kategorie_id, int verpflegung_id, int preis_min, int preis_max, DateTime startdatum, DateTime enddatum)
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
                    if (verpflegung_id!=0)
                    {
                        reisenGefiltert = reisenGefiltert.Where(x => x.Unterkunft.Verpflegung.ID == verpflegung_id).ToList();
                    }
                    if (preis_min!=0)
                    {
                        reisenGefiltert = reisenGefiltert.Where(x => x.Preis_Erwachsener>=preis_min).ToList();
                    }
                    if (preis_max != 0)
                    {
                        reisenGefiltert = reisenGefiltert.Where(x => x.Preis_Erwachsener <= preis_max).ToList();
                    }
                    if (startdatum >= DateTime.Now)
                    {   
                        reisenGefiltert = reisenGefiltert.Where(x => x.AlleReisedaten.Any(y => y.Startdatum >= startdatum)).ToList();
                    }
                    if (enddatum > startdatum)
                    {
                        reisenGefiltert = reisenGefiltert.Where(x => x.AlleReisedaten.Any(y => y.Enddatum > startdatum)).ToList();
                        reisenGefiltert = reisenGefiltert.Where(x => x.AlleReisedaten.Any(y => y.Enddatum <= enddatum)).ToList();
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
        /// Ermittelt die Restplätze einer Reise zu einem speziellen Datum aus der DB
        /// </summary>
        /// <returns>die Anzahl der Restplätze oder -1 wenn ein Fehler auftritt</returns>      
        public static int Restplätze(int reisedatum_id)
        {
            Debug.WriteLine("ReiseVerwaltung - Restplätze");
            Debug.Indent();
            int restplätze=-1;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    List<Reisedurchfuehrung> reisedurchfuehrungen = context.AlleReisedurchfuehrungen.Where(x => x.Reisedatum.ID == reisedatum_id).ToList();
                    List<Buchung> buchungen = context.AlleBuchungen.Where(x => x.Reisedatum.ID == reisedatum_id&&x.BuchungStorniert==null).ToList();
                    int anzahlReisen = reisedurchfuehrungen.Count;
                    int anzahlBuchungen = buchungen.Count;
                    restplätze = anzahlReisen - anzahlBuchungen;

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

        /// <summary>
        /// Sucht anhand einer Reise_ID die dazugehörige Unterkunft
        /// </summary>
        /// <param name="reise_id">die ID der Reise</param>
        /// <returns>die zur Reise dazugehörende Unterkunft</returns>
        public static Unterkunft LadeUnterkunftZuReise(int reise_id)
        {
            Debug.WriteLine("ReiseVerwaltung - Lade Unterkunft zu Reise");
            Debug.Indent();

            Unterkunft unterkunft = new Unterkunft();
            List<Reise> alleReisen = new List<Reise>();
            alleReisen = LadeAlleReisen();

            using (var context = new reisebueroEntities())
            {
                try
                {
                    foreach (var reise in alleReisen)
                    {
                        if (reise.ID == reise_id)
                        {
                            unterkunft = reise.Unterkunft;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden der Unterkunft!");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }

            Debug.Unindent();
            return unterkunft;
        }

        /// <summary>
        /// Sucht zu einer ID das Verpflegungsobjekt
        /// </summary>
        /// <param name="verpflegung_id">die ID der Verpflegung</param>
        /// <returns>eine Verpflegung</returns>
        public static Verpflegung SucheVerpflegung(int verpflegung_id)
        {
            Debug.WriteLine("ReiseVerwaltung - SucheVerpflegung");
            Debug.Indent();

            Verpflegung gesuchteVerpflegung = new Verpflegung();

            using (var context = new reisebueroEntities())
            {
                try
                {
                    gesuchteVerpflegung = context.AlleVerpflegungen.Where(x => x.ID == verpflegung_id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden der Verpflegung!");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return gesuchteVerpflegung;
        }

        /// <summary>
        /// Sucht eine Reise anhand einer Reisedatum_ID und liefert diese zurück
        /// </summary>
        /// <param name="reisedatum_id">Die ID des Reisedatum</param>
        /// <returns>die gesuchte Reise</returns>
        public static Reise SucheReiseZuDatum(int reisedatum_id)
        {
            Debug.WriteLine("ReiseVerwaltung - Suche Reise Zu Datum");
            Debug.Indent();

            Reise gesuchteReise = new Reise();
            Reisedatum suchDatum = new Reisedatum();
            List<Reise> alleReisen = new List<Reise>();
            alleReisen = LadeAlleReisen();

            using (var context = new reisebueroEntities())
            {
                try
                {
                    suchDatum = context.AlleReisedaten.Where(x => x.ID == reisedatum_id).FirstOrDefault();
                    int reiseId = suchDatum.Reise.ID;
                    gesuchteReise = alleReisen.Where(x => x.ID == reiseId).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Suchen der Reise zu einem Datum!");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }

            Debug.Unindent();
            return gesuchteReise;
        }

        /// <summary>
        /// Sucht ein Reisedatum anhand der Reisedatum_ID und liefert dieses zurück
        /// </summary>
        /// <param name="reisedatum_id">die ID des gesuchten Reisedatums</param>
        /// <returns>das gesuchte Reisedatum</returns>
        public static Reisedatum SucheReisedatum(int reisedatum_id)
        {
            Debug.WriteLine("ReiseVerwaltung - Suche Reisedatum");
            Debug.Indent();

            Reisedatum gesuchtesReisedatum = new Reisedatum();

            using (var context = new reisebueroEntities())
            {
                try
                {
                    gesuchtesReisedatum = context.AlleReisedaten.Where(x => x.ID == reisedatum_id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Suchen des Reisedatums!");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }

            Debug.Unindent();
            return gesuchtesReisedatum;
        }

     

    }
}
