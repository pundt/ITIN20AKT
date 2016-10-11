using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Hier wird das Datum erhoben für die Datumtabelle und ReiseAnzahl für ReiseAnzahltabelle
    /// </summary>
    public class ReisedurchfuehrenModel
    {
        /// <summary>
        /// zu der Reise_ID werden die dazugehörigen Daten gespeichert
        /// </summary>
        [Display(Name ="Reise ID")]
        public int Reise_id { get; set; }

        /// <summary>
        /// Bestimmt das Anfangsdatum
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",ApplyFormatInEditMode =true)]
        public DateTime StartDatum { get; set; }

        /// <summary>
        /// Bestimmt das Enddatum
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDatum { get; set; }

        /// <summary>
        /// Bestimmt die Anmeldefrist
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Anmeldefrist { get; set; }

        /// <summary>
        /// Bestimmt wieviele Reisen davon zur Verfügung stehen
        /// </summary>
        public int ReiseAnzahl { get; set; }

        /// <summary>
        /// wird benötigt, wenn weitere Daten vorhanden sind
        /// </summary>
        public bool WeitereReisenHinzufuegen { get; set; }
    }
}