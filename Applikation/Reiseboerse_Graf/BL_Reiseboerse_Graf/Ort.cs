//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BL_Reiseboerse_Graf
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ort
    {
        public Ort()
        {
            this.AlleAdressen = new HashSet<Adresse>();
            this.AlleReisen = new HashSet<Reise>();
        }
    
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public System.DateTime ErstelltAm { get; set; }
    
        public virtual ICollection<Adresse> AlleAdressen { get; set; }
        public virtual Land Land { get; set; }
        public virtual ICollection<Reise> AlleReisen { get; set; }
    }
}
