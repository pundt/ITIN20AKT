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
    
    public partial class Unterkunft_Bild
    {
        public int ID { get; set; }
        public System.DateTime ErstelltAm { get; set; }
    
        public virtual Bild Bild { get; set; }
        public virtual Unterkunft Unterkunft { get; set; }
    }
}
