using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UI_Reiseboerse_Graf.Models
{
    public class AlterKindValidierung:ValidationAttribute
    {
        /// <summary>
        /// Prüft ob das Geburtsdatum gültig ist also kleiner gleich 13 Jahre
        /// </summary>
        /// <param name="value">den übergebenen Wert also das Feld im Model</param>
        /// <returns>die Validierung - gültig oder die Ausgabe der Validierungsmeldung</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime pruefdatum = (DateTime)value;
            if (pruefdatum>=DateTime.Now.AddYears(-13))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Kinder müssen unter 13 Jahren sein");
            }
        }
    }
}