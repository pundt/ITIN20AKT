using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI_Reiseboerse_Graf.Models
{
    public class BenutzerModel
    {


        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "ungültige Mail")]
        [Remote("EmailFrei", "Validation", ErrorMessage = "Email Adresse bereits vergeben")]
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
        [System.ComponentModel.DataAnnotations.Compare("Passwort")]
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
        public bool Geschlecht { get; set; }


        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        [DataType(DataType.DateTime, ErrorMessage = "ungültige Mail")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        public DateTime GeburtsDatum { get; set; }

    }
}