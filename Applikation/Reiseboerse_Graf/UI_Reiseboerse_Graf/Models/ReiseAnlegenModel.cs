using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class ReiseAnlegenModel
    {
        public int Id { get; set; }
        
        public string Titel { get; set; }

        public string Beschreibung { get; set; }

        public List<LandModel> Reiseland { get; set; }

        public int Land_id { get; set; }

        public string NeuesLand { get; set; }

        public List<OrtModel> ReiseOrt { get; set; }

        public int Ort_id { get; set; }

        public string NeuerOrt { get; set; }

        public double PreisErw { get; set; }

        public double PreisKind { get; set; }

        public List<VerpflegungModel> Verpflegung { get; set; }

        public List<UnterkunftdetailModel> Unterkunft { get; set; }

        public int Unterkunft_id { get; set; }

        public string NeueUnterkunftBeschreibung { get; set; }

        public string NeueUnterkunftBezeichnung { get; set; }

        public int NeueUnterkunftKategorie { get; set; }

        public int Verpflegung_id { get; set; }

        public DateTime StartDatum { get; set; }

        public DateTime EndDatum { get; set; }

        public DateTime Anmeldefrist { get; set; }

    }
}