using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class FilterModel
    {

        public decimal MinPreis { get; set; }

        public decimal MaxPreis { get; set; }

        /// <summary>
        /// Liste von LandModel für die DropDownListe
        /// </summary>        
        public List<LandModel> Land { get; set; }

        public int Land_id { get; set; }
        /// <summary>
        /// Liste von OrtModel für die DropDownListe
        /// </summary>
        public List<OrtModel> Ort { get; set; }

        /// <summary>
        /// Hotelkategorie (Sterne) für Auswahl der Sterne (Checkbox)
        /// </summary>
        [Range(1, 5, ErrorMessage = "Nur 1 bis 5 zulässig!")]

        public int Ort_ID { get; set; }

        public int HotelKategorie { get; set; }

        /// <summary>
        /// Liste für KategorieModel für DropDownListe (z.B. "Busreise")
        /// </summary>
        public List<KategorieModel> Kategorie { get; set; }

        public int Kategorie_ID { get; set; }

        /// <summary>
        /// Liste für VerpflegungModel für DropDownListe (z.B. "Halbpension")
        /// </summary>
        public List<VerpflegungModel> Verpflegung { get; set; }

        public int Verpflegungs_ID { get; set; }

        /// <summary>
        /// Zur Auswahl eines Startdatums
        /// </summary>
        public DateTime Startdatum { get; set; }

        /// <summary>
        /// Zur Auswahl eines Enddatums
        /// </summary>
        public DateTime Enddatum { get; set; }
    }



}