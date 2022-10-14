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
    public partial class Home : System.Web.UI.Page
    {
        IndicadoresDal Indicadores = new IndicadoresDal();

        protected void Page_Load(object sender, EventArgs e)
        {

            ClientScript.RegisterStartupScript(GetType(), "Grafica", "fjsMostrarGraficaHome('IndicaClasificados','divGrafIndica','ContentPlaceHolder1_ddlTotalClasificados'); fjsMostrarGraficaHome('IndicaTotal','divGrafIndicaTotal','ContentPlaceHolder1_ddlTotalIndicadores');", true);
        }

        protected void SIPS_Click(object sender, EventArgs e)
        {
            Response.Redirect("MosaicoSips.aspx");
        }
        protected void SIPOL_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomeSIPOL.aspx");
        }
        protected void RAMO33_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomeRamo33.aspx");
        }
    }
}