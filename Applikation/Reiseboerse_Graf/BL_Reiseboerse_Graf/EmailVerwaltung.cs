using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;

/// <summary>
/// Geschäftslogik inklusive Datenbankverbindung
/// </summary>
namespace BL_Reiseboerse_Graf
{
    /// <summary>
    /// Verwaltung des automatisierten Mailversands bei Bestätigungen
    /// </summary>
    public class EmailVerwaltung
    {
        /// <summary>
        /// Buchung wird per Email bestätigt, gibt true zurück, wenn die Email versendet worden ist
        /// </summary>
        /// <param name="eMail">die Email-Adresse des buchenden Kunden</param>
        /// <param name="text">der Text in der Mail</param>
        /// <returns>true oder false</returns>
        public static bool BuchungBestaetigen(string eMail, string text)
        {
            Debug.WriteLine("EmailVerwaltung - Buchung bestätigen");
            Debug.Indent();

            bool gesendet = false;

            try
            {
                MailMessage msg = new MailMessage();
                MailAddress firmenAdresse = new MailAddress("muster@itfox.at", "office@reisebüro.at");

                msg.From = firmenAdresse;
                msg.IsBodyHtml = true;
                msg.Body = text;
                msg.Subject = "Buchungsbestätigung für " + eMail;
                msg.To.Add(new MailAddress(eMail));

                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("testuser", "123user!");
                client.Send(msg);

                gesendet = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler beim Email-Versand der Buchungsbestätigung!");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return gesendet;
        }
    }
}
