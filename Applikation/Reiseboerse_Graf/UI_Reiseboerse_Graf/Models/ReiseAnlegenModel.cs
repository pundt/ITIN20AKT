using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Benutzeroberfläche
/// </summary>
namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// legt das Model für neue Reisen an
    /// </summary>
    public class ReiseAnlegenModel
    {
        /// <summary>
        /// id der Reise
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Titel der Reise
        /// </summary>
        [Required(ErrorMessage ="Pflichtfeld")]
        public string Titel { get; set; }

        /// <summary>
        /// Beschreibung der Reise 
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.MultilineText)]
        public string Beschreibung { get; set; }

        /// <summary>
        /// Liste von allen angelegten Ländern
        /// </summary>
        public List<LandModel> Reiseland { get; set; }

        /// <summary>
        /// Land_id/ mit der wird die Reise eruiert und gespeichert
        /// </summary>
        public int Land_id { get; set; }

        /// <summary>
        /// falls das Land noch nicht angelegt ist, kann ein neues angelegt werden
        /// </summary>
        public string NeuesLand { get; set; }

        /// <summary>
        /// Liste von allen angelegten Orten
        /// </summary>
        public List<OrtModel> ReiseOrt { get; set; }

        /// <summary>
        /// Ort_id / mit der wird der Ort eruiert und gespeichert
        /// </summary>
        public int Ort_id { get; set; }

        /// <summary>
        /// falls ein neuer Ort angelegt werden muss
        /// </summary>
        public string NeuerOrt { get; set; }

        /// <summary>
        /// Preis für Erwachsene
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")] 
        [Range(0,9999,ErrorMessage ="Preis darf nicht höher als 9999 sein")]     
        public double PreisErw { get; set; }

        /// <summary>
        /// Preis für Kinder
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        [Range(0, 9999, ErrorMessage = "Preis darf nicht höher als 9999 sein")]
        public double PreisKind { get; set; }

        /// <summary>
        /// Liste von allen Verpflegungsmöglichkeiten
        /// </summary>
        public List<VerpflegungModel> Verpflegung { get; set; }

        /// <summary>
        /// Verpflegungs_ID
        /// </summary>
        public int Verpflegung_id { get; set; }

        /// <summary>
        /// Liste von allen Unterkünften
        /// </summary>
        public List<UnterkunftdetailModel> Unterkunft { get; set; }

        /// <summary>
        /// falls die Unterkunft noch nicht angelegt ist, kann eine neue angelegt werden
        /// </summary>
        public UnterkunftdetailModel NeueUnterkunft { get; set; }

        /// <summary>
        /// Unterkunft_id
        /// </summary>
        public int Unterkunft_id { get; set; }
     

    }
}