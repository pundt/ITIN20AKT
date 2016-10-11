using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

/// <summary>
/// Geschäftslogik inklusive Datenbankverbindung
/// </summary>
namespace BL_Reiseboerse_Graf
{
    public class Tools
    {
        /// <summary>
        /// Diese Methode vergleicht Email und Passwort mit den Daten in der DB
        /// </summary>
        /// <param name="email">Email-Adresse des Users</param>
        /// <param name="passwort">Passwort aus der Oberfläche</param>
        /// <returns>true oder false</returns>
        public static bool PasswortUndEmailVergleich(string email, string passwort)
        {
            reisebueroEntities context = new reisebueroEntities();

            SHA512 hash = SHA512.Create();

            byte[] pw = hash.ComputeHash(Encoding.UTF8.GetBytes(passwort));

            using (context)
            {
                foreach (Benutzer b in context.AlleBenutzer)
                {
                    if (b.Email == email && pw.SequenceEqual(b.Passwort))
                    {
                        return true;
                    }
                } 
            }

            return false;
        }

        /// <summary>
        /// Diese Methode vergleicht das Passwort mit den in der DB
        /// existierenden Daten
        /// </summary>
        /// <param name="passwort">Passwort aus der Oberfläche in Klartext</param>
        /// <returns>true oder false</returns>
        public static bool PasswortVergleich(string passwort)
        {
            reisebueroEntities context = new reisebueroEntities();

            SHA512 hash = SHA512.Create();

            byte[] pw = hash.ComputeHash(Encoding.UTF8.GetBytes(passwort));

            using (context)
            {
                foreach (Benutzer b in context.AlleBenutzer)
                {
                    if (b.Passwort == pw)
                    {
                        return true;
                    }
                } 
            }

            return false;
        }

        /// <summary>
        /// Diese Methode vergleicht das Passwort und gibt es als ByteArray zurück
        /// </summary>
        /// <param name="passwort">Passwort in Klartext</param>
        /// <returns>ByteArray</returns>
        public static byte[] PasswortZuByteArray(string passwort)
        {
            //reisebueroEntities context = new reisebueroEntities();

            SHA512 hash = SHA512.Create();

            byte[] pw = hash.ComputeHash(Encoding.UTF8.GetBytes(passwort));

            //using (context)
            //{
            //    foreach (Benutzer b in context.AlleBenutzer)
            //    {
            //        if (pw.SequenceEqual(b.Passwort))
            //        {
            //            return pw;
            //        }
            //    }

            //}
            return pw;
        }

        /// <summary>
        /// Prüft ob ein Benutzer ein Mitarbeiter ist, wenn ja gibt die Methode true, ansonsten false zurück
        /// </summary>
        /// <param name="email">Die Email-Adresse des zu prüfenden Benutzers</param>
        /// <returns>true oder false</returns>
        public static bool BistDuMitarbeiter(string email)
        {
            Debug.WriteLine("Tools - Bist du Mitarbeiter");
            Debug.Indent();

            bool istMitarbeiter = false;

            reisebueroEntities context = new reisebueroEntities();

            try
            {
                using (context)
                {
                    //Gibt es einen Benutzer, bei der die Email Adresse dem Parameter entspricht
                    // UND das Feld Ist_Mitarbeiter TRUE ist
                    istMitarbeiter = context.AlleBenutzer.Any(x => x.Email == email && x.Ist_Mitarbeiter);

                    //foreach (var item in context.AlleBenutzer)
                    //{
                    //    if (item.Email == email && item.Ist_Mitarbeiter)
                    //    {
                    //        istMitarbeiter = true;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler beim Prüfen ob ein Benutzer ein Mitarbeiter ist");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return istMitarbeiter;
        }

        /// <summary>
        /// Prüft ob eine Email schon vergeben worden ist, wenn ja, dann gibt true zurück
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool EmailVorhanden(string email)
        {
            Debug.WriteLine("Tools - Email Vorhanden");
            Debug.Indent();

            bool emailVorhanden = false;

            reisebueroEntities context = new reisebueroEntities();

            try
            {
                using (context)
                {
                    emailVorhanden = context.AlleBenutzer.Any(x => x.Email == email);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler beim Prüfen ob Email vorhanden ist");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return emailVorhanden;
        }
    }
}
