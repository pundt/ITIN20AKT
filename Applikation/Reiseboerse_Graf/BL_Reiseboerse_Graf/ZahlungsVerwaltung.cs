using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BL_Reiseboerse_Graf
{
    /// <summary>
    /// Verwaltung der Zahlungsvorgänge
    /// </summary>
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
        /// <param name="zahlungsart_id">die ID der ausgewählten Zahlungsart</param>
        /// <returns>die gefundene ID oder 0 wenn Zahlung NULL ist</returns>
        public static int NeueZahlungSpeichern(Zahlung neueZahlung, int zahlungsart_id)
        {
            Debug.WriteLine("ZahlungsVerwaltung - Neue Zahlung Speichern");
            Debug.Indent();

            int neueID = -1;

            using (var context = new reisebueroEntities())
            {
                try
                {
                    neueZahlung.Zahlungsart = context.AlleZahlungsarten.Where(x => x.ID == zahlungsart_id).FirstOrDefault();
                    context.AlleZahlungen.Add(neueZahlung);
                    context.SaveChanges();
                    neueID = neueZahlung.ID;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Speichern der Zahlung!");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return neueID;
        }

        /// <summary>
        /// Zuordnung einer Zahlung zu den Buchungen (Eintrag in die Zwischentabelle Zahlung_Buchung) 
        /// </summary>
        /// <param name="buchungIDs">Liste von Buchungs IDs</param>
        /// <param name="zahlung_id">die ID der Zahlung</param>
        /// <returns>die Anzahl der betroffenen Zeilen</returns>
        public static int ZuordnungZahlungBuchung(List<int> buchungIDs, int zahlung_id)
        {
            Debug.WriteLine("Zahlungsverwaltung - Zuordnung Zahlung_Buchung");
            Debug.Indent();
            int zeilen = 0;
            using (var context = new reisebueroEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        foreach (var id in buchungIDs)
                        {
                            Buchung_Zahlung bz = new Buchung_Zahlung()
                            {
                                Buchung_ID = id
                            };
                            bz.Zahlung = context.AlleZahlungen.Where(x => x.ID == zahlung_id).FirstOrDefault();
                            context.AlleBuchung_Zahlungen.Add(bz);                            
                        }
                        zeilen=context.SaveChanges();
                        transaction.Complete();

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Fehler beim Zuordnen der Zahlung_Buchung");
                        Debug.WriteLine(ex.Message);
                        Debugger.Break();
                    }
                    Debug.Unindent();
                }
                return zeilen;
            }
        }

        /// <summary>
        /// Püft eine Kreditkartennummer auf ihre Gültigkeit mittels Luhn-Algorithmus
        /// </summary>
        /// <param name="nummer">die zu prüfende Kartennummer</param>
        /// <returns>true für gültig oder false für nicht gültig</returns>
        public static bool PruefeLuhn(string nummer)
        {
            Debug.WriteLine("ZahlungsVerwaltung - Pruefe Luhn");
            Debug.Indent();

            int sum = 0;

            try
            {
                int len = nummer.Length;
                for (int i = 0; i < len; i++)
                {
                    int add = (nummer[i] - '0') * (2 - (i + len) % 2);
                    add -= add > 9 ? 9 : 0;
                    sum += add;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler bei Luhn-Prüfung");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return sum % 10 == 0;
        }
    }
}
