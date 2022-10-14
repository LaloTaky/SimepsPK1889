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
using System.Data;
using System.Text;

namespace SIMEPS
{
    public partial class BDIndicadoresProgramaCSV : System.Web.UI.Page
    {

        IndicadoresDal data = new IndicadoresDal();
        ExcelUtilities comun = new ExcelUtilities();
        public int iCiclo = DateTime.Now.Year;
        Logger log = new Logger();
        protected void Page_Load(object sender, EventArgs e)
        {
            String nombreTemporalExcel = "";
            String sParametroIndicador = "";
            String sParametroNivel = "";
            String sUniversoSimeps = "";
            String sParametroOpcion = "";
            ReportesDal nuevoReporte = new ReportesDal("TD_RESOURCE_STORE");


            decimal anio = Request.Params["pCiclo"] == "-1" ? iCiclo : Convert.ToDecimal(Request.Params["pCiclo"].ToString());
            string ramo = Request.Params["pRamo"] == "-1" ? "0" : Request.Params["pRamo"].ToString();
            decimal dMatriz = Request.Params["pIdMatriz"] == "-1" ? -1 : Convert.ToDecimal(Request.Params["pIdMatriz"].ToString());
            string dTipo = Request.Params["pTipo"];
            string nombreTemporalCSV = comun.GetTimestamp(DateTime.Now);
            int resBorrar = 0;

            if (Request.Params["pIdIndicador"] != null)
                sParametroIndicador = Request.Params["pIdIndicador"];
            else sParametroIndicador = "0";
            sUniversoSimeps = "-1";
            if (Request.Params["pNivel"] != null)
                sParametroNivel = Request.Params["pNivel"];
            if (Request.Params["pOpcion"] != null)
                sParametroOpcion = Request.Params["pOpcion"];
            Logger log = new Logger();
            try
            {
                if (dTipo == "FIN")
                {
                    IndicadoresDal data = new IndicadoresDal();
                    ExcelUtilities comun = new ExcelUtilities();
                    decimal ciclo = Convert.ToDecimal(anio);
                    SIMEPS.Modelo.TipoReporte tipoReporte = nuevoReporte.tipoReporteDal(Convert.ToInt32(Utils.IdTiposReportes.baseIndicadoresFinCSV));
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
                        if (decimal.TryParse(anio.ToString(), out anio))
                        {
                            DataSet dataRpt = data.ConsultaReporteCSV(anio, "0", 0, 2, "FIN");
                            var dataTable = dataRpt.Tables[1];
                            //Datatable to CSV
                            var lines = new List<string>();
                            string[] columnNames = dataTable.Columns.Cast<DataColumn>().Where(column => column.ColumnName != "record_Id").
                                                              Select(column => column.ColumnName).
                                                              ToArray();
                            var header = string.Join(",", columnNames);
                            lines.Add(header);
                            List<string> lsRows = new List<string>();
                            foreach (DataRow row in dataTable.Rows)
                            {
                                string sValor = "";
                                for (int i = 0; i < row.ItemArray.Length - 1; i++)
                                {
                                    sValor = (sValor != "") ? sValor + "," + row.ItemArray[i] : row.ItemArray[i].ToString();
                                }
                                lsRows.Add(sValor);
                            }
                            lines.AddRange(lsRows);
                            comun.eliminarTemporales(Server.MapPath("~/Descargas"));
                            string filename = Server.MapPath("~/Descargas") + "\\" + nombreTemporalCSV + ".csv";
                            File.WriteAllLines(filename, lines, Encoding.UTF8);
                            var resultado = nuevoReporte.guardarDocumento(rutaArchivo, nombreArchivo, filename, tipoReporte.ID_TIPO_REPORTE, Convert.ToInt16(ciclo), null, null, null, null, null, null);

                            if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                            {
                                comun.descargarArchivo(Response, filename, nombreArchivo);
                            }
                        }

                    }
                }
                else if (dTipo == "PROGRAMA")
                {
                    String unidad = "0";
                    String palabraClave = "0";
                    decimal dIndicador = Convert.ToDecimal(sParametroIndicador);
                    var programasPorCicloRamo = data.ConsultarPrograma(anio, ramo, "0", "0", Convert.ToDecimal("-1"), Convert.ToDecimal("0"), "0", "A", "1", Convert.ToDecimal("-1")).ToList();
                    SIMEPS.Modelo.Programa programa = data.ConsultarPrograma(anio, ramo, unidad, palabraClave, dMatriz, dIndicador, sParametroNivel, null, sUniversoSimeps, -1).FirstOrDefault();
                    SIMEPS.Modelo.TipoReporte tipoReporte = nuevoReporte.tipoReporteDal(Convert.ToInt32(Utils.IdTiposReportes.baseIndicadoresCSV));
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
                        DataSet dataRpt = null;
                        dataRpt = data.ConsultaReporteCSV(anio, ramo, dMatriz, -1, "");
                        var dataTable = dataRpt.Tables[1];
                        //Datatable to CSV
                        var lines = new List<string>();

                        string[] columnNames = dataTable.Columns.Cast<DataColumn>().Where(column => column.ColumnName != "record_Id").
                                                        Select(column => column.ColumnName).
                                                        ToArray();
                        var header = string.Join(",", columnNames);
                        lines.Add(header);
                        List<string> lsRows = new List<string>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string sValor = "";
                            for (int i = 0; i < row.ItemArray.Length - 1; i++)
                            {
                                sValor = (sValor != "") ? sValor + "," + row.ItemArray[i] : row.ItemArray[i].ToString();
                            }
                            lsRows.Add(sValor);
                        }
                        lines.AddRange(lsRows);
                        comun.eliminarTemporales(Server.MapPath("~/Descargas"));
                        string filename = Server.MapPath("~/Descargas") + "\\" + nombreTemporalCSV + ".csv";
                        File.WriteAllLines(filename, lines, Encoding.UTF8);

                        var resultado = nuevoReporte.guardarDocumento(rutaArchivo, nombreArchivo, filename, tipoReporte.ID_TIPO_REPORTE, programa.CICLO, programa.RAMO_DEP, programa.MODALIDAD, programa.CLAVE, null, null, programa.ID_MATRIZ);

                        if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                        {
                            comun.descargarArchivo(Response, filename, nombreArchivo);
                        }
                    }
                }
            }
            catch (Exception error)
            {
                if (!error.Message.Equals("Excepción de descarga de archivo."))
                {
                    log.LogMessageToFile("Parametros: " + "Matriz=" + dMatriz + " Ciclo=" + anio + " Ramo=" + ramo + " Indicador=" + sParametroIndicador);
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