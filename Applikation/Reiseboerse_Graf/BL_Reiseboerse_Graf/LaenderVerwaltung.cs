using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Reiseboerse_Graf
{
    /// <summary>
    /// Die Businesslogik der Datenbanktabellen Länder, Orte, Adressen,Unterkünfte
    /// </summary>
    public class LaenderVerwaltung
    {
        /// <summary>
        /// Liest alle Orte aus der Datenbank aus
        /// </summary>
        /// <returns>Liste aller Orte</returns>
        public static List<Ort> AlleOrte()
        {
            Debug.WriteLine("LaenderVerwaltung - Lade alle Orte");
            Debug.Indent();
            List<Ort> alleOrte = new List<Ort>();
            using (reisebueroEntities context = new reisebueroEntities())
            {
                try
                {
                    alleOrte = context.AlleOrte.ToList();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden aller Orte aus der DB");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }

            }
            Debug.Unindent();
            return alleOrte;


        }
        /// <summary>
        /// Liest alle Unterkünfte aus der Datenbank aus
        /// </summary>
        /// <returns>Liste aller Unterkünfte</returns>
        public static List<Unterkunft> AlleUnterkuenfte()
        {
            Debug.WriteLine("Laenderverwaltung - Lade alle Unterkuenfte");
            Debug.Indent();
            List<Unterkunft> alleUnterkuenfte = new List<Unterkunft>();
            using (reisebueroEntities context = new reisebueroEntities())
            {
                try
                {
                    alleUnterkuenfte = context.AlleUnterkuenfte.Include("Verpflegung").ToList();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden aller Unterkuenfte");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            return alleUnterkuenfte;



        }
        /// <summary>
        /// Liefert alle Verpflegegungen aus der Datenbank
        /// </summary>
        /// <returns> Liste aller Verpflegungen</returns>
        public static List<Verpflegung> alleVerpflegung()
        {
            Debug.WriteLine("LaenderVerwaltung - Lade alle Verpflegungen");
            Debug.Indent();
            List<Verpflegung> alleVerpflegung = new List<Verpflegung>();

            using (reisebueroEntities context = new reisebueroEntities())
            {
                try
                {
                    alleVerpflegung = context.AlleVerpflegungen.ToList();
                    Debug.WriteLine("Speichern erfolgreich beim Laden aller Verpflegungen");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden aller Verpflegungen");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }

            }
            Debug.Unindent();
            return alleVerpflegung;
        }

        /// <summary>
        /// Speichert einen neuen Ort in der Datenbank
        /// </summary>
        /// <param name="neuerort">die Bezeichnung des Ortes</param>
        /// <param name="land_id">die ID des Landes</param>
        /// <returns>bei Erfolg die ID des neuen Orts in der Datenbank bei Fehler -1</returns>
        public static int SpeicherNeuenOrt(string neuerort, int land_id)
        {
            int index = -1;

            Debug.WriteLine("LaenderVerwaltung - SpeicherNeuenOrt");
            Debug.Indent();

            using (reisebueroEntities context = new reisebueroEntities())
            {
                try
                {
                    Land neuesLand = context.AlleLaender.Where(x => x.ID == land_id).FirstOrDefault();
                    Ort neuerOrt = new Ort();
                    neuerOrt.Bezeichnung = neuerort;
                    neuerOrt.Land = neuesLand;
                    context.AlleOrte.Add(neuerOrt);
                    context.SaveChanges();
                    index = neuerOrt.ID;

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim SpeichernNeuenOrt");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                    Debug.Unindent();
                }
            }
            return index;

        }

        /// <summary>
        /// Speichert ein neues Land in der Datenbank
        /// </summary>
        /// <param name="neuesLand">die Bezeichnung des neuen Landes</param>
        /// <returns>bei Erfolg die ID des neuen Landes</returns>
        public static int SpeicherNeuesLand(string neuesLand)
        {
            int index = 0;

            Debug.WriteLine("Länderverwaltung - SpeicherNeuesLand");
            Debug.Indent();

            Land land = new Land();
            if (neuesLand != null)
            {
                land.Bezeichnung = neuesLand;
                
                using (reisebueroEntities context = new reisebueroEntities())
                {
                    try
                    {
                        context.AlleLaender.Add(land);
                        context.SaveChanges();
                        index = land.ID;                        
                        Debug.WriteLine("Speichern erfolgreich");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Fehler beim SpeicherNeuesLand");
                        Debug.WriteLine(ex.Message);
                        Debugger.Break();
                        Debug.Unindent();
                    }
                }
            }

            return land.ID;
        }

        /// <summary>
        /// Speichert eine neue Unterkunft in der Datenbank
        /// </summary>
        /// <param name="neueUnterkunft">das zu speichernde Unterkunft Objekt</param>
        /// <returns>bei Erfolg die ID der neuen Unterkunft bei Fehler -1</returns>
        public static int SpeichereNeueUnterkunft(Unterkunft neueUnterkunft)
        {
            int index = -1;
            Debug.WriteLine("Länderverwaltung - SpeichereNeueUnterkunft");
            Debug.Indent();

            if (neueUnterkunft != null)
            {
                using (reisebueroEntities context = new reisebueroEntities())
                {
                    try
                    {
                        context.AlleUnterkuenfte.Add(neueUnterkunft);
                        context.SaveChanges();
                        Debug.WriteLine("Speichern erfolgreich");
                        return neueUnterkunft.ID;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Fehler beim SpeicherNeueUnterkunft");
                        Debug.WriteLine(ex.Message);
                        Debugger.Break();
                        Debug.Unindent();
                    }
                }
            }
            return index;
        }

        /// <summary>
        /// Sucht einen Ort aus der Datenbank anhand einer ID
        /// </summary>
        /// <param name="ort_id">die ID des zu suchenden Ortes</param>
        /// <returns>bei Erfolg das Ort Objekt bei Fehler null</returns>
        public static Ort SucheOrt(int ort_id)
        {
            Ort gesuchterOrt = null;
            using (reisebueroEntities context = new reisebueroEntities())
            {
                try
                {   
                    gesuchterOrt = context.AlleOrte.Include("Land").Where(x => x.ID == ort_id).FirstOrDefault();
                    Debug.WriteLine("Ortsuche erfolgreich");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Ortsuchen");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                    Debug.Unindent();
                }
            }
            return gesuchterOrt;
        }

        /// <summary>
        /// Sucht ein Land anhand einer ID aus der Datenbank
        /// </summary>
        /// <param name="land_id">die ID des zu suchenden Landes</param>
        /// <returns>bei Erfolg das Land Objekt bei Fehler null</returns>
        public static Land SucheLand(int land_id)
        {
            Land gesuchtesLand = null;
            using (reisebueroEntities context = new reisebueroEntities())
            {
                try
                {
                    gesuchtesLand = context.AlleLaender.Where(x => x.ID == land_id).FirstOrDefault();
                    Debug.WriteLine("Landsuche erfolgreich");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler bei Landsuche");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                    Debug.Unindent();
                }
            }
            return gesuchtesLand;
        }

        /// <summary>
        /// Sucht ein Adressobjekt anhand von Adressdaten aus der Datenbank
        /// </summary>
        /// <param name="adresse">die Adressdaten des zu suchenden Adressobjektes</param>
        /// <returns>bei Erfolg die gesuchte Adresse bei Fehler null</returns>
        public static Adresse SucheAdresse(string adresse)
        {
            Adresse gesuchteAdresse = null;
            using (reisebueroEntities context = new reisebueroEntities())
            {
                try
                {
                    gesuchteAdresse = context.AlleAdressen.Where(x => x.Adressdaten.Contains(adresse)).FirstOrDefault();
                    Debug.WriteLine("Adresssuche erfolgreich");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler bei Landsuche");
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                    Debug.Unindent();
                }
            }
            return gesuchteAdresse;
        }

    }
}
