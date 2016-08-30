using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL_Reiseboerse_Graf;


namespace daten_test
{
    class Program
    {
        static void Main()
        {
            ReiseVerwaltung rv = new ReiseVerwaltung();

            
           List<Reise> reiseliste = ReiseVerwaltung.LadeAlleReisen();
            int i = 0;
            foreach (var item in reiseliste)
            {
                i++;
                Console.WriteLine("Reise: "+i);
                Console.WriteLine("Id");
                Console.WriteLine(item.ID);
                Console.WriteLine("Titel");
                Console.WriteLine(item.Titel);
                Console.WriteLine("ErstelltAm");
                Console.WriteLine(item.ErstelltAm);
                Console.WriteLine("Beschreibung");
                Console.WriteLine(item.Beschreibung);
                Console.WriteLine("Ort");
                Console.WriteLine(item.Ort);
                Console.WriteLine("Preis_Erwachsener");
                Console.WriteLine(item.Preis_Erwachsen);
                Console.WriteLine("Preis-Kind");
                Console.WriteLine(item.Preis_Kind);
                Console.WriteLine("Unterkunft");
                Console.WriteLine(item.Unterkunft);
                Console.WriteLine("/n");
                Console.ReadLine();
            }

            
        }
    }
}
