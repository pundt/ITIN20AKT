using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class FilterModel
    {

        private decimal[,] preis = new decimal[3, 2] { { 0, 499 }, { 500, 1499 }, { 1500, 9999 } };

        /// <summary>
        /// Preis-Array für die Checkbox zur Auswahl
        /// </summary>
        public decimal[,] Preis
        {
            get { return preis = new decimal[3, 2] { { 0, 499 }, { 500, 1499 }, { 1500, 9999 } }; }
        }

        /// <summary>
        /// Liste von LandModel für die DropDownListe
        /// </summary>
        public List<LandModel> Land { get; set; }

        /// <summary>
        /// Liste von OrtModel für die DropDownListe
        /// </summary>
        public List<OrtModel> Ort { get; set; }

        /// <summary>
        /// Hotelkategorie (Sterne) für Auswahl der Sterne (Checkbox)
        /// </summary>
        [Range(1, 5, ErrorMessage = "Nur 1 bis 5 zulässig!")]
        public int HotelKategorie { get; set; }

        /// <summary>
        /// Liste für KategorieModel für DropDownListe (z.B. "Busreise")
        /// </summary>
        public List<KategorieModel> Kategorie { get; set; }

        /// <summary>
        /// Liste für VerpflegungModel für DropDownListe (z.B. "Halbpension")
        /// </summary>
        public List<VerpflegungModel> Verpflegung { get; set; }

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