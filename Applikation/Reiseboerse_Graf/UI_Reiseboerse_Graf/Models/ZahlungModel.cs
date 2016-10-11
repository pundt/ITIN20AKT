using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// Benutzeroberfläche
/// </summary>
namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Model zur Eingabe einer Zahlung (Formular)
    /// </summary>
    public class ZahlungModel
    {
        /// <summary>
        /// Vorname des Karten-/Kontoinhabers
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z--]+$", ErrorMessage = "Nur Buchstaben von A bis Z erlaubt")]
        public string Vorname { get; set; }
        /// <summary>
        /// Nachname des Karten-/Kontoinhabers
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z--]+$", ErrorMessage = "Nur Buchstaben von A bis Z erlaubt")]
        public string Nachname { get; set; }
        /// <summary>
        /// Nummer der Karte bzw. des Konto
        /// Remotevalidierung (bei Kreditkarte Luhnalgorithmus bei Kontonummer auf Gültigkeit prüfen)
        /// </summary>
        [Remote("LuhnUndIBANPruefung", "Validierung", ErrorMessage = "Geben Sie eine gültige IBAN- oder Kreditkartennummer ein")]
        public string Nummer { get; set; }
        /// <summary>
        /// Id der Zahlungsart (wird über Dropdownliste selektiert)
        /// </summary>
        [Display(Name ="Zahlungsart")]
        public int Zahlungsart_ID { get; set; }
        /// <summary>
        /// Dropdownliste Zahlungsarten (eine Liste von Zahlungsartmodeln)
        /// </summary>
        public List<ZahlungsartModel> Zahlungsarten { get; set; }
        //public int Reisedatum_ID { get; set; }
    }
}