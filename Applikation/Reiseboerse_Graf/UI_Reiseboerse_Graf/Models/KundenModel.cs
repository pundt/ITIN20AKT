using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class KundenModel
    {
        /// <summary>
        /// eventuell auto increment wird aber warscheinlich von sql server gehandelt
        /// </summary>
        public int Id { get; set; }



        public string Vorname { get; set; }


        public string Nachname { get; set; }


        public string Email { get; set; }


        public string Telefon { get; set; }


        public string Passwort { get; set; }


        public string Land { get; set; }


        public int Plz { get; set; }


        public string Adresse { get; set; }


        public DateTime GeburtsDatum { get; set; }
    }
}