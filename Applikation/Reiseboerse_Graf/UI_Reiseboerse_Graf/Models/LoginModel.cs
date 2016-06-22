using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class LoginModel
    {

        private string passwort;

        public string Passwort
        {
            get { return passwort; }
            set { passwort = value; }
        }

        //Wir als Benutzername zum Einloggen verwendet.
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        

        public int SessionId { get; set; }
    }
}