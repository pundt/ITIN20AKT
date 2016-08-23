using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Reiseboerse_Graf
{
    public class BuchungsVerwaltung
    {
        /// <summary>
        /// Lädt alle Buchungen zu einer bestimmten Reise aus der Datenbank
        /// </summary>
        /// <returns>Liste von Buchungen oder null bei einem Fehler</returns>
        public static List<Buchung> LadeAlleBuchungen(int reise_id)
        {
            Debug.WriteLine("Buchungsverwaltung - Lade alle Buchungen Reise");
            Debug.Indent();
            List<Buchung> buchungsListe = new List<Buchung>();
            using (var context = new reisebueroEntities())
            {
                try
                {
                    buchungsListe = context.AlleBuchungen.
                        Where(x => x.Reisedurchfuehrung.Reisedatum.Reise.ID == reise_id).ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden der Buchungen einer Reise");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return buchungsListe;
        }

        /// <summary>
        /// Lädt alle Buchungen die nicht storniert wurden aus der Datenbank
        /// </summary>
        /// <returns>Liste von Buchungen oder null bei einem Fehler</returns>
        public static List<Buchung> LadeAlleAktivenBuchungen()
        {
            Debug.WriteLine("Buchungsverwaltung - Lade alle Buchungen");
            Debug.Indent();
            List<Buchung> buchungsListe = new List<Buchung>();
            using (var context=new reisebueroEntities())
            {
                try
                {
                    buchungsListe=context.AlleBuchungen.Where(x => x.BuchungStorniert == null).ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden der Buchungen");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return buchungsListe;
        }
    }
}
