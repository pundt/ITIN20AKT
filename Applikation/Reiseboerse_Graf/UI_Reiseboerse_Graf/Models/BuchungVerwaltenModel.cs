using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Benutzeroberfläche
/// </summary>
namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Model zur Ansicht der Buchung für den Mitarbeiter
    /// </summary>
    public class BuchungVerwaltenModel
    {
        /// <summary>
        /// ID der Buchung
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Benutzername des Benutzers, unter dessen Name die Buchung getätigt wurde
        /// </summary>
        [DisplayName("Benutzername")]
        public string BenutzerName { get; set; }
        /// <summary>
        /// Geburtsdatum des Reisenden
        /// </summary>
        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}")]
        public DateTime Geburtsdatum { get; set; }
        /// <summary>
        /// Vorname des Reisenden
        /// </summary>
        public string Vorname { get; set; }
        /// <summary>
        /// Nachname des Reisenden
        /// </summary>
        public string Nachname { get; set; }
        /// <summary>
        /// Passnummer des Reisenden
        /// </summary>
        public string Passnummer { get; set; }
        /// <summary>
        /// Titel der Reise
        /// </summary>
        public string Reisetitel { get; set; }
        /// <summary>
        /// Reisedatum_ID (Reise zu bestimmten Datum)
        /// </summary>
        public int Reisedatum_ID { get; set; }
        /// <summary>
        /// Startdatum der Reise
        /// </summary>
        public DateTime Startdatum { get; set; }
        /// <summary>
        /// Enddatum der Reise
        /// </summary>
        public DateTime Enddatum { get; set; }
        /// <summary>
        /// Anzeige des Reisezeitraums als ShortDateString
        /// </summary>
        public string Datum
        {
            get { return string.Format("{0} - {1}",Startdatum.ToShortDateString(), Enddatum.ToShortDateString()); }
        }

    }
}