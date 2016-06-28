using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class LoginModel
    {
        
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Passwort { get; set; }
        /// <summary>
        /// Session id zählt automatisch hoch 
        /// </summary>
        public int SessionId { get; set; }
    }
}