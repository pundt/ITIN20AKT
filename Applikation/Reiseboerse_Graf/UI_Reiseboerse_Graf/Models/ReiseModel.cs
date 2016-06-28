using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class ReiseModel
    {
        public int ID { get; set; }
        [Required]
        public string Titel { get; set; }
        [Required]
        [MinLength(30)]
        public string Beschreibung { get; set; }
        [Required]
        [DisplayFormat(DataFormatString ="{0:dd.MM.yy}")]
        public DateTime Beginndatum { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        public DateTime Enddatum { get; set; }
        [Required]
        public string Verpflegung { get; set; }
        [Required]
        public string Ort { get; set; }
        [Required]
        public string Hotel { get; set; }
        [Required]
        public int Hotel_ID { get; set; }
        [Required]
        public decimal Preis { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        public DateTime Anmeldefrist { get; set; }
        [Required]
        public int Restplätze { get; set; }
        public string LinkzuBild { get; set; }

    }
}