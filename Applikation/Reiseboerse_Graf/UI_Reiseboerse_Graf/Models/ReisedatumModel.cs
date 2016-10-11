﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// es werden alle Daten (Datum) erfasst und deren Anzahl / bei mehreren Datenangaben kann man wieder auf die Seite zurückkommen
    /// </summary>
    public class ReisedatumModel
    {
        /// <summary>
        /// Die ID des Reisedatums
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Das Beginndatum
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}",ApplyFormatInEditMode =true)]
        public DateTime Beginndatum { get; set; }

       /// <summary>
       /// Enddatum
       /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}",ApplyFormatInEditMode =true)]
        public DateTime Enddatum { get; set; }

        /// <summary>
        /// Anmeldefrist
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}",ApplyFormatInEditMode =true)]
        public DateTime Anmeldefrist { get; set; }

        /// <summary>
        /// Restplätze
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        public int Restplätze { get; set; }

        /// <summary>
        /// Datumsanzeige
        /// </summary>
        [Display(Name ="Datum")]
        public string DatumsAnzeige
        {
            get { return string.Format("{0} bis {1}",Beginndatum.ToShortDateString(),Enddatum.ToShortDateString()); }
        }

        /// <summary>
        /// Prüft ob zu diesem Reisedatum schon Buchungen existieren
        /// </summary>
        public bool BuchungenVorhanden { get; set; }

    }
}