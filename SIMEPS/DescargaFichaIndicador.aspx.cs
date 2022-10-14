
using Microsoft.Office.Interop.Excel;
using SIMEPS.Comun;
using SIMEPS.Dal;
using SIMEPS.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIMEPS
{
    public partial class DescargaFichaIndicador : System.Web.UI.Page
    {
        public int iCiclo = DateTime.Now.Year;
        public string iMatriz = string.Empty;
        public string iIndicador = string.Empty;
        public string sComponente = string.Empty;
        List<Tuple<int, string, bool>> tFilaColor = new List<Tuple<int, string, bool>>();
        List<RangosGraficas> lGraficas = new List<RangosGraficas>();
        int iFila = 1;
        int iColumna = 1, columnaFinal = 0;
        IndicadoresDal indicadores = new IndicadoresDal();
        ExcelUtilities comun = new ExcelUtilities();
        Logger log = new Logger();
        Workbook wb = null;
        Microsoft.Office.Interop.Excel.Application xlApp = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            String nombreTemporalExcel = "";
            String sParametroMatriz = "";
            String sParametroRamo = "";
            String sParametroCiclo = "";
            String sUniversoSimeps = "";
            String sParametroOpcion = "";
            IndicadoresDal simeps = new IndicadoresDal();
            ReportesDal nuevoReporte = new ReportesDal("TD_RESOURCE_STORE");
            int resBorrar = 0;
            List<HistoricoIndicador> VALORES_GRAFICA = new List<HistoricoIndicador>();

            var oIndicador = Session["oIndicador"];
            VALORES_GRAFICA = (List<HistoricoIndicador>)Session["oHistorico"];
            List<IndicadorMapaR33> PromediosEstatales = (List<IndicadorMapaR33>)Session["oPromediosEstatales"];
            List<ValoDesepenoRamo33> DesempenoEstatales = (List<ValoDesepenoRamo33>)Session["oDesempenoEstatales"];


            if (Request.Params["idIndicador"] != null)
                iIndicador = Request.Params["idIndicador"].ToString();


            var indicador = simeps.ConsultarIndicadorRamo33(Int32.Parse(iMatriz), iCiclo, Int32.Parse(iIndicador)); //Actualizar falta matriz


            Logger log = new Logger();
            if (!sParametroMatriz.Equals("") && !sParametroCiclo.Equals(""))
            {
                try
                {
                    //nombreTemporalExcel = comun.GetTimestamp(DateTime.Now);
                    //comun.eliminarTemporales(Server.MapPath("~/Descargas"));
                    //decimal dMatriz = Convert.ToDecimal(sParametroMatriz);
                    //decimal ciclo = Convert.ToDecimal(sParametroCiclo);
                    //String ramo = sParametroRamo;
                    //String unidad = "0";
                    //String palabraClave = "0";
                    //var programasPorCicloRamo = indicadores.ConsultarPrograma(ciclo, ramo, "0", "0", Convert.ToDecimal("-1"), Convert.ToDecimal("0"), "0", "C", "1", Convert.ToDecimal("-1"));
                    //SIMEPS.Modelo.Programa programa = indicadores.ConsultarPrograma(ciclo, ramo, unidad, palabraClave, dMatriz, 0, "0", null, sUniversoSimeps, -1).FirstOrDefault();
                    //SIMEPS.Modelo.TipoReporte tipoReporte = nuevoReporte.tipoReporteDal(Convert.ToInt32(Utils.IdTiposReportes.fichaTecnicaIndicador));
                    //string nombreArchivo = "";
                    //nombreArchivo = tipoReporte.NOMBRE_ARCHIVO + "_" + programa.CICLO + "_" + programa.RAMO_DEP + "_" + programa.MODALIDAD + "_" + string.Format("{0:000}", programa.CLAVE) + "_" + programa.ID_MATRIZ + "." + tipoReporte.FORMATO;

                    //string rutaArchivo = "SIPS\\" + programa.CICLO + "\\" + tipoReporte.NOMBRE_ARCHIVO + "\\" + programa.RAMO_DEP;

                    byte[] archivoRecuperado = null;
                    //bool existeTR = false;

                    //if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                    //{
                    //    archivoRecuperado = nuevoReporte.recuperarDocumento(nombreArchivo);
                    //}

                    //if (sParametroOpcion.ToLower() == "genera")
                    //{
                    //    existeTR = nuevoReporte.buscarTR(nombreArchivo, tipoReporte.ID_TIPO_REPORTE, programa.CICLO, programa.RAMO_DEP, programa.MODALIDAD, programa.CLAVE, null, null, programa.ID_MATRIZ);
                    //}

                    //if (sParametroOpcion.ToLower() == "actualiza" || (sParametroOpcion == "genera" && !existeTR))
                    //{
                    //    nuevoReporte.BorrarReporte(nombreArchivo, tipoReporte.ID_TIPO_REPORTE);
                    //}

                    //if (archivoRecuperado != null && sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                    //{
                    //    comun.descargarArchivo(Response, archivoRecuperado, nombreArchivo);
                    //}
                    //else
                    //if (archivoRecuperado == null)
                    //{
                    //    int contadorNivel = 0;
                    //    iFila = 2;
                    //    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    //    // xlApp.Visible = true;
                    //    wb = comun.crearWorkBook(xlApp);
                    //    Worksheet ws = comun.crearWorkSheet(wb, Server.MapPath("~/img"), false, 80, 133, 79);
                    //    ws.Name = "Federal";
                    //    iFila = 10;
                    //    //Se obtiene el valor min. y max. del ciclo
                    //    List<int> lAnios = new List<int>();


                    //    foreach (Objetivo objetivo in programa.OBJETIVOS)
                    //    {

                    //        string supuestos = "";
                    //        Dictionary<String, String> propiedades = obtenerPropiedadesNivel(objetivo.NIVEL);
                    //        if (objetivo.SUPUESTOS.Count() > 0)
                    //            supuestos = objetivo.SUPUESTOS.Select(i => i.DESC_SUPUESTO).Aggregate((current, next) => current + "  " + next);

                    //        if (objetivo.NIVEL != contadorNivel)
                    //        {
                    //            //Se agrega el Nombre del Nivel
                    //            iFila++;
                    //            ws.Cells[iFila, "A"] = objetivo.NIVEL_TEXTO;
                    //            contadorNivel = objetivo.NIVEL;
                    //            tFilaColor.Add(new Tuple<int, string, bool>(iFila, propiedades["ColorTitulo"], true));
                    //        }
                    //        //Subtitulo del nivel
                    //        iFila++;
                    //        ws.Cells[iFila, "A"] = objetivo.DESC_NIVEL;
                    //        tFilaColor.Add(new Tuple<int, string, bool>(iFila, propiedades["ColorSubTitulo"], false));
                    //        foreach (Indicador indicador in objetivo.INDICADORES)
                    //        {
                    //            //Se agregan las descripciones
                    //            iFila++;
                    //            iColumna = 1;
                    //            int iFilaFinal = iFila + 4;
                    //            iFilaFinal = agregarCamposPorNivel(ws, indicador, iFilaFinal, propiedades["ColorSubTitulo"]);
                    //            iColumna++;
                    //            agregarHistoricoIndicador(ws, indicador, propiedades["ColorSubTitulo"], lAnios, propiedades["ColorGraficaS1"], propiedades["ColorGraficaS2"]);
                    //            ws.Cells[iFila, iColumna] = supuestos;
                    //            comun.estableceEstiloEtiquetas(ws.get_Range(comun.columnLetter(iColumna) + iFila, comun.columnLetter(iColumna) + iFilaFinal), true, -1, propiedades["ColorSubTitulo"], XlVAlign.xlVAlignTop, "ITC Avant Garde", 10, false, false, true, true, null, 36);
                    //            iFila = iFilaFinal;
                    //        }
                    //    }
                    //    columnaFinal = iColumna;
                    //    iFila = 2;
                    //    agregarDatosGenerales(ws, programa);
                    //    agregarTitulos(ws);
                    //    agregarGraficaPresupuestos(ws, programa.PRESUPUESTOS);
                    //    //Se pintan las graficas
                    //    if (lGraficas.Count() > 0)
                    //    {
                    //        foreach (RangosGraficas grafica in lGraficas)
                    //            comun.graficaLineal(ws, grafica.serie1, grafica.serie2, grafica.categoria, null, (grafica.pGrafica.Left + 20), (grafica.pGrafica.Top + 15), 227.5, 150, "Meta Planeada histórica", "Meta Alcanzada histórica", null, grafica.colorSerie1, grafica.colorSerie2, 11, 0, 1, XlLegendPosition.xlLegendPositionBottom, false, null, false, true, 3, 2, XlMarkerStyle.xlMarkerStyleAutomatic, false);
                    //    }
                    //    ws.Range["A:A"].ColumnWidth = 74.43;
                    //    ((Worksheet)wb.Sheets[1]).Delete();
                    //    String filename = Server.MapPath("~/Descargas") + "\\" + nombreTemporalExcel + ".xlsx";
                    //    wb.SaveAs(filename);
                    //    comun.terminarProcesoExcel(xlApp, wb);

                    //    var resultado = nuevoReporte.guardarDocumento(rutaArchivo, nombreArchivo, filename, tipoReporte.ID_TIPO_REPORTE, programa.CICLO, programa.RAMO_DEP, programa.MODALIDAD, programa.CLAVE, null, null, programa.ID_MATRIZ);

                    //    if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                    //    {
                    //        comun.descargarArchivo(Response, filename, nombreArchivo);
                    //    }
                    //}
                }
                catch (Exception error)
                {
                    if (!error.Message.Equals("Excepción de descarga de archivo."))
                    {
                        log.LogMessageToFile("Parametros: " + "Matriz=" + sParametroMatriz + " Ciclo=" + sParametroCiclo + " Ramo=" + sParametroRamo);
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

                        if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                        {
                            //Response.Write("<script language='javascript'>");
                            //Response.Write("alert('No se pudo generar correctamente el archivo.');");
                            //Response.Write("window.history.back();");
                            //Response.Write("<" + "/script>");
                        }
                    }

                }
                finally
                {
                    comun.terminarProcesoExcel(xlApp, wb);
                }
            }
        }
    }

}