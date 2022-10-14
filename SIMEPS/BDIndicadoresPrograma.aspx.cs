using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using SIMEPS.Modelo;
using System.Reflection;
using System.IO;
using SIMEPS.Dal;
using SIMEPS.Comun;
using System.Runtime.InteropServices;
using System.Linq;

namespace SIMEPS
{
    public partial class BDIndicadoresPrograma : System.Web.UI.Page
    {
        
        int iFila = 1;
        IndicadoresDal indicadores = new IndicadoresDal();
        ExcelUtilities comun = new ExcelUtilities();
        Logger log = new Logger();
        Workbook wb=null;
        Microsoft.Office.Interop.Excel.Application xlApp = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            String nombreTemporalExcel = "";
            String sParametroMatriz = "";
            String sParametroRamo = "";
            String sParametroCiclo = "";
            String sUniversoSimeps = "";
            Logger log = new Logger();


            if (Request.Params["pIdMatriz"] != null)
                sParametroMatriz = Request.Params["pIdMatriz"];

            if (Request.Params["pCiclo"] != null)
                sParametroCiclo = Request.Params["pCiclo"];

            if (Request.Params["pRamo"] != null)
                sParametroRamo = Request.Params["pRamo"];

            sUniversoSimeps = "-1";

            if (!sParametroMatriz.Equals("") && !sParametroCiclo.Equals("") && !sParametroRamo.Equals(""))
            {
                try
                {
                    nombreTemporalExcel = comun.GetTimestamp(DateTime.Now);
                    comun.eliminarTemporales(Server.MapPath("~/Descargas"));
                    decimal dMatriz = Convert.ToDecimal(sParametroMatriz);
                    decimal ciclo = Convert.ToDecimal(sParametroCiclo);
                    String ramo = sParametroRamo;
                    String unidad = "0";
                    String palabraClave = "0";
                    int[] contadorNiveles = { 0, 0, 0, 0 };
                    iFila = 2;

                    SIMEPS.Modelo.Programa programa = indicadores.ConsultarPrograma(ciclo, ramo, unidad, palabraClave, dMatriz, 0, "0", null, sUniversoSimeps, -1).FirstOrDefault();

                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    wb = comun.crearWorkBook(xlApp);
                    Worksheet ws = comun.crearWorkSheet(wb, Server.MapPath("~/img"), false, 55, 180, 100);
                    ws.Name = "Indicadores";

                    agregarEncabezados(ws, programa);

                    foreach (Objetivo objetivo in programa.OBJETIVOS)
                    {
                        foreach (Indicador indicador in objetivo.INDICADORES)
                        {
                            iFila++;
                            int iColumna = 1;

                            ws.Cells[iFila, comun.columnLetter(iColumna)] = programa.SIGLAS_DEP;
                            comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, false);

                            iColumna++;
                            ws.Cells[iFila, comun.columnLetter(iColumna)] = programa.CICLO;
                            comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, true);

                            iColumna++;
                            ws.Cells[iFila, comun.columnLetter(iColumna)] = objetivo.DESC_NIVEL;
                            comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, false);

                            iColumna++;
                            ws.Cells[iFila, comun.columnLetter(iColumna)] = comun.ObtenerNombreNivel(objetivo, contadorNiveles)["NombreNivel"];
                            comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, true);

                            iColumna++;
                            ws.Cells[iFila, comun.columnLetter(iColumna)] = indicador.NOMBRE_IND;
                            comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, false);

                            iColumna++;
                            ws.Cells[iFila, comun.columnLetter(iColumna)] = indicador.DEFINICION_IND;
                            comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, false);

                            iColumna++;
                            ws.Cells[iFila, comun.columnLetter(iColumna)] = indicador.METODO_CALCULO_IND;
                            comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, false);

                            iColumna++;
                            ws.Cells[iFila, comun.columnLetter(iColumna)] = indicador.FRECUENCIA_MEDICION;
                            comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, false);

                            iColumna++;
                            ws.Cells[iFila, comun.columnLetter(iColumna)] = indicador.UNIDAD_MEDIDA;
                            comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, false);

                            iColumna++;
                            ws.Cells[iFila, comun.columnLetter(iColumna)] = indicador.LINEA_BASE;
                            comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, false);

                            iColumna++;
                            ws.Cells[iFila, comun.columnLetter(iColumna)] = indicador.CICLO_LINEA_BASE;
                            comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, false);

                            iColumna++;
                            ws.Cells[iFila, comun.columnLetter(iColumna)] = indicador.SENTIDO_INDICADOR;
                            comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, true);

                            iColumna++;
                            ws.Cells[iFila, comun.columnLetter(iColumna)] = indicador.TIPO_RELATIVO;
                            comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, true);

                            iColumna++;

                            foreach (HistoricoIndicador historico in indicador.HISTORICOS)
                            {
                                int icolumnaHistorico = iColumna;
                                String sAnio = historico.ANIO;

                                icolumnaHistorico = comun.buscarColumna(ws, icolumnaHistorico, historico.ANIO);

                                ws.Cells[iFila, comun.columnLetter(icolumnaHistorico)] = historico.META_ABS_PLANEADA;
                                comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, false);
                                icolumnaHistorico++;

                                ws.Cells[iFila, comun.columnLetter(icolumnaHistorico)] = historico.META_ABS_ALCANZADA;
                                comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, false);
                                icolumnaHistorico++;

                                ws.Cells[iFila, comun.columnLetter(icolumnaHistorico)] = historico.META_REL_PLANEADA;
                                comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, false);
                                icolumnaHistorico++;

                                ws.Cells[iFila, comun.columnLetter(icolumnaHistorico)] = historico.META_REL_ALCANZADA;
                                comun.estableceEstiloDatos(ws.Cells[iFila, comun.columnLetter(iColumna)], true, -1, false);
                                icolumnaHistorico++;

                            }

                        }
                    }

                    ((Worksheet)wb.Sheets[1]).Delete();


                    String filename = Server.MapPath("~/Descargas") + "\\" + nombreTemporalExcel + ".xlsx";
                    wb.SaveAs(filename);

                    comun.terminarProcesoExcel(xlApp, wb);

                    comun.descargarArchivo(Response, filename, "Base descarga.xlsx");
                }
                catch (Exception error)
                {
                    if (!error.Message.Equals("Excepción de descarga de archivo."))
                    {
                        log.LogMessageToFile("Parametros: " + "Matriz=" + sParametroMatriz + " Ciclo=" + sParametroCiclo + " Ramo=" + sParametroRamo);
                        log.LogMessageToFile(error.Message);
                        log.LogMessageToFile(error.StackTrace);

                        //Response.Write("<script language='javascript'>");
                        //Response.Write("alert('No se pudo generar correctamente el archivo.');");
                        //Response.Write("window.history.back();");
                        //Response.Write("<" + "/script>");
                    }
                }
                finally
                {
                    comun.terminarProcesoExcel(xlApp, wb);
                }

            }


        }



        /// <summary>
        /// Escribe en el excel los encabezados
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        private void agregarEncabezados(Worksheet ws, SIMEPS.Modelo.Programa programa)
        {
            int iColumna = 1;

            ws.Cells[iFila, comun.columnLetter(iColumna)] = "Dependencia";
            comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

            iColumna++;
            ws.Cells[iFila, comun.columnLetter(iColumna)] = "Año";
            comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

            iColumna++;
            ws.Cells[iFila, comun.columnLetter(iColumna)] = "Objetivo";
            comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

            iColumna++;
            ws.Cells[iFila, comun.columnLetter(iColumna)] = "Nivel";
            comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

            iColumna++;
            ws.Cells[iFila, comun.columnLetter(iColumna)] = "Nombre indicador";
            comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

            iColumna++;
            ws.Cells[iFila, comun.columnLetter(iColumna)] = "Definición";
            comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

            iColumna++;
            ws.Cells[iFila, comun.columnLetter(iColumna)] = "Método Cálculo";
            comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

            iColumna++;
            ws.Cells[iFila, comun.columnLetter(iColumna)] = "Frecuencia de medición";
            comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

            iColumna++;
            ws.Cells[iFila, comun.columnLetter(iColumna)] = "Unidad de Medida";
            comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

            iColumna++;
            ws.Cells[iFila, comun.columnLetter(iColumna)] = "Linea base";
            comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

            iColumna++;
            ws.Cells[iFila, comun.columnLetter(iColumna)] = "Año linea base";
            comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

            iColumna++;
            ws.Cells[iFila, comun.columnLetter(iColumna)] = "Sentido indicador";
            comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

            iColumna++;
            ws.Cells[iFila, comun.columnLetter(iColumna)] = "Tipo relativo";
            comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);


            for (int anio = DateTime.Now.Year; anio >= 2009; anio--)
            {
                iColumna++;
                ws.Cells[iFila, comun.columnLetter(iColumna)] = "Meta absoluta planeada " + anio;
                comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

                iColumna++;
                ws.Cells[iFila, comun.columnLetter(iColumna)] = "Meta absoluta alcanzada " + anio;
                comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

                iColumna++;
                ws.Cells[iFila, comun.columnLetter(iColumna)] = "Meta relativa planeada " + anio;
                comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);

                iColumna++;
                ws.Cells[iFila, comun.columnLetter(iColumna)] = "Meta relativa alcanzada " + anio;
                comun.estableceEstiloColumnas(ws.Cells[iFila, comun.columnLetter(iColumna)], true, 60, true);
            }

            Range rHeader = ws.get_Range("A" + 1, comun.columnLetter(iColumna) + 1);
            comun.estableceEstiloHeader(rHeader, true, 100, "#203764", true);

            ws.Cells[1, "F"] = programa.NOMBRE;
            comun.estableceEstiloTitulo(ws.Cells[1, "F"], false, "", 18);
        }

    }
}