using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class BuchungGesamtModel
    {
        public int ReiseID { get; set; }
        public List<BuchungErwachsenModel> BuchungErwachsen { get; set; }
        public List<BuchungKindModel> BuchungKind { get; set; }
        public decimal Gesamtpreis { get; set; }
        public string Reisetitel { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}")]
        public DateTime Startdatum { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Enddatum { get; set; }
        public List<int> BuchungIDs { get; set; }

    }
}