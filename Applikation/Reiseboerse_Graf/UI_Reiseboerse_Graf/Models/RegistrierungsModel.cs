using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class RegistrierungsModel
    {
        public string Email { get; set; }
        public string Passwort { get; set; }
        public string PasswortWiederholung { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Telefon { get; set; }
        public string Land { get; set; }
        public int Plz { get; set; }
        public string Adresse { get; set; }
        public DateTime GeburtsDatum { get; set; }
        /// <summary>
        /// Dropdown feld in view mit männlich oder weiblich.
        /// prüfung bei männlich set = true bei weiblich set = false
        /// </summary>
        private bool geschlecht;
        public bool Geschlecht
        {
            get { return geschlecht; }
            set { geschlecht = value; }
        }
    }
}