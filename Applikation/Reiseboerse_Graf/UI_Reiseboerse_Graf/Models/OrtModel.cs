using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Das Model, das den Namen eines Ortes und eine eindeutige ID beinhaltet
    /// </summary>
    public class OrtModel
    {
        /// <summary>
        /// Eindeutige ID des Ortes, wird zur Zuweisung in DropDown-Listen verwendet
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Bezeichnung eines Ortes, z.B. "Wien"
        /// </summary>
        public string Bezeichnung { get; set; }
    }
}