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
            using (var context = new reisebueroEntities())
            {
                try
                {
                    buchungsListe = context.AlleBuchungen.Where(x => x.BuchungStorniert == null).ToList();
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

        /// <summary>
        /// Lädt alle Buchungen eines Benutzers
        /// </summary>
        /// <param name="benutzer_id">ID des Benutzers</param>
        /// <returns>Liste von Buchungen oder Null bei Fehler</returns>
        public static List<Buchung> LadeAlleEinzelBuchungenBenutzer(int benutzer_id)
        {
            Debug.WriteLine("Buchungsverwaltung - Lade alle Buchungen Benutzer");
            Debug.Indent();
            List<Buchung> buchungsListe = new List<Buchung>();
            using (var context = new reisebueroEntities())
            {
                try
                {
                    buchungsListe = context.AlleBuchungen.Include("Reisedurchfuehrung.Reisedatum.Reise")
                                     .Where(x => x.Benutzer.ID == benutzer_id).ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden der Buchungen eines Benutzers");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return buchungsListe;
        }

        /// <summary>
        /// Lädt alle Buchung gebündelt pro Reise eines Benutzers
        /// </summary>
        /// <param name="benutzer_id">ID des Benutzers</param>
        /// <returns>Liste von Buchungen oder Null bei Fehler</returns>
        public static List<Buchung> LadeAlleReiseBuchungenBenutzer(int benutzer_id)
        {
            Debug.WriteLine("Buchungsverwaltung - Lade alle ReiseBuchungen Benutzer");
            Debug.Indent();
            List<Buchung> buchungsListe = new List<Buchung>();
            using (var context = new reisebueroEntities())
            {
                try
                {
                    buchungsListe = context.AlleBuchungen.Include("Reisedurchfuehrung.Reisedatum").Include("Reisedurchfuehrung.Reisedatum.Reise")
                                     .Where(x => x.Benutzer.ID == benutzer_id).ToList();
                    buchungsListe.GroupBy(x => x.Reisedurchfuehrung.Reisedatum.Startdatum).ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden der Buchungen pro Reise eines Benutzers");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return buchungsListe;
        }




        /// <summary>
        /// Ermittelt die aktuelle Reisedurchfuehrungs_ID zu einer Reise, die gebucht werden kann
        /// </summary>
        /// <param name="reise_id">ID der Reise</param>
        /// <param name="startdatum">das Startdatum der aktuellen Reise</param>
        /// <returns>die aktuelle ID</returns>
        public static int Ermittle_aktID(int reise_id, DateTime startdatum)
        {
            Debug.WriteLine("Buchungsverwaltung - Ermittle aktuelle Reisedurchfuehrung_ID");
            Debug.Indent();
            int aktId = 0;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    aktId = (from r in context.AlleReisedurchfuehrungen
                             where r.Reisedatum.Reise.ID == reise_id && r.Buchung == null &&
                             r.Reisedatum.Startdatum == startdatum
                             select r.ID).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden der Buchungen eines Benutzers");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return aktId;
        }

        /// <summary>
        /// Neue Buchung in der Datenbank speichern
        /// </summary>
        /// <param name="buchung">Die zu speichernde Buchung</param>
        /// <returns>true wenn erfolgreich ansonsten false</returns>
        public static bool NeueBuchungSpeichern(Buchung buchung, string email)
        {
            Debug.WriteLine("Buchungsverwaltung - Neue Buchung Speichern");
            Debug.Indent();

            bool erfolgreich = false;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    Benutzer benutzer = context.AlleBenutzer.Include("Adresse.Ort").Where(x => x.Email == email).FirstOrDefault();
                    buchung.Benutzer = benutzer;
                    context.AlleBuchungen.Add(buchung);
                    context.SaveChanges();
                    erfolgreich = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Speichern einer Buchung!");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }


                Debug.Unindent();
                return erfolgreich;
            }
        }

        /// <summary>
        /// Überprüft ob eine Buchung inkl. Zahlung abgeschlossen wurde und storniert diese falss unabgeschlossen
        /// </summary>
        /// <param name="reisedurchfuehrungID">die ID der Buchung (reisedurchfuehrung ID)</param>
        /// <returns>true wenn erfolgreich ansonsten false</returns>
        public static bool BuchungPruefen(int reisedurchfuehrungID)
        {
            Debug.WriteLine("Buchungsverwaltung - Buchung Prüfen");
            Debug.Indent();

            bool wurdeStorniert = false;
            using (var context = new reisebueroEntities())
            {
                try
                {

                    Buchung gesuchteBuchung = context.AlleBuchungen.Where(x => x.Reisedurchfuehrung_ID == reisedurchfuehrungID && x.Buchung_Zahlung != null).FirstOrDefault();

                    if (gesuchteBuchung == null)
                    {
                        context.AlleBuchungenStorniert.Add(new BuchungStorniert() { Reisedurchfuehrung_ID = gesuchteBuchung.Reisedurchfuehrung_ID });
                        wurdeStorniert = true;
                    }
                }

                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Prüfen einer Buchung!");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
                Debug.Unindent();
                return wurdeStorniert;
            }
        }
    }
