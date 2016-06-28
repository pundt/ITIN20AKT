using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class RegistrierungsModel
    {
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "ungültige Mail")]
        public string Email { get; set; }
        [StringLength(maximumLength: 16, MinimumLength = 8, ErrorMessage = "Mind 8 Zeichen")]
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Passwort { get; set; }

        /// <summary>
        /// Wiederholung des Passwortes. Wird nicht in DB gespeichert!
        /// </summary>
        [StringLength(maximumLength: 16, MinimumLength = 8, ErrorMessage = "Mind 8 Zeichen")]
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Compare("Passwort", ErrorMessage = "Passwörter stimmen nicht überein")]
        public string PasswortWiederholung { get; set; }
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Vorname { get; set; }
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Nachname { get; set; }
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Land { get; set; }
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public int Plz { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Adresse { get; set; }
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public DateTime GeburtsDatum { get; set; }

        /// <summary>
        /// Dropdown feld in view mit männlich oder weiblich.
        /// prüfung bei männlich set = true bei weiblich set = false
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        private bool geschlecht;
        public bool Geschlecht
        {
            get { return geschlecht; }
            set { geschlecht = value; }
        }
    }
}