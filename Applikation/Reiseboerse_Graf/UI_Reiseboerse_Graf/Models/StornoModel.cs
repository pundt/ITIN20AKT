using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class StornoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Passnummer { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}")]
        public DateTime Geburtsdatum { get; set; }
        public DateTime GebuchtAm { get; set; }
        public decimal Preis { get; set; }
    }
}