using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// legt das Model für neue Reisen an
    /// </summary>
    public class ReiseAnlegenModel
    {
        // id
        public int Id { get; set; }
        // Titel
        [Required(ErrorMessage ="Pflichtfeld")]
        public string Titel { get; set; }
        // Beschreibung
        [Required(ErrorMessage = "Pflichtfeld")]
        public string Beschreibung { get; set; }
        //Liste von allen angelegten Ländern
        public List<LandModel> Reiseland { get; set; }
        // Land_id
        public int Land_id { get; set; }
        // falls das Land noch nicht angelegt ist, kann ein neues angelegt werden
        public string NeuesLand { get; set; }
        // Liste von allen angelegten Orten
        public List<OrtModel> ReiseOrt { get; set; }
        // Ort_id
        public int Ort_id { get; set; }
        // falls der Ort noch nicht angelegt ist, kann ein neuer angelegt werden
        public string NeuerOrt { get; set; }
        // Preis für Erwachsene
        [Required(ErrorMessage = "Pflichtfeld")]      
        public double PreisErw { get; set; }
        // Preis für Kinder
        [Required(ErrorMessage = "Pflichtfeld")]
        public double PreisKind { get; set; }
        // Liste von allen Verpflegungsmöglichkeiten
        public List<VerpflegungModel> Verpflegung { get; set; }
        // Verpflegungs_ID
        public int Verpflegung_id { get; set; }
        // Liste von allen Unterkünften
        public List<UnterkunftdetailModel> Unterkunft { get; set; }
        // falls die Unterkunft noch nicht angelegt ist, kann eine neue angelegt werden
        public UnterkunftdetailModel NeueUnterkunft { get; set; }
        // Unterkunft_id
        public int Unterkunft_id { get; set; }
     

    }
}