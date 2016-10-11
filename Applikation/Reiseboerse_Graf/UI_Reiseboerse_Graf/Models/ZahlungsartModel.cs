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
    /// Model für die Zahlungsart beinhaltet ID und Bezeichnung
    /// wird für Befüllen der Dropdownliste verwendet
    /// </summary>
    public class ZahlungsartModel
    {
        /// <summary>
        /// Eindeutige ID der Zahlungsart, wird zur Zuweisung in DropDown-Listen verwendet
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Bezeichnung der Zahlungsart bspw. "Überweisung"
        /// </summary>
        public string Bezeichnung { get; set; }
    }
}