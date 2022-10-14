using SIMEPS.Comun;
using SIMEPS.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIMEPS
{
   public partial class ProgramaFin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            addHeader();
        }
         
         /// <summary>
         /// Método para pintar el header de Programas Fin
         /// </summary>
         public void addHeader()
         {
            if (HttpContext.Current.Request["pCiclo"] != null)
               LblCiclo.Text = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request["pCiclo"]);//HttpContext.Current.Request["pCiclo"];
            if (HttpContext.Current.Request["pDependencia"] != null)
               LblDependencia.Text = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request["pDependencia"]);//HttpContext.Current.Request["pDependencia"];
         }
   }
}

