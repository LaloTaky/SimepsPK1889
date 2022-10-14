using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Modelo
{
   public class Programa
   {
      public string PP { get; set; }
      public string NOMBRE { get; set; }
      public int ID_MATRIZ { get; set; }
      public string UNIDAD { get; set; }
      public short CICLO { get; set; }
      public string SIGLAS_UNIDAD { get; set; }
      public string SIGLAS_DEP { get; set; }
      public string DESC_APROBACION_DICTAMEN { get; set; }
      public string OBJETIVO_ESTRATEGICO { get; set; }
      public string OBJETIVO_NACIONAL { get; set; }
      public string DESC_UNIDAD { get; set; }
      public string DESC_META { get; set; }
      public string DESC_PROGRAMA_SEC_INST { get; set; }
      public string NOMBRE_MATRIZ { get; set; }
      public string MODALIDAD { get; set; }
      public int CLAVE { get; set; }
      public int RAMO_DEP { get; set; }
      public string LIGA { get; set; }
      public string DEPENDENCIA { get; set; }
      public string OBJ_EST_DEP_ENT { get; set; }
      public string CICLO_APROBACION { get; set; }
      public string PROCESO_APROBACION { get; set; }
      public string TASAPERMANENCIA { get; set; }

      public List<Objetivo> OBJETIVOS { get; set; }
      public Nullable<byte> ID_NIVEL_APROBACION { get; set; }
      public List<Valoracion> VALORACIONES { get; set; }
      public List<Presupuesto> PRESUPUESTOS { get; set; }


   }
}