using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using SIMEPS.Dal;
using SIMEPS.Modelo;
using SIMEPS.Comun;
using System.Runtime.InteropServices;
using System.Linq;

namespace SIMEPS
{
    public partial class FichaIndicadores : System.Web.UI.Page
    {
        int iFila = 1;
        int iMaximaColumna = 9;
        IndicadoresDal indicadores = new IndicadoresDal();
        ExcelUtilities comun = new ExcelUtilities();
        Logger log = new Logger();
        Microsoft.Office.Interop.Excel.Application xlApp = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            String nombreTemporalExcel = "";
            String sParametroMatriz = "";
            String sParametroRamo = "";
            String sParametroCiclo = "";
            String sParametroIndicador = "";
            String sParametroNivel = "";
            String sUniversoSimeps = "";
            String sParametroOpcion = "";

            Workbook wb = null;
            Worksheet ws = null;
            ReportesDal nuevoReporte = new ReportesDal("TD_RESOURCE_STORE");
            int resBorrar = 0;

            if (Request.Params["pIdMatriz"] != null)
                sParametroMatriz = Request.Params["pIdMatriz"];

            if (Request.Params["pRamo"] != null)
                sParametroRamo = Request.Params["pRamo"];

            if (Request.Params["pCiclo"] != null)
                sParametroCiclo = Request.Params["pCiclo"];

            if (Request.Params["pIdIndicador"] != null)
                sParametroIndicador = Request.Params["pIdIndicador"];
            else sParametroIndicador = "0";

            sUniversoSimeps = "-1";

            if (Request.Params["pNivel"] != null)
                sParametroNivel = Request.Params["pNivel"];
            else sParametroNivel = "0";

            if (Request.Params["pOpcion"] != null)
                sParametroOpcion = Request.Params["pOpcion"];

            if (!sParametroMatriz.Equals("") && !sParametroCiclo.Equals("") && !sParametroRamo.Equals(""))
            {
                try
                {
                    decimal dMatriz = Convert.ToDecimal(sParametroMatriz);
                    decimal ciclo = Convert.ToDecimal(sParametroCiclo);
                    String ramo = sParametroRamo;
                    String unidad = "0";
                    String palabraClave = "0";
                    decimal dIndicador = Convert.ToDecimal(sParametroIndicador);
                    var programasPorCicloRamo = indicadores.ConsultarPrograma(ciclo, ramo, "0", "0", Convert.ToDecimal("-1"), Convert.ToDecimal("0"), "0", "A", "1", Convert.ToDecimal("-1")).ToList();
                    SIMEPS.Modelo.Programa programa = indicadores.ConsultarPrograma(ciclo, ramo, unidad, palabraClave, dMatriz, dIndicador, sParametroNivel, null, sUniversoSimeps, -1).FirstOrDefault();
                    SIMEPS.Modelo.TipoReporte tipoReporte;
                    tipoReporte = nuevoReporte.tipoReporteDal(Convert.ToInt32(Utils.IdTiposReportes.fichaIndicadores));
                    string nombreCarpeta = tipoReporte.NOMBRE_ARCHIVO;
                    nombreTemporalExcel = GetTimestamp(DateTime.Now);
                    string nombreArchivo = "";
                    if (sParametroIndicador != "0" && sParametroNivel != "0")
                    {
                        tipoReporte = nuevoReporte.tipoReporteDal(Convert.ToInt32(Utils.IdTiposReportes.detalleFichaIndicador));
                        nombreCarpeta = tipoReporte.NOMBRE_ARCHIVO;
                        nombreArchivo = nombreCarpeta + "_" + programa.CICLO + "_" + programa.RAMO_DEP + "_" + programa.MODALIDAD + "_" + string.Format("{0:000}", programa.CLAVE) + "_" + sParametroNivel + "_" + sParametroIndicador + "." + tipoReporte.FORMATO;
                    }
                    else
                    {
                        nombreArchivo = nombreCarpeta + "_" + programa.CICLO + "_" + programa.RAMO_DEP + "_" + programa.MODALIDAD + "_" + string.Format("{0:000}", programa.CLAVE) + "_" + programa.ID_MATRIZ + "." + tipoReporte.FORMATO;
                    }

                    string rutaArchivo = "SIPS\\" + programa.CICLO + "\\" + nombreCarpeta + "\\" + programa.RAMO_DEP;

                    byte[] archivoRecuperado = null;
                    bool existeTR = false;

                    if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                    {
                        archivoRecuperado = nuevoReporte.recuperarDocumento(nombreArchivo);
                    }

                    if (sParametroOpcion.ToLower() == "genera")
                    {
                        if (Convert.ToInt32(tipoReporte.ID_TIPO_REPORTE) == Convert.ToInt32(Utils.IdTiposReportes.fichaIndicadores))
                        {
                            existeTR = nuevoReporte.buscarTR(nombreArchivo, tipoReporte.ID_TIPO_REPORTE, programa.CICLO, programa.RAMO_DEP, programa.MODALIDAD, programa.CLAVE, null, null, programa.ID_MATRIZ);
                        }
                        else
                        {
                            existeTR = nuevoReporte.buscarTR(nombreArchivo, tipoReporte.ID_TIPO_REPORTE, null, null, null, null, Convert.ToInt32(sParametroIndicador), null, null);
                        }
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
                        eliminarTemporales();
                        //Se inicializa en 9 debido a que es donde terminan los titulos principales
                        int maximaColumna = 9;
                        wb = crearWorkBook();
                        int[] contadorNiveles = { 0, 0, 0, 0 };
                        foreach (Objetivo objetivo in programa.OBJETIVOS)
                        {
                            foreach (Indicador indicador in objetivo.INDICADORES)
                            {
                                Dictionary<String, String> nombre = ObtenerNombreNivel(objetivo, contadorNiveles);
                                ws = crearWorkSheet(wb);
                                ws.Name = nombre["NombreHoja"];

                                iFila = 1;
                                agregarDatosGenerales(ws, objetivo, nombre["NombreNivel"], programa);
                                agregarHistoricoIndicador(ws, indicador);
                                agregarDetalleIndicador(ws, indicador);
                                agregarVariables(ws, indicador.VARIABLES);

                                ocultarCeldas(ws, iMaximaColumna);

                            }
                        }
                        ((Worksheet)wb.Sheets[1]).Delete();
                        ((Worksheet)wb.Sheets[1]).Activate();
                        String filename = Server.MapPath("~/Descargas") + "\\" + nombreTemporalExcel + ".xlsx";
                        wb.SaveAs(filename);
                        comun.terminarProcesoExcel(xlApp, wb);
                        if (Convert.ToInt32(tipoReporte.ID_TIPO_REPORTE) == Convert.ToInt32(Utils.IdTiposReportes.fichaIndicadores))
                        {
                            var resultado = nuevoReporte.guardarDocumento(rutaArchivo, nombreArchivo, filename, tipoReporte.ID_TIPO_REPORTE, programa.CICLO, programa.RAMO_DEP, programa.MODALIDAD, programa.CLAVE, null, null, programa.ID_MATRIZ);
                        }
                        else
                        {
                            var resultado = nuevoReporte.guardarDocumento(rutaArchivo, nombreArchivo, filename, tipoReporte.ID_TIPO_REPORTE, null, null, null, null, Convert.ToInt32(sParametroIndicador), null, null);
                        }


                        if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                        {
                            comun.descargarArchivo(Response, filename, nombreArchivo);
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




        /// <summary>
        /// Crea un libro de trabajo de excel
        /// </summary>
        /// <returns>Workbook de excel</returns>
        public Workbook crearWorkBook()
        {
            //            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            //xlApp.Visible = true;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            return xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        }

        /// <summary>
        /// Crea una nueva hoja dentro del excel
        /// </summary>
        /// <param name="wb">Libro de trabajo</param>
        /// <returns>Hoja de excel</returns>
        public Worksheet crearWorkSheet(Workbook wb)
        {
            Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.Add
            (System.Reflection.Missing.Value,
                        wb.Worksheets[wb.Worksheets.Count],
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value);
            ws.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

            ws.Shapes.AddPicture(Server.MapPath("~/img") + "\\logoConeval.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 95, 50);
            Range r = ws.Cells[1, "A"];
            r.Rows.RowHeight = 55;

            return ws;
        }

        /// <summary>
        /// Agrega los datos generales de los indicadores al excel
        /// </summary>
        /// <param name="ws">Hoja de excel donde se escribiran los datos</param>
        /// <param name="objetivo">Datos a escribir en el excel</param>
        public void agregarDatosGenerales(Worksheet ws, Objetivo objetivo, String nombreNivel, SIMEPS.Modelo.Programa programa)
        {
            //Encabezado
            iFila++;
            int iFilaInicial = iFila;

            ws.Cells[iFila, "A"] = "FICHA TÉCNICA DEL INDICADOR";
            Range aRange = ws.get_Range("A" + iFila.ToString(), "H" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange);

            //Datos generales
            iFila++;
            ws.Cells[iFila, "A"] = "Programa";
            ws.Cells[iFila, "B"] = programa.PP + " " + programa.NOMBRE;
            aRange = ws.get_Range("B" + iFila.ToString(), "H" + iFila.ToString());
            aRange.Merge();

            iFila++;
            ws.Cells[iFila, "A"] = "Nivel de Objetivo";
            ws.Cells[iFila, "B"] = nombreNivel;

            iFila++;
            ws.Cells[iFila, "B"] = objetivo.DESC_NIVEL;
            estableceEstiloDatos(ws.Cells[iFila, "B"], false, -1, false, true);
            aRange = ws.get_Range("B" + iFila, "H" + iFila);
            estableceEstiloDatos(aRange, false, 35, true, false);

            ws.Cells[iFila, "A"] = "Objetivo";

            iFila++;
            ws.Cells[iFila, "A"] = "Estatus aprobación";
            ws.Cells[iFila, "B"] = programa.DESC_APROBACION_DICTAMEN;

            int iFilaFinal = iFila;

            aRange = ws.get_Range("A" + iFilaInicial.ToString(), "A" + iFilaFinal);
            estableceEstiloEtiquetas(aRange, false, -1);

            aRange = ws.get_Range("A" + iFilaInicial, "H" + iFilaFinal);
            aRange.BorderAround2(XlLineStyle.xlContinuous, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, Type.Missing, Type.Missing);

        }

        /// <summary>
        /// Agrega los datos del detalle de un indicador al excel
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        /// <param name="detalleIndicador">Datos del detalle del indicador</param>
        public void agregarDetalleIndicador(Worksheet ws, Indicador indicador)
        {
            iFila++;
            iFila++;
            //Datos de Detalle
            ws.Cells[iFila, "A"] = "CARACTERÍSTICAS PRINCIPALES DEL INDICADOR";
            Range aRange = ws.get_Range("A" + iFila, "F" + iFila);
            aRange.Merge();
            estableceEstiloTitulo(aRange);

            iFila++;
            int iniciaRango = iFila;

            intercalarColor(ws, iFila, false, null);
            ws.Cells[iFila, "A"] = "Definición";
            estableceEstiloEtiquetas(ws.Cells[iFila, "A"], true, 50);
            ws.Cells[iFila, "B"] = indicador.DEFINICION_IND;
            aRange = ws.get_Range("B" + iFila, "F" + iFila);
            estableceEstiloDatos(aRange, true, -1, true, true);

            iFila++;
            intercalarColor(ws, iFila, false, null);
            ws.Cells[iFila, "A"] = "Método de Cálculo";
            estableceEstiloEtiquetas(ws.Cells[iFila, "A"], false, 50);
            ws.Cells[iFila, "B"] = indicador.METODO_CALCULO_IND;
            aRange = ws.get_Range("B" + iFila, "F" + iFila);
            estableceEstiloDatos(aRange, true, -1, true, true);

            iFila++;
            intercalarColor(ws, iFila, false, null);
            ws.Cells[iFila, "A"] = "Tipo Relativo";
            estableceEstiloEtiquetas(ws.Cells[iFila, "A"], true, -1);
            ws.Cells[iFila, "B"] = indicador.TIPO_RELATIVO;
            aRange = ws.get_Range("B" + iFila, "C" + iFila);
            estableceEstiloDatos(aRange, true, -1, true, false);

            ws.Cells[iFila, "D"] = "Unidad de medida";
            estableceEstiloEtiquetas(ws.Cells[iFila, "D"], true, -1);
            ws.Cells[iFila, "E"] = indicador.UNIDAD_MEDIDA;
            aRange = ws.get_Range("E" + iFila, "F" + iFila);
            estableceEstiloDatos(aRange, true, -1, true, false);

            iFila++;
            intercalarColor(ws, iFila, false, null);
            ws.Cells[iFila, "A"] = "Frecuencia de medición";
            estableceEstiloEtiquetas(ws.Cells[iFila, "A"], true, -1);
            ws.Cells[iFila, "B"] = indicador.FRECUENCIA_MEDICION;
            aRange = ws.get_Range("B" + iFila, "C" + iFila);
            estableceEstiloDatos(aRange, true, -1, true, false);

            ws.Cells[iFila, "D"] = "Sentido del indicador";
            estableceEstiloEtiquetas(ws.Cells[iFila, "D"], true, -1);
            ws.Cells[iFila, "E"] = indicador.SENTIDO_INDICADOR;
            aRange = ws.get_Range("E" + iFila, "F" + iFila);
            estableceEstiloDatos(aRange, true, -1, true, false);

            iFila++;
            intercalarColor(ws, iFila, false, null);
            ws.Cells[iFila, "A"] = "Dimensión";
            estableceEstiloEtiquetas(ws.Cells[iFila, "A"], true, -1);
            ws.Cells[iFila, "B"] = indicador.DESC_DIMENSION;
            aRange = ws.get_Range("B" + iFila, "F" + iFila);
            estableceEstiloDatos(aRange, true, -1, true, false);

            iFila++;
            intercalarColor(ws, iFila, false, null);
            ws.Cells[iFila, "A"] = "Línea base";
            estableceEstiloEtiquetas(ws.Cells[iFila, "A"], true, -1);
            ws.Cells[iFila, "B"] = indicador.LINEA_BASE;
            aRange = ws.get_Range("B" + iFila, "C" + iFila);
            estableceEstiloDatos(aRange, true, -1, true, false);

            ws.Cells[iFila, "D"] = "Año de la línea base";
            estableceEstiloEtiquetas(ws.Cells[iFila, "D"], true, -1);
            ws.Cells[iFila, "E"] = indicador.CICLO_LINEA_BASE;
            aRange = ws.get_Range("E" + iFila, "F" + iFila);
            estableceEstiloDatos(aRange, true, -1, true, false);

            iFila++;
            intercalarColor(ws, iFila, false, null);
            ws.Cells[iFila, "A"] = "Meta absoluta planeada";
            estableceEstiloEtiquetas(ws.Cells[iFila, "A"], true, -1);
            ws.Cells[iFila, "B"] = indicador.META_ABS_PLANEADA;
            aRange = ws.get_Range("B" + iFila, "C" + iFila);
            estableceEstiloDatos(aRange, true, -1, true, false);

            ws.Cells[iFila, "D"] = "Meta absoluta alcanzada";
            estableceEstiloEtiquetas(ws.Cells[iFila, "D"], true, -1);
            ws.Cells[iFila, "E"] = indicador.META_ABS_ALCANZADA;
            aRange = ws.get_Range("E" + iFila, "F" + iFila);
            estableceEstiloDatos(aRange, true, -1, true, false);

            iFila++;
            intercalarColor(ws, iFila, false, null);
            ws.Cells[iFila, "A"] = "Meta relativa planeada";
            estableceEstiloEtiquetas(ws.Cells[iFila, "A"], true, -1);
            ws.Cells[iFila, "B"] = indicador.META_REL_PLANEADA;
            aRange = ws.get_Range("B" + iFila, "C" + iFila);
            estableceEstiloDatos(aRange, true, -1, true, false);

            ws.Cells[iFila, "D"] = "Meta relativa alcanzada";
            estableceEstiloEtiquetas(ws.Cells[iFila, "D"], true, -1);
            ws.Cells[iFila, "E"] = indicador.META_REL_ALCANZADA;
            aRange = ws.get_Range("E" + iFila, "F" + iFila);
            estableceEstiloDatos(aRange, true, -1, true, false);

            //aRange = ws.get_Range("A33", "A40");
            //estableceEstiloEtiquetas(aRange,true,-1);

        }

        /// <summary>
        /// Escribe la tabla con los datos del historico de metas del indicador
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        /// <param name="historico">Datos del historico</param>
        /// <param name="ultimaColumna">Ultima columna ligada a el ultimo año del historico</param>
        /// <returns></returns>
        private void agregarHistoricoIndicador(Worksheet ws, Indicador indicador)
        {
            iFila++;
            iFila = iFila + 23;

            //Estructura para datos de la gráfica
            ws.Cells[iFila.ToString(), "A"] = "Año";
            estableceEstiloTituloHistorico(ws.Cells[iFila, "A"]);

            intercalarColor(ws, iFila + 1, true, ws.Cells[(iFila + 1).ToString(), "A"]);
            ws.Cells[(iFila + 1).ToString(), "A"] = "Meta relativa planeada";

            intercalarColor(ws, iFila + 2, true, ws.Cells[(iFila + 2).ToString(), "A"]);
            ws.Cells[(iFila + 2).ToString(), "A"] = "Meta relativa alcanzada";
            Range aRange = ws.get_Range("A" + iFila.ToString(), "A" + (iFila + 2).ToString());
            estableceEstiloEtiquetas(aRange, true, -1);

            int ultimaColumna = 2;
            int iFilaInicial = iFila;
            int iFinHistorico = 2;

            foreach (HistoricoIndicador historico in indicador.HISTORICOS)
            {
                iFila = iFilaInicial;


                ws.Cells[iFila, ultimaColumna] = historico.ANIO;
                estableceEstiloTituloHistorico(ws.Cells[iFila, ultimaColumna]);

                iFila++;
                intercalarColor(ws, iFila, true, ws.Cells[iFila, ultimaColumna]);
                ws.Cells[iFila, ultimaColumna] = historico.META_PLANEADA.Equals("-") ? "#N/A" : historico.META_PLANEADA;
                estableceEstiloDatos(ws.Cells[iFila, ultimaColumna], true, -1, false, true);

                iFila++;
                intercalarColor(ws, iFila, true, ws.Cells[iFila, ultimaColumna]);
                ws.Cells[iFila, ultimaColumna] = historico.META_ALCANZADA.Equals("-") ? "#N/A" : historico.META_ALCANZADA;
                estableceEstiloDatos(ws.Cells[iFila, ultimaColumna], true, -1, false, true);

                iFinHistorico = ultimaColumna;

                ultimaColumna++;
            }

            int iFilaFinal = iFila;

            //Rango Toda la tabla de datos
            var startCellDatos = (Range)ws.Cells[iFilaInicial, "A"];
            var endCellDatos = (Range)ws.Cells[iFilaFinal, iFinHistorico];
            Range rangeDatos = ws.get_Range(startCellDatos, endCellDatos);

            //Rango Serie 1
            var startCellSerie1 = (Range)ws.Cells[iFilaInicial + 1, "B"];
            var endCellSerie1 = (Range)ws.Cells[iFilaInicial + 1, iFinHistorico];
            Range rangeSerie1 = ws.get_Range(startCellSerie1, endCellSerie1);

            //Rango Serie 2
            var startCellSerie2 = (Range)ws.Cells[iFilaInicial + 2, "B"];
            var endCellSerie2 = (Range)ws.Cells[iFilaInicial + 2, iFinHistorico];
            Range rangeSerie2 = ws.get_Range(startCellSerie2, endCellSerie2);

            //Rango Categoria
            //var startCellCategoria = (Range)ws.Cells[iFilaInicial, "B"];
            //var endCellCategoria = (Range)ws.Cells[iFilaFinal, iFinHistorico];
            //Range rangeCategoria = ws.get_Range(startCellCategoria, endCellCategoria);

            var startCellCategoria = (Range)ws.Cells[iFilaInicial, "B"];
            var endCellCategoria = (Range)ws.Cells[iFilaInicial, columnLetter(iFinHistorico)];
            Range rangeCategoria = ws.get_Range(startCellCategoria, endCellCategoria);



            agregarGraficaIndicador(ws, rangeDatos, rangeSerie1, rangeSerie2, rangeCategoria, indicador);

            //Se usa para ocultar columnas no usadas
            if (ultimaColumna > iMaximaColumna) iMaximaColumna = ultimaColumna;
        }

        /// <summary>
        /// Agrega la grafica del indicador
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        /// <param name="rango">Rango de celdas donde esta la fuente de datos</param>
        /// <param name="indicador">Indicador</param>
        private void agregarGraficaIndicador(Worksheet ws, Range rangeDatos, Range rangeSerie1, Range rangeSerie2, Range rangeCategoria, Indicador indicador)
        {

            var charts = ws.ChartObjects() as Microsoft.Office.Interop.Excel.ChartObjects;
            var chartObject = charts.Add(60, 180, 340, 300) as Microsoft.Office.Interop.Excel.ChartObject;
            var chart = chartObject.Chart;

            chart.HasTitle = true;
            String sTitulo = "";
            if (indicador.NOMBRE_IND != null)
            {
                if (indicador.NOMBRE_IND.Length > 255)
                {
                    sTitulo = indicador.NOMBRE_IND.Substring(0, 255); ;
                }

                else
                {
                    sTitulo = indicador.NOMBRE_IND;
                }
            }


            chart.ChartTitle.Text = sTitulo;
            chart.ApplyLayout(9, chart.ChartType);
            chart.ChartStyle = 2;

            chart.ChartTitle.Font.Size = 12;
            chart.ChartTitle.Font.Italic = true;

            //chart.SetSourceData(rango);
            chart.ChartType = XlChartType.xlLineMarkers;

            SeriesCollection seriesCollection = (SeriesCollection)chart.SeriesCollection(Missing.Value);


            Series series1 = seriesCollection.NewSeries();
            series1.Values = rangeSerie1;
            series1.Name = "Meta relativa planeada";
            series1.Format.Line.ForeColor.RGB = (int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbBlue;


            Series series2 = seriesCollection.NewSeries();
            series2.Values = rangeSerie2;
            series2.Name = "Meta relativa alcanzada";
            series2.Format.Line.ForeColor.RGB = (int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbYellow;

            Axis xAxis = (Axis)chart.Axes(XlAxisType.xlCategory, XlAxisGroup.xlPrimary);
            xAxis.HasTitle = true;
            xAxis.AxisTitle.Caption = "Año";
            xAxis.CategoryNames = rangeCategoria;
            /*
            chart.HasTitle = true;
            chart.ChartTitle.Text = "Población infantil en situación de malnutrición 1.1 Prevalencia de desnutrición crónica en niños y niñas menores de 5 años; 1.2 Prevalencia de anemia en niños y niñas menores de 5 años de edad; 1.3 Prevalencia de sobrepeso y obesidad en niños y niñas de 0 - 11 años de edad".ToString();//indicador.NOMBRE_IND;
            chart.ApplyLayout(9, chart.ChartType);
            chart.ChartStyle = 2;

            chart.ChartTitle.Font.Size = 12;
            chart.ChartTitle.Font.Italic = true;
            */

            chart.Legend.Position = XlLegendPosition.xlLegendPositionBottom;
            chart.Legend.Width = 250;
            chart.Legend.Left = 40;





        }

        /// <summary>
        /// Agrega las variables del indicador al excel
        /// </summary>
        /// <param name="ws">Worksheet</param>
        /// <param name="variable">Datos de la variable a escribir</param>
        /// <param name="ultimaFila">Ultima fila que se escribio en excel y sirve para poder escribir mas de una variable</param>
        /// <returns></returns>
        public void agregarVariables(Worksheet ws, List<VariableIndicador> variables)
        {
            iFila++;
            iFila++;

            ws.Cells[iFila, "A"] = "CARACTERÍSTICAS DE LAS VARIABLES";
            Range aRange = ws.get_Range("A" + iFila.ToString(), "F" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange);

            foreach (VariableIndicador variable in variables)
            {
                iFila++;
                intercalarColor(ws, iFila, false, null);
                ws.Cells[iFila, "A"] = "Nombre de la Variable:";
                estableceEstiloEtiquetas(ws.Cells[iFila, "A"], true, 50);
                ws.Cells[iFila, "B"] = variable.NOMBRE;
                aRange = ws.get_Range("B" + iFila.ToString(), "F" + iFila.ToString());
                estableceEstiloDatos(aRange, true, -1, true, true);

                iFila++;
                intercalarColor(ws, iFila, false, null);
                ws.Cells[iFila, "A"] = "Medio de Verificación:";
                estableceEstiloEtiquetas(ws.Cells[iFila, "A"], true, 50);
                ws.Cells[iFila, "B"] = variable.DESC_MEDIO_VERIFICACION;
                aRange = ws.get_Range("B" + iFila.ToString(), "F" + iFila.ToString());
                estableceEstiloDatos(aRange, true, -1, true, true);

                iFila++;
            }

        }

        /// <summary>
        /// Genera el nombre que se dara a la hoja de excel dependiendo del nivel y el numero de objetivo
        /// </summary>
        /// <param name="nivel">Nivel</param>
        /// <param name="ObjetivoNum">Número del objetivo</param>
        /// <returns>String con el nombre de la hoja de excel</returns>
        public Dictionary<String, String> ObtenerNombreNivel(Objetivo objetivo, int[] contadorNiveles)
        {
            Dictionary<String, String> NombreNivel = new Dictionary<string, string>();
            String sNombre = "";

            switch (objetivo.NIVEL)
            {
                case 1:
                    sNombre = "Fin";
                    contadorNiveles[0]++;
                    NombreNivel.Add("NombreNivel", sNombre);
                    NombreNivel.Add("NombreHoja", sNombre + " " + Convert.ToString(contadorNiveles[0]));
                    break;
                case 2:
                    sNombre = "Propósito";
                    contadorNiveles[1]++;
                    NombreNivel.Add("NombreNivel", sNombre);
                    NombreNivel.Add("NombreHoja", sNombre + " " + Convert.ToString(contadorNiveles[1]));
                    break;
                case 3:
                    sNombre = "Componente";
                    contadorNiveles[2]++;
                    NombreNivel.Add("NombreNivel", sNombre);
                    NombreNivel.Add("NombreHoja", sNombre + " " + Convert.ToString(contadorNiveles[2]));
                    break;
                case 4:
                    sNombre = "Actividad";
                    contadorNiveles[3]++;
                    NombreNivel.Add("NombreNivel", sNombre);
                    NombreNivel.Add("NombreHoja", sNombre + " " + Convert.ToString(contadorNiveles[3]));
                    break;
            }

            return NombreNivel;
        }

        /// <summary>
        /// Se encarga de eliminar los archivos temporales creados con aterioridad
        /// </summary>
        public void eliminarTemporales()
        {
            try
            {
                DirectoryInfo source = new DirectoryInfo(Server.MapPath("~/Descargas"));
                foreach (FileInfo fi in source.GetFiles())
                {
                    var creationTime = fi.LastWriteTime;
                    if (creationTime < (DateTime.Now - new TimeSpan(0, 1, 0, 0)))
                    {
                        fi.Delete();
                    }
                }
            }
            catch (Exception e)
            {
                log.LogMessageToFile(e.StackTrace);
            }
        }

        /// <summary>
        /// Establece los estilos de tipo titulo al rango de celdas proporcionado
        /// </summary>
        /// <param name="aRange">Rango de celdas a establecer estilo</param>
        public void estableceEstiloTitulo(Range aRange)
        {
            aRange.Merge();
            aRange.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            aRange.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#14689C"));
            aRange.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            aRange.Cells.Font.Bold = true;
            aRange.VerticalAlignment = XlVAlign.xlVAlignTop;
        }

        /// <summary>
        /// Establece los estilos de tipo titulo del historico al rango de celdas proporcionado
        /// </summary>
        /// <param name="aRange">Rango de celdas a establecer estilo</param>
        public void estableceEstiloTituloHistorico(Range aRange)
        {
            aRange.Merge();
            aRange.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            aRange.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#00B050"));
            aRange.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            aRange.Cells.Font.Bold = true;
            aRange.VerticalAlignment = XlVAlign.xlVAlignTop;
        }

        /// <summary>
        /// Establece los estilos de tipo titulo al rango de celdas proporcionado
        /// </summary>
        /// <param name="aRange">Rango de celdas a establecer estilo</param>
        public void estableceEstiloEtiquetas(Range aRange, bool conBorde, int rowHeight)
        {
            aRange.Cells.Font.Bold = true;
            aRange.Columns.AutoFit();
            aRange.VerticalAlignment = XlVAlign.xlVAlignTop;
            if (rowHeight != -1) aRange.Rows.RowHeight = rowHeight;
            if (conBorde) aRange.BorderAround2(XlLineStyle.xlDot, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, Type.Missing, Type.Missing);
        }

        /// <summary>
        /// Establece los estilos de tipo titulo al rango de celdas proporcionado
        /// </summary>
        /// <param name="aRange">Rango de celdas a establecer estilo</param>
        public void estableceEstiloDatos(Range aRange, bool conBorde, int rowHeigh, bool combinar, bool wraptext)
        {
            if (combinar) aRange.Merge();
            if (wraptext) aRange.Cells.WrapText = true;
            aRange.VerticalAlignment = XlVAlign.xlVAlignTop;
            if (rowHeigh != -1) aRange.Rows.RowHeight = rowHeigh;
            if (conBorde) aRange.BorderAround2(XlLineStyle.xlDot, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, Type.Missing, Type.Missing);
        }

        /// <summary>
        /// Permite rellenar el color de las filas de forma intercalada
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        /// <param name="iNumFila">Fila a aplicar el relleno</param>
        /// <param name="historico">INdicador si es para colores del historico</param>
        /// <param name="rango">Rango de celdas que pertenecen a la fila donde se aplicara el color</param>
        public void intercalarColor(Worksheet ws, int iNumFila, bool historico, Range rango)
        {
            Range r = rango;
            if (r == null) r = ws.get_Range("A" + iNumFila, "F" + iNumFila);

            if (iNumFila % 2 != 0)
            {
                if (historico) r.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#E2EFDA"));
                else r.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#D9D9D9"));
            }
        }

        /// <summary>
        /// Devuelve la letra de la columna 
        /// </summary>
        /// <param name="column">Número de columna</param>
        /// <returns></returns>
        public String columnLetter(int column)
        {
            if (column <= 0)
                return "";
            if (column <= 26)
            {
                return (char)(column + 64) + "";
            }

            if (column % 26 == 0)
            {
                return columnLetter((column / 26) - 1) + columnLetter(26);
            }

            return columnLetter(column / 26) + columnLetter(column % 26);
        }

        /// <summary>
        /// Permite ocultar las columnas no usadas
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="maximaColumna"></param>
        public void ocultarCeldas(Worksheet ws, int maximaColumna)
        {
            ws.Cells[1, "XFD"] = "fin";
            Range last = ws.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing);
            ws.Cells[1, "XFD"] = "fin";
            Range aRange = ws.get_Range(columnLetter(maximaColumna) + "1", columnLetter(last.Column) + "1");
            aRange.Cells.EntireColumn.Hidden = true;
        }

        /// <summary>
        /// Establece el formato de tabla
        /// </summary>
        /// <param name="SourceRange">Rango de celdas al que se aplciara el formato de tabla</param>
        /// <param name="TableName">Nombre que se le asignara a la tabla</param>
        /// <param name="TableStyleName">Estilo de la tabla</param>
        public void FormatAsTable(Range SourceRange, string TableName, string TableStyleName)
        {
            SourceRange.Worksheet.ListObjects.Add(XlListObjectSourceType.xlSrcRange,
            SourceRange, System.Type.Missing, XlYesNoGuess.xlNo, System.Type.Missing).Name =
                TableName;
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects[TableName].TableStyle = TableStyleName;
        }

        /// <summary>
        /// Obtiene un string con la estampa de tiempo
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}