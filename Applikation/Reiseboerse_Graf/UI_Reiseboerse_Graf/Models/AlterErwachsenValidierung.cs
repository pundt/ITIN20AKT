using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UI_Reiseboerse_Graf.Models
{
    public class AlterErwachsenValidierung:ValidationAttribute
    {
        /// <summary>
        /// Prüft ob das Geburtsdatum gültig ist also größer 14 Jahre
        /// </summary>
        /// <param name="value">den übergebenen Wert also das Feld im Model</param>
        /// <returns>die Validierung - gültig oder die Ausgabe der Validierungsmeldung</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime pruefdatum = (DateTime)value;
            if (pruefdatum<DateTime.Now.AddYears(-13))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Erwachsene müssen über 14 Jahre sein");
            }
        }
    }
}