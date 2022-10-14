using Microsoft.Reporting.WebForms;
using SIMEPS.Comun;
using SIMEPS.Dal;
using SIMEPS.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIMEPS
{
    public partial class DetalleIndicador : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["pIdIndicador"] == null) Response.Redirect("MosaicoSips.aspx");

            if (!IsPostBack)
            {
                ReportViewer1.ServerReport.ReportServerCredentials = new ConevalReportServerCredentials();
                ServerReport serverReport = ReportViewer1.ServerReport;
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["urlReportServer"]);
                ReportViewer1.ServerReport.ReportPath = ConfigurationManager.AppSettings["urlReportePaemirHistoricoIndicador"];
                Microsoft.Reporting.WebForms.ReportParameter indicador = new Microsoft.Reporting.WebForms.ReportParameter();
                indicador.Name = "pIdIndicador";

                if (Request.QueryString["pIdIndicador"] != null)
                {
                    indicador.Values.Add(Request.QueryString["pIdIndicador"]);
                }


                ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { indicador });
                ReportViewer1.ShowRefreshButton = false;
                ReportViewer1.ShowFindControls = false;
                ReportViewer1.ShowPageNavigationControls = false;
                ReportViewer1.ShowToolBar = false;
            }

        }

        protected void ReportViewer_OnLoad(object sender, EventArgs e)
        {

            foreach (RenderingExtension extension in ReportViewer1.ServerReport.ListRenderingExtensions())
            {
                if (extension.Name != "CSV" && extension.Name != "PDF" && extension.Name != "EXCELOPENXML")
                {
                    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    fieldInfo.SetValue(extension, false);
                }
                else
                {
                    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    fieldInfo.SetValue(extension, true);
                }
            }

        }

        //Método para escribir el encabezado de la dependencia y el ciclo
        protected void divHead_Load(object sender, EventArgs e)
        {
            addHeader();
        }
        public void addHeader()
        {
            IndicadoresDal data = new IndicadoresDal();
            decimal anio = (String.IsNullOrEmpty(Request.Params["pCiclo"]) ? 0 : Convert.ToDecimal(Request.Params["pCiclo"].ToString()));
            string ramo = (String.IsNullOrEmpty(Request.Params["pRamo"]) ? "0" : Request.Params["pRamo"].ToString());
            string unidad = (String.IsNullOrEmpty(Request.Params["pUnidad"]) ? "0" : Request.Params["pUnidad"].ToString());
            string palabraClave = "0";
            decimal dMatriz = (String.IsNullOrEmpty(Request.Params["pIdMatriz"]) ? -1 : Convert.ToDecimal(Request.Params["pIdMatriz"].ToString()));
            decimal dIndicador = (String.IsNullOrEmpty(Request.Params["pIdIndicador"]) ? 0 : Convert.ToDecimal(Request.Params["pIdIndicador"].ToString()));
            string sNivel = (String.IsNullOrEmpty(Request.Params["pNivel"]) ? "0" : Request.Params["pNivel"].ToString());
            string sPantalla = (String.IsNullOrEmpty(Request.Params["t"]) ? "0" : Request.Params["t"].ToString());
            string universoSimeps = "-1";
            decimal idIndicadorSectorial = -1;
            List<Modelo.Programa> programas = data.ConsultarPrograma(anio, ramo, unidad, palabraClave, dMatriz, dIndicador, sNivel, sPantalla, universoSimeps, idIndicadorSectorial);

            foreach (Modelo.Programa progra in programas)
            {
                LblTituloPrograma.Text = progra.NOMBRE;
                LblTituloObjetivo.Text = "Objetivo:" + (progra.OBJETIVOS.Count() > 0 ? progra.OBJETIVOS.FirstOrDefault().DESC_NIVEL : "");
                break;
            }


        }

    }
}