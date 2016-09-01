﻿using System;
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

        






    }
}
