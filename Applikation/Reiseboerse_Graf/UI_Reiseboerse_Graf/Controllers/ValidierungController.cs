using BL_Reiseboerse_Graf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace UI_Reiseboerse_Graf.Controllers
{
    public class ValidierungController : Controller
    {
        /// <summary>
        /// Überprüfe in der DB ob email schon registriert ist.
        /// https://msdn.microsoft.com/en-us/library/gg508808(v=vs.98).aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-2
        /// 
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        //public ActionResult EmailFrei(string Username)
        //{

        //    if (testdbEntities.EmailExist(Username))
        //        return true;
        //}

        /// Überprüft ob eine Email bereits vorhanden ist
        /// wenn ja, dann gib auf der Registrierungs-View false,
        /// ansonsten gib true aus
        public JsonResult EmailFrei(string email)
        {
            bool benutzerExistiert = false;

            benutzerExistiert = Tools.EmailVorhanden(email);

            if (benutzerExistiert)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Überprüft ob ein Alter 14 Jahre überschreitet
        /// wenn ja, dann erlaube weitere Schritte,
        /// wenn nein, dann zeige Fehlermeldung
        /// </summary>
        /// <returns>true oder false</returns>
        public JsonResult AlterErwachsen()
        {
            string geburtsDatumKey = Request.Params.AllKeys.Where(x => x.ToLower().Contains("geburtsdatum")).FirstOrDefault();
            string geburtsDatum = Request.Params[geburtsDatumKey ?? ""];

            if (!string.IsNullOrEmpty(geburtsDatum) && DateTime.Parse(geburtsDatum) <= DateTime.Now.AddYears(-14))
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Überprüft ob ein Alter 13 Jahre unterschreitet
        /// wenn ja, dann erlaube weitere Schritte,
        /// wenn nein, dann zeige Fehlermeldung
        /// </summary>
        /// <returns>true oder false</returns>
        public JsonResult AlterKind()
        {
            string geburtsDatumKey = Request.Params.AllKeys.Where(x => x.ToLower().Contains("geburtsdatum")).FirstOrDefault();
            string geburtsDatum = Request.Params[geburtsDatumKey ?? ""];

            if (!string.IsNullOrEmpty(geburtsDatum) && DateTime.Parse(geburtsDatum) > DateTime.Now.AddYears(-14))
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Prüft von der Zahlungs-Ansicht die eingegeben IBAN- bzw. Kreditkartennummer.
        /// Die Kreditkartennummer wird durch den Luhn-Algorithmus überprüft,
        /// die IBAN wird durch eine Regular Expression überprüft.
        /// Wenn Ergebnis true ist, dann erlaube weitere Schritte,
        /// wenn Ergebnis false ist, dann gib eine Fehlermeldung aus
        /// </summary>
        /// <returns>true oder false</returns>
        public JsonResult LuhnUndIBANPruefung()
        {
            string kartenNummerKey = Request.Params.AllKeys.Where(x => x.ToLower().Contains("nummer")).FirstOrDefault();
            string kartenNummer = Request.Params[kartenNummerKey ?? ""];

            if (!kartenNummer.Contains("AT") && kartenNummer.Length >= 12 && kartenNummer.Length <= 16)
            {
                if (ZahlungsVerwaltung.PruefeLuhn(kartenNummer))
                    return Json(true, JsonRequestBehavior.AllowGet);
                else
                    return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Regex.IsMatch(kartenNummer, "^[A-Z0-9]{10,36}$"))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

    }

}