﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

/// <summary>
/// Geschäftslogik inklusive Datenbankverbindung
/// </summary>
namespace BL_Reiseboerse_Graf
{
    /// <summary>
    /// Verwaltung der Benutzer (Anlegen, Ändern, Anmelden) in der Datenbank
    /// </summary>
    public class BenutzerVerwaltung
    {
        /// <summary>
        /// Überprüft ob Anmeldedaten ok sind
        /// </summary>
        /// <param name="benutzerName">Die vergebene Email-Adresse</param>
        /// <param name="passwort">Das vergebene Passwort</param>
        /// <returns>true oder false</returns>
        public static bool Anmelden(string benutzerName, string passwort)
        {
            Debug.WriteLine("BenutzerVerwaltung - Anmelden");
            return Tools.PasswortUndEmailVergleich(benutzerName, passwort);
        }

        /// <summary>
        /// Liefert alle Kunden aus der DB
        /// </summary>
        /// <returns>Liste aller Kunden</returns>
        public static List<Benutzer> AlleBenutzer()
        {
            List<Benutzer> benutzerListe = new List<Benutzer>();
            reisebueroEntities context = new reisebueroEntities();
            benutzerListe =  context.AlleBenutzer.ToList();
            return benutzerListe;
        }

        /// <summary>
        /// Liefert alle Laender aus der DB
        /// </summary>
        /// <returns>Liste alle Laender</returns>
        public static List<Land> AlleLaender()
        {
            reisebueroEntities context = new reisebueroEntities();

            List<Land> alleLaender = context.AlleLaender.ToList();

            return alleLaender;
        }

        /// <summary>
        /// Sucht den Benutzer anhand seiner Email aus der DB 
        /// </summary>
        /// <param name="email">die Email des gesuchten Benutzers</param>
        /// <returns>den Benutzer oder NULL kein benutzer gefunden wird oder bei Fehler</returns>
        public static Benutzer BenutzerSuchen(string email)
        {
            Debug.WriteLine("BenutzerVerwaltung - BenutzerSuche(email)");
            Debug.Indent();
            Benutzer gesuchterBenutzer = null;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    gesuchterBenutzer = context.AlleBenutzer.Where(x => x.Email == email).FirstOrDefault();
                    int id = gesuchterBenutzer.ID;
                    gesuchterBenutzer = context.AlleBenutzer.Find(id);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Suchen des Benutzers");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return gesuchterBenutzer;
        }

        /// <summary>
        /// Speichert das übergebene Objekt in die Datenbank
        /// </summary>
        /// <param name="benutzer">das Datenbankobjekt Benutzer</param>
        /// <returns>die Anzahl der betroffenen Zeilen</returns>
        public static int Aktualisieren(Benutzer benutzer)
        {
            Debug.WriteLine("BenutzerVerwaltung - Aktualisieren(id)");
            Debug.Indent();
            int zeilen = 0;
            Benutzer gesuchterBenutzer = null;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    gesuchterBenutzer = context.AlleBenutzer.Where(x => x.ID == benutzer.ID).FirstOrDefault();
                    gesuchterBenutzer.Nachname = benutzer.Nachname;
                    gesuchterBenutzer.ID = benutzer.ID;
                    gesuchterBenutzer.Geburtsdatum = benutzer.Geburtsdatum;
                    gesuchterBenutzer.Vorname = benutzer.Vorname;
                    gesuchterBenutzer.Land = benutzer.Land;
                    gesuchterBenutzer.Passwort = benutzer.Passwort;
                    gesuchterBenutzer.Telefon = benutzer.Telefon;
                    gesuchterBenutzer.Titel = benutzer.Titel;
                    zeilen=context.SaveChanges();
                    Debug.WriteLineIf(zeilen == 1, "Benutzer erfolgreich geändert!");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Aktualisieren des Benutzers");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return zeilen;

        }
    }
}
