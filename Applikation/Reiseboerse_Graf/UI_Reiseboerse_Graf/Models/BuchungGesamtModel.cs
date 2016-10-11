using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Benutzeroberfläche
/// </summary>
namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Viewmodel, dass alle Informationen eines Buchungsprozesses beinhaltet
    /// </summary>
    public class BuchungGesamtModel
    {
        /// <summary>
        /// ID der Reise
        /// </summary>
        public int ReiseID { get; set; }
        /// <summary>
        /// Liste aller Buchungen Erwachsenen (pro Buchung Erwachsen eine Eingabemaske)
        /// </summary>
        public List<BuchungErwachsenModel> BuchungErwachsen { get; set; }
        /// <summary>
        /// Liste aller Buchungen Kinder (pro Buchung Kind eine Eingabemaske)
        /// </summary>
        public List<BuchungKindModel> BuchungKind { get; set; }
        /// <summary>
        /// Berechneter Gesamtpreis (Anzahl Erwachsene * Preis Erwachsene + Anzahl Kinder * Preis Kinder)
        /// </summary>
        public decimal Gesamtpreis { get; set; }
        /// <summary>
        /// Titel der Reise
        /// </summary>
        public string Reisetitel { get; set; }
        /// <summary>
        /// Startdatum der Reise (als ShortDateString)
        /// </summary>
        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}")]
        public DateTime Startdatum { get; set; }
        /// <summary>
        /// Enddatum der Reise (als ShortDateString)
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Enddatum { get; set; }
        /// <summary>
        /// Liste aller BuchungsIDs (jede reisende Person hat eigene BuchungsID, damit Stornierungen einzeln
        /// vorgenommen werden können)
        /// </summary>
        public List<int> BuchungIDs { get; set; }

    }
}