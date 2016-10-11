using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Model zur Erfassung der Daten der Reisenden (Eingabemaske)
    /// </summary>
    public class BuchungenModel
    {
        public int ID { get; set; }
        /// <summary>
        /// Vorname des Reisenden (Pflichtefeld)
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        public string Vorname { get; set; }
        /// <summary>
        /// Nachname des Reisenden (Pflichtfeld)
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        public string Nachname { get; set; }
        /// <summary>
        /// Reisepassnummer (Validierung auf gültige Reisepassnummer, Pflichtfeld)
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [RegularExpression("^[A-Z]{1}[0-9]{7}$",ErrorMessage ="Geben Sie eine gültige Reisepassnummer ein")]
        [Display(Name ="Reisepassnummer")]
        public string ReisePassNummer { get; set; }
        /// <summary>
        /// ID der Zahlungsart (Überweisung, Kreditkarte, ...) Auswahl erfolgt über DropdownListe
        /// </summary>
        [Required]
        public int Zahlungsart_id { get; set; }
        /// <summary>
        /// ID des Reisedatums für die Zuordnung der Buchung zum richtigen Reisedatum 
        /// </summary>
        public int Reisedatum_ID { get; set; }
        /// <summary>
        /// Anzeige des vollständigen Namens, der aus Vor- und Nachname erzeugt wird
        /// </summary>
        [Display(Name="Vollständiger Name")]
        public string Name
        {
            get { return string.Format("{0} {1}",Vorname,Nachname); }
        }
        /// <summary>
        /// Prüfung ob Anmeldefrist noch nicht vorbei ist, solange ist Buchung noch stornierbar
        /// </summary>
        public bool Stornierbar { get; set; }



    }
}