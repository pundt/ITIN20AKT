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
    
    public partial class Reisedatum
    {
        public Reisedatum()
        {
            this.AlleBuchungen = new HashSet<Buchung>();
            this.AlleReisedurchfuehrungen = new HashSet<Reisedurchfuehrung>();
        }
    
        public int ID { get; set; }
        public System.DateTime Startdatum { get; set; }
        public System.DateTime Enddatum { get; set; }
        public System.DateTime Anmeldefrist { get; set; }
        public System.DateTime ErstelltAm { get; set; }
    
        public virtual ICollection<Buchung> AlleBuchungen { get; set; }
        public virtual Reise Reise { get; set; }
        public virtual ICollection<Reisedurchfuehrung> AlleReisedurchfuehrungen { get; set; }
    }
}
