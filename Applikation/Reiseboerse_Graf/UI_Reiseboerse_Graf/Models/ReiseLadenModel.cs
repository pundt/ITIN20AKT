using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_Reiseboerse_Graf.Models
{
    public class ReiseLadenModel
    {
        public FilterModel Filter { get; set; }
        public IEnumerable<UI_Reiseboerse_Graf.Models.ReiseModel> Reisen{ get; set; }
    }
}