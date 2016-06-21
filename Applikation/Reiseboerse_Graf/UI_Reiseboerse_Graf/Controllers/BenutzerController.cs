﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_Reiseboerse_Graf.Models;

namespace UI_Reiseboerse_Graf.Controllers
{
    public class BenutzerController : Controller
    {
        // GET: Benutzer
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GebuchteReisen()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string passwort)
        {
            BenutzerModel bm = new BenutzerModel();
            email = bm.Email;
            passwort = bm.Passwort;

            return View("~/Views/Home/Index.cshtml");

        }
        [HttpPost]
        public ActionResult Logout()
        {
            /// Session variable von derzeitigem benutzer wird auf null gesetzt.
            return View();
        }

    }
}