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
    /// Das Model, das in Verwaltung der Reisen verwendet wird.
    /// Es dient zur Auflistung aller Reisen
    /// </summary>
    public class ReiseVerwaltenModel
    {
        /// <summary>
        /// Die ID der Reise
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Der Titel einer Reise
        /// </summary>
        public string Reisetitel { get; set; }
    }
}