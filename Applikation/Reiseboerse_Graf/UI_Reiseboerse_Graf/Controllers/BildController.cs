using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI_Reiseboerse_Graf.Controllers
{
    public class BildController : Controller
    {
        [AllowAnonymous]
        public ActionResult Laden(int id)
        {
            string contentType = "image/jpeg";
            byte[] data = BL_Reiseboerse_Graf.ReiseVerwaltung.Bildausleser(id);   //de facto - aus db laden / oder aus BL laden

            return new FileContentResult(data, contentType);
        }
    }
}