﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Reiseboerse_Graf
{
    public class LaenderVerwaltung
    {
        /// <summary>
        /// Liefert alle Orte aus der DB
        /// </summary>
        /// <returns>Liste aller Orte</returns>
        public static List<Ort> AlleOrte()
        {
            Debug.WriteLine("LaenderVerwaltung - Lade alle Orte");
            Debug.Indent();
            List<Ort> alleOrte = new List<Ort>();
            using (reisebueroEntities context = new reisebueroEntities())
            {
                try
                {
                    alleOrte = context.AlleOrte.ToList();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden aller Orte aus der DB");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();

                }

            }
            Debug.Unindent();
            return alleOrte;


        }
        /// <summary>
        /// Liefert alle Unterkuenfte aus der DB
        /// </summary>
        /// <returns>Liste aller Unterkuenfte</returns>
        public static List<Unterkunft> AlleUnterkuenfte()
        {
            Debug.WriteLine("Laenderverwaltung - Lade alle Unterkuenfte");
            Debug.Indent();
            List<Unterkunft> alleUnterkuenfte = new List<Unterkunft>();
            using (reisebueroEntities context = new reisebueroEntities())
            {
                try
                {
                    alleUnterkuenfte = context.AlleUnterkuenfte.Include("Verpflegung").ToList();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden aller Unterkuenfte");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();


                }


            }
            Debug.Unindent();
            return alleUnterkuenfte;



        }
        /// <summary>
        /// Liefert alle Verpflegegungen aus der DB
        /// </summary>
        /// <returns> Liste aller Verpflegungen</returns>
        public static List<Verpflegung> alleVerpflegung()
        {
            Debug.WriteLine("LaenderVerwaltung - Lade alle Verpflegungen");
            Debug.Indent();
            List<Verpflegung> alleVerpflegung = new List<Verpflegung>();

            using (reisebueroEntities context = new reisebueroEntities())
            {
                try
                {

                    alleVerpflegung = context.AlleVerpflegungen.ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden aller Verpflegungen");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();


                }

            }
            Debug.Unindent();
            return alleVerpflegung;
        }

        public static int SpeicherNeuenOrt(string neuerort, int land_id)
        {
            int index = -1;

            Debug.WriteLine("LaenderVerwaltung - SpeicherNeuenOrt");
            Debug.Indent();

            using (reisebueroEntities context = new reisebueroEntities())
            {
                try
                {
                    Land neuesLand = context.AlleLaender.Where(x => x.ID == land_id).FirstOrDefault();
                    Ort neuerOrt = new Ort();
                    neuerOrt.Bezeichnung = neuerort;
                    neuerOrt.Land = neuesLand;
                    context.AlleOrte.Add(neuerOrt);
                    index = context.SaveChanges();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim SpeichernNeuenOrt");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                    Debug.Unindent();
                }
            }
            return index;

        }
        public static int SpeicherNeuesLand(string neuesLand)
        {
            int index = -1;

            Debug.WriteLine("Länderverwaltung - SpeicherNeuesLand");
            Debug.Indent();

            if (neuesLand != null)
            {
                Land land = new Land();
                land.Bezeichnung = neuesLand;
                using (reisebueroEntities context = new reisebueroEntities())
                {
                    try
                    {
                        context.AlleLaender.Add(land);
                        index = context.SaveChanges();
                        Debug.WriteLine("Speichern erfolgreich");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Fehler beim SpeicherNeuesLand");
                        Debug.WriteLine(ex.Message);
                        Debugger.Break();
                        Debug.Unindent();
                    }                    
                }
            }
            return index;
        }
        public static int SpeichereNeueUnterkunft(string beschreibung, string bezeichnung, int kategorie)
        {
            int index = -1;
            Debug.WriteLine("Länderverwaltung - SpeichereNeueUnterkunft");
            Debug.Indent();

            if (bezeichnung != null && beschreibung != null && kategorie !=0)
            {
                Unterkunft unterkunft = new Unterkunft();
                unterkunft.Beschreibung = beschreibung;
                unterkunft.Bezeichnung = bezeichnung; ;
                unterkunft.Kategorie = kategorie;
                using (reisebueroEntities context = new reisebueroEntities())
                {
                    try
                    {
                        context.AlleUnterkuenfte.Add(unterkunft);
                        index = context.SaveChanges();
                        Debug.WriteLine("Speichern erfolgreich");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Fehler beim SpeicherNeueUnterkunft");
                        Debug.WriteLine(ex.Message);
                        Debugger.Break();
                        Debug.Unindent();
                    }
                }
            }
            return index;
        }
    }
}
