using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Model für Verpflegung (beinhaltet ID und Bezeichnung)
    /// wird für befüllen der Dropdownlisten verwendet
    /// </summary>
    public class VerpflegungModel
    {
        /// <summary>
        /// Id für die eindeutige Zuweisung
        /// </summary>
        public int Id { get; set; }

        public string Bezeichnung { get; set; }
    }
}