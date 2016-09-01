using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class ZahlungModel
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Nummer { get; set; }
        public int Zahlungsart_ID { get; set; }
        public List<ZahlungsartModel> Zahlungsarten { get; set; }
        public int Reisedatum_ID { get; set; }
    }
}