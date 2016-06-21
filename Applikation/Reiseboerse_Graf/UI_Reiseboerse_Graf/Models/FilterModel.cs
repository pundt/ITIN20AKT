using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class FilterModel
    {
        public int Kontinent_ID { get; set; }
        public int Land_ID { get; set; }
        public int Ort_ID { get; set; }
        public List<int> Kategorien_ID { get; set; }
        public int Preis_ID { get; set; }
    }
}