using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIMEPS
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected const string GENERAL_MESSAGE = "Página en Mantenimiento";
        protected const string MESSAGE_500 = "Ocurrio un error. ";
        protected const string MESSAGE_404 = "No se econtró la página solicitada. ";
        protected void Page_Load(object sender, EventArgs e)
        {
            MuestraError();
        }
        private void MuestraError()
        {
            var tipoError = Convert.ToString(Request.QueryString["tipo"]); ;
            var mje = GENERAL_MESSAGE;
            switch (tipoError)
            {
                case "500":
                    mje = MESSAGE_500;
                    break;
                case "404":
                    mje = MESSAGE_404;
                    break;
                default:
                    mje = GENERAL_MESSAGE;
                    break;
            }
            LblLeyenda.Text = mje;
        }
        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Server.Transfer("Home.aspx");
        }
    }
}