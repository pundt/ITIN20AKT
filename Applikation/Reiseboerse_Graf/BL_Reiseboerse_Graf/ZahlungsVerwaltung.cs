using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
        public static int NeueZahlungSpeichern(Zahlung neueZahlung, int zahlungsart_id)
        {
            Debug.WriteLine("ZahlungsVerwaltung - Neue Zahlung Speichern");
            Debug.Indent();

            int neueID = -1;

            using (var context = new reisebueroEntities())
            {
                try
                {
                    Zahlungsart art=context.AlleZahlungsarten.Where(x => x.ID == zahlungsart_id).FirstOrDefault();
                    neueZahlung.Zahlungsart = art;
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

        public static void ZuordnungZahlungBuchung(List<int> buchungIDs, int zahlungID)
        {
            Debug.WriteLine("Zahlungsverwaltung - Zuordnung Zahlung_Buchung");
            Debug.Indent();

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
                            bz.Zahlung.ID = zahlungID;
                            context.AlleBuchung_Zahlungen.Add(bz);
                        }
                        context.SaveChanges();
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
            }
        }
    }
}
