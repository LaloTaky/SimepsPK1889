using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SIMEPS.Modelo
{
    
    public class ComponenteRamo33
    {
        public ComponenteRamo33()
        {
            VALORES_GRAFICA = new List<HistoricoIndicador>();
        }

        public short? Ciclo { get; set; }
        public int? IdIndicador { get; set; }
        public string NombreIndicador { get; set; }
        public int IdMatriz { get; internal set; }
        public decimal? Meta_Abs_Alcanzada { get; internal set; }
        public decimal? Meta_Abs_Planeada { get; internal set; }
        public decimal? Meta_Rel_Alcanzada { get; internal set; }
        public decimal? Meta_Rel_Planeada { get; internal set; }
        public string Metodo_Calculo_Ind { get; internal set; }
        public string Modalidad { get; internal set; }
        public string Unidad_Medida { get; internal set; }
        public short Version { get; internal set; }
        public string Url { get; internal set; }
        public List<HistoricoIndicador> VALORES_GRAFICA { get; set; }
        public List<IndicadorMapaR33> PromediosEstatales { get; set; }

        public List<ValoDesepenoRamo33> DesempenoEstatales { get; set; }
        public int Index { get; internal set; }
        public string DescCobertura { get; internal set; }
    }
}