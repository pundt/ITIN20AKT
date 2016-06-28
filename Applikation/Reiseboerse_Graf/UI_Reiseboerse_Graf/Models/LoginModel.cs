using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class LoginModel
    {
        /// <summary>
        /// Model das an den login gebunden ist und validiert ob passwort und benutzername richtig rind
        /// </summary>
        
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Passwort { get; set; }
    }
}