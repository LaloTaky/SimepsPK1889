using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Modelo
{
   public class IndicadorSectorial
   {
        public string METATEXT { get; set; }
        public int ID_INDICADOR { get; set; }
      public int NUM_OBJETIVO { get; set; }
      public string OBJETIVO { get; set; }
      public string INDICADOR { get; set; }
      public int NUM_INDICADOR { get; set; }
      public string FUENTE { get; set; }
      public string TIPO { get; set; }
      public string TIPO_INDICADOR { get; set; }

        public int ID_OBJ { get; set; }
      public int ID_IND { get; set; }
      public string SUB { get; set; }
      public string LIGA { get; set; }

      public string NOMBRE { get; set; }
      public string DESCRIPCION { get; set; }
      public string METODO { get; set; }
      public string VALOR_LB { get; set; }
      public string PERIODICIDAD { get; set; }
      public string UDM { get; set; }
      public decimal? META { get; set; }
      public int ID_NIVEL { get; set; }
      public string NIVEL { get; set; }

      public int ID_MATRIZ { get; set; }
      public int CICLO { get; set; }
      public int RAMO { get; set; }
      public string Nombre_Ramo { get; set; }
      public string CAMBIO_TEXTO { get; set; }
	  
        public string META_ALCANZADA { get; set; }
        public string PORCENTAJE_AVANCE { get; set; }

        public string LB_COLOR { get; set; }
        public string MALCANZADA_COLOR { get; set; }
        public string PORCENTAJE_COLOR { get; set; }
        public string META_COLOR { get; set; }

        public bool CLARIDAD { get; set; }
        public bool RELEVANCIA { get; set; }
        public bool MONITOREABILIDAD { get; set; }
        public bool PERTINENCIA { get; set; }

        public string MAX_META_ALCANZADA { get; set; }
        public string MIN_META_ALCANZADA { get; set; }
        public string AVG_META_ALCANZADA { get; set; }
        public string MAX_META_PLANEADA { get; set; }
        public string MIN_META_PLANEADA { get; set; }
        public string AVG_META_PLANEADA { get; set; }

        public string TIPO_INDICADOR_GRAFICA { get; set; }
        public string TENDENCIA { get; set; }
        public string NIVEL_DESAGREGACION { get; set; }

        public string COMENTARIO { get; set; }

        public string ENFOQUE_RES { get; set; }
        public string ENFOQUE_INDICADOR { get; set; }
        public string ACUM_PER { get; set; }
        public string DOF_META_DESCRIPCION { get; set; }
        public string DOF_LB_DESCRIPCION { get; set; }
        public bool ADECUACION { get; set; }
    }
}