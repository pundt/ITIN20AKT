﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class BuchungHinzufuegenModel
    {
        public List<BuchungenModel> Buchungen { get; set; }
        public BuchungAnzahlModel AnzahlModel { get; set; }
    }
}