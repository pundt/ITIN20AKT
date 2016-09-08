using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Reiseboerse_Graf
{
    public class BewertungVerwaltung
    {
        /// <summary>
        /// Speichert die Bewertung zu einer Reise
        /// </summary>
        /// <param name="reise_id">ID der Reise</param>
        /// <param name="bewertung">Bewertung von 1 bis 5</param>
        /// <returns>1 wenn erfolgreich wenn nicht erfolgreich -1</returns>
        public static int BewertungSpeichern(int reise_id, int bewertung)
        {
            Debug.WriteLine("ReiseVerwaltung - BewertungSpeichern");
            Debug.Indent();

            int erfolgreich = -1;

            using (var context = new reisebueroEntities())
            {
                try
                {
                    Reise gesuchteReise = context.AlleReisen.Where(x => x.ID == reise_id).FirstOrDefault();
                    Bewertung neueBewertung = new Bewertung()
                    {
                        Reise = gesuchteReise,
                        Wertung = bewertung
                    };
                    context.AlleBewertungen.Add(neueBewertung);
                    erfolgreich = context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Speichern der Bewertung!");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }

            Debug.Unindent();
            return erfolgreich;
        }

        /// <summary>
        /// Lädt alle Bewertungen und berechnet den Durchschnitt
        /// </summary>
        /// <param name="reise_id">ID der Reise</param>
        /// <returns>den Durchschnitt aller Bewertungen</returns>
        public static int LadeBewertungReise(int reise_id)
        {
            Debug.WriteLine("ReiseVerwaltung - LadeBewertungReise");
            Debug.Indent();

            int bewertung = 0;

            using (var context = new reisebueroEntities())
            {
                try
                {
                    List<Bewertung> liste = context.AlleBewertungen.Where(x => x.Reise.ID == reise_id).ToList();
                    if (liste.Count >= 1)
                    {
                        double avg = liste.Average(x => x.Wertung);
                        bewertung = (int)avg;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden der Bewertung einer Reise!");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return bewertung;
        }


    }
}
