using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class BildHinzufuegenModel:ReiseVerwaltenModel
    {
        public int Unterkunftsbild { get; set; }

        public int Reisebild { get; set; }
    }
}