using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Modelo
{
    public class Objetivo
    {
        public Nullable<long> ID { get; set; }
        public string DESC_NIVEL { get; set; }
        public int ID_NIVEL { get; set; }
        public List<Indicador> INDICADORES { get; set; }
        public int NIVEL { get; set; }
        public String NIVEL_TEXTO { get; set; }
        public List<Supuesto> SUPUESTOS { get; set; }
        public int ID_PARENT { get; set; }
        public decimal NUMERACION { get; set; }
    }
}