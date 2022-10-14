using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Modelo
{
    public class HistoricoIndicador
    {
        public Nullable<long> NO { get; set; }
        public string ANIO { get; set; }
        public string META_PLANEADA { get; set; }
        public string META_ALCANZADA { get; set; }
        public bool RELATIVA { get; set; }
        public bool ABSOLUTA { get; set; }
        public bool INDICADORCOMPLEMENTARIO { get; set; }
        public Nullable<decimal> META_ABS_PLANEADA { get; set; }
        public Nullable<decimal> META_ABS_ALCANZADA { get; set; }
        public Nullable<decimal> META_REL_PLANEADA { get; set; }
        public Nullable<decimal> META_REL_ALCANZADA { get; set; }
        public string GRAFICA { get; set; }
    }
}