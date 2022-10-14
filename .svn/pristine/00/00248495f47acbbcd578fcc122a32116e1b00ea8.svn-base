using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIMEPS.Dal;
using SIMEPS.Modelo;
using System.Data;
using SIMEPS.Comun;
using System.IO;
using System.Text;

namespace SIMEPS
{
   public partial class MosaicoFin : System.Web.UI.Page
   {
      public String valorT = "";
      public int iCiclo = DateTime.Now.Year;

      protected void Page_Load(object sender, EventArgs e)
      {
         string dCiclo = iCiclo.ToString();
         IndicadoresDal simeps = new IndicadoresDal();
         Parametro parametro = simeps.ConsultarParametro("URL_REPORTE_HISTORICO_IND_FIN").FirstOrDefault();
         
         if (Request.Params["pCiclo"] != null)
         {
            odsMosaicos.SelectParameters["pCiclo"].DefaultValue = Request.Params["pCiclo"].ToString();
            iCiclo = Convert.ToInt16(Request.Params["pCiclo"].ToString());
         }
         else
         {
            odsMosaicos.SelectParameters["pCiclo"].DefaultValue = (iCiclo).ToString();
         }
         if (parametro != null)
         {
            ImgHistoricoIndFinExcel.PostBackUrl = parametro.VALOR + "Base_historica_fin_" + iCiclo + ".xls";
            ImgHistoricoIndFinCSV.PostBackUrl = parametro.VALOR + "Base_historica_fin_" + iCiclo + ".csv";
         }
      }
   }
}