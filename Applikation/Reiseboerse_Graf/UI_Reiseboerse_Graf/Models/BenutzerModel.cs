using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class BenutzerModel
    {
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "ungültige Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 16, MinimumLength = 8, ErrorMessage = "Mind 8 Zeichen")]
        public string Passwort { get; set; }

        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Vorname { get; set; }

        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Nachname { get; set; }

        /// <summary>
        /// Dropdown feld in view mit männlich oder weiblich.
        /// prüfung bei männlich set = true bei weiblich set = false
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public bool Geschlecht { get; set; }

    }
}