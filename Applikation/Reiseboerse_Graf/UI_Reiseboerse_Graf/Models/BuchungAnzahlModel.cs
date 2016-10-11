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
    /// Model, das die Daten enthält um die passenden Felder für die Dateneingabe der Reisenden zu erzeugen
    /// Es wird für jeden Erwachsenen und jedes Kind eine Eingabemaske erzeugt
    /// </summary>
    public class BuchungAnzahlModel
    {
        /// <summary>
        /// ID des Reisedatums (Reise zu einem bestimmten Datum)
        /// </summary>
        public int Reisedatum_ID { get; set; }
        /// <summary>
        /// Titel der Reise
        /// </summary>
        public string Reisetitel { get; set; }
        /// <summary>
        /// Anzahl der erwachsenen Reisenden
        /// </summary>
        public int Anzahl_Erwachsene { get; set; }
        /// <summary>
        /// Anzahl der reisenden Kinder
        /// </summary>
        public int Anzahl_Kinder { get; set; }
        /// <summary>
        /// Preis pro Erwachsener
        /// </summary>
        public decimal Preis_Erwachsene { get; set; }
        /// <summary>
        /// Preis pro Kind
        /// </summary>
        public decimal Preis_Kind { get; set; }
        /// <summary>
        /// Berechnung der Gesamtanzahl aller Reisenden
        /// </summary>
        private int anzahl;

        public int Anzahl
        {
            get { return Anzahl_Erwachsene+Anzahl_Kinder; }
        }

    }
}