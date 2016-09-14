using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class BuchungAnzeigenModel
    {
        public int Benutzer_ID { get; set; }
        public int ReiseID { get; set; }
        public string Reisetitel { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}")]
        public DateTime Startdatum { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Enddatum { get; set; }
        public int? Bewertung { get; set; }
        public int Reisedatum_ID { get; set; }
    }
}