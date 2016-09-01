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
       
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Verpflegung { get; set; }

        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Ort { get; set; }

        public int Ort_id { get; set; }

        public int Land_id { get; set; }
        public string Land { get; set; }

        public int Hotelkategorie { get; set; }

        public int Verpflegungs_id { get; set; }

        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Unterkunft { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        public decimal Preis { get; set; }

        public int Reisedatum_ID { get; set; }

        public List<ReisedatumModel> Reisedaten { get; set; }

    }
}