using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Model, das die Daten enthält um die passenden Felder für die Dateneingabe der Reisenden zu erzeugen
    /// Es wird für jeden Erwachsenen und jedes Kind eine Eingabemaske erzeugt
    /// </summary>
    public class BuchungAnzahlModel
    {
        public int Reisedatum_ID { get; set; }
        public string Reisetitel { get; set; }
        public int Anzahl_Erwachsene { get; set; }
        public int Anzahl_Kinder { get; set; }
        public decimal Preis_Erwachsene { get; set; }
        public decimal Preis_Kind { get; set; }
        private int anzahl;

        public int Anzahl
        {
            get { return Anzahl_Erwachsene+Anzahl_Kinder; }
        }

    }
}