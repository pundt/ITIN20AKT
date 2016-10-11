using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Viewmodel, das alle Informationen zur Erfassung der Reisedaten beinhaltet#
    /// Erzeugung der korrekten Anzahl an Eingabemasken
    /// </summary>
    public class BuchungHinzufuegenModel
    {
        /// <summary>
        /// Liste von Buchungen für Erwachsene (pro Erwachsenem eine Eingabemaske)
        /// Anzahl ist auf AnzahlModel gespeichert
        /// </summary>
        public List<BuchungErwachsenModel> BuchungenErwachsen { get; set; }
        /// <summary>
        /// Liste aller Buchungen für Kinder (pro Kind eine Einagbemaske)
        /// Anzahl ist auf AnzahlModel gespeichert
        /// </summary>
        public List<BuchungKindModel> BuchungenKind { get; set; }
        /// <summary>
        /// beinhaltet Informationen über ausgewählte Anzahl der Reisenden, sowie die Einzelpreise
        /// </summary>
        public BuchungAnzahlModel AnzahlModel { get; set; }
    }
}