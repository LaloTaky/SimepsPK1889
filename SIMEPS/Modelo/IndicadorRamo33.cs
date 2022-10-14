using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SIMEPS.Modelo
{
    public class IndicadorRamo33
    {
        public int Clave { get; internal set; }
        public int Ramo { get; internal set; }
        public string Modalidad { get; internal set; }
        public short Version { get; internal set; }
        public string Proposito { get; internal set; }
        public string DescNivel { get; internal set; }
        public string DescAmbito { get; internal set; }
        public int CantidadIndicadores { get; set; }
        public List<ComponenteRamo33> Indicadores { get; internal set; }
        public int Index { get; internal set; }
        public List<MatrizRamo33> Componentes { get; internal set; }
    }
}