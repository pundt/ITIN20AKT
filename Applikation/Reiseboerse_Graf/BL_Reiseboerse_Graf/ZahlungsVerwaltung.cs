using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Reiseboerse_Graf
{
    public class ZahlungsVerwaltung
    {
        /// <summary>
        /// Liest alle Zahlungsarten aus der DB aus
        /// </summary>
        /// <returns>eine Liste von Zahlungsarten falls ein Fehler auftritt NULL</returns>
        public static List<Zahlungsart> LadeAlleZahlungsArten()
        {
            Debug.WriteLine("ZahlungsVerwaltung - Lade alle Zahlungsarten");
            Debug.Indent();
            List<Zahlungsart> liste = null;
            try
            {
                using (var context = new reisebueroEntities())
                {
                    liste = context.AlleZahlungsarten.ToList();
                    Debug.WriteLine("{0} Zahlungsarten geladen", liste.Count);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler beim Laden aller Zahlungsarten");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }
            Debug.Unindent();
            return liste;
        }

        /// <summary>
        /// Speichert die neue Zahlung in der DB und liefert die ID dieser zurück
        /// </summary>
        /// <param name="neueZahlung">die zu speichernde Zahlung</param>
        /// <returns>die gefundene ID oder 0 wenn Zahlung NULL ist</returns>
        public static int NeueZahlungSpeichern(Zahlung neueZahlung)
        {
            Debug.WriteLine("ZahlungsVerwaltung - Neue Zahlung Speichern");
            Debug.Indent();

            int neueID = 0;

            try
            {
                using (var context = new reisebueroEntities())
                {
                    context.AlleZahlungen.Add(neueZahlung);
                    context.SaveChanges();

                    Zahlung gefunden = context.AlleZahlungen.Where(x => x.Nachname == neueZahlung.Nachname && x.Vorname == neueZahlung.Vorname && x.Nummer == neueZahlung.Nummer).FirstOrDefault();

                    if (gefunden != null)
                    {
                        neueID = gefunden.ID;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler beim Speichern der Zahlung!");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }
            Debug.Unindent();
            return neueID;
        }

        public static void ZuordnungZahlungBuchung(int buchungsdatum, int zahlungID)
        {
            Debug.WriteLine("Zahlungsverwaltung - Zuordnung Zahlung_Buchung");
            Debug.Indent();

            try
            {
                //using (var context = new reisebueroEntities())
                //{
                //    foreach (var id in reisedurchfuehrungIDs)
                //    {
                //        Buchung_Zahlung bz = new Buchung_Zahlung()
                //        {
                //            Reisedurchfuehrung_ID = id
                //        };
                //    bz.Zahlung.ID = zahlungID;
                //    context.AlleBuchung_Zahlungen.Add(bz);
                //}
                //context.SaveChanges();
                //Debug.Unindent();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler beim Zuordnen der Zahlung_Buchung");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }
        }
    }
}
