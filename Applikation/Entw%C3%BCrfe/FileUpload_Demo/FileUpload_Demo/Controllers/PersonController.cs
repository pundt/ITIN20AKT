using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileUpload_Demo.Controllers
{
    public class PersonController : Controller
    {
        [HttpGet]
        public ActionResult Anlegen_EinfachesFormular()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Anlegen_EinfachesFormular(string vorname, string nachname, DateTime gebDat)
        {
            Debug.WriteLine("Person speichern");
            Debug.Indent();
            Debug.WriteLine("Vorname: " + vorname);
            Debug.WriteLine("Nachname: " + nachname);
            Debug.WriteLine("Geb.-Dat.: " + gebDat);
            Debug.Unindent();
            return View();
        }

        [HttpGet]
        public ActionResult Anlegen_ProfilBild()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Anlegen_ProfilBild(string vorname, string nachname, DateTime gebDat, HttpPostedFileBase profilBild)
        {
            Debug.WriteLine("Person speichern (Profilbild)");
            Debug.Indent();
            Debug.WriteLine("Vorname: " + vorname);
            Debug.WriteLine("Nachname: " + nachname);
            Debug.WriteLine("Geb.-Dat.: " + gebDat);

            if (profilBild != null && profilBild.ContentLength > 0)
            {
                Debug.WriteLine("File.fileName: " + profilBild.FileName);
                Debug.WriteLine("File.ContentLength: " + profilBild.ContentLength);
                Debug.WriteLine("File.ContentType: " + profilBild.ContentType);

                byte[] bildArray = new byte[profilBild.ContentLength];
                profilBild.InputStream.Read(bildArray, 0, profilBild.ContentLength);

                /// speichere byte[]
            }

            Debug.Unindent();
            return View();
        }

    }
}