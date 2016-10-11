using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// Benutzeroberfläche
/// </summary>
namespace UI_Reiseboerse_Graf.Models
{
    /// <summary>
    /// Erbt von BuchungenModel, validiert das Geburtsdatum für Kinder
    /// </summary>
    public class BuchungKindModel: BuchungenModel
    {
        /// <summary>
        /// Geburtsdatum wird mittels Remote Validierung (Validierungscontroller) überprüft
        /// nur gültig wenn Alter kleiner 14 Jahre ist
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",ApplyFormatInEditMode =true)]
        [Remote("AlterKind","Validierung",ErrorMessage ="Kinder müssen unter 14 Jahre sein")]
        public DateTime Geburtsdatum { get; set; }
    }
}