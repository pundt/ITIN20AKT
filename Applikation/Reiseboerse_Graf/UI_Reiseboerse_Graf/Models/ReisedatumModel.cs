using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class ReisedatumModel
    {
        /// <summary>
        /// Beginndatum
        /// </summary>
        public int ID { get; set; }
        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}",ApplyFormatInEditMode =true)]
        public DateTime Beginndatum { get; set; }
       /// <summary>
       /// Enddatum
       /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}",ApplyFormatInEditMode =true)]
        public DateTime Enddatum { get; set; }
        /// <summary>
        /// Anmeldefrist
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}",ApplyFormatInEditMode =true)]
        public DateTime Anmeldefrist { get; set; }
        /// <summary>
        /// Restplätze
        /// </summary>
        [Required(ErrorMessage = "Pflichtfeld")]
        public int Restplätze { get; set; }
        /// <summary>
        /// Datumsanzeige
        /// </summary>
        private string datumsAnzeige;
        [Display(Name ="Datum")]
        public string DatumsAnzeige
        {
            get { return string.Format("{0} bis {1}",Beginndatum.ToShortDateString(),Enddatum.ToShortDateString()); }
        }
        
        public bool BuchungenVorhanden { get; set; }

    }
}