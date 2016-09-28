using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI_Reiseboerse_Graf.Models
{
    public class BuchungKindModel: BuchungenModel
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Geburtsdatum { get; set; }
    }
}