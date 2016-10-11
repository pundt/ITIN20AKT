using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Das Model, das alle grundsätzlichen Informationen zu einer Reise beinhaltet
    /// </summary>
    public class ReiseModel
    {
        /// <summary>
        /// Die eindeutige ID einer Reise, diese zählt automatisch hoch
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// Der Titel einer Reise, z.B. "Wandern in den Bergen", ist ein Pflichtfeld
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Titel { get; set; }      
        
        /// <summary>
        /// Die Verpflegung einer Reise, ist ein PFlichtfeld
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Verpflegung { get; set; }

        /// <summary>
        /// Der Ort einer Reise, z.B. "Graz", ist ein Pflichtfeld
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Ort { get; set; }

        /// <summary>
        /// Die eindeutige ID eines Ortes
        /// </summary>
        public int Ort_id { get; set; }

        /// <summary>
        /// Die eindeutige ID eines Landes
        /// </summary>
        public int Land_id { get; set; }

        /// <summary>
        /// Die Bezeichnung eines Landes, z.B. "Deutschland"
        /// </summary>
        public string Land { get; set; }

        /// <summary>
        /// Die Hotelkategorie von 1 bis 5 (Sterne)
        /// </summary>
        public int Hotelkategorie { get; set; }

        /// <summary>
        /// Die eindeutige ID der Verpflegung
        /// </summary>
        public int Verpflegungs_id { get; set; }

        /// <summary>
        /// Die Bezeichnung einer Unterkunftm z.B. "Hotel Muster", ist ein Pflichtfeld
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld", AllowEmptyStrings = false)]
        public string Unterkunft { get; set; }

        /// <summary>
        /// Der Preis einer Reise, ist ein Pflichtfeld
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        public decimal Preis { get; set; }

        /// <summary>
        /// Die ID eines Reisedatums, das aus dem DropDown-Menü ausgewählt wurde
        /// </summary>
        public int Reisedatum_ID { get; set; }

        /// <summary>
        /// Die Liste von ReisedatumModel, zur Befüllung des DropDown-Menüs
        /// </summary>
        public List<ReisedatumModel> Reisedaten { get; set; }

        /// <summary>
        /// Die Bewertung einer Reise von 1 bis 5 (1 für sehr schlecht, 5 für sehr gut)
        /// </summary>
        public int Bewertung { get; set; }

    }
}