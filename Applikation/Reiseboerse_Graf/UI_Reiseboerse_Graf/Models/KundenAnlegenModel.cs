using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Das Model zur Registrierung eines Benutzers/Kunden
    /// </summary>
    public class KundenAnlegenModel
    {
        /// <summary>
        /// Die ID zur eindeutigen Zuweisung eines Benutzers
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Die Email eines Benutzers, ist ein Pflichtfeld und es wird überprüft
        /// ob ein Benutzer ein Kunde oder ein Mitarbeiter ist.
        /// (Mitarbeiter-Emails sind bereits in der DB vorhanden)
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ungültige E-Mail Adresse!")]
        [Remote("EmailFrei", "Validierung", ErrorMessage = "Email Adresse bereits vergeben")]
        [DisplayName("Email")]
        public string Email { get; set; }

        /// <summary>
        /// Das Passwort eines Benutzers, ist ein Pflichtfeld und es sind nur 8 bis 16 Zeichen erlaubt
        /// </summary>
        [StringLength(maximumLength: 16, MinimumLength = 8, ErrorMessage = "Mind. 8 Zeichen!")]
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [DisplayName("Passwort")]
        public string Passwort { get; set; }

        /// <summary>
        /// Wiederholung des Passwortes zur Überprüfung. Wird nicht in DB gespeichert!
        /// </summary>
        [StringLength(maximumLength: 16, MinimumLength = 8, ErrorMessage = "Mind. 8 Zeichen!")]
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Passwort",ErrorMessage ="Passwörter stimmen nicht überein")]
        [DisplayName("Passwort wiederholen")]
        public string PasswortWiederholung { get; set; }

        /// <summary>
        /// Vorname eines Benutzers
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DisplayName("Vorname")]
        public string Vorname { get; set; }

        /// <summary>
        /// Nachname eines Benutzers
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DisplayName("Nachname")]
        public string Nachname { get; set; }

        /// <summary>
        /// Telefonnummer eines Benutzers
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DisplayName("Telefonnr")]
        public string Telefon { get; set; }

        /// <summary>
        /// Die Land-ID, die der Benutzer mittels DropDown-Menü auswählt.
        /// Speichert die ID des Landes
        /// </summary>
        [Required]
        public int Land_ID { get; set; }

        /// <summary>
        /// Liste von Ländern, zur Befüllung des DropDown-Menüs
        /// </summary>
        [DisplayName("Land")]
        public List<LandModel> Land { get; set; }

        /// <summary>
        /// Die Ort-ID, die der Benutzer mittels DropDown-Menü auswählt.
        /// Speichert die ID des Ortes
        /// </summary>
        [Required]
        public int Ort_ID { get; set; }

        /// <summary>
        /// Liste von Orten, zur Befüllung des DropDwon-Menüs
        /// </summary>
        [DisplayName("Ort")]
        public List<OrtModel> Ort { get; set; }

        /// <summary>
        /// Die Adresse eines Benutzers
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Adresse")]
        public string Adresse { get; set; }

        /// <summary>
        /// Das Geschlecht eines Benutzers (Wahr für Weiblich und Falsch für Männlich)
        /// </summary>
        [DisplayName("Geschlecht")]
        public bool Geschlecht { get; set; }

        /// <summary>
        /// Titel eines Benutzers
        /// </summary>
        [DisplayName("Titel")]
        public string Titel { get; set; }

        /// <summary>
        /// Geburtsdatum eines Benutzers, ist ein Pflichtfeld
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld!", AllowEmptyStrings = false)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        [DisplayName("Geburtsdatum")]
        public DateTime GeburtsDatum { get; set; }

    }
}