using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class BuchungGesamtModel
    {
        public List<BuchungErwachsenModel> BuchungErwachsen { get; set; }
        public List<BuchungKindModel> BuchungKind { get; set; }
        public decimal Gesamtpreis { get; set; }
        public string Reisetitel { get; set; }
        public DateTime Startdatum { get; set; }
        public DateTime Enddatum { get; set; }

    }
}