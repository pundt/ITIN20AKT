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
    
    public partial class Zahlungsart
    {
        public Zahlungsart()
        {
            this.AlleZahlungen = new HashSet<Zahlung>();
        }
    
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public Nullable<System.DateTime> ErstelltAm { get; set; }
    
        public virtual ICollection<Zahlung> AlleZahlungen { get; set; }
    }
}
