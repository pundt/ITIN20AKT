using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Viewmodel für die Startseite mit Anzeige aller Reisen
    /// </summary>
    public class ReiseLadenModel
    {
        /// <summary>
        /// Filtermodel für die Filterung der Reisen nach bestimmten Kriterien
        /// </summary>
        public FilterModel Filter { get; set; }
        /// <summary>
        /// Liste von Reisemodeln (aus der Datenbank geladen und auf Reise umgemappt)
        /// </summary>
        public List<ReiseModel> Reisen{ get; set; }
        /// <summary>
        /// Model für die Suche mittels Schlagworten (Beschreibungstext der Reise wird durchsucht)
        /// </summary>
        public string TextSuche { get; set; }
        //IEnumerable<UI_Reiseboerse_Graf.Models.ReiseModel
    }
}