using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class BuchungAnzahlModel
    {
        public int Reise_ID { get; set; }
        public DateTime Beginndatum { get; set; }
        public int Anzahl_Erwachsene { get; set; }
        public int Anzahl_Kinder { get; set; }
        public decimal Preis_Erwachsene { get; set; }
        public decimal Preis_Kind { get; set; }
        private int anzahl;

        public int Anzahl
        {
            get { return Anzahl_Erwachsene+Anzahl_Kinder; }
        }

    }
}