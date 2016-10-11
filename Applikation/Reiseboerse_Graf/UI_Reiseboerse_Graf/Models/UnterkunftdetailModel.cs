using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Benutzeroberfläche
/// </summary>
namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Model für Unterkunftsanzeige/Erstellung
    /// </summary>
    public class UnterkunftdetailModel
    {
        /// <summary>
        /// Id der Unterkunft
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Bezeichnung der Unterkunft
        /// </summary>
        public string Bezeichnung { get; set; }
        /// <summary>
        /// Verpflegungs_id/ für eroieren einer Verpflegungsart
        /// </summary>
        public int Verpflegung_ID { get; set; }
        /// <summary>
        /// VerpflegungsModel/ für jede Unterkunft muss eine Verpflegung vorhanden sein
        /// </summary>
        public VerpflegungModel Verpflegung { get; set; }
        /// <summary>
        /// Beschreibung der Unterkunft
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Beschreibung { get; set; }
        /// <summary>
        /// Kategorie der Unterkunft / Bewertunsschema
        /// </summary>
        public int Kategorie { get; set; }

        /// <summary>
        /// es wird geprüft ob eine neue Unterkunft angelegt wurde
        /// </summary>
        /// <param name="unterkunft"></param>
        /// <returns></returns>
        public static bool PruefeUnterkunft(UnterkunftdetailModel unterkunft)
        {
            if (unterkunft.Beschreibung != null && unterkunft.Bezeichnung!= null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}