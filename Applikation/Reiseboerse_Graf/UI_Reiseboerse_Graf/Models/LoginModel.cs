using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class LoginModel
    {
        private string email;

        private string passwort;

        public string Passwort
        {
            get { return passwort; }
            set { passwort = value; }
        }


        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        

        public int SessionId { get; set; }
    }
}