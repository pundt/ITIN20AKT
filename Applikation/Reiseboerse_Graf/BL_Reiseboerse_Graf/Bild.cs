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
    
    public partial class Bild
    {
        public Bild()
        {
            this.AlleReise_Bilder = new HashSet<Reise_Bild>();
            this.AlleUnterkunft_Bilder = new HashSet<Unterkunft_Bild>();
        }
    
        public int ID { get; set; }
        public byte[] Bilddaten { get; set; }
        public System.DateTime ErstelltAm { get; set; }
    
        public virtual ICollection<Reise_Bild> AlleReise_Bilder { get; set; }
        public virtual ICollection<Unterkunft_Bild> AlleUnterkunft_Bilder { get; set; }
    }
}
