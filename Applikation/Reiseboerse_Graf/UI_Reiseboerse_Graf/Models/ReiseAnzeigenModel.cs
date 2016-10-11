using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Benutzeroberfläche
/// </summary>
namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Viewmodel für die Detailansicht einer Reise
    /// von dort aus kann ein Buchungsprozess gestartet werden
    /// </summary>
    public class ReiseAnzeigenModel
    {
        /// <summary>
        /// Das Reisedetailmodel mit den Informationen über Unterkunft, Verpflegung, Ort/Land, Beschreibung einer
        /// Reise
        /// </summary>
        public ReisedetailModel Reisedetail { get; set; }
        /// <summary>
        /// Reisedatummodel mit den Daten über Anmeldefrist, Start- und Enddatum dieser Reise
        /// </summary>
        public ReisedatumModel Reisedatum { get; set; }
    }
}