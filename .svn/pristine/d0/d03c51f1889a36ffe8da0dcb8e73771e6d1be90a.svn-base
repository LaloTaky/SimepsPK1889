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
    public partial class DescargarBasePND : System.Web.UI.Page
    {
        List<Tuple<int, string, bool>> tFilaColor = new List<Tuple<int, string, bool>>();
        List<RangosGraficas> lGraficas = new List<RangosGraficas>();
        int iFila = 1;
        int iColumna = 1, columnaFinal = 27;
        int In = 2, letra;
        IndicadoresDal indicadores = new IndicadoresDal();
        ExcelUtilities comun = new ExcelUtilities();
        Logger log = new Logger();
        Workbook wb = null;
        Microsoft.Office.Interop.Excel.Application xlApp = null;
        String nombreSector = "";
        int idProgramaSectorial;


        protected void Page_Load(object sender, EventArgs e)
        {
            string pId = "", pIdIndicador = "", pTipo = "";
            string nombreTemporalExcel = "";
            String sParametroOpcion = "";
            int resBorrar = 0;

            if (Request.Params["id"] != null)
                pId = Request.Params["id"].ToString();
            if (Request.Params["id"] == "null")
                pId = "-1";
            if (Request.Params["idIndicador"] != null)
                pIdIndicador = Request.Params["idIndicador"];
            if (Request.Params["idIndicador"] == "null")
                pIdIndicador = "-1";
            if (Request.Params["tipo"] != null)
                pTipo = Request.Params["tipo"];

            if (Request.Params["pOpcion"] != null)
                sParametroOpcion = Request.Params["pOpcion"];

            ReportesDal nuevoReporte = new ReportesDal("TD_RESOURCE_STORE");
            try

            {
                int idObjetivo = Convert.ToInt16(pId), idIndicador = Convert.ToInt16(pIdIndicador);

                if (idObjetivo == -1 && idIndicador == -1)
                {
                    byte[] archivoRecuperado = null;
                    bool existeTR = false;
                    SIMEPS.Modelo.TipoReporte tipoReporte = nuevoReporte.tipoReporteDal(Convert.ToInt32(Utils.IdTiposReportes.baseDeDatosDelPND));
                    if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                    {
                        archivoRecuperado = nuevoReporte.recuperarDocumento(tipoReporte.NOMBRE_ARCHIVO + "." + tipoReporte.FORMATO);
                    }
                    if (sParametroOpcion.ToLower() == "genera")
                    {
                        existeTR = nuevoReporte.buscarTR(tipoReporte.NOMBRE_ARCHIVO + "." + tipoReporte.FORMATO, tipoReporte.ID_TIPO_REPORTE, null, null, null, null, null, null, null);
                    }
                    if (sParametroOpcion.ToLower() == "actualiza" || (sParametroOpcion == "genera" && !existeTR))
                    {
                        nuevoReporte.BorrarReporte(tipoReporte.NOMBRE_ARCHIVO + "." + tipoReporte.FORMATO, tipoReporte.ID_TIPO_REPORTE);
                    }
                    if (archivoRecuperado != null && sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                    {
                        comun.descargarArchivo(Response, archivoRecuperado, tipoReporte.NOMBRE_ARCHIVO + "." + tipoReporte.FORMATO);
                    }
                    else if (archivoRecuperado == null && !existeTR)
                    {
                        string rutaArchivo = "SIPOL";
                        nombreTemporalExcel = comun.GetTimestamp(DateTime.Now);
                        comun.eliminarTemporales(Server.MapPath("~/Descargas"));
                        xlApp = new Microsoft.Office.Interop.Excel.Application();
                        // xlApp.Visible = true;
                        wb = comun.crearWorkBook(xlApp);
                        Worksheet ws = comun.crearWorkSheetSinLogo(wb, false);
                        ws.Name = "Base de Datos del PND";
                        ws.Range["A:E"].ColumnWidth = 10.71;
                        ws.Range["G:AA"].ColumnWidth = 10.71;
                        ws.Range["F:F"].ColumnWidth = 18.71;

                        TextoTitulos(ws, idObjetivo, idIndicador);
                        DatosPND(ws, idObjetivo, idIndicador);
                        ((Worksheet)wb.Sheets[1]).Delete();
                        String filename = Server.MapPath("~/Descargas") + "\\" + nombreTemporalExcel + ".xlsx";
                        wb.SaveAs(filename);
                        comun.terminarProcesoExcel(xlApp, wb);
                        var resultado = nuevoReporte.guardarDocumento(rutaArchivo, tipoReporte.NOMBRE_ARCHIVO + "." + tipoReporte.FORMATO, filename, tipoReporte.ID_TIPO_REPORTE, null, null, null, null, null, null, null);

                        if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                        {
                            comun.descargarArchivo(Response, filename, tipoReporte.NOMBRE_ARCHIVO + "." + tipoReporte.FORMATO);
                        }
                    }

                }
                else
                {
                    Modelo.ProgramaSectorial ProgramaSectorial = new Modelo.ProgramaSectorial();
                    ProgramaSectorial = indicadores.ConsultarProgramaSectoriales(idObjetivo, false).FirstOrDefault();
                    string sector = ProgramaSectorial.NOMBRESECTOR.Trim().Replace(' ', '_'); ;
                    string programaSec = ProgramaSectorial.NOMBRE.Trim().Replace(' ', '_'); ;
                    SIMEPS.Modelo.TipoReporte tipoReporte = nuevoReporte.tipoReporteDal(pTipo.Equals("csv") ? Convert.ToInt32(Utils.IdTiposReportes.baseDeDatosCSV) : Convert.ToInt32(Utils.IdTiposReportes.baseDeDatos));
                    string nombreArchivo = tipoReporte.NOMBRE_ARCHIVO + "_" + sector + "_" + programaSec + "." + tipoReporte.FORMATO;
                    string rutaArchivo = "SIPOL\\" + tipoReporte.NOMBRE_ARCHIVO + "\\" + sector;

                    byte[] archivoRecuperado = null;
                    bool existeTR = false;

                    if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                    {
                        archivoRecuperado = nuevoReporte.recuperarDocumento(nombreArchivo);
                    }

                    if (sParametroOpcion.ToLower() == "genera")
                    {
                        existeTR = nuevoReporte.buscarTR(nombreArchivo, tipoReporte.ID_TIPO_REPORTE, null, null, null, null, null, ProgramaSectorial.ID_PROG_SECTORIAL, null);
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
                        nombreTemporalExcel = comun.GetTimestamp(DateTime.Now);
                        comun.eliminarTemporales(Server.MapPath("~/Descargas"));
                        xlApp = new Microsoft.Office.Interop.Excel.Application();
                        // xlApp.Visible = true;
                        wb = comun.crearWorkBook(xlApp);
                        Worksheet ws = comun.crearWorkSheetSinLogo(wb, false);
                        ws.Name = "Base de Datos del PND";
                        ws.Range["A:E"].ColumnWidth = 10.71;
                        ws.Range["G:AA"].ColumnWidth = 10.71;
                        ws.Range["F:F"].ColumnWidth = 18.71;
                        TextoTitulos(ws, idObjetivo, idIndicador);
                        DatosPND(ws, idObjetivo, idIndicador);
                        ((Worksheet)wb.Sheets[1]).Delete();
                        if (pTipo.Equals("csv"))
                        {
                            String filename = Server.MapPath("~/Descargas") + "\\" + nombreTemporalExcel + "." + tipoReporte.FORMATO;
                            wb.SaveAs(filename, XlFileFormat.xlCSV);
                            comun.terminarProcesoExcel(xlApp, wb);

                            var resultado = nuevoReporte.guardarDocumento(rutaArchivo, nombreArchivo, filename, tipoReporte.ID_TIPO_REPORTE, null, null, null, null, null, ProgramaSectorial.ID_PROG_SECTORIAL, null);

                            if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                            {
                                comun.descargarArchivo(Response, filename, nombreArchivo);
                            }
                        }
                        else
                        {
                            String filename = Server.MapPath("~/Descargas") + "\\" + nombreTemporalExcel + "." + tipoReporte.FORMATO;
                            wb.SaveAs(filename);
                            comun.terminarProcesoExcel(xlApp, wb);

                            var resultado = nuevoReporte.guardarDocumento(rutaArchivo, nombreArchivo, filename, tipoReporte.ID_TIPO_REPORTE, null, null, null, null, null, ProgramaSectorial.ID_PROG_SECTORIAL, null);

                            if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                            {
                                comun.descargarArchivo(Response, filename, nombreArchivo);
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                if (error.Message.Equals("Excepción de descarga de archivo."))
                {
                    comun.terminarProcesoExcel(xlApp, wb);
                    //R3496
                    //Response.Write("window.history.back();");
                }

                else
                {
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
                        //R3496
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

        public void TextoTitulos(Worksheet ws, int IdObjetivo, int IdIndicador)
        {
            string letter = "";
            int letra, CicloMax;


            List<Modelo.Meta> Valores = new List<Meta>();
            Valores = indicadores.ConsultarMetasIndicador(0, 3);

            if (Valores.Count > 0)
            {
                CicloMax = 1 + (Valores.Max(v => v.CICLO));
                letra = 12;

                ws.Cells[iFila, "A"] = "Programa";
                ws.Cells[iFila, "B"] = "Ramo";
                ws.Cells[iFila, "C"] = "Tipo Programa";
                ws.Cells[iFila, "D"] = "Objetivo (DOF)";
                ws.Cells[iFila, "E"] = "Indicador (DOF)";
                ws.Cells[iFila, "F"] = "Descripción General (DOF)";
                ws.Cells[iFila, "G"] = "Método cálculo (DOF)";
                ws.Cells[iFila, "H"] = "Fuente información (DOF)";
                ws.Cells[iFila, "I"] = "Unidad medida (DOF)";
                ws.Cells[iFila, "J"] = "Periodicidad (DOF)";
                ws.Cells[iFila, "K"] = "Línea Base (DOF)";
                Range ColorGris = ws.get_Range("A" + iFila, "K" + iFila);
                EstiloTitulosDB(ColorGris, "#FFFFFF", "#808080");
                foreach (var valor in Valores)
                {
                    letter = comun.columnLetter(letra);
                    // Se coloca el 2013, dado que antes de este ciclo no hay información \\
                    if (valor.CICLO < 2013)
                    {
                        letter = comun.columnLetter(letra);
                        ws.Cells[iFila, letter] = "Valor alcanzado " + valor.CICLO;
                        Range ColorAzul1 = ws.get_Range(letter + iFila, letter + iFila);
                        EstiloTitulosDB(ColorAzul1, "#FFFFFF", "#0070C0");
                        letra++;
                    }
                    else if (valor.CICLO != CicloMax)
                    {
                        letter = comun.columnLetter(letra);
                        ws.Cells[iFila, letter] = "Meta intermedia " + valor.CICLO;
                        Range ColorAzul2 = ws.get_Range(letter + iFila, letter + iFila);
                        EstiloTitulosDB(ColorAzul2, "#FFFFFF", "#202164");
                        letra++;

                        letter = comun.columnLetter(letra);
                        ws.Cells[iFila, letter] = "Valor alcanzado " + valor.CICLO;
                        Range ColorAzul1 = ws.get_Range(letter + iFila, letter + iFila);
                        EstiloTitulosDB(ColorAzul1, "#FFFFFF", "#202164");
                        letra++;
                    }
                }

                letter = comun.columnLetter(letra);
                ws.Cells[iFila, letter] = "Meta " + CicloMax + " planeada (DOF)";
                Range Final = ws.get_Range(letter + iFila, letter + iFila);
                EstiloTitulosDB(Final, "#FFFFFF", "#202164");

                Range Completo = ws.get_Range("A" + iFila, letter + iFila);
                Completo.Rows.RowHeight = 55.5;
                Completo.WrapText = true;
                Completo.VerticalAlignment = XlVAlign.xlVAlignCenter;
                Completo.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                Completo.Font.Bold = true;
                Completo.Font.Size = 10;
                iFila++;
            }
        }
        public void DatosPND(Worksheet ws, int IdObjetivo, int IdIndicador)
        {
            List<ObjetivoSectorial> ListaObjetivosSectoriales = new List<ObjetivoSectorial>();
            List<Modelo.IndicadorSectorial> ListaIndicadoresSectoriales = new List<Modelo.IndicadorSectorial>();
            List<Modelo.ProgramaSectorial> ListaProgramasSectoriales = new List<Modelo.ProgramaSectorial>();
            string letter = "";
            int CicloMax, CicloMin;
            ListaProgramasSectoriales = indicadores.ConsultarProgramaSectoriales(IdObjetivo, false);


            foreach (ProgramaSectorial programa in ListaProgramasSectoriales)
            {
                ListaObjetivosSectoriales = indicadores.ConsultarObjetivosSectoriales(programa.ID_PROG_SECTORIAL);

                foreach (ObjetivoSectorial objSectorial in ListaObjetivosSectoriales)
                {
                    ListaIndicadoresSectoriales = indicadores.ConsultarIndicadoresSectoriales(programa.ID_PROG_SECTORIAL, 2, objSectorial.OBJETIVO, 1);
                    foreach (Modelo.IndicadorSectorial indSectorial in ListaIndicadoresSectoriales)
                    {
                        Modelo.IndicadorSectorial detalleIndicador = indicadores.ConsultarDetalleIndicador(indSectorial.ID_INDICADOR, 1).FirstOrDefault();

                        List<Modelo.Meta> Valores = indicadores.ConsultarMetasIndicador(indSectorial.ID_INDICADOR, 2);

                        if (programa.NOMBRE != null) ws.Cells[iFila, "A"] = programa.NOMBRE;
                        else ws.Cells[iFila, "A"] = "-";

                        if (detalleIndicador.Nombre_Ramo != null) ws.Cells[iFila, "B"] = detalleIndicador.Nombre_Ramo;
                        else ws.Cells[iFila, "B"] = "-";

                        if (detalleIndicador.TIPO != null) ws.Cells[iFila, "C"] = detalleIndicador.TIPO;
                        else ws.Cells[iFila, "C"] = "-";

                        if (detalleIndicador.OBJETIVO != null) ws.Cells[iFila, "D"] = detalleIndicador.OBJETIVO;
                        else ws.Cells[iFila, "D"] = "-";

                        if (detalleIndicador.NOMBRE != null) ws.Cells[iFila, "E"] = detalleIndicador.NOMBRE;
                        else ws.Cells[iFila, "E"] = "-";

                        if (detalleIndicador.DESCRIPCION != null) ws.Cells[iFila, "F"] = detalleIndicador.DESCRIPCION;
                        else ws.Cells[iFila, "F"] = "-";

                        if (detalleIndicador.METODO != null) ws.Cells[iFila, "G"] = detalleIndicador.METODO;
                        else ws.Cells[iFila, "G"] = "-";

                        if (detalleIndicador.FUENTE != null) ws.Cells[iFila, "H"] = detalleIndicador.FUENTE;
                        else ws.Cells[iFila, "H"] = "-";

                        if (detalleIndicador.UDM != null) ws.Cells[iFila, "I"] = detalleIndicador.UDM;
                        else ws.Cells[iFila, "I"] = "-";

                        if (detalleIndicador.PERIODICIDAD != null) ws.Cells[iFila, "J"] = detalleIndicador.PERIODICIDAD;
                        else ws.Cells[iFila, "J"] = "-";

                        if (detalleIndicador.VALOR_LB != null) ws.Cells[iFila, "K"] = detalleIndicador.VALOR_LB;
                        else ws.Cells[iFila, "K"] = "-";

                        if (Valores.Count > 0)
                        {
                            CicloMax = Valores.Max(v => v.CICLO);
                            CicloMin = Valores.Min(v => v.CICLO);

                            if (CicloMin == 2006)
                                letra = 12;
                            else
                                letra = 19;

                            foreach (var valor in Valores)
                            {
                                letter = comun.columnLetter(letra);
                                // Se coloca el 2013, dado que antes de este ciclo no hay información \\
                                if (valor.CICLO < 2013)
                                {
                                    ws.Cells[iFila, letter] = valor.VALOR;
                                    letra++;
                                }
                                else if (valor.CICLO != CicloMax)
                                {
                                    if (valor.MI == null)
                                    {
                                        letter = comun.columnLetter(letra);
                                        ws.Cells[iFila, letter] = "";
                                        letra++;
                                    }
                                    else
                                    {
                                        letter = comun.columnLetter(letra);
                                        ws.Cells[iFila, letter] = valor.MI;
                                        letra++;
                                    }
                                    if (valor.VALOR == null)
                                    {
                                        letter = comun.columnLetter(letra);
                                        ws.Cells[iFila, letter] = "";
                                        letra++;
                                    }
                                    else
                                    {
                                        letter = comun.columnLetter(letra);
                                        ws.Cells[iFila, letter] = valor.VALOR;
                                        letra++;
                                    }
                                }
                            }

                            letter = comun.columnLetter(letra);
                            if (detalleIndicador.META != null) ws.Cells[iFila, letter] = detalleIndicador.META;
                            else ws.Cells[iFila, letter] = "-";

                            Range Final = ws.get_Range(letter + iFila, letter + iFila);
                        }
                        else
                        {
                            letter = comun.columnLetter(iColumna);
                            if (detalleIndicador.META != null) ws.Cells[iFila, letter] = detalleIndicador.META;
                            else ws.Cells[iFila, letter] = "-";
                            Range Final = ws.get_Range(letter + iFila, letter + iFila);
                        }
                        letter = comun.columnLetter(letra);
                        Range estilo = ws.get_Range("A" + iFila, letter + iFila);
                        estilo.WrapText = true;
                        estilo.VerticalAlignment = XlVAlign.xlVAlignTop;
                        estilo.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                        estilo.Interior.Color = Color.White;
                        estilo.Font.Size = 9;
                        estilo.Rows.RowHeight = 160;
                        iFila++;
                        iColumna = letra;
                    }
                }
            }

            Range borde = ws.get_Range("A" + In, letter + (iFila - 1));
            borde.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
            borde.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            borde.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            borde.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            borde.Borders.Color = Color.Black;
            borde.Borders.Weight = XlBorderWeight.xlMedium;
            borde.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlLineStyleNone;
            borde.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlLineStyleNone;
            borde.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            borde.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
            ValoresNulos(ws, iFila, letra);
        }

        public void ValoresNulos(Worksheet ws, int UltimaFila, int UltimaLetra)
        {
            int letra = 12;
            string letter = "", TextoCells = "";
            letter = comun.columnLetter(letra);

            for (int x = letra; x < UltimaLetra; x++)
            {
                for (int y = 2; y < UltimaFila; y++)
                {
                    if (((Range)ws.Cells[y, x]).Value2 != null)
                        TextoCells += ((Range)ws.Cells[y, x]).Value2.ToString();
                }
                if (TextoCells == "")
                {
                    letter = comun.columnLetter(x);
                    ws.Range[letter + ":" + letter].ColumnWidth = 0;
                }
                TextoCells = "";
            }
            for (int x = letra; x < UltimaLetra; x++)
            {
                for (int y = 2; y < UltimaFila; y++)
                {
                    if (((Range)ws.Cells[y, x]).Value2 == null)
                        ws.Cells[y, x] = "-";
                }
            }
        }
        public void EstiloTitulosDB(Range Rango, string ColorTexto, string ColorFondo)
        {
            Rango.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(ColorTexto));
            Rango.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(ColorFondo));
            Rango.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            Rango.Borders.Color = Color.White;
        }
    }
}