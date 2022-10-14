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
using System.Drawing;
using System.ComponentModel;

namespace SIMEPS
{
    public partial class DescargaMIR : System.Web.UI.Page
    {
        //Fila, Color(Hex), titulo o subtitulo
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
            ReportesDal nuevoReporte = new ReportesDal("TD_RESOURCE_STORE");
            int resBorrar = 0;

            if (Request.Params["pIdMatriz"] != null)
                sParametroMatriz = Request.Params["pIdMatriz"];

            if (Request.Params["pRamo"] != null)
                sParametroRamo = Request.Params["pRamo"];

            if (Request.Params["pCiclo"] != null)
                sParametroCiclo = Request.Params["pCiclo"];

            if (Request.Params["pOpcion"] != null)
                sParametroOpcion = Request.Params["pOpcion"];

            sUniversoSimeps = "-1";


            Logger log = new Logger();
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
                    var programasPorCicloRamo = indicadores.ConsultarPrograma(ciclo, ramo, "0", "0", Convert.ToDecimal("-1"), Convert.ToDecimal("0"), "0", "C", "1", Convert.ToDecimal("-1"));
                    SIMEPS.Modelo.Programa programa = indicadores.ConsultarPrograma(ciclo, ramo, unidad, palabraClave, dMatriz, 0, "0", null, sUniversoSimeps, -1).FirstOrDefault();
                    SIMEPS.Modelo.TipoReporte tipoReporte = nuevoReporte.tipoReporteDal(Convert.ToInt32(Utils.IdTiposReportes.mir));
                    string nombreArchivo = "";
                    nombreArchivo = tipoReporte.NOMBRE_ARCHIVO + "_" + programa.CICLO + "_" + programa.RAMO_DEP + "_" + programa.MODALIDAD + "_" + string.Format("{0:000}", programa.CLAVE) + "_" + programa.ID_MATRIZ + "." + tipoReporte.FORMATO;

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
                        int contadorNivel = 0;
                        iFila = 2;
                        xlApp = new Microsoft.Office.Interop.Excel.Application();
                        // xlApp.Visible = true;
                        wb = comun.crearWorkBook(xlApp);
                        Worksheet ws = comun.crearWorkSheet(wb, Server.MapPath("~/img"), false, 80, 133, 79);
                        ws.Name = "MIR";
                        iFila = 10;
                        //Se obtiene el valor min. y max. del ciclo
                        List<int> lAnios = new List<int>();
                        int anioMin = Int16.Parse(programa.OBJETIVOS.SelectMany(a => a.INDICADORES).SelectMany(b => b.HISTORICOS).Select(c => c.ANIO.Replace("*", "")).Min());
                        int anioMax = Int16.Parse(programa.OBJETIVOS.SelectMany(a => a.INDICADORES).SelectMany(b => b.HISTORICOS).Select(c => c.ANIO.Replace("*", "")).Max());
                        for (int i = anioMin; i <= anioMax; i++)
                            lAnios.Add(i);

                        foreach (Objetivo objetivo in programa.OBJETIVOS)
                        {

                            string supuestos = "";
                            Dictionary<String, String> propiedades = obtenerPropiedadesNivel(objetivo.NIVEL);
                            if (objetivo.SUPUESTOS.Count() > 0)
                                supuestos = objetivo.SUPUESTOS.Select(i => i.DESC_SUPUESTO).Aggregate((current, next) => current + "  " + next);

                            if (objetivo.NIVEL != contadorNivel)
                            {
                                //Se agrega el Nombre del Nivel
                                iFila++;
                                ws.Cells[iFila, "A"] = objetivo.NIVEL_TEXTO;
                                contadorNivel = objetivo.NIVEL;
                                tFilaColor.Add(new Tuple<int, string, bool>(iFila, propiedades["ColorTitulo"], true));
                            }
                            //Subtitulo del nivel
                            iFila++;
                            ws.Cells[iFila, "A"] = objetivo.DESC_NIVEL;
                            tFilaColor.Add(new Tuple<int, string, bool>(iFila, propiedades["ColorSubTitulo"], false));
                            foreach (Indicador indicador in objetivo.INDICADORES)
                            {
                                //Se agregan las descripciones
                                iFila++;
                                iColumna = 1;
                                int iFilaFinal = iFila + 4;
                                iFilaFinal = agregarCamposPorNivel(ws, indicador, iFilaFinal, propiedades["ColorSubTitulo"]);
                                iColumna++;
                                agregarHistoricoIndicador(ws, indicador, propiedades["ColorSubTitulo"], lAnios, propiedades["ColorGraficaS1"], propiedades["ColorGraficaS2"]);
                                ws.Cells[iFila, iColumna] = supuestos;
                                comun.estableceEstiloEtiquetas(ws.get_Range(comun.columnLetter(iColumna) + iFila, comun.columnLetter(iColumna) + iFilaFinal), true, -1, propiedades["ColorSubTitulo"], XlVAlign.xlVAlignTop, "ITC Avant Garde", 10, false, false, true, true, null, 36);
                                iFila = iFilaFinal;
                            }
                        }
                        columnaFinal = iColumna;
                        iFila = 2;
                        agregarDatosGenerales(ws, programa);
                        agregarTitulos(ws);
                        agregarGraficaPresupuestos(ws, programa.PRESUPUESTOS);
                        //Se pintan las graficas
                        if (lGraficas.Count() > 0)
                        {
                            foreach (RangosGraficas grafica in lGraficas)
                                comun.graficaLineal(ws, grafica.serie1, grafica.serie2, grafica.categoria, null, (grafica.pGrafica.Left + 20), (grafica.pGrafica.Top + 15), 227.5, 150, "Meta Planeada histórica", "Meta Alcanzada histórica", null, grafica.colorSerie1, grafica.colorSerie2, 11, 0, 1, XlLegendPosition.xlLegendPositionBottom, false, null, false, true, 3, 2, XlMarkerStyle.xlMarkerStyleAutomatic, false);
                        }
                        ws.Range["A:A"].ColumnWidth = 74.43;
                        ((Worksheet)wb.Sheets[1]).Delete();
                        String filename = Server.MapPath("~/Descargas") + "\\" + nombreTemporalExcel + ".xlsx";
                        wb.SaveAs(filename);
                        comun.terminarProcesoExcel(xlApp, wb);

                        var resultado = nuevoReporte.guardarDocumento(rutaArchivo, nombreArchivo, filename, tipoReporte.ID_TIPO_REPORTE, programa.CICLO, programa.RAMO_DEP, programa.MODALIDAD, programa.CLAVE, null, null, programa.ID_MATRIZ);

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
        /// <summary>
        /// Agregar la gráfica de presupuestos en la descripción general del Excel
        /// </summary>
        /// <param name="ws">Worksheet</param>
        /// <param name="presupuestos">Listado de los presupuestos que se pintarán como seríe en las gráficas</param>
        public void agregarGraficaPresupuestos(Worksheet ws, List<Presupuesto> presupuestos)
        {
            int Count = presupuestos.Count;
            object[] aAprobado = new object[Count];
            object[] aEjercido = new object[Count];
            object[] aCiclos = new object[Count];
            if (Count > 0)
            {
                for (int i = 0; i < presupuestos.Count; i++)
                {
                    aAprobado[i] = Convert.ToInt32(presupuestos[i].IMPORTE_ORIGINAL_MDP);
                    aEjercido[i] = Convert.ToInt32(presupuestos[i].IMPORTE_EJERCIDO_MDP);
                    aCiclos[i] = Convert.ToInt32(presupuestos[i].CICLO);
                }
            }
            int columna = 0;
            string letter = comun.columnLetter(columnaFinal);
            if (letter == "J")
                columna = columnaFinal - 2;
            else
                columna = columnaFinal - 4;
            Range rango = ws.get_Range(comun.columnLetter(columna) + 2, comun.columnLetter(columna) + 2);
            int cAprobado = Color.FromArgb(213, 155, 91).ToArgb();
            int cEjercido = Color.FromArgb(49, 125, 237).ToArgb();
            comun.graficaLineal(ws, aAprobado, aEjercido, aCiclos, "Presupuesto del programa", rango.Left + 20, rango.Top + 20, 305, 180, "Presupuesto Aprobado", "Presupuesto Ejercido", null, cAprobado, cEjercido, 2, 18, 9, XlLegendPosition.xlLegendPositionBottom, false, XlLineStyle.xlContinuous, false, true, 20, 2, XlMarkerStyle.xlMarkerStyleCircle, false);
            agregarEstilosFilasNiveles(ws);
        }
        /// <summary>
        /// Se agrega el texto del Indicador en su respectiva columna
        /// </summary>
        /// <param name="ws">Worksheet</param>
        /// <param name="indicador">Objeto de tipo Indicador</param>
        /// <param name="iFilaFinal">útlima fila donde se pintará cada Indicador</param>
        /// <param name="colorSubTitulo">Color del subtitulo por Nivel</param>
        private int agregarCamposPorNivel(Worksheet ws, Indicador indicador, int iFilaFinal, string colorSubTitulo)
        {
            Range aRange = null;


            comun.estableceEstiloEtiquetas(ws.get_Range(comun.columnLetter(iColumna) + iFila, comun.columnLetter(iColumna) + iFilaFinal), true, -1, colorSubTitulo, XlVAlign.xlVAlignTop, "ITC Avant Garde", 10, false, false, true, true, XlHAlign.xlHAlignCenter, 45);

            iColumna++;
            ws.Cells[iFila, (iColumna)] = indicador.NOMBRE_IND;
            comun.estableceEstiloEtiquetas(ws.get_Range(comun.columnLetter(iColumna) + iFila, comun.columnLetter(iColumna) + iFilaFinal), true, -1, colorSubTitulo, XlVAlign.xlVAlignTop, "ITC Avant Garde", 10, false, false, true, true, null, 26);

            iColumna++;
            ws.Cells[iFila, iColumna] = indicador.DEFINICION_IND;
            comun.estableceEstiloEtiquetas(ws.get_Range(comun.columnLetter(iColumna) + iFila, comun.columnLetter(iColumna) + iFilaFinal), true, -1, colorSubTitulo, XlVAlign.xlVAlignTop, "ITC Avant Garde", 10, false, false, true, true, null, 30);

            iColumna++;
            ws.Cells[iFila, iColumna] = indicador.METODO_CALCULO_IND;
            comun.estableceEstiloEtiquetas(ws.get_Range(comun.columnLetter(iColumna) + iFila, comun.columnLetter(iColumna) + iFilaFinal), true, -1, colorSubTitulo, XlVAlign.xlVAlignTop, "ITC Avant Garde", 10, false, false, true, true, null, 36);

            //Detalle de Indicador titulos
            iColumna++;
            int fFinal = iFila;
            ws.Cells[fFinal, iColumna] = "Unidad de Medida";
            fFinal++;
            ws.Cells[fFinal, iColumna] = "Sentido Indicador";
            fFinal++;
            ws.Cells[fFinal, iColumna] = "Dimensión del Indicador";
            fFinal++;
            ws.Cells[fFinal, iColumna] = "Frecuencia de Medición";
            fFinal++;
            ws.Cells[fFinal, iColumna] = "Línea base";
            aRange = ws.get_Range(comun.columnLetter(iColumna) + iFila, comun.columnLetter(iColumna) + fFinal);
            comun.estableceEstiloEtiquetas(aRange, true, 35, colorSubTitulo, XlVAlign.xlVAlignCenter, "ITC Avant Garde", 10, true, false, false, false, null, 27);

            //Detalle Indicador Datos
            iColumna++;
            fFinal = iFila;
            ws.Cells[fFinal, iColumna] = indicador.UNIDAD_MEDIDA;
            fFinal++;
            ws.Cells[fFinal, iColumna] = indicador.SENTIDO_INDICADOR;
            fFinal++;
            ws.Cells[fFinal, iColumna] = indicador.DESC_DIMENSION;
            fFinal++;
            ws.Cells[fFinal, iColumna] = indicador.FRECUENCIA_MEDICION;
            fFinal++;
            ws.Cells[fFinal, iColumna] = indicador.LINEA_BASE;

            aRange = ws.get_Range(comun.columnLetter(iColumna) + iFila, comun.columnLetter(iColumna) + fFinal);
            comun.estableceEstiloEtiquetas(aRange, true, 35, colorSubTitulo, XlVAlign.xlVAlignCenter, "ITC Avant Garde", 10, false, false, false, false, XlHAlign.xlHAlignLeft, 18);

            iColumna++;
            //Se concatena los medios de verificación y los muestra en la misma celda
            string mediosVerificacion = "";
            if (indicador.VARIABLES.Count() > 0)
                mediosVerificacion = indicador.VARIABLES.Select(i => i.DESC_MEDIO_VERIFICACION).Aggregate((current, next) => current + "  " + next);
            ws.Cells[iFila, iColumna] = mediosVerificacion;
            comun.estableceEstiloEtiquetas(ws.get_Range(comun.columnLetter(iColumna) + iFila, comun.columnLetter(iColumna) + iFilaFinal), true, -1, colorSubTitulo, XlVAlign.xlVAlignTop, "ITC Avant Garde", 10, false, false, true, true, null, 25);
            return iFilaFinal;

        }
        /// <summary>
        /// Agrega los estilos a las Filas de los Niveles, Fin, propósito, componente y actividad
        /// </summary>
        /// <param name="ws">worksheet</param>
        private void agregarEstilosFilasNiveles(Worksheet ws)
        {
            foreach (var tuple in tFilaColor)
            {
                Range aRange = ws.get_Range("A" + tuple.Item1, comun.columnLetter(columnaFinal) + tuple.Item1);
                aRange.Merge();
                comun.estableceEstiloHeader(aRange, false, -1, tuple.Item2, false);
                if (tuple.Item3)
                    comun.estableceEstiloTitulo(aRange, true, "ITC Avant Garde", 11);
                else
                    estableceEstiloSubTitulo(aRange, true, "ITC Avant Garde", 11);
            }
        }

        /// <summary>
        /// Agrega el histórico del indicador de forma dinámica
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        /// <param name="indicador">Objeto Indicador</param>
        /// <param name="colorBorde">Color del Borde del Nivel</param>
        /// <param name="lAnios">Lista de los rangos de años que se pintarán en el histórico</param>
        /// <param name="colorSerie1">Color de la gráfica para la serie 1</param>
        /// <param name="colorSerie2">Color de la gráfica para la serie 2</param>        
        /// <param name="lAnios">Rango de los años de los Indicadores</param>
        private void agregarHistoricoIndicador(Worksheet ws, Indicador indicador, string colorBorde, List<int> lAnios, string colorSerie1, string colorSerie2)
        {
            int filaInicio = iFila;
            int pColumna = iColumna;
            string metaTipo = "";
            if (indicador != null)
                metaTipo = indicador.HISTORICOS.FirstOrDefault().RELATIVA ? "Relativo" : "Absoluta";

            ws.Cells[iFila, iColumna] = "Tipo de valor de la meta";
            ws.Cells[iFila + 1, iColumna] = "Año";
            ws.Cells[iFila + 2, iColumna] = "Meta planeada histórica";
            ws.Cells[iFila + 3, iColumna] = "Meta alcanzada histórica";
            Range rango = ws.get_Range(comun.columnLetter(pColumna) + (iFila), comun.columnLetter(pColumna) + (iFila + 3));
            comun.estableceEstiloEtiquetas(rango, true, -1, colorBorde, XlVAlign.xlVAlignBottom, "ITC Avant Garde", 10, true, false, true, false, XlHAlign.xlHAlignLeft, 19);
            iColumna++;

            ws.Cells[filaInicio, iColumna] = metaTipo;
            if (lAnios.Count() > 0)
            {
                for (int i = 0; i < lAnios.Count(); i++)
                {
                    HistoricoIndicador historico = indicador.HISTORICOS.Where(h => h.ANIO == lAnios[i].ToString()).FirstOrDefault();
                    filaInicio = iFila + 1;
                    if (historico != null)
                    {
                        ws.Cells[filaInicio, iColumna] = historico.ANIO;
                        comun.estableceEstiloEtiquetas(ws.Cells[filaInicio, iColumna], true, -1, colorBorde, XlVAlign.xlVAlignBottom, "ITC Avant Garde", 10, true, false, false, false, null, 6);

                        filaInicio++;
                        ws.Cells[filaInicio, iColumna] = historico.META_PLANEADA.Equals("-") ? "#N/A" : historico.META_PLANEADA;
                        comun.estableceEstiloEtiquetas(ws.Cells[filaInicio, iColumna], true, -1, colorBorde, XlVAlign.xlVAlignBottom, "ITC Avant Garde", 10, false, false, false, false, XlHAlign.xlHAlignRight, 6);

                        filaInicio++;
                        ws.Cells[filaInicio, iColumna] = historico.META_ALCANZADA.Equals("-") ? "#N/A" : historico.META_ALCANZADA;
                        comun.estableceEstiloEtiquetas(ws.Cells[filaInicio, iColumna], true, -1, colorBorde, XlVAlign.xlVAlignBottom, "ITC Avant Garde", 10, false, false, false, false, XlHAlign.xlHAlignRight, 6);
                    }
                    else
                    {
                        comun.estableceEstiloEtiquetas(ws.Cells[filaInicio, iColumna], true, -1, colorBorde, XlVAlign.xlVAlignBottom, "ITC Avant Garde", 10, true, false, false, false, XlHAlign.xlHAlignRight, 6);
                        ws.Cells[filaInicio, iColumna] = lAnios[i];

                        filaInicio++;
                        ws.Cells[filaInicio, iColumna] = "#N/A";
                        comun.estableceEstiloEtiquetas(ws.Cells[filaInicio, iColumna], true, -1, colorBorde, XlVAlign.xlVAlignBottom, "ITC Avant Garde", 10, false, false, false, false, XlHAlign.xlHAlignRight, 6);

                        filaInicio++;
                        ws.Cells[filaInicio, iColumna] = "#N/A";
                        comun.estableceEstiloEtiquetas(ws.Cells[filaInicio, iColumna], true, -1, colorBorde, XlVAlign.xlVAlignBottom, "ITC Avant Garde", 10, false, false, false, false, XlHAlign.xlHAlignRight, 6);
                    }
                    iColumna++;
                }
                int fColumna = iColumna - 1;

                rango = ws.get_Range(comun.columnLetter(pColumna + 1) + iFila, comun.columnLetter(fColumna) + iFila);
                comun.estableceEstiloEtiquetas(rango, true, -1, colorBorde, XlVAlign.xlVAlignCenter, "ITC Avant Garde", 10, false, false, false, true, XlHAlign.xlHAlignLeft, -1);

                rango = ws.get_Range(comun.columnLetter(pColumna) + (iFila + 1), comun.columnLetter(fColumna) + (iFila + 1));
                rango.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorBorde));

                rango = ws.get_Range(comun.columnLetter(pColumna) + (iFila + 2), comun.columnLetter(fColumna) + (iFila + 2));
                rango.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));

                rango = ws.get_Range(comun.columnLetter(pColumna) + (iFila + 3), comun.columnLetter(fColumna) + (iFila + 3));
                rango.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorBorde));

                rango = ws.get_Range(comun.columnLetter(pColumna) + (iFila), comun.columnLetter(fColumna) + (filaInicio + 1));
                ColorConverter cc = new System.Drawing.ColorConverter();
                rango.Borders.Color = ColorTranslator.ToOle((Color)cc.ConvertFromString(colorBorde));
                rango.Borders.LineStyle = XlLineStyle.xlDash;
                rango.Font.Name = "ITC Avant Garde";
                rango.Font.Size = 10;
                //Se obtienen los rangos para graficar el hitórico.
                //Rango Serie 1
                var startCellSerie1 = (Range)ws.Cells[iFila + 2, comun.columnLetter(pColumna + 1)];
                var endCellSerie1 = (Range)ws.Cells[iFila + 2, fColumna];
                Range rangeSerie1 = ws.get_Range(startCellSerie1, endCellSerie1);

                //Rango Serie 2
                var startCellSerie2 = (Range)ws.Cells[iFila + 3, comun.columnLetter(pColumna + 1)];
                var endCellSerie2 = (Range)ws.Cells[iFila + 3, fColumna];
                Range rangeSerie2 = ws.get_Range(startCellSerie2, endCellSerie2);

                //Rango Categoria               
                var startCellCategoria = (Range)ws.Cells[iFila + 1, comun.columnLetter(pColumna + 1)];
                var endCellCategoria = (Range)ws.Cells[iFila + 1, fColumna];
                Range rangeCategoria = ws.get_Range(startCellCategoria, endCellCategoria);

                Range posGrafica = ws.get_Range("A" + iFila, "A" + iFila);
                RangosGraficas rGrafica = new RangosGraficas();
                rGrafica.serie1 = rangeSerie1;
                rGrafica.serie2 = rangeSerie2;
                rGrafica.categoria = rangeCategoria;
                rGrafica.pGrafica = posGrafica;
                rGrafica.colorSerie1 = int.Parse(colorSerie1);
                rGrafica.colorSerie2 = int.Parse(colorSerie2);
                lGraficas.Add(rGrafica);
            }
        }
        /// <summary>
        /// Agrega los datos generales del reporte
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        /// <param name="programa">Objeto de tipo Programa</param>
        private void agregarDatosGenerales(Worksheet ws, SIMEPS.Modelo.Programa programa)
        {
            int filaInicio = iFila;
            int columnaPreEtiqueta = 0;
            string letter = comun.columnLetter(columnaFinal);
            if (letter == "J")
                columnaPreEtiqueta = columnaFinal - 4;
            else
                columnaPreEtiqueta = columnaFinal - 6;
            int columnaPreR = columnaPreEtiqueta + 1;
            Range range = ws.get_Range("A" + iFila, comun.columnLetter(columnaFinal) + iFila);
            comun.intercalarColor(ws, iFila, range, "#BDD7EE");
            ws.Cells[iFila, "A"] = "Dependencia";
            ws.Cells[iFila, "B"] = programa.DEPENDENCIA;

            iFila++;
            range = ws.get_Range("A" + iFila, comun.columnLetter(columnaFinal) + iFila);
            comun.intercalarColor(ws, iFila, range, "#BDD7EE");
            ws.Cells[iFila, "A"] = "Denominación del Programa";
            ws.Cells[iFila, "B"] = programa.MODALIDAD + "-" + programa.CLAVE + "-" + programa.NOMBRE_MATRIZ;

            iFila++;
            range = ws.get_Range("A" + iFila, comun.columnLetter(columnaFinal) + iFila);
            comun.intercalarColor(ws, iFila, range, "#BDD7EE");
            ws.Cells[iFila, "A"] = "Unidad Responsable";
            ws.Cells[iFila, "B"] = programa.DESC_UNIDAD;
            ws.Cells[iFila, comun.columnLetter(columnaPreEtiqueta)] = "Presupuesto aprobado";
            ws.Cells[iFila, comun.columnLetter(columnaPreR)] = (programa != null) ? Convert.ToInt32(programa.PRESUPUESTOS.Where(i => i.CICLO == programa.CICLO).Select(i => i.IMPORTE_ORIGINAL_MDP).FirstOrDefault()) + " mdp" : null;

            iFila++;
            range = ws.get_Range("A" + iFila, comun.columnLetter(columnaFinal) + iFila);
            comun.intercalarColor(ws, iFila, range, "#BDD7EE");
            ws.Cells[iFila, "A"] = "Meta nacional";
            ws.Cells[iFila, "B"] = programa.DESC_META;
            ws.Cells[iFila, comun.columnLetter(columnaPreEtiqueta)] = "Presupuesto ejercido";
            ws.Cells[iFila, comun.columnLetter(columnaPreR)] = (programa != null) ? Convert.ToInt32(programa.PRESUPUESTOS.Where(i => i.CICLO == programa.CICLO).Select(i => i.IMPORTE_EJERCIDO_MDP).FirstOrDefault()) + " mdp" : null;

            iFila++;
            range = ws.get_Range("A" + iFila, comun.columnLetter(columnaFinal) + iFila);
            comun.intercalarColor(ws, iFila, range, "#BDD7EE");
            ws.Cells[iFila, "A"] = "Programa sectorial:";
            ws.Cells[iFila, "B"] = programa.DESC_PROGRAMA_SEC_INST;

            iFila++;
            range = ws.get_Range("A" + iFila, comun.columnLetter(columnaFinal) + iFila);
            comun.intercalarColor(ws, iFila, range, "#BDD7EE");
            ws.Cells[iFila, "A"] = "Ciclo";
            ws.Cells[iFila, "B"] = programa.CICLO;

            iFila++;
            range = ws.get_Range("A" + iFila, comun.columnLetter(columnaFinal) + iFila);
            comun.intercalarColor(ws, iFila, range, "#BDD7EE");
            ws.Cells[iFila, "A"] = "Estatus del programa en Aprobación de indicadores";
            ws.Cells[iFila, "B"] = programa.DESC_APROBACION_DICTAMEN;
            Range aRangeImg = ws.get_Range("C" + iFila, "C" + iFila);
            ws.Cells[iFila, "C"] = programa.ID_NIVEL_APROBACION;
            //  ws.Shapes.AddPicture(Server.MapPath("~/img") + "\\paloma_verde.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, aRangeImg.Left, aRangeImg.Top, 15, 15);
            ws.Cells[iFila, "D"] = "Año dictamen";
            ws.Cells[iFila, "E"] = programa.CICLO;
            range = ws.get_Range("D" + iFila, "D" + iFila);
            comun.estableceEstiloEtiquetas(range, false, 24, "", XlVAlign.xlVAlignBottom, "", 11, true, false, false, false, null, 37);

            int filaFinal = iFila;
            Range aRange = ws.get_Range("A" + filaInicio.ToString(), "A" + filaFinal);
            Range rHeader = ws.get_Range("A" + 1, comun.columnLetter(columnaFinal) + 1);
            rHeader.Merge();
            comun.estableceEstiloHeader(rHeader, true, 80, "#2F75B5", true);
            comun.estableceEstiloEtiquetas(aRange, false, -1, "", XlVAlign.xlVAlignBottom, "", 11, true, true, false, false, null, -1);
            aRange = ws.get_Range(comun.columnLetter(columnaPreEtiqueta) + filaInicio.ToString(), comun.columnLetter(columnaPreEtiqueta) + filaFinal);
            comun.estableceEstiloEtiquetas(aRange, false, -1, "", XlVAlign.xlVAlignBottom, "", 11, true, true, false, false, null, -1);
            ws.Cells[1, "A"] = "MATRIZ DE INDICADORES PARA RESULTADOS";
            comun.estableceEstiloTitulo(rHeader, true, "ITC Avant Garde", 18);

            aRange = ws.get_Range("A" + (filaInicio - 1), comun.columnLetter(columnaFinal) + filaFinal);
            aRange.BorderAround2(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, Type.Missing, Type.Missing);
            aRange = ws.get_Range("A" + (filaInicio), comun.columnLetter(columnaFinal) + filaFinal);
            aRange.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlLineStyleNone;
            aRange = ws.get_Range("A" + (filaFinal), comun.columnLetter(columnaFinal) + filaFinal);
            aRange.BorderAround2(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, Type.Missing, Type.Missing);
            aRange = ws.get_Range("A" + filaInicio.ToString(), comun.columnLetter(columnaFinal) + (filaFinal - 1));
            aRange.Rows.RowHeight = 38;
        }

        /// <summary>
        /// Obtiene las propiedades del Nivel de acuerdo al Id
        /// </summary>
        /// <param name="nivel">Id del Nivel</param>
        /// <returns></returns>
        public Dictionary<String, String> obtenerPropiedadesNivel(int nivel)
        {
            Dictionary<String, String> PropiedadesNivel = new Dictionary<string, string>();
            //String sNombre = "";

            switch (nivel)
            {
                case 1:
                    //sNombre = "Fin";
                    PropiedadesNivel.Add("ColorTitulo", "#305496");
                    PropiedadesNivel.Add("ColorSubTitulo", "#D9E1F2");
                    PropiedadesNivel.Add("ColorGraficaS1", System.Drawing.Color.FromArgb(213, 155, 91).ToArgb().ToString());
                    PropiedadesNivel.Add("ColorGraficaS2", System.Drawing.Color.FromArgb(49, 125, 237).ToArgb().ToString());
                    break;
                case 2:
                    //sNombre = "Propósito";
                    PropiedadesNivel.Add("ColorTitulo", "#3C559C");
                    PropiedadesNivel.Add("ColorSubTitulo", "#C0C0F2");
                    PropiedadesNivel.Add("ColorGraficaS1", System.Drawing.Color.FromArgb(215, 243, 95).ToArgb().ToString());
                    PropiedadesNivel.Add("ColorGraficaS2", System.Drawing.Color.FromArgb(88, 211, 248).ToArgb().ToString());
                    break;
                case 3:
                    //sNombre = "Componente";
                    PropiedadesNivel.Add("ColorTitulo", "#6B717F");
                    PropiedadesNivel.Add("ColorSubTitulo", "#C8D4DA");
                    PropiedadesNivel.Add("ColorGraficaS1", System.Drawing.Color.FromArgb(204, 21, 12).ToArgb().ToString());
                    PropiedadesNivel.Add("ColorGraficaS2", System.Drawing.Color.FromArgb(0, 0, 255).ToArgb().ToString());
                    break;
                case 4:
                    //sNombre = "Actividad";
                    PropiedadesNivel.Add("ColorTitulo", "#387F98");
                    PropiedadesNivel.Add("ColorSubTitulo", "#A0CCDC");
                    PropiedadesNivel.Add("ColorGraficaS1", System.Drawing.Color.FromArgb(71, 173, 112).ToArgb().ToString());
                    PropiedadesNivel.Add("ColorGraficaS2", System.Drawing.Color.FromArgb(17, 90, 197).ToArgb().ToString());
                    break;
            }
            return PropiedadesNivel;
        }
        /// <summary>
        /// Agrega los titulos generales de los niveles del Indicador
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        private void agregarTitulos(Worksheet ws)
        {
            iFila++;
            Range range = ws.get_Range("A" + iFila, comun.columnLetter(columnaFinal) + iFila);
            ws.Cells[iFila, "A"] = "Gráfica";
            ws.Cells[iFila, "B"] = "Indicador";
            ws.Cells[iFila, "C"] = "Definición";
            ws.Cells[iFila, "D"] = "Metodo Cálculo";
            ws.Cells[iFila, "E"] = "Detalle indicador";
            ws.Cells[iFila, "G"] = "Medio de Verificación";
            ws.Cells[iFila, "H"] = "Metas";
            ws.Cells[iFila, comun.columnLetter(columnaFinal)] = "Supuestos";
            comun.estableceEstiloHeader(range, false, -1, "#2F75B5", false);
            comun.estableceEstiloTitulo(range, true, "ITC Avant Garde", 11);

            iFila++;
            range = ws.get_Range("A" + iFila, comun.columnLetter(columnaFinal) + iFila);
            range.Rows.RowHeight = 4.5;
            comun.intercalarColor(ws, iFila, range, "#FFFFFF");
        }

        /// <summary>
        /// Colo el estilo para los subtitulos de la tabla de niveles
        /// </summary>
        /// <param name="aRange">Rango en el que se colocará el estilo</param>
        /// <param name="alignCenter">Si se alinea en el centro</param>
        /// <param name="font">Fuente</param>
        /// <param name="size">El tamaño de la Fuente</param>
        private void estableceEstiloSubTitulo(Range aRange, bool alignCenter, string font, int size)
        {
            aRange.Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            aRange.VerticalAlignment = XlVAlign.xlVAlignBottom;
            aRange.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            aRange.Cells.Font.Size = size;
            aRange.Cells.Font.Bold = false;
            if (!String.IsNullOrEmpty(font)) aRange.Font.Name = font;
        }
    }
    public class RangosGraficas
    {
        public Range serie1 { get; set; }
        public Range serie2 { get; set; }
        public Range categoria { get; set; }
        public Range pGrafica { get; set; }
        public int colorSerie1 { get; set; }
        public int colorSerie2 { get; set; }
    }
}
