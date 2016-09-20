using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class BuchungKindModel: BuchungenModel
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [AlterKindValidierung(ErrorMessage ="Kinder müssen unter 14 Jahre alt sein")]
        public DateTime Geburtsdatum { get; set; }
    }
}