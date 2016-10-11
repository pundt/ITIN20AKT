using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Das Model, das an den Login gebunden ist und validiert ob Passwort und Benutzername richtig sind
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Die Email-Adresse (bzw. Benutzername) des Benutzers, ist ein Pflichtfeld
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Das Passwort eines Benutzers, ist ein Pflichtfeld und es sind nur 8-16 Zeichen erlaubt
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        [StringLength(16, ErrorMessage = "8-16 Zeichen", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Passwort { get; set; }

        /// <summary>
        /// Das Boole'sche Feld (wahr oder falsch), das überprüft, ob ein Benutzer angemeldet bleiben möchte oder nicht
        /// </summary>
        [Display(Name = "Möchten Sie angemeldet bleiben?")]
        public bool AngemeldetBleiben { get; set; }

    }
}