using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Das Model, das in der Verwaltung für die Mitarbeiter verwendet wird
    /// </summary>
    public class StornoModel
    {
        /// <summary>
        /// Die ID der Buchung, zur eindeutigen Zuweisung einer Stornierung
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name eines Benutzers (Vor- und Nachname)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Reisepassnummer eines Benutzers
        /// </summary>
        public string Passnummer { get; set; }

        /// <summary>
        /// Geburtsdatum eines Benutzers
        /// </summary>
        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}")]
        public DateTime Geburtsdatum { get; set; }

        /// <summary>
        /// Das Buchungsdatum der Reise
        /// </summary>
        public DateTime GebuchtAm { get; set; }

        /// <summary>
        /// Der gesamte Preis der Reise
        /// </summary>
        public decimal Preis { get; set; }
    }
}