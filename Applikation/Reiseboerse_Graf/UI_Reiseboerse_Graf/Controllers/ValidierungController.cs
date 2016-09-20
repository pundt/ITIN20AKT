using BL_Reiseboerse_Graf;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }

}