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
    
    public partial class Verpflegung
    {
        public Verpflegung()
        {
            this.AlleUnterkuenfte = new HashSet<Unterkunft>();
        }
    
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public System.DateTime ErstelltAm { get; set; }
    
        public virtual ICollection<Unterkunft> AlleUnterkuenfte { get; set; }
    }
}
