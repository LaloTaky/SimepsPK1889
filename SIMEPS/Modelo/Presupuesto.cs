using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Modelo
{
    public class Presupuesto
    {
        public short CICLO { get; set; }
        public Nullable<decimal> IMPORTE_ORIGINAL_MDP { get; set; }
        public Nullable<decimal> IMPORTE_EJERCIDO_MDP { get; set; }
    }
}