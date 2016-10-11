using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class UnterkunftdetailModel
    {
        // Id
        public int ID { get; set; }
        // Bezeichnung
        public string Bezeichnung { get; set; }
        //Verpflegungs_id
        public int Verpflegung_ID { get; set; }
        // Unterkunftsverpflegung
        public VerpflegungModel Verpflegung { get; set; }
        //Beschreibung
        [DataType(DataType.MultilineText)]
        public string Beschreibung { get; set; }
        // Kategorie
        public int Kategorie { get; set; }

        // es wird geprüft ob eine neue Unterkunft angelegt wurde
        public static bool PruefeUnterkunft(UnterkunftdetailModel unterkunft)
        {
            if (unterkunft.Beschreibung != null && unterkunft.Bezeichnung!= null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}