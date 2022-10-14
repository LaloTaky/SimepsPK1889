using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Modelo
{
   public class ProgramaSectorial
   {
      public int ID_PROG_SECTORIAL { get; set; }
	  public int ID_SECTOR { get; set; }
      public int CLASIFICACION { get; set; }
      public string NOMBRE { get; set; }
      public string URL_ICONO { get; set; }
      public string LIGA { get; set; }
      public int CONSECUTIVO { get; set; }
      public string EVALUACIONDESC { get; set; }
      public string EVALUACIONLLIGA { get; set; }
      public string EVALUACIONIMAGEN { get; set; }
      public string NOMBRESECTOR { get; set; }
   }
}