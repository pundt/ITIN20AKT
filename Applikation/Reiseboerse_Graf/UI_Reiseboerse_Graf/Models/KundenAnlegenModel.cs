using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI_Reiseboerse_Graf.Models
{
    public class KundenAnlegenModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ungültige E-Mail Adresse!")]
        [Remote("EmailFrei", "Validation", ErrorMessage = "Email Adresse bereits vergeben")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [StringLength(maximumLength: 16, MinimumLength = 8, ErrorMessage = "Mind. 8 Zeichen!")]
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [DisplayName("Passwort")]
        public string Passwort { get; set; }

        /// <summary>
        /// Wiederholung des Passwortes. Wird nicht in DB gespeichert!
        /// </summary>
        [StringLength(maximumLength: 16, MinimumLength = 8, ErrorMessage = "Mind. 8 Zeichen!")]
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Passwort")]
        [DisplayName("Passwort wiederholen")]
        public string PasswortWiederholung { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DisplayName("Vorname")]
        public string Vorname { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DisplayName("Nachname")]
        public string Nachname { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DisplayName("Telefonnummer")]
        public string Telefon { get; set; }

        [Required]
        public int Land_ID { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DisplayName("Land")]
        public List<LandModel> Land { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DisplayName("Plz")]
        public int Plz { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DisplayName("Adresse")]
        public string Adresse { get; set; }


        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DisplayName("Geschlecht")]
        public bool Geschlecht { get; set; }

        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DisplayName("Titel")]
        public string Titel { get; set; }


        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DataType(DataType.DateTime, ErrorMessage = "Ungültige E-Mail Adresse!")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        [DisplayName("Geburtsdatum")]
        public DateTime GeburtsDatum { get; set; }

    }
}