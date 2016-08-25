using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class BuchungenModel
    {
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        public string Vorname { get; set; }
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        public string Nachname { get; set; }
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        //[RegularExpression("^[A-Z0-9{5,10}]$",ErrorMessage ="Geben Sie eine gültige Reisepassnummer ein")]
        [Display(Name ="Reisepassnummer")]
        public string ReisePassNummer { get; set; }
        [Required]
        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}")]
        public DateTime Geburtsdatum { get; set; }
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        public int Zahlungsart_id { get; set; }
        public int Reisedurchfuehrung_ID { get; set; }



    }
}