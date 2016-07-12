﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class ReiseModel
    {
        /// <summary>
        /// Auto increment
        /// </summary>
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Titel { get; set; }
       
        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString ="{0:dd.MM.yy}")]
        public DateTime Beginndatum { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        public DateTime Enddatum { get; set; }

        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Verpflegung { get; set; }

        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Ort { get; set; }

        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Land { get; set; }

        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Unterkunft { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        public decimal Preis { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        public DateTime Anmeldefrist { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        public int Restplätze { get; set; }

    }
}