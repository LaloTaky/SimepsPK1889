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
using Excel = Microsoft.Office.Interop.Excel;

namespace SIMEPS
{
    public partial class DescargarFTIndicadores : System.Web.UI.Page
    {

        List<Tuple<int, string, bool>> tFilaColor = new List<Tuple<int, string, bool>>();
        List<RangosGraficas> lGraficas = new List<RangosGraficas>();
        int iFila = 1;
        int iColumna = 12, columnaFinal = 0;
        IndicadoresDal indicadores = new IndicadoresDal();
        ExcelUtilities comun = new ExcelUtilities();
        Logger log = new Logger();
        object missing = Type.Missing;

        protected void Page_Load(object sender, EventArgs e)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbooks m_objBooks = (Excel.Workbooks)xlApp.Workbooks;
            Excel.Workbook wb = (Excel.Workbook)m_objBooks.Add(missing);

            string pId = "", pIdIndicador = "";
            string nombreTemporalExcel = "", nom1 = "", nom2 = "";
            String sParametroOpcion = "";
            int resBorrar = 0;
            ReportesDal nuevoReporte = new ReportesDal("TD_RESOURCE_STORE");
            if (Request.Params["id"] != null)
                pId = Request.Params["id"].ToString();

            if (Request.Params["pOpcion"] != null)
                sParametroOpcion = Request.Params["pOpcion"];

            if (Request.Params["idIndicador"] != null)
                pIdIndicador = Request.Params["idIndicador"];

            try
            {
                int cont = 1;

                int id = Convert.ToInt16(pId), idIndicador = Convert.ToInt16(pIdIndicador);

                List<ObjetivoSectorial> ListaObjetivosSectoriales = new List<ObjetivoSectorial>();
                List<ObjetivoSectorial> ListaInversaObj = new List<ObjetivoSectorial>();
                List<Modelo.IndicadorSectorial> ListaIndicadoresSectoriales = new List<Modelo.IndicadorSectorial>();
                List<Modelo.IndicadorSectorial> ListaInversaInd = new List<Modelo.IndicadorSectorial>();
                Modelo.ProgramaSectorial programa = indicadores.ConsultarProgramaSectoriales(id, false).FirstOrDefault();

                /*Construyendo Nombre Archivo*/
                string sector = programa.NOMBRESECTOR.Trim().Replace(' ', '_');
                string programaSec = programa.NOMBRE.Trim().Replace(' ', '_');
                SIMEPS.Modelo.TipoReporte tipoReporte = nuevoReporte.tipoReporteDal(Convert.ToInt32(Utils.IdTiposReportes.fichasTecnicasIndicadores));
                string nombreArchivo = "";
                if (programa.ID_SECTOR == 6)
                {
                    nombreArchivo = tipoReporte.NOMBRE_ARCHIVO + "_" + sector + "_" + programaSec.Substring(0, 34) + "." + tipoReporte.FORMATO;
                }
                else
                {
                    nombreArchivo = tipoReporte.NOMBRE_ARCHIVO + "_" + sector + "_" + programaSec + "." + tipoReporte.FORMATO;
                }
                string rutaArchivo = "SIPOL\\" + tipoReporte.NOMBRE_ARCHIVO + "\\" + sector;

                byte[] archivoRecuperado = null;
                bool existeTR = false;


                if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                {
                    archivoRecuperado = nuevoReporte.recuperarDocumento(nombreArchivo);
                }

                if (sParametroOpcion.ToLower() == "genera")
                {
                    existeTR = nuevoReporte.buscarTR(nombreArchivo, tipoReporte.ID_TIPO_REPORTE, null, null, null, null, null, programa.ID_PROG_SECTORIAL, null);
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
                    ListaObjetivosSectoriales = indicadores.ConsultarObjetivosSectoriales(id);
                    ListaInversaObj = ListaObjetivosSectoriales.OrderByDescending(o => o.NUM_OBJETIVO).ToList();

                    List<SIMEPS.Modelo.ProgramaIndicadorMIR> programas = new List<ProgramaIndicadorMIR>();

                    foreach (ObjetivoSectorial objSectorial in ListaInversaObj)
                    {
                        ListaIndicadoresSectoriales = indicadores.ConsultarIndicadoresSectoriales(id, 2, objSectorial.OBJETIVO, 1);
                        ListaInversaInd = ListaIndicadoresSectoriales.OrderByDescending(i => i.INDICADOR).ToList();

                        foreach (Modelo.IndicadorSectorial indSectorial in ListaInversaInd)
                        {
                            Modelo.IndicadorSectorial detalleIndicador = indicadores.ConsultarDetalleIndicador(indSectorial.ID_INDICADOR, 1).FirstOrDefault();
                            List<Modelo.Meta> datosGrafica = indicadores.ConsultarMetasIndicador(indSectorial.ID_INDICADOR, 2);
                            programas = indicadores.ConcultarProgramaIndicador(indSectorial.ID_INDICADOR);

                            Excel.Sheets m_objSheets = (Excel.Sheets)wb.Worksheets;
                            Excel.Worksheet ws = (Excel.Worksheet)(m_objSheets.Add(missing));

                            string nombre = indSectorial.INDICADOR;
                            string numero = nombre.Substring(0, nombre.IndexOf(' '));

                            if (nom1 != numero)
                                ws.Name = "Indicador " + numero;
                            else
                                ws.Name = "Indicador  " + numero;

                            columnaFinal = iColumna;
                            iFila = 2;
                            ws.Range["A:A"].ColumnWidth = 17.86;
                            ws.Range["B:E"].ColumnWidth = 10.71;
                            ws.Range["F:F"].ColumnWidth = 16.29;

                            agregarDatosGenerales(ws, detalleIndicador, programa);
                            agregarDatos(ws, detalleIndicador);
                            ProgramasAliados(ws, programas);
                            Grafica(ws, datosGrafica);
                            nom1 = numero;
                            cont++;
                        }

                    }
                    for (int i = xlApp.ActiveWorkbook.Worksheets.Count; i > 0; i--)
                    {
                        Worksheet wkSheet = (Worksheet)xlApp.ActiveWorkbook.Worksheets[i];
                        if (wkSheet.Name == "Hoja1" || wkSheet.Name == "Hoja2" || wkSheet.Name == "Hoja3")
                            wkSheet.Delete();
                    }
                    String filename = Server.MapPath("~/Descargas") + "\\" + nombreTemporalExcel + ".xlsx";
                    wb.SaveAs(filename);
                    comun.terminarProcesoExcel(xlApp, wb);

                    var resultado = nuevoReporte.guardarDocumento(rutaArchivo, nombreArchivo, filename, tipoReporte.ID_TIPO_REPORTE, null, null, null, null, null, programa.ID_PROG_SECTORIAL, null);

                    if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                    {
                        comun.descargarArchivo(Response, filename, nombreArchivo);
                    }
                }
            }
            catch (Exception error)
            {
                if (error.Message.Equals("Excepción de descarga de archivo."))
                {
                    comun.terminarProcesoExcel(xlApp, wb);
                }

                else
                {
                    if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                    {
                        //Response.Write("<script language='javascript'>");
                        //Response.Write("alert('No se pudo generar correctamente el archivo.');");
                        //Response.Write("window.history.back();");
                        //Response.Write("<" + "/script>");
                    }

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
            finally
            {
                comun.terminarProcesoExcel(xlApp, wb);
            }
        }

        private void agregarDatosGenerales(Worksheet ws, Modelo.IndicadorSectorial detalleIndicador, Modelo.ProgramaSectorial programa)
        {
            int filaInicio = iFila;
            string letter = comun.columnLetter(columnaFinal);

            Range rangeDes = ws.get_Range("B" + iFila, "E" + iFila);
            Range colorTitulo = ws.get_Range("A" + iFila, "A" + iFila);

            rangeDes.Rows.RowHeight = 69.75;
            comun.intercalarColorSinLogo(ws, iFila, rangeDes, "#EDEDED", colorTitulo);
            rangeDes.Merge();
            comun.estableceEstiloTexto(rangeDes, true, "Helvetica Light", 10, 12, "#000000", false, true);
            comun.EstablecerEstiloTituloDatos(colorTitulo, true, "Helvetica Light", 12, true, 0);
            comun.establecerBordes(rangeDes, colorTitulo, "#395089");
            ws.Cells[iFila, "A"] = "Objetivo:";
            if (detalleIndicador.OBJETIVO != null) ws.Cells[iFila, "B"] = detalleIndicador.OBJETIVO;
            else ws.Cells[iFila, "B"] = "";

            iFila++;
            rangeDes = ws.get_Range("B" + iFila, "E" + iFila);
            colorTitulo = ws.get_Range("A" + iFila, "A" + iFila);
            rangeDes.Rows.RowHeight = 61.5;
            rangeDes = ws.get_Range("B" + iFila, "E" + iFila);
            comun.intercalarColorSinLogo(ws, iFila, rangeDes, "#EDEDED", colorTitulo);
            rangeDes.Merge();
            comun.estableceEstiloTexto(rangeDes, true, "Helvetica Light", 10, 12, "#000000", false, true);
            comun.EstablecerEstiloTituloDatos(colorTitulo, true, "Helvetica Light", 12, true, 0);
            comun.establecerBordes(rangeDes, colorTitulo, "#395089");
            ws.Cells[iFila, "A"] = "Indicador:";
            if (detalleIndicador.NOMBRE != null) ws.Cells[iFila, "B"] = detalleIndicador.NOMBRE;
            else ws.Cells[iFila, "B"] = "-";

            iFila++;
            rangeDes = ws.get_Range("B" + iFila, "E" + iFila);
            colorTitulo = ws.get_Range("A" + iFila, "A" + iFila);
            rangeDes.Rows.RowHeight = 83.25;
            rangeDes = ws.get_Range("B" + iFila, "E" + iFila);
            comun.intercalarColorSinLogo(ws, iFila, rangeDes, "#EDEDED", colorTitulo);
            rangeDes.Merge();
            comun.estableceEstiloTexto(rangeDes, true, "Helvetica Light", 10, 12, "#000000", false, true);
            comun.EstablecerEstiloTituloDatos(colorTitulo, true, "Helvetica Light", 12, true, 0);
            comun.establecerBordes(rangeDes, colorTitulo, "#395089");
            ws.Cells[iFila, "A"] = "Descripción:";
            if (detalleIndicador.DESCRIPCION != null) ws.Cells[iFila, "B"] = detalleIndicador.DESCRIPCION;
            else ws.Cells[iFila, "B"] = "-";

            iFila++;
            rangeDes = ws.get_Range("B" + iFila, "E" + iFila);
            colorTitulo = ws.get_Range("A" + iFila, "A" + iFila);
            rangeDes.Rows.RowHeight = 93.25;
            rangeDes = ws.get_Range("B" + iFila, "E" + iFila);
            comun.intercalarColorSinLogo(ws, iFila, rangeDes, "#EDEDED", colorTitulo);
            rangeDes.Merge();
            comun.estableceEstiloTexto(rangeDes, true, "Helvetica Light", 10, 12, "#000000", false, true);
            comun.EstablecerEstiloTituloDatos(colorTitulo, true, "Helvetica Light", 12, true, 0);
            comun.establecerBordes(rangeDes, colorTitulo, "#395089");
            ws.Cells[iFila, "A"] = "Método de cálculo:";
            if (detalleIndicador.METODO != null) ws.Cells[iFila, "B"] = detalleIndicador.METODO;
            else ws.Cells[iFila, "B"] = "-";

            iFila++;
            rangeDes = ws.get_Range("B" + iFila, "E" + iFila);
            colorTitulo = ws.get_Range("A" + iFila, "A" + iFila);
            rangeDes.Rows.RowHeight = 67.5;
            rangeDes = ws.get_Range("B" + iFila, "E" + iFila);
            comun.intercalarColorSinLogo(ws, iFila, rangeDes, "#EDEDED", colorTitulo);
            rangeDes.Merge();
            comun.estableceEstiloTexto(rangeDes, true, "Helvetica Light", 10, 12, "#000000", false, true);
            comun.EstablecerEstiloTituloDatos(colorTitulo, true, "Helvetica Light", 12, true, 0);
            comun.establecerBordes(rangeDes, colorTitulo, "#395089");
            ws.Cells[iFila, "A"] = "Fuentes de información:";
            if (detalleIndicador.METODO != null) ws.Cells[iFila, "B"] = detalleIndicador.FUENTE;
            else ws.Cells[iFila, "B"] = "-";

            int filaFinal = iFila;

            Range rHeader = ws.get_Range("A" + 1, letter + 1);
            rHeader.Merge();
            comun.establecerBordesHeader(rHeader, true, 45, "#FFFFFF", true, "#395089");
            ws.Cells[1, "A"] = programa.NOMBRE;
            comun.EstablecerEstiloTitulo(rHeader, true, "Future", 14, "#FFFFFF", "#39508A", true, 0);

            Range grafica = ws.get_Range("F" + 3, letter + iFila);
            grafica.Merge();
            comun.EstablecerEstiloTitulo(grafica, true, "Helvetica Light", 12, "#FFFFFF", "#686F76", true, 0);

            Range tituloGrafica = ws.get_Range("F" + 2, letter + 2);
            tituloGrafica.Merge();
            comun.EstablecerEstiloTitulo(tituloGrafica, true, "Helvetica Light", 12, "#FFFFFF", "#686F76", true, 0);

            comun.establecerBordesGrafica(grafica, tituloGrafica, "#395089");
        }

        public void agregarDatos(Worksheet ws, Modelo.IndicadorSectorial detalleIndicador)
        {
            iFila++;
            int filaInicio = iFila;
            string letter = comun.columnLetter(columnaFinal);

            Range rLineaBase = ws.get_Range("A" + iFila, "B" + iFila);
            Range rDatoLB = ws.get_Range("C" + iFila, "C" + iFila);
            Range rPeriodicidad = ws.get_Range("D" + iFila, "E" + iFila);
            Range rDatoPeriodicidad = ws.get_Range("F" + iFila, "G" + iFila);
            Range rUnidadM = ws.get_Range("H" + iFila, "I" + iFila);
            Range rDatoUnidadM = ws.get_Range("j" + iFila, letter + iFila);
            Range rDatos = ws.get_Range("A" + iFila, letter + iFila);
            rDatos.Rows.RowHeight = 27.75;

            rLineaBase.Merge();
            rPeriodicidad.Merge();
            rDatoPeriodicidad.Merge();
            rUnidadM.Merge();
            rDatoUnidadM.Merge();

            ws.Cells[iFila, "A"] = "Línea Base";
            if (detalleIndicador.VALOR_LB != null) ws.Cells[iFila, "C"] = detalleIndicador.VALOR_LB;
            else ws.Cells[iFila, "C"] = "-";
            ws.Cells[iFila, "D"] = "Periodicidad";
            if (detalleIndicador.PERIODICIDAD != null) ws.Cells[iFila, "F"] = detalleIndicador.PERIODICIDAD;
            else ws.Cells[iFila, "F"] = "-";
            ws.Cells[iFila, "H"] = "Unidad de Medida";
            if (detalleIndicador.UDM != null) ws.Cells[iFila, "J"] = detalleIndicador.UDM;
            else ws.Cells[iFila, "J"] = "-";

            comun.estableceEstiloDatosFicha(rLineaBase, rDatoLB, rPeriodicidad, rDatoPeriodicidad, rUnidadM, rDatoUnidadM, rDatos, true, "Calibri", 11, "#FFFFFF", "#92D050", "#808080", "#203764", true);
            comun.establecerBordes(rDatos, rDatos, "#395089");

            iFila++;
            rDatos = ws.get_Range("A" + iFila, letter + iFila);
            rDatos.Cells.Rows.RowHeight = 2.75;
        }

        public void ProgramasAliados(Worksheet ws, List<SIMEPS.Modelo.ProgramaIndicadorMIR> programas)
        {
            iFila++;
            int filaInicio = iFila;
            string letter = comun.columnLetter(columnaFinal);

            Range rTitulo = ws.get_Range("A" + iFila, letter + iFila);
            rTitulo.Merge();
            ws.Cells[iFila, "A"] = "Programas alineados al indicador";
            comun.EstablecerEstiloTitulo(rTitulo, true, "Future", 11, "#333F4F", "#FFFFFF", true, 25.5);


            if (programas.Count > 0)
            {
                foreach (SIMEPS.Modelo.ProgramaIndicadorMIR aliado in programas)
                {
                    iFila++;
                    Range pALiados = ws.get_Range("A" + iFila, letter + iFila);
                    pALiados.Merge();
                    ws.Cells[iFila, "A"] = aliado.PP + " " + aliado.NOMBRE;
                    comun.EstableceEstiloProgramaAliado(iFila, pALiados, "#FFFFFF", true, 11, "Future", "#8EA9DB", "#808080");
                }
            }

        }


        public void Grafica(Worksheet ws, List<Modelo.Meta> datosGrafica)
        {
            int fin = 0;
            int inicio = 0, inicio2 = 0;


            if (datosGrafica.Count > 0)
            {
                var CicloMax = datosGrafica.Max(M => M.CICLO);
                var CicloMin = datosGrafica.Min(m => m.CICLO);
                ws.Cells[2, "F"] = "Histórico " + CicloMin + " - " + CicloMax;

                int Count = datosGrafica.Count;
                object[] ArrCiclo = new object[Count];
                object[] ArrMI = new object[Count];
                object[] ArrValor = new object[Count];
                object[] ArrLB = new object[Count];
                object[] ArrMeta = new object[Count];

                var cicloMax = datosGrafica.Max(x => x.CICLO);

                foreach (Modelo.Meta dato in datosGrafica)
                {
                    ArrCiclo[inicio] = dato.CICLO;
                    ArrMI[inicio] = dato.MI;
                    ArrValor[inicio] = dato.VALOR;
                    ArrLB[inicio] = dato.VALORLB;

                    if (cicloMax.ToString() == dato.CICLO.ToString())
                        ArrMeta[inicio] = dato.META;
                    else
                        ArrMeta[inicio] = "";

                    inicio++;
                }
                comun.Grafica4Lineas(ws, inicio2, fin, ArrCiclo, ArrMI, ArrValor, ArrLB, ArrMeta);
            }

        }
        public class RangosGraficas
        {
            public Range serie1 { get; set; }
            public Range serie2 { get; set; }
            public Range serie3 { get; set; }
            public Range serie4 { get; set; }
            public Range categoria { get; set; }
        }
    }

}
