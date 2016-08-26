using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class BuchungAnzeigenModel
    {
        public string Reisetitel { get; set; }
        public DateTime Startdatum { get; set; }
        public DateTime Enddatum { get; set; }
    }
}