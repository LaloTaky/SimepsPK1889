using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Modelo
{
    public class Contador
    {
        public int PROGRAMAS { get; set; }
        public int INDICADORES { get; set; }
        public int PROGRAMAS_APROBADOS { get; set; }
        public Double PROMEDIO_PERMANENCIA { get; set; }
        public Double PROMEDIO_METAS { get; set; }
        public int VERSION { get; set; }
    }
}