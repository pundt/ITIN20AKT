using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Model zur Anzeige von Buchungen in der Buchungshistory für den Benutzer
    /// </summary>
    public class BuchungAnzeigenModel
    {
        /// <summary>
        /// ID des Benutzers für die Zuordnung der Buchungen
        /// </summary>
        public int Benutzer_ID { get; set; }
        /// <summary>
        /// ID der Reise
        /// </summary>
        public int ReiseID { get; set; }
        /// <summary>
        /// Titel der Reise
        /// </summary>
        public string Reisetitel { get; set; }
        /// <summary>
        /// Startdatum der Reise (formatiert als ShortDateString)
        /// </summary>
        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}")]
        public DateTime Startdatum { get; set; }
        /// <summary>
        /// Enddatum der Reise (formatiert als ShortDateString)
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Enddatum { get; set; }
        /// <summary>
        /// Bewertung der Reise (falls noch keine Bewertung vorhanden null)
        /// </summary>
        public int? Bewertung { get; set; }
        /// <summary>
        /// ID des Reisedatums 
        /// </summary>
        public int Reisedatum_ID { get; set; }
    }
}