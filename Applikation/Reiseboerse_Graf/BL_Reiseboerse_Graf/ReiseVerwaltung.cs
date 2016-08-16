﻿using System;
using System.Collections.Generic;
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
        //public static List<Reise> LadeAlleReisen()
        //{
        //    return null;
        //}

        /// <summary>
        /// Lädt alle Reisen, die den Filterkriterien entsprechen
        /// </summary>
        /// <param name="kontinent_id">ID des ausgewählten Kontinents aus der Dropdownbox</param>
        /// <param name="land_id">ID des ausgewählten Landes aus der Dropdownbox</param>
        /// <param name="stadt_id">ID der ausgewählten Stadt aus der Dropdownbox</param>
        /// <param name="kategorien_id">Alle IDs der ausgewählten Kategorien (Checkboxen)</param>
        /// <returns>eine Liste von Reisen</returns>
        //public static List<Reise> LadeReisenGefiltert(int kontinent_id, int land_id, int stadt_id, List<int> kategorien_id)
        //{

        //    return null;
        //}

        ///liefert die aktuellen Restplätze der Reise mit der entsprechenden ID aus der DB zurück
        public static int Restplätze(int reise_id)
        {
            

            return 1;
        }

        /// <summary>
        /// Liest aus der Datebank ein Bild aus
        /// </summary>
        /// <param name="id">Id des Bildes</param>
        /// <returns>Bild als Bytearray</returns>
        public static byte[] Bildausleser(int id)
        {
            //Sobald DB eingebunden wurde funktioniert es so

            //reisebueroEntities ent = new reisebueroEntities();
            //Bild gesuchtesBild = (from b in ent.Bild where b.id == id select b).FirstOrDefault();
            //byte[] c = gesuchtesBild.bild1;
            //return c;

            //damit es jetzt in diesem Zwischenstadium kompiliert
            return null;
        }






    }
}
