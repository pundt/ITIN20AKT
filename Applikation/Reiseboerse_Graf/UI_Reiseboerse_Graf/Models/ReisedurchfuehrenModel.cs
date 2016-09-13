using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UI_Reiseboerse_Graf.Models
{
    public class ReisedurchfuehrenModel
    {
        [Required]
        public int Reise_id { get; set; }



        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        public DateTime StartDatum { get; set; }


        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        public DateTime EndDatum { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        public DateTime Anmeldefrist { get; set; }

        
        public int ReiseAnzahl { get; set; }
        public bool WeitereReisenHinzufuegen { get; set; }
    }
}