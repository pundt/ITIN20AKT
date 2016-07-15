using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class FilterModel 
    {
        // Dieses Feld sind nur für die Oberfläche
        [Display(Name = "Preis von")]
        public decimal DisplayMinPreis { get; set; }

        // Dieses Feld sind nur für die Oberfläche
        [Display(Name = "bis")]
        public decimal DisplayMaxPreis { get; set; }

        // minPreis, wenn kein Preis gesetzt wird, dann ist der Wert 0 
        public int PreisMin { get; set; }

        // maxValue vergeben
        // wenn kein Wert eingegeben wird, dann wird ein maximaler gesetzt
        public int PreisMax { get; set; }

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

        [Display(Name ="Kategorie Unterkunft")]
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