using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL_Reiseboerse_Graf;
using System.Diagnostics;

namespace UI_Reiseboerse_Graf.Controllers
{
    public class BildController : Controller
    {
        /// <summary>
        /// Sucht für eine Reise zugehöriges Bild
        /// </summary>
        /// <param name="id">ID der Reise</param>
        /// <returns>ein zufälliges Bild dieser Reise</returns>
        [HttpGet]
        public ActionResult BildzuReise(int id)
        {
            Debug.WriteLine("Bild - Bild zu Reise - GET ");
            Debug.Indent();
            List<int> IDListe=BildVerwaltung.LadeBildID(id);
            //Random rnd = new Random();
            //int aktid = rnd.Next(1, IDListe.Count + 1);
            int aktid = IDListe[0];
            Debug.Unindent();
            return Laden(aktid);
        }

        /// <summary>
        /// Sucht für eine Unterkunft zugehöriges Bild
        /// </summary>
        /// <param name="id">ID der Unterkunft</param>
        /// <returns>ein zufälliges Bild dieser Unterkunft</returns>
        [HttpGet]
        public ActionResult BildzuUnterkunft(int id)
        {
            Debug.WriteLine("Bild - Bild zu Unterkunft - GET ");
            Debug.Indent();
            List<int> IDListe = BildVerwaltung.LadeUnterkunftBildID(id);
            //Random rnd = new Random();
            //int aktid = rnd.Next(1, IDListe.Count + 1);
            int aktid = IDListe[0];
            Debug.Unindent();
            return Laden(aktid);
        }



        /// <summary>
        /// Lädt ein Bild anhand einer Spezifischen Id
        /// </summary>
        /// <param name="id">Id einer datei</param>
        /// <returns>FileContentResult mit Bild und Datentyp</returns>
        [AllowAnonymous]
        public ActionResult Laden(int id)
        {
            Debug.WriteLine("Bild - Bild laden anhand ID - GET");
            Debug.Indent();
            string contentType = "image/jpeg";
            byte[] bilddaten = BildVerwaltung.LadeBild(id).Bilddaten;

            Debug.Unindent();
            return new FileContentResult(bilddaten, contentType);
            
        }
    }
}