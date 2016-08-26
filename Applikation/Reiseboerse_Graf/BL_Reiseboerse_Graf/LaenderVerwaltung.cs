using System;
using System.Collections.Generic;
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
            reisebueroEntities context = new reisebueroEntities();

            List<Ort> alleOrte = context.AlleOrte.ToList();

            return alleOrte;
        }
        public static List<Unterkunft> AlleUnterkuenfte()
        {
            reisebueroEntities context = new reisebueroEntities();

            List<Unterkunft> alleUnterkuenfte = context.AlleUnterkuenfte.ToList();

            return alleUnterkuenfte;
        }
        public static List<Verpflegung> alleVerpflegung()
        {
            reisebueroEntities context = new reisebueroEntities();

            List < Verpflegung > alleVerpflegung = context.AlleVerpflegungen.ToList();

            return alleVerpflegung;
        }
    }
}
