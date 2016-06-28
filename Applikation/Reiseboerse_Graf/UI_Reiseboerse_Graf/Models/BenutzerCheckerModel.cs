using BL_Reiseboerse_Graf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class BenutzerCheckerModel
    {
        public void Benutzer_Checker()
        {

            using (testdbEntities1 context = new testdbEntities1())
               (mysqlServerLoginLocal);
            MySqlCommand cmd = new MySqlCommand("SELECT benutzer FROM Userinfo WHERE benutzer = '" + textBox1.Text + "';");
            cmd.Connection = con;
            MySqlDataReader Reader;
            con.Open();
            Reader = cmd.ExecuteReader();
            Reader.Read();
            string benutzer = Reader.GetValue(0).ToString();
            con.Close();
            if (benutzer == textBox1.Text)
            {

                MessageBox.Show("Der Benutzer existiert");
            }

        }
    }
}