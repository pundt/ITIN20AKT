using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Das View-Model, das zum Anzeigen der Oberfläche Reise\Laden benötigt wird
    /// </summary>
    public class ReiseLadenModel
    {
        /// <summary>
        /// Das FilterModel, das alle benötigten Informationen für den Filter enthält
        /// </summary>
        public FilterModel Filter { get; set; }

        /// <summary>
        /// Enthält alle Reisen zum Anzeigen auf der Seite
        /// </summary>
        public List<ReiseModel> Reisen{ get; set; }

        /// <summary>
        /// Dient zum Auslesen des Suchtextes aus der Oberfläche
        /// </summary>
        public string TextSuche { get; set; }
    }
}