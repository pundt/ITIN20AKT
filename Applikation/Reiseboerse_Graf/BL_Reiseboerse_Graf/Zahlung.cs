//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BL_Reiseboerse_Graf
{
    using System;
    using System.Collections.Generic;
    
    public partial class Zahlung
    {
        public Zahlung()
        {
            this.AlleBuchung_Zahlungen = new HashSet<Buchung_Zahlung>();
        }
    
        public int ID { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Nummer { get; set; }
        public System.DateTime ErstelltAm { get; set; }
    
        public virtual ICollection<Buchung_Zahlung> AlleBuchung_Zahlungen { get; set; }
        public virtual Zahlungsart Zahlungsart { get; set; }
    }
}
