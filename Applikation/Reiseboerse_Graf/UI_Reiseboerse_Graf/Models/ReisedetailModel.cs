using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// ReisedetailModel beinhaltet alle Detailinformationen einer Reise
    /// erbt von ReiseModel
    /// </summary>
    public class ReisedetailModel:ReiseModel
    {
        /// <summary>
        /// Beschreibungstext der Reise
        /// muss zwischen 4 und 30 Zeichen lang sein
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Mind 4 Zeichen")]
        public string Beschreibung { get; set; }

        /// <summary>
        /// Einzelpreis pro Erwachsenem
        /// </summary>
        [Display(Name ="Preis Erwachsene")]
        public decimal Preis_Erwachsene { get; set; }

        /// <summary>
        /// Einzelpreis pro Kind
        /// </summary>
        [Display(Name = "Preis Kinder")]
        public decimal Preis_Kind { get; set; }

        /// <summary>
        /// ID der Unterkunft (zur Zuordnung der Unterkunft zur Reise)
        /// Detailinformationen der Unterkunft werden im Unterkunftdetailmodel gespeichert
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        public int Unterkunft_ID { get; set; }
    }
}