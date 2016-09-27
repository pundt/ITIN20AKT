using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class BuchungVerwaltenModel
    {
        public int ID { get; set; }
        [DisplayName("Benutzername")]
        public string BenutzerName { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}")]
        public DateTime Geburtsdatum { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Passnummer { get; set; }
        public string Reisetitel { get; set; }
        public int Reisedatum_ID { get; set; }
        public DateTime Startdatum { get; set; }
        public DateTime Enddatum { get; set; }
        public string Datum
        {
            get { return string.Format("{0} - {1}",Startdatum.ToShortDateString(), Enddatum.ToShortDateString()); }
        }

    }
}