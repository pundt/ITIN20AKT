using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Reiseboerse_Graf
{
    public class LaenderVerwaltung
    {
        /// <summary>
        /// Liefert alle Orte aus der DB
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
        /// Liefert alle Unterkuenfte aus der DB
        /// </summary>
        /// <returns>Liste aller Unterkuenfte</returns>
        public static List<Unterkunft> AlleUnterkuenfte()
        {
            Debug.WriteLine("Laenderverwaltung - Lade alle Unterkuenfte");
            Debug.Indent();
            List<Unterkunft> alleUnterkuenfte = new List<Unterkunft>();
            using (reisebueroEntities context = new reisebueroEntities())
            {
                try
                {
                    alleUnterkuenfte = context.AlleUnterkuenfte.ToList();

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
        /// Liefert alle Verpflegegungen aus der DB
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
    }
}
