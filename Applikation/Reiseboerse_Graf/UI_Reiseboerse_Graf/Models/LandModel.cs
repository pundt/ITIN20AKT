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
    /// Das Model, das den Namen eines Landes und die eindeutige ID beinhaltet
    /// </summary>
    public class LandModel
    {
        /// <summary>
        /// Bezeichnung des Landes, z.B. "Deutschland"
        /// </summary>
        public string landName { get; set; }

        /// <summary>
        /// Eindeutige ID des Landes, wird zur Zuweisung in DropDown-Listen verwendet
        /// </summary>
        public int land_ID { get; set; }
    }
}