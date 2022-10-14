using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Modelo
{
    public class MosaicoFondoRamo33
    {
        public string SiglasFondo { get; set; }
        public string NombreFondo { get; set; }
        public FondoRamo33[] Componentes { get; set; }
        public string IconoFondo { get; internal set; }
    }
}