using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class ReisedurchfuehrenModel
    {
        public int? Reise_id { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EndDatum { get; set; }
        public DateTime Anmeldefrist { get; set; }
        public int ReiseAnzahl { get; set; }
        public bool WeitereReisenHinzufuegen { get; set; }
    }
}