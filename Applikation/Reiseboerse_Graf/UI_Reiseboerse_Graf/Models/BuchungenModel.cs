using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Model zur Erfassung der Daten der Reisenden
    /// </summary>
    public class BuchungenModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        public string Vorname { get; set; }
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        public string Nachname { get; set; }
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [RegularExpression("^[A-Z]{1}[0-9]{7}$",ErrorMessage ="Geben Sie eine gültige Reisepassnummer ein")]
        [Display(Name ="Reisepassnummer")]
        public string ReisePassNummer { get; set; }       
        [Required]
        public int Zahlungsart_id { get; set; }
        public int Reisedatum_ID { get; set; }
        [Display(Name="Vollständiger Name")]
        public string Name
        {
            get { return string.Format("{0} {1}",Vorname,Nachname); }
        }
        public bool Stornierbar { get; set; }



    }
}