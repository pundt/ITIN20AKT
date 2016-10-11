using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Benutzeroberfläche
/// </summary>
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
        /// <summary>
        /// Bezeichnung der Bezeichnung
        /// </summary>
        public string Bezeichnung { get; set; }
    }
}