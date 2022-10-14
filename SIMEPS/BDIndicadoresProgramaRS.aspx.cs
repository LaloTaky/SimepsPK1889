using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIMEPS.Comun;
using SIMEPS.Dal;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace SIMEPS
{
    public partial class BDIndicadoresProgramaRS : System.Web.UI.Page
    {
        Utils utils = new Utils();
        IndicadoresDal indicadores = new IndicadoresDal();
        Logger log = new Logger();
        protected void Page_Load(object sender, EventArgs e)
        {
            String sParametroMatriz = "";
            String sParametroRamo = "";
            String sParametroCiclo = "";
            String sParametroPantalla = "";

            //==
            String nombreTemporalExcel = "";
            String sParametroIndicador = "";
            String sParametroNivel = "";
            String sUniversoSimeps = "";
            String sParametroTipoReporte = "";
            String sParametroOpcion = "";
            ExcelUtilities comun = new ExcelUtilities();
            ReportesDal nuevoReporte = new ReportesDal("TD_RESOURCE_STORE");
            int resBorrar = 0;
            if (Request.Params["pIdMatriz"] != null)
                sParametroMatriz = Request.Params["pIdMatriz"];

            if (Request.Params["pCiclo"] != null)
                sParametroCiclo = Request.Params["pCiclo"];

            if (Request.Params["pRamo"] != null)
                sParametroRamo = Request.Params["pRamo"];

            if (Request.Params["pPantalla"] != null)
                sParametroPantalla = Request.Params["pPantalla"];

            //====
            if (Request.Params["pIdIndicador"] != null)
                sParametroIndicador = Request.Params["pIdIndicador"];
            else sParametroIndicador = "0";


            sUniversoSimeps = "-1";

            if (Request.Params["pNivel"] != null)
                sParametroNivel = Request.Params["pNivel"];

            if (Request.Params["pOpcion"] != null)
                sParametroOpcion = Request.Params["pOpcion"];

            if (!sParametroMatriz.Equals("") && !sParametroCiclo.Equals("") && !sParametroRamo.Equals(""))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        if (sParametroPantalla == "PROGRAMA")
                        {
                            decimal dMatriz = Convert.ToDecimal(sParametroMatriz);
                            decimal ciclo = Convert.ToDecimal(sParametroCiclo);
                            String ramo = sParametroRamo;
                            String unidad = "0";
                            String palabraClave = "0";
                            decimal dIndicador = Convert.ToDecimal(sParametroIndicador);
                            var programasPorCicloRamo = indicadores.ConsultarPrograma(ciclo, ramo, "0", "0", Convert.ToDecimal("-1"), Convert.ToDecimal("0"), "0", "A", "1", Convert.ToDecimal("-1")).ToList();
                            SIMEPS.Modelo.Programa programa = indicadores.ConsultarPrograma(ciclo, ramo, unidad, palabraClave, dMatriz, dIndicador, sParametroNivel, null, sUniversoSimeps, -1).FirstOrDefault();
                            SIMEPS.Modelo.TipoReporte tipoReporte = nuevoReporte.tipoReporteDal(Convert.ToInt32(Utils.IdTiposReportes.baseIndicadores));
                            string nombreArchivo = "";
                            nombreArchivo = tipoReporte.NOMBRE_ARCHIVO + "_" + programa.CICLO + "_" + programa.RAMO_DEP + "_" + programa.MODALIDAD + "_" + string.Format("{0:000}", programa.CLAVE == null ? 0 : programa.CLAVE) + "_" + programa.ID_MATRIZ + "." + tipoReporte.FORMATO;

                            string rutaArchivo = "SIPS\\" + programa.CICLO + "\\" + tipoReporte.NOMBRE_ARCHIVO + "\\" + programa.RAMO_DEP;

                            byte[] archivoRecuperado = null;
                            bool existeTR = false;

                            if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                            {
                                archivoRecuperado = nuevoReporte.recuperarDocumento(nombreArchivo);
                            }

                            if (sParametroOpcion.ToLower() == "genera")
                            {
                                existeTR = nuevoReporte.buscarTR(nombreArchivo, tipoReporte.ID_TIPO_REPORTE, programa.CICLO, programa.RAMO_DEP, programa.MODALIDAD, programa.CLAVE, null, null, programa.ID_MATRIZ);
                            }

                            if (sParametroOpcion.ToLower() == "actualiza" || (sParametroOpcion == "genera" && !existeTR))
                            {
                                nuevoReporte.BorrarReporte(nombreArchivo, tipoReporte.ID_TIPO_REPORTE);
                            }

                            if (archivoRecuperado != null && sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                            {
                                comun.descargarArchivo(Response, archivoRecuperado, nombreArchivo);
                            }
                            else if (archivoRecuperado == null && !existeTR)
                            {
                                ReportViewer1.ServerReport.ReportServerCredentials = new ConevalReportServerCredentials();
                                ServerReport serverReport = ReportViewer1.ServerReport;
                                ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["urlReportServer"]);
                                ReportViewer1.ServerReport.ReportPath = ConfigurationManager.AppSettings["urlReportePaemirBDIndicadores"];
                                List<ReportParameter> paramList = new List<ReportParameter>();

                                paramList.Add(new ReportParameter("pCiclo", Request.QueryString["pCiclo"]));
                                paramList.Add(new ReportParameter("pRamo", Request.QueryString["pRamo"]));
                                paramList.Add(new ReportParameter("pIdMatriz", Request.QueryString["pIdMatriz"]));
                                paramList.Add(new ReportParameter("pTipoExtension", "1"));
                                paramList.Add(new ReportParameter("pPantalla", sParametroPantalla));

                                ReportViewer1.ServerReport.Timeout = 1200000;
                                ReportViewer1.ServerReport.SetParameters(paramList);
                                utils.DescargaArchivoRS(Response, ReportViewer1, "Excel", tipoReporte.FORMATO, "", rutaArchivo, nombreArchivo, tipoReporte.ID_TIPO_REPORTE, programa.CICLO, programa.RAMO_DEP, programa.MODALIDAD, programa.CLAVE, sParametroOpcion, null, null, programa.ID_MATRIZ);

                            }
                        }
                        else if (sParametroPantalla == "FIN")
                        {
                            decimal ciclo = Convert.ToDecimal(sParametroCiclo);
                            SIMEPS.Modelo.TipoReporte tipoReporte = nuevoReporte.tipoReporteDal(Convert.ToInt32(Utils.IdTiposReportes.baseIndicadoresFin));
                            string nombreArchivo = tipoReporte.NOMBRE_ARCHIVO + "_" + ciclo + "." + tipoReporte.FORMATO;
                            string rutaArchivo = "SIPOL\\" + tipoReporte.NOMBRE_ARCHIVO;

                            byte[] archivoRecuperado = null;
                            bool existeTR = false;


                            if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                            {
                                archivoRecuperado = nuevoReporte.recuperarDocumento(nombreArchivo);
                            }

                            if (sParametroOpcion.ToLower() == "genera")
                            {
                                existeTR = nuevoReporte.buscarTR(nombreArchivo, tipoReporte.ID_TIPO_REPORTE, Convert.ToInt16(ciclo), null, null, null, null, null, null);
                            }

                            if (sParametroOpcion.ToLower() == "actualiza" || (sParametroOpcion == "genera" && !existeTR))
                            {
                                nuevoReporte.BorrarReporte(nombreArchivo, tipoReporte.ID_TIPO_REPORTE);
                            }

                            if (archivoRecuperado != null && sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                            {
                                comun.descargarArchivo(Response, archivoRecuperado, nombreArchivo);
                            }
                            else if (archivoRecuperado == null && !existeTR)
                            {
                                ReportViewer1.ServerReport.ReportServerCredentials = new ConevalReportServerCredentials();
                                ServerReport serverReport = ReportViewer1.ServerReport;
                                ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["urlReportServer"]);
                                ReportViewer1.ServerReport.ReportPath = ConfigurationManager.AppSettings["urlReportePaemirBDIndicadores"];
                                List<ReportParameter> paramList = new List<ReportParameter>();

                                paramList.Add(new ReportParameter("pCiclo", Request.QueryString["pCiclo"]));
                                paramList.Add(new ReportParameter("pRamo", Request.QueryString["pRamo"]));
                                paramList.Add(new ReportParameter("pIdMatriz", Request.QueryString["pIdMatriz"]));
                                paramList.Add(new ReportParameter("pTipoExtension", "1"));
                                paramList.Add(new ReportParameter("pPantalla", sParametroPantalla));

                                ReportViewer1.ServerReport.Timeout = 1200000;
                                ReportViewer1.ServerReport.SetParameters(paramList);
                                utils.DescargaArchivoRS(Response, ReportViewer1, "Excel", tipoReporte.FORMATO, "", rutaArchivo, nombreArchivo, tipoReporte.ID_TIPO_REPORTE, Convert.ToInt16(ciclo), null, null, null, sParametroOpcion, null, null, null);

                                if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                                {
                                    //Response.Write("<script language='javascript'>");
                                    //Response.Write("alert('No se pudo generar correctamente el archivo.');");
                                    //Response.Write("window.history.back();");
                                    //Response.Write("<" + "/script>");
                                }
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        if (!error.Message.Equals("Excepción de descarga de archivo."))
                        {
                            log.LogMessageToFile("Parametros: " + "Matriz=" + sParametroMatriz + " Ciclo=" + sParametroCiclo + " Ramo=" + sParametroRamo + " Indicador=" + sParametroIndicador);
                            log.LogMessageToFile(error.Message);
                            log.LogMessageToFile(error.StackTrace);

                            if (sParametroOpcion.ToLower() == "actualiza" || sParametroOpcion.ToLower() == "genera")
                            {
                                Response.Clear();
                                Response.ClearHeaders();
                                Response.ClearContent();
                                Response.HeaderEncoding = System.Text.Encoding.Default;
                                Response.Charset = "ISO-8859-15";
                                Response.Status = "500 INTERNAL SERVER ERROR";
                                Response.StatusCode = 500;
                                Response.StatusDescription = "INTERNAL SERVER ERROR";
                                Response.End();
                            }
                        }
                    }
                }
            }
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}