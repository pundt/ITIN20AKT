using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Geschäftslogik inklusive Datenbankverbindung
/// </summary>
namespace BL_Reiseboerse_Graf
{
    /// <summary>
    /// Die Businesslogik der Buchungen, die auf die Datenbank zugreift
    /// </summary>
    public class BuchungsVerwaltung
    {
        /// <summary>
        /// Lädt alle Buchungen aus der Datenbank deren Anmeldefrist in der Vergangenheit liegt
        /// </summary>
        /// <param name="reisedatum_id">Reisedatum ID</param>
        /// <returns>Liste von Buchungen oder null bei einem Fehler</returns>
        public static List<Buchung> LadeAlleBuchungen()
        {
            Debug.WriteLine("Buchungsverwaltung - Lade alle Buchungen Reisedatum");
            Debug.Indent();
            List<Buchung> buchungsListe = null;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    buchungsListe = new List<Buchung>();
                    buchungsListe = context.AlleBuchungen.Include("Benutzer").Include("Reisedatum.Reise").
                        Where(x => x.Reisedatum.Anmeldefrist <= DateTime.Now).ToList();
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
        /// Lädt alle Buchungen zu allen Reisen an einem bestimmten Datum und einer bestimmten Person aus der Datenbank
        /// </summary>
        /// <param name="reisedatum_id">die ID des Reisedatums</param>
        /// <param name="benutzer_id">die ID des Benutzers</param>
        /// <returns>Liste von Buchungen oder null bei einem Fehler</returns>
        public static List<Buchung> LadeAlleBuchungenZuReiseDatumUndBenutzer(int reisedatum_id, int benutzer_id)
        {
            Debug.WriteLine("Buchungsverwaltung - Lade alle Buchungen Reisedatum und Benutzer");
            Debug.Indent();
            List<Buchung> buchungsListe = null;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    buchungsListe = new List<Buchung>();
                    buchungsListe = (from r in context.AlleBuchungen.Include("Reisedatum")
                                     where r.Reisedatum.ID == reisedatum_id && r.Benutzer.ID == benutzer_id && r.BuchungStorniert == null
                                     select r).ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden der Buchungen eines Reisedatums");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return buchungsListe;
        }

        /// <summary>
        /// Lädt alle Buchungen die storniert wurden aus der Datenbank
        /// </summary>
        /// <returns>Liste von Buchungen oder null bei einem Fehler</returns>
        public static List<Buchung> LadeAlleStorniertenBuchungen()
        {
            Debug.WriteLine("Buchungsverwaltung - Lade alle stornierten Buchungen");
            Debug.Indent();
            List<Buchung> buchungsListe = null;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    buchungsListe = new List<Buchung>();
                    buchungsListe = context.AlleBuchungen.Include("Benutzer").Include("Reisedatum.Reise")
                                            .Where(x => x.BuchungStorniert != null)
                                            .ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden der stornierten Buchungen");
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
            List<Buchung> buchungsListe = null;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    buchungsListe=new List<Buchung>();
                    buchungsListe = context.AlleBuchungen.Include("Reisedatum.Reise")
                                     .Where(x => x.Benutzer.ID == benutzer_id && x.BuchungStorniert == null).ToList();
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
        /// Storniert eine Buchung anhand einer Buchungs_ID
        /// </summary>
        /// <param name="benutzer_id">die ID der Buchung</param>
        /// <returns>true wenn Stornieren erfolgreich sonst false</returns>
        public static bool Stornieren(int buchung_id)
        {
            Debug.WriteLine("Buchungsverwaltung - Stornieren");
            Debug.Indent();
            bool erfolgreich = false;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    Buchung buchung = context.AlleBuchungen.Where(x => x.ID == buchung_id).FirstOrDefault();
                    context.AlleBuchungenStorniert.Add(new BuchungStorniert()
                    {
                        Buchung_ID = buchung.ID
                    });
                    context.SaveChanges();
                    erfolgreich = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Stornieren einer Buchung");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return erfolgreich;

        }



        /// <summary>
        /// Speichert eine neue Buchung in der Datenbank
        /// </summary>
        /// <param name="buchung">Die zu speichernde Buchung</param>
        /// <param name="email">die Email Adresse des aktuellen Benutzers</param>
        /// <param name="reisedatum_ID">ID des Reisedatums</param>
        /// <returns>die ID der neuen Buchung, bei einem Fehler -1</returns>
        public static int NeueBuchungSpeichern(Buchung buchung, int reisedatum_ID, string email)
        {
            Debug.WriteLine("Buchungsverwaltung - Neue Buchung Speichern");
            Debug.Indent();

            int neueID = -1;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    Benutzer benutzer = context.AlleBenutzer.Include("Adresse.Ort").Where(x => x.Email == email).FirstOrDefault();
                    Reisedatum datum = context.AlleReisedaten.Where(x => x.ID == reisedatum_ID).FirstOrDefault();
                    buchung.Benutzer = benutzer;
                    buchung.Reisedatum = datum;
                    context.AlleBuchungen.Add(buchung);
                    context.SaveChanges();
                    neueID = buchung.ID;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Speichern einer Buchung!");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
                Debug.Unindent();
                return neueID;
            }
        }

        /// <summary>
        /// Überprüft ob eine Buchung inkl. Zahlung abgeschlossen wurde und storniert diese falls unabgeschlossen
        /// </summary>
        /// <param name="id">die ID der Buchung (reisedurchfuehrung ID)</param>
        /// <returns>true wenn erfolgreich ansonsten false</returns>
        public static bool BuchungPruefen(int id)
        {
            Debug.WriteLine("Buchungsverwaltung - Buchung Prüfen");
            Debug.Indent();

            bool wurdeStorniert = false;
            using (var context = new reisebueroEntities())
            {
                try
                {

                    Buchung gesuchteBuchung = context.AlleBuchungen.Where(x => x.ID == id && x.Buchung_Zahlung != null).FirstOrDefault();

                    if (gesuchteBuchung == null)
                    {
                        context.AlleBuchungenStorniert.Add(new BuchungStorniert() { Buchung_ID = id });
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

        /// <summary>
        /// Entfernt die Stornierung einer Buchung anhand der ID
        /// </summary>
        /// <param name="id">die Id der Buchung</param>
        /// <returns>true wenn Aufheben der Stornierung erfolgreich sonst false</returns>
        public static bool StornierungAufheben(int id)
        {
            Debug.WriteLine("Buchungsverwaltung - Buchung Prüfen");
            Debug.Indent();
            bool erfolgreich = false;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    BuchungStorniert buchung = context.AlleBuchungenStorniert.Find(id);
                    context.AlleBuchungenStorniert.Remove(buchung);
                    context.SaveChanges();
                    erfolgreich = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Aufhebung einer Stornierung!");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return erfolgreich;

        }

        /// <summary>
        /// Prüft anhand der Buchungs_ID ob die Anmeldefrist schon vorbei ist
        /// (nicht mehr stornierbar für Kunden)
        /// </summary>
        /// <param name="buchung_id">die ID der Buchung</param>
        /// <returns>true wenn die Anmeldefrist noch nicht vorbei ist, sonst false</returns>
        public static bool Stornierbar(int buchung_id)
        {
            Debug.WriteLine("Buchungsverwaltung - Stornierbar");
            Debug.Indent();
            bool stornierbar = false;
            using (var context = new reisebueroEntities())
            {
                try
                {
                    Buchung buchung = context.AlleBuchungen.Find(buchung_id);
                    if (buchung.Reisedatum.Anmeldefrist >= DateTime.Now)
                    {
                        stornierbar = true;
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Prüfen ob Buchung stornierbar!");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return stornierbar;

        }
        /// <summary>
        /// Prüft ob es Buchungen zu einem Reisedatum gibt
        /// </summary>
        /// <param name="id">Reisedatum_id</param>
        /// <returns>true wenn Buchungen vorhanden sind, false falls nicht</returns>
        public static bool BuchungenVorhanden(int id)
        {
            Debug.WriteLine("Buchungsverwaltung - BuchungenVorhanden - ID");
            Debug.Indent();
            bool vorhanden = false;
            List<Buchung> Buchungen = new List<Buchung>();

            using (var context = new reisebueroEntities())
            {
                try
                {

                    Buchungen = context.AlleBuchungen.Where(x => x.Reisedatum.ID == id).ToList();

                    if (Buchungen.Count > 0)
                    {
                        vorhanden = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Prüfen ob es zu Reisedatum Buchungen gibt");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();


                }
                Debug.Unindent();
            }
            return vorhanden;



        }
    }
}





