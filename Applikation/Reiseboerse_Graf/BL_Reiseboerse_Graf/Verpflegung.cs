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
    
    public partial class Verpflegung
    {
        public Verpflegung()
        {
            this.AlleUnterkuenfte = new HashSet<Unterkunft>();
        }
    
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public Nullable<System.DateTime> ErstelltAm { get; set; }
    
        public virtual ICollection<Unterkunft> AlleUnterkuenfte { get; set; }
    }
}
