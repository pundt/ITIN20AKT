using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class BuchungErwachsenModel:BuchungenModel
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [AlterErwachsenValidierung(ErrorMessage ="Erwachsene ab 14 Jahre")]
        public DateTime Geburtsdatum { get; set; }
    }
}