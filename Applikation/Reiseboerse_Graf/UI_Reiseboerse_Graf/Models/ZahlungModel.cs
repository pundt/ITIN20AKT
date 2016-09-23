using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI_Reiseboerse_Graf.Models
{
    public class ZahlungModel
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        [Remote("LuhnUndIBANPruefung", "Validierung", ErrorMessage = "Geben Sie eine gültige IBAN- oder Kreditkartennummer ein")]
        public string Nummer { get; set; }
        [Display(Name ="Zahlungsart")]
        public int Zahlungsart_ID { get; set; }
        public List<ZahlungsartModel> Zahlungsarten { get; set; }
        //public int Reisedatum_ID { get; set; }
    }
}