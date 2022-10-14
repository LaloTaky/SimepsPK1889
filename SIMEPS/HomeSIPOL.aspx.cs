using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIMEPS.Dal;
using SIMEPS.Modelo;

namespace SIMEPS
{
    public partial class HomeSIPOL : System.Web.UI.Page
    {
        protected void ModuloSIPOL_Clic(object sender, EventArgs e)
        {

            Response.Redirect("MosaicoSectores.aspx");
        }

        protected void ModuloMetasNacionales_Clic(object sender, EventArgs e)
        {

            Response.Redirect("MetasNacionales.aspx");
        }

        protected void ModuloSIPOL4T_Clic(object sender, EventArgs e)
        {

            Response.Redirect("MosaicoSectores19-24.aspx");
        }
    }
    
}