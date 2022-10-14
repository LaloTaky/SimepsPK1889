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
    public partial class DetalleIndicadorFin : System.Web.UI.Page
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
            addHeader();
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
        
        public void addHeader()
        {
            IndicadoresDal data = new IndicadoresDal();
            int ramo = (String.IsNullOrEmpty(Request.Params["pRamo"]) ? 0 : Convert.ToInt16(Request.Params["pRamo"].ToString()));
            int anio = (String.IsNullOrEmpty(Request.Params["pCiclo"]) ? 0 : Convert.ToInt16(Request.Params["pCiclo"].ToString()));
            string unidad = (String.IsNullOrEmpty(Request.Params["pUnidad"]) ? "0" : Request.Params["pUnidad"].ToString());
            int iMatriz = (String.IsNullOrEmpty(Request.Params["pIdMatriz"]) ? 0 : Convert.ToInt32(Request.Params["pIdMatriz"].ToString()));
            Modelo.ProgramaFin progFin = data.getProgramaFinal(anio, ramo, unidad).Where(i => i.ID_MATRIZ == iMatriz).FirstOrDefault();
            if (progFin != null)
            {
               LblDependencia.Text = progFin.DEPENDENCIA;
               LblTituloPrograma.Text = progFin.NOMBRE;
               LblTituloInd.Text = progFin.NOMBRE_INDICADOR;
               LblTituloObjetivo.Text = progFin.DESC_NIVEL;
            }
        }
    }
}