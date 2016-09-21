using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class UnterkunftdetailModel
    {
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public int Verpflegung_ID { get; set; }
        public VerpflegungModel Verpflegung { get; set; }
        [DataType(DataType.MultilineText)]
        public string Beschreibung { get; set; }
        public int Kategorie { get; set; }


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