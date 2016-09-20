using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BL_Reiseboerse_Graf
{
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
    }
}
