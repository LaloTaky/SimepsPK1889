using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIMEPS
{
    public partial class FichasMonitoreo : System.Web.UI.Page
   {
      public int iCiclo = 2016;

      protected void Page_Load(object sender, EventArgs e)
      {
         
         if (Request.Params["pCiclo"] != null)
         {
            iCiclo = Convert.ToInt16(Request.Params["pCiclo"]);
         }
      }
   }
}