using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Modelo
{
    public class DetalleIndicadorRamo33
    {
        public int? IdIndicador { get; set; }
        public int CICLO { get; set; }
        public string UNIDAD_MEDIDA { get; set; }
        public string NombreIndicador { get; set; }
        public string DEFINICION_IND { get; set; }
        public string METODO_CALCULO_IND { get; set; }
        public string FRECUENCIA_MEDICION { get; set; }
        public bool? CALIF_ADECUACION { get; set; }
        public bool? CALIF_CLARIDAD { get; set; }
        public bool? CALIF_MONITOREABILIDAD { get; set; }
        public bool? CALIF_RELEVANCIA { get; set; }
        public decimal? META_ABS_ALCANZADA { get; set; }
        public decimal? META_ABS_PLANEADA { get; set; }
        public decimal? META_REL_ALCANZADA { get; set; }
        public decimal? META_REL_PLANEADA { get; set; }
        public string DescCobertura { get; set; }
        public short Version { get; set; }
        public string DESC_MEDIOS_VERIFICACION { get; set; }
    }
}