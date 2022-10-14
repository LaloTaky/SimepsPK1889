﻿using SIMEPS.Modelo;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Drawing;
using System.Reflection;
using System.Data.Common;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using Workbook = Microsoft.Office.Interop.Excel.Workbook;
using Worksheet = Microsoft.Office.Interop.Excel.Worksheet;
using Color = System.Drawing.Color;

namespace SIMEPS.Comun
{
    public class ExcelUtilities
    {
        Logger log = new Logger();
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        uint iProcessId = 0;


        /// <summary>
        /// Crea un libro de trabajo de excel
        /// </summary>
        /// <returns>Workbook de excel</returns>
        public Workbook crearWorkBook(Microsoft.Office.Interop.Excel.Application xlApp)
        {
            //xlApp = new Microsoft.Office.Interop.Excel.Application();
            //xlApp.Visible = true;
            return xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        }

        /// <summary>
        /// Crea una nueva hoja dentro del excel
        /// </summary>
        /// <param name="wb">Libro de trabajo</param>
        /// <returns>Hoja de excel</returns>
        public Worksheet crearWorkSheet(Workbook wb, String sPath, bool bCeldasblancas, int height, int imgAlt, int imgAnch)
        {
            Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.Add
            (System.Reflection.Missing.Value,
                        wb.Worksheets[wb.Worksheets.Count],
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value);

            if (bCeldasblancas) ws.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

            ws.Shapes.AddPicture(sPath + "\\logoConeval.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, imgAlt, imgAnch);
            Range r = (Range)ws.Cells[1, "A"];
            r.Rows.RowHeight = height;

            return ws;
        }
        public Worksheet crearWorkSheetSinLogo(Workbook wb, bool bCeldasblancas)
        {
            Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.Add(System.Reflection.Missing.Value, wb.Worksheets[wb.Worksheets.Count], System.Reflection.Missing.Value, System.Reflection.Missing.Value);

            if (bCeldasblancas) ws.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

            return ws;
        }
        /// <summary>
        /// Se encarga de eliminar los archivos temporales creados con aterioridad
        /// </summary>
        public void eliminarTemporales(String sPath)
        {
            try
            {
                DirectoryInfo source = new DirectoryInfo(sPath);
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
        /// Obtiene un string con la estampa de tiempo
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
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
        /// Realiza la descarga del excel creado
        /// </summary>
        /// <param name="PathToExcelFile">Ruta donde se encuentra el archivo excel</param>
        /// <param name="nombreArchivo">Nombre del Archivo al generar la descarga</param>     
        ///
        public void descargarArchivo(HttpResponse response, String PathToExcelFile, string nombreArchivo)
        {
            try
            {
                FileInfo file = new FileInfo(PathToExcelFile);
                String fileName = nombreArchivo;
                if (file.Exists)
                {
                    response.Clear();
                    response.ClearHeaders();
                    response.ClearContent();
                    response.Cookies["fileDownloadToken"].Value = "true";
                    response.HeaderEncoding = System.Text.Encoding.Default;
                    response.Charset = "ISO-8859-15";
                    response.AddHeader("content-disposition", "attachment; filename=\"" + fileName + "\"");
                    response.AddHeader("Content-Type", "application/Excel");
                    response.ContentType = "application/vnd.xls; charset=utf-8";
                    response.AddHeader("Content-Length", file.Length.ToString());
                    response.WriteFile(file.FullName);
                    response.End();
                }
                else
                {
                    response.Write("This file does not exist.");
                }
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Subproceso anulado."))
                {
                    throw new Exception("Excepción de descarga de archivo.");
                }
                else
                {
                    log.LogMessageToFile(e.Message);
                    log.LogMessageToFile(e.StackTrace);
                }
            }

        }
        /// <summary>
        /// Realiza la descarga de excel consultado de un file table
        /// </summary>
        /// <param name="response">Objeto Response</param>
        /// <param name="arrFile">Arreglo de Bytes de archivo</param>
        /// <param name="nombreArchivo">Nombre del Archivo</param>
        public void descargarArchivo(HttpResponse response, byte[] arrFile, string nombreArchivo)
        {
            try
            {
                response.Clear();
                response.ClearHeaders();
                response.ClearContent();
                response.Cookies["fileDownloadToken"].Value = "true";
                response.HeaderEncoding = System.Text.Encoding.Default;
                response.Charset = "ISO-8859-15";
                response.AddHeader("content-disposition", "attachment; filename=\"" + nombreArchivo + "\"");
                response.AddHeader("Content-Type", "application/Excel");
                response.ContentType = "application/vnd.xls; charset=utf-8";
                response.OutputStream.Write(arrFile, 0, arrFile.Length);
                response.End();
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Subproceso anulado."))
                {
                    throw new Exception("Excepción de descarga de archivo.");
                }
                else
                {
                    log.LogMessageToFile(e.Message);
                    log.LogMessageToFile(e.StackTrace);
                }
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
        /// Establece los estilos de tipo titulo al rango de celdas proporcionado
        /// </summary>
        /// <param name="aRange">Rango de celdas a establecer estilo</param>
        public void estableceEstiloColumnas(Range aRange, bool conBorde, int rowHeight, bool wraptext)
        {
            aRange.Cells.Font.Bold = true;
            aRange.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#B4C6E7"));
            aRange.Columns.AutoFit();
            aRange.VerticalAlignment = XlVAlign.xlVAlignTop;
            if (rowHeight != -1) aRange.Rows.RowHeight = rowHeight;
            aRange.Columns.ColumnWidth = 15;
            if (conBorde) aRange.Cells.BorderAround2(XlLineStyle.xlDot, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, Type.Missing, Type.Missing);
            if (wraptext) aRange.Cells.WrapText = true;
        }

        /// <summary>
        /// Establece los estilos de tipo titulo al rango de celdas proporcionado
        /// </summary>
        /// <param name="aRange">Rango de celdas a establecer estilo</param>
        public void estableceEstiloHeader(Range aRange, bool conBorde, int rowHeight, string colorCelda, bool autofit)
        {
            aRange.Cells.Font.Bold = true;
            aRange.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorCelda));
            if (autofit) aRange.Columns.AutoFit();
            aRange.VerticalAlignment = XlVAlign.xlVAlignTop;
            if (rowHeight != -1) aRange.Rows.RowHeight = rowHeight;
            if (conBorde) aRange.BorderAround2(XlLineStyle.xlDot, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, Type.Missing, Type.Missing);
        }
        public void establecerBordesHeader(Range aRange, bool conBorde, int rowHeight, string colorCelda, bool autofit, string colorBorde)
        {
            aRange.Cells.Font.Bold = true;
            aRange.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorCelda));
            if (autofit) aRange.Columns.AutoFit();
            aRange.VerticalAlignment = XlVAlign.xlVAlignTop;
            if (rowHeight != -1) aRange.Rows.RowHeight = rowHeight;
            if (conBorde)
            {
                aRange.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlDot;
                aRange.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlDot;
                aRange.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlDot;
                aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorBorde));
                aRange.Borders.Weight = XlBorderWeight.xlHairline;
                aRange.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlLineStyleNone;
                aRange.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlLineStyleNone;
                aRange.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
                aRange.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
            }
        }
        /// <summary>
        /// Establece los estilos de tipo titulo al rango de celdas proporcionado
        /// </summary>
        /// <param name="aRange">Rango de celdas a establecer estilo</param>
        public void estableceEstiloDatos(Range aRange, bool conBorde, int rowHeight, bool bAutofit)
        {
            if (bAutofit) aRange.Columns.AutoFit();
            aRange.VerticalAlignment = XlVAlign.xlVAlignTop;
            if (rowHeight != -1) aRange.Rows.RowHeight = rowHeight;
            if (conBorde) aRange.BorderAround2(XlLineStyle.xlDot, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, Type.Missing, Type.Missing);
        }

        /// <summary>
        /// Permite buscar de forma recursiva la columna donde debe escribir los datos de los historicos
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        /// <param name="iColumnaActual">Columna donde se va a buscar</param>
        /// <param name="sAnioBuscado">Anio que se esta buscando</param>
        /// <returns></returns>
        public int buscarColumna(Worksheet ws, int iColumnaActual, String sAnioBuscado)
        {
            String sAnioColumna = (string)ws.Cells[2, columnLetter(iColumnaActual)].value;

            if (sAnioColumna == null)
            {
                return iColumnaActual;
            }
            else if (sAnioColumna.IndexOf(sAnioBuscado) > -1 || sAnioColumna.Equals(""))
            {
                return iColumnaActual;
            }
            else
            {
                iColumnaActual++;
                return buscarColumna(ws, iColumnaActual, sAnioBuscado);
            }

        }

        /// <summary>
        /// Establece los estilos de tipo titulo al rango de celdas proporcionado
        /// </summary>
        /// <param name="aRange">Rango de celdas a establecer estilo</param>
        public void estableceEstiloTitulo(Range aRange, bool alignCenter, string font, int size)
        {
            if (alignCenter) aRange.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            aRange.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            aRange.Cells.Font.Bold = true;
            aRange.Cells.Font.Size = size;
            if (!String.IsNullOrEmpty(font)) aRange.Font.Name = font;
            aRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
        }

        public void estableceEstiloTexto(Range aRange, bool alignCenter, string FontTexto, int size, int bSize, string color, bool negrita, bool bNegrita)
        {
            if (alignCenter)
                aRange.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            aRange.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(color));
            aRange.Cells.Font.Bold = negrita;
            aRange.Cells.Font.Size = size;

            if (!String.IsNullOrEmpty(FontTexto))
                aRange.Font.Name = FontTexto;

            aRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
            aRange.WrapText = true;
        }
        public void EstablecerEstiloTituloDatos(Range rPrograma, bool alignCenter, string font, int size, bool negrita, double altoRow)
        {

            if (alignCenter)
                rPrograma.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;


            rPrograma.Cells.Font.Bold = negrita;
            rPrograma.Cells.Font.Size = size;
            if (altoRow != 0)
                rPrograma.Cells.Rows.RowHeight = altoRow;


            if (!String.IsNullOrEmpty(font))
                rPrograma.Font.Name = font;

            rPrograma.VerticalAlignment = XlVAlign.xlVAlignCenter;
            rPrograma.WrapText = alignCenter;
        }
        public void EstableceEstiloProgramaAliado(int iFila, Range rPrograma, string ColorTexto, bool TextoNegrita, int SizeTexto, string fontTexto, string ColorFondo1, string ColorFondo2)
        {
            rPrograma.Font.Name = fontTexto;
            rPrograma.Font.Size = SizeTexto;
            rPrograma.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(ColorTexto));
            rPrograma.Font.Bold = TextoNegrita;

            rPrograma.Rows.RowHeight = 23.25;
            rPrograma.VerticalAlignment = XlVAlign.xlVAlignCenter;
            rPrograma.HorizontalAlignment = XlHAlign.xlHAlignLeft;

            rPrograma.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            rPrograma.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            rPrograma.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
            rPrograma.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            rPrograma.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));
            rPrograma.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlLineStyleNone;
            rPrograma.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlLineStyleNone;
            rPrograma.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            rPrograma.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;

            if (iFila % 2 != 1)
                rPrograma.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(ColorFondo1));
            if (iFila % 2 != 0)
                rPrograma.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(ColorFondo2));
        }
        public void EstablecerEstiloTitulo(Range rPrograma, bool alignCenter, string font, int size, string colorFondo, string colorTexto, bool negrita, double altoRow)
        {
            if (alignCenter)
                rPrograma.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            rPrograma.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorTexto));
            rPrograma.Cells.Font.Bold = negrita;
            rPrograma.Cells.Font.Size = size;
            if (altoRow != 0)
                rPrograma.Cells.Rows.RowHeight = altoRow;
            rPrograma.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorFondo));

            if (!String.IsNullOrEmpty(font))
                rPrograma.Font.Name = font;

            rPrograma.VerticalAlignment = XlVAlign.xlVAlignCenter;
            rPrograma.WrapText = alignCenter;
        }
        public void estableceEstiloDatosFicha(Range rLb, Range rDatoLB, Range rPer, Range rDatoPer, Range rUM, Range rDatoUM, Range Datos, bool alignCenter, string font, int size, string colorTexto, string colorFondoLB, string colorFondoPer, string colorFondoUM, bool negritaTexto)
        {
            if (alignCenter)
                Datos.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            if (!String.IsNullOrEmpty(font))
                Datos.Font.Name = font;

            Datos.VerticalAlignment = XlVAlign.xlVAlignCenter;
            Datos.WrapText = true;
            Datos.Cells.Font.Size = size;

            rLb.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorTexto));
            rLb.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorFondoLB));
            rLb.Cells.Font.Bold = negritaTexto;
            rDatoLB.Font.Color = Color.Black;

            rPer.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorTexto));
            rPer.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorFondoPer));
            rPer.Cells.Font.Bold = negritaTexto;
            rDatoPer.Font.Color = Color.Black;

            rUM.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorTexto));
            rUM.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorFondoUM));
            rUM.Cells.Font.Bold = negritaTexto;
            rDatoUM.Font.Color = Color.Black;
        }

        /// <summary>
        /// Permite cerrar el proceso de excel creado por la aplicación
        /// </summary>
        /// <param name="xlApp">Aplicacion excel</param>
        /// <param name="wb">Libro de trabajo de excel</param>
        public void terminarProcesoExcel(Microsoft.Office.Interop.Excel.Application xlApp, Workbook wb)
        {
            try
            {
                GetWindowThreadProcessId((IntPtr)xlApp.Hwnd, out iProcessId);
                if (wb != null) wb.Close(false);
                if (xlApp != null) xlApp.Quit();
                if (wb != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                if (xlApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);

                GC.Collect();
                GC.WaitForPendingFinalizers();

                System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                foreach (System.Diagnostics.Process p in process)
                {
                    if (p.Id == iProcessId)
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// Permite rellenar el color de las filas de forma intercalada
        /// </summary>
        /// <param name="ws">Worksheet</param>
        /// <param name="iNumFila">Fila a aplicar el relleno</param>
        /// <param name="r">Rango de celdas que pertenecen a la fila donde se aplicara el color</param>
        /// <param name="color">Color de la celda.</param>
        public void intercalarColor(Worksheet ws, int iNumFila, Range r, string color)
        {
            if (iNumFila % 2 != 0)
                r.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(color));
            else
                r.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));


        }
        public void intercalarColorSinLogo(Worksheet ws, int iNumFila, Range r, string color, Range r2)
        {
            if (iNumFila % 2 != 1)
            {
                r.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(color));
                r2.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(color));
            }
            if (iNumFila % 2 != 0)
            {
                r.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#DDE7F7"));
                r2.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#DDE7F7"));
            }

            if (iNumFila % 2 != 1)
            {
                r2.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#686F76"));
            }
            if (iNumFila % 2 != 0)
            {
                r2.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#39508A"));
            }

        }

        /// <summary>
        /// Establece los estilos de acuerdo al rango.
        /// </summary>
        /// <param name="aRange">rango de celdas donde se aplicará el estilo</param>
        /// <param name="conBorde">Si la celda llevará borde</param>
        /// <param name="rowHeight">tamaño de altura</param>
        /// <param name="colorBorde">color del borde que se le aplicará a las celdas ó celda</param>
        /// <param name="alignVertical">La alineación vertical de las celdas</param>
        /// <param name="font">Fuente de las celdas</param>
        /// <param name="size">Tamaño de las celdas</param>
        /// <param name="bold">Si se colocará el texto en negritas</param>
        /// <param name="bAutoFit">Ajuste Automático</param>
        /// <param name="wrapText">Ajuste de texto</param>
        /// <param name="combinar">combinación de celdas</param>
        /// <param name="alignHorizontal">Tipo de alineación Horizontal</param>
        /// <param name="widthColumn">Tamaño de ancho de la columna</param>
        public void estableceEstiloEtiquetas(Range aRange, bool conBorde, int rowHeight, string colorBorde, XlVAlign alignVertical, string font, int size, bool bold, bool bAutoFit, bool wrapText, bool combinar, XlHAlign? alignHorizontal, int widthColumn)
        {
            if (combinar) aRange.Merge();
            aRange.Cells.Font.Bold = bold;
            if (bAutoFit) aRange.Columns.AutoFit();
            aRange.VerticalAlignment = alignVertical;
            if (alignHorizontal != null) aRange.HorizontalAlignment = alignHorizontal;
            if (rowHeight != -1) aRange.Rows.RowHeight = rowHeight;
            if (conBorde)
            {
                ColorConverter cc = new System.Drawing.ColorConverter();
                aRange.BorderAround2(XlLineStyle.xlDot, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, Type.Missing, Type.Missing);
                aRange.Borders.Color = ColorTranslator.ToOle((Color)cc.ConvertFromString(colorBorde));
                aRange.Borders.LineStyle = XlLineStyle.xlDot;
            }
            if (!String.IsNullOrEmpty(font)) aRange.Cells.Font.Name = font;
            if (size != -1) aRange.Cells.Font.Size = size;
            aRange.Cells.WrapText = wrapText;
            if (widthColumn != -1) aRange.ColumnWidth = widthColumn;
        }
        /// <summary>
        /// Agregar Graficas
        /// </summary>
        /// <param name="ws">worksheet</param>
        /// <param name="oSerie1">Objeto del serie 1 (Range o long[])</param>
        /// <param name="oSerie2">Objeto del serie 2 (Range o long[])</param>
        /// <param name="oCategoria">Objeto para agregar las categorias (Range o string[])</param>
        /// <param name="titulo">Titulo de la gráfica</param>
        /// <param name="iPosLeft">Posición a la Izquierda de la gráfica</param>
        /// <param name="iPosTop">Posición de la parte superior de la gráfica</param>
        /// <param name="iWidth">Ancho de la gráfica</param>
        /// <param name="iHeight">Altura de la gráfica</param>
        /// <param name="nameSerie1">Leyenda de la Serie 1</param>
        /// <param name="nameSerie2">Leyenda de la Serie 2</param>
        /// <param name="nameCategoria">Leyenda de la Categoria</param>
        /// <param name="colorSerie1">Color de la gráfica de la Serie 1 int(RGB) si pasa null valor por default es azul</param>
        /// <param name="colorSerie2">Color de la gráfica de la Serie 2 de tipo int (RGB) si pasa null valor por default es amarillo</param>
        /// <param name="CharStyle">Número de estilo d ela gráfica</param>
        /// <param name="fontSizeTitle">Tamaño del titulo de la gráfica</param>
        /// <param name="legendPosition">Posicion de la leyenda</param>
        /// <param name="NumberFormatY">Si agrega el formato numérico del eje de las Y</param>
        /// <param name="lineStyle">Estilo de la línea del border</param>
        /// <param name="italic">Titulo con estilo de letra italic</param>
        /// <param name="StyleSerie">Valor booleano para definir estilos de la serie</param>
        /// <param name="TamañoSeries">Tamaño de punto de datos</param>
        /// <param name="TamañoLineSerie">Tamaño de linea de la serie</param>
        /// <param name = "StyleMarkerSerie" >Estilo para definir los puntos de datos</param>
        /// <param name = "AxisValueHide" >Valor booleano para mostrar o no los label de y</param>
        /// 
        /// 


        public void graficaLineal(Worksheet ws, Object oSerie1, Object oSerie2, Object oCategoria, string titulo, double iPosLeft, double iPosTop, double iWidth, double iHeight, string nameSerie1, string nameSerie2, string nameCategoria, int colorSerie1, int colorSerie2, int CharStyle, int fontSizeTitle, int ChartLayout, XlLegendPosition? legendPosition, bool NumberFormatY, XlLineStyle? lineStyle, bool italic, bool StyleSerie, int TamañoSeries, int TamañoLineSerie, XlMarkerStyle StyleMarkerSerie, bool AxisValueHide)
        {
            Series series1 = null, series2 = null;
            ChartObject chartObject = null;
            Array serieArray = null, aCategoria = null;
            ChartObjects charts = ws.ChartObjects() as Microsoft.Office.Interop.Excel.ChartObjects;
            chartObject = charts.Add(iPosLeft, iPosTop, iWidth, iHeight) as Microsoft.Office.Interop.Excel.ChartObject;
            chartObject.Border.LineStyle = lineStyle;
            if (lineStyle != null) chartObject.Border.Color = System.Drawing.Color.FromArgb(0, 0, 0).ToArgb();
            Chart chart = chartObject.Chart;
            String sTitulo = "";
            chart.ChartType = XlChartType.xlLineMarkers;

            SeriesCollection seriesCollection = (SeriesCollection)chart.SeriesCollection(Missing.Value);
            series1 = seriesCollection.NewSeries();

            //Se agregan los valores de la serie 1
            if (oSerie1 is Array)
            {
                serieArray = (Array)oSerie1;
                if (serieArray.LongLength > 0)
                    series1.Values = concatArray(serieArray);
            }
            else
            {
                if (oSerie1 is Range)
                    series1.Values = oSerie1;
            }

            if (!String.IsNullOrEmpty(nameSerie1)) series1.Name = nameSerie1;

            series2 = seriesCollection.NewSeries();

            //Se agregan los valores de la serie 2
            if (oSerie2 is Array)
            {
                serieArray = (Array)oSerie2;
                if (serieArray.LongLength > 0)
                    series2.Values = concatArray(serieArray);
            }
            else
            {
                if (oSerie2 is Range)
                    series2.Values = oSerie2;
            }

            if (!String.IsNullOrEmpty(nameSerie2)) series2.Name = nameSerie2;

            //Layout Chart
            chart.ApplyLayout(ChartLayout);
            chart.ChartStyle = CharStyle;

            Axis xAxis = (Axis)chart.Axes(XlAxisType.xlCategory, XlAxisGroup.xlPrimary);
            if (oCategoria != null)
            {
                if (oCategoria is Array)
                {
                    aCategoria = (Array)oCategoria;
                    if (aCategoria.LongLength > 0)
                        xAxis.CategoryNames = concatArray(aCategoria);
                }
                else
                {
                    if (oCategoria is Range)
                        xAxis.CategoryNames = oCategoria;
                }
            }
            //    xAxis.CategoryNames = oCategoria;
            if (!String.IsNullOrEmpty(nameCategoria))
            {
                xAxis.HasTitle = true;
                xAxis.AxisTitle.Caption = nameCategoria;
            }
            else
                xAxis.HasTitle = false;


            Axis xAxisValue = (Axis)chart.Axes(XlAxisType.xlValue, XlAxisGroup.xlPrimary);
            if (NumberFormatY && !AxisValueHide) xAxisValue.TickLabels.NumberFormat = "#,##0.0";
            if (AxisValueHide) xAxisValue.TickLabels.Delete();
            xAxisValue.HasTitle = false;

            if (legendPosition != null)
                chart.Legend.Position = (XlLegendPosition)legendPosition;
            else
                chart.HasLegend = false;

            if (StyleSerie)
            {
                if (StyleMarkerSerie != null)
                    if (TamañoSeries != 0)
                        series1.MarkerStyle = StyleMarkerSerie;
                series1.MarkerSize = TamañoSeries;
                series1.Format.Line.Weight = TamañoLineSerie;
                series2.MarkerStyle = StyleMarkerSerie;
                series2.MarkerSize = TamañoSeries;
                series2.Format.Line.Weight = TamañoLineSerie;
            }

            if (colorSerie2 != 0)
            {
                series2.Format.Line.ForeColor.RGB = colorSerie2;
                series2.Format.Fill.ForeColor.RGB = colorSerie2;
            }

            if (colorSerie1 != 0)
            {
                series1.Format.Line.ForeColor.RGB = colorSerie1;
                series1.Format.Fill.ForeColor.RGB = colorSerie1;
            }
            //Titulo de la gráfica
            if (titulo != null)
            {
                chart.HasTitle = true;
                if (titulo.Length > 255)
                    sTitulo = titulo.Substring(0, 255);
                else
                    sTitulo = titulo;
                chart.ChartTitle.Text = sTitulo;
                chart.ChartTitle.Font.Size = fontSizeTitle;
                chart.ChartTitle.Font.Italic = italic;
            }
            else
                chart.HasTitle = false;
            if (series1.HasDataLabels)
            {
                //Posiciona la label en el centro                
                DataLabels lb = (DataLabels)series1.DataLabels();
                lb.Position = XlDataLabelPosition.xlLabelPositionCenter;
                lb.Format.TextFrame2.TextRange.Font.Fill.ForeColor.ObjectThemeColor = Microsoft.Office.Core.MsoThemeColorIndex.msoThemeColorBackground1;
                lb.Format.TextFrame2.TextRange.Font.Bold = Microsoft.Office.Core.MsoTriState.msoTrue;
                lb.Format.TextFrame2.TextRange.Font.Size = 8;
            }

            if (series2.HasDataLabels)
            {
                //Posiciona la label en el centro                
                DataLabels lb = (DataLabels)series2.DataLabels();
                lb.Position = XlDataLabelPosition.xlLabelPositionCenter;
                lb.Format.TextFrame2.TextRange.Font.Fill.ForeColor.ObjectThemeColor = Microsoft.Office.Core.MsoThemeColorIndex.msoThemeColorBackground1;
                lb.Format.TextFrame2.TextRange.Font.Bold = Microsoft.Office.Core.MsoTriState.msoTrue;
                lb.Format.TextFrame2.TextRange.Font.Size = 8;
            }
        }

        public void Grafica4Lineas(Worksheet ws, int inicio, int fin, Object ciclo, Object MI, Object Valor, Object LB, Object Meta)
        {
            object misValue = System.Reflection.Missing.Value;

            Series series1 = null, series2 = null, series3 = null, series4 = null;
            Array serieArray = null, aCategoria = null;

            ChartObjects xlCharts = (ChartObjects)ws.ChartObjects(Type.Missing);
            ChartObject myChart = (ChartObject)xlCharts.Add(339.5, 100, 445, 250);
            Chart chartPage = myChart.Chart;

            chartPage.ChartType = XlChartType.xlLineMarkers;
            chartPage.Legend.Position = XlLegendPosition.xlLegendPositionBottom;
            chartPage.Legend.Top = 217;
            chartPage.Legend.Left = 0;
            chartPage.Legend.Width = 435;
            chartPage.Legend.Height = 25;


            SeriesCollection seriesCollection = (SeriesCollection)chartPage.SeriesCollection(Missing.Value);

            series1 = seriesCollection.NewSeries();
            series2 = seriesCollection.NewSeries();
            series3 = seriesCollection.NewSeries();
            series4 = seriesCollection.NewSeries();

            //Se agregan los valores de la serie 1
            if (MI is Array)
            {
                serieArray = (Array)MI;
                if (serieArray.LongLength > 0)
                    series3.Values = ToArray(serieArray);
            }
            else
            {
                if (MI is Range)
                    series3.Values = MI;
            }


            //Se agregan los valores de la serie 2
            if (Valor is Array)
            {
                serieArray = (Array)Valor;
                if (serieArray.LongLength > 0)
                    series2.Values = ToArray(serieArray);
            }
            else
            {
                if (Valor is Range)
                    series2.Values = Valor;
            }


            //Se agregan los valores de la serie 3
            if (LB is Array)
            {
                serieArray = (Array)LB;
                if (serieArray.LongLength > 0)
                    series1.Values = ToArray(serieArray);
            }
            else
            {
                if (LB is Range)
                    series1.Values = LB;
            }


            //Se agregan los valores de la serie 4
            if (Meta is Array)
            {
                serieArray = (Array)Meta;
                if (serieArray.LongLength > 0)
                    series4.Values = ToArray(serieArray);
            }
            else
            {
                if (Meta is Range)
                    series4.Values = Meta;
            }


            series3.Name = "Meta Planeada";
            series3.Format.Line.ForeColor.RGB = (int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbDarkBlue;
            series3.MarkerStyle = XlMarkerStyle.xlMarkerStyleCircle;
            series3.MarkerBackgroundColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#072a5f"));

            series4.Name = "Meta Planeada 2024";
            series4.Format.Line.ForeColor.RGB = (int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbDarkGray;
            series4.MarkerStyle = XlMarkerStyle.xlMarkerStyleCircle;
            series4.MarkerBackgroundColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#868484"));

            series2.Name = "Meta Alcanzada";
            series2.Format.Line.ForeColor.RGB = (int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbDarkGreen;
            series2.MarkerStyle = XlMarkerStyle.xlMarkerStyleCircle;
            series2.MarkerBackgroundColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#00a94f"));

            series1.Name = "Línea Base";
            series1.Format.Line.ForeColor.RGB = (int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbDarkGray;
            series1.MarkerStyle = XlMarkerStyle.xlMarkerStyleDot;
            series1.MarkerBackgroundColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#c1c1c1"));


            Axis xAxis = (Axis)chartPage.Axes(XlAxisType.xlCategory, XlAxisGroup.xlPrimary);

            if (ciclo != null)
            {
                if (ciclo is Array)
                {
                    aCategoria = (Array)ciclo;
                    if (aCategoria.LongLength > 0)
                        xAxis.CategoryNames = ToArray(aCategoria);
                }
                else
                {
                    if (ciclo is Range)
                        xAxis.CategoryNames = ciclo;
                }
            }

        }

        /// <summary>
        /// Concatena el array de objetos en un string para su pintado en la gráfica respetando los #NA
        /// </summary>
        /// <param name="array">Arreglo de Objetos</param>
        /// <returns></returns>
        public string concatArray(Array array)
        {
            string val = "", ValorX = "";
            try
            {
                for (int j = 0; j < array.LongLength; j++)
                {
                    if (j == array.LongLength - 1)
                    {
                        ValorX = Convert.ToString(array.GetValue(j));
                        val = val + ValorX.Replace('*', ' ');
                    }
                    else
                    {
                        ValorX = Convert.ToString(array.GetValue(j));
                        val = val + ValorX.Replace('*', ' ') + ",";
                    }
                }
                //Cadena con valores para grafica
                return "{ " + val + " }";
            }
            catch (Exception error)
            {
                Logger log = new Logger();
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return "{}";
            }
        }
        public string ToArray(Array array)
        {
            string val = "", ValorX = "";
            try
            {
                for (int j = 0; j < array.LongLength; j++)
                {
                    if (j == array.LongLength - 1)
                    {
                        ValorX = Convert.ToString(array.GetValue(j));
                        if (ValorX != "" && ValorX != "0")
                            val = val + ValorX;
                        else
                            val = val + "#N/A";
                    }
                    else
                    {
                        ValorX = Convert.ToString(array.GetValue(j));
                        if (ValorX != "" && ValorX != "0")
                            val = val + ValorX + ",";
                        else
                            val = val + "#N/A,";
                    }
                }
                //Cadena con valores para grafica
                return "{ " + val + " }";
            }
            catch (Exception error)
            {
                Logger log = new Logger();
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return "{}";
            }
        }

        public void establecerBordes(Range rango, Range bRango, string color)
        {
            rango.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlDot;
            rango.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlDot;
            rango.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlDot;
            rango.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(color));
            rango.Borders.Weight = XlBorderWeight.xlHairline;
            rango.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlLineStyleNone;
            rango.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlLineStyleNone;
            rango.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            rango.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;

            bRango.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlDot;
            bRango.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlDot;
            bRango.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlDot;
            bRango.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(color));
            bRango.Borders.Weight = XlBorderWeight.xlHairline;
            bRango.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlLineStyleNone;
            bRango.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlLineStyleNone;
            bRango.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            bRango.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
        }
        public void establecerBordesGrafica(Range rango, Range bRango, string color)
        {

            rango.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlDot;
            rango.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(color));
            rango.Borders.Weight = XlBorderWeight.xlHairline;
            rango.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlLineStyleNone;
            rango.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlLineStyleNone;
            rango.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            rango.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;


            bRango.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlDot;
            bRango.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(color));
            bRango.Borders.Weight = XlBorderWeight.xlHairline;
            bRango.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlLineStyleNone;
            bRango.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlLineStyleNone;
            bRango.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            bRango.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
        }

        public System.Data.DataTable ToDataTable<T>(List<T> items)
        {
            try
            {
                System.Data.DataTable dataTable = new System.Data.DataTable(typeof(T).Name);

                //Obtiene todas las propiedades
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Establece los nombres de las columnas con los nombres de las propiedades
                    dataTable.Columns.Add(prop.Name);
                }
                // Establece el tipo de dato de las columnas
                for (int i = 0; i < Props.Length; i++)
                {
                    if (Props[i].PropertyType.FullName.Contains("Decimal"))
                    {
                        dataTable.Columns[i].DataType = System.Type.GetType("System.Decimal");
                    }
                    else if (Props[i].PropertyType.FullName.Contains("Int32"))
                    {
                        dataTable.Columns[i].DataType = System.Type.GetType("System.Int32");
                    }
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        object aux = Props[i].GetValue(item, null);
                        if (Props[i].PropertyType.FullName.Contains("Date"))
                        {
                            aux = (DateTime?)Props[i].GetValue(item, null);
                            if (aux != null)
                                aux = ((DateTime)aux).ToString("yyyy-MM-dd HH:mm:ss.fff");
                            else
                                aux = null;
                        }

                        if (Props[i].PropertyType.FullName.Contains("Decimal"))
                        {
                            aux = (Decimal?)Props[i].GetValue(item, null);
                            if (aux == null)
                                aux = default(decimal);
                        }
                        //Inserta los valores de las propiedades como valores de los renglones
             
                        if (aux != null)
                        {
                            values[i] = aux.ToString();
                        }
                        else
                        {
                            values[i] = string.Empty;
                        }
                    }
                    dataTable.Rows.Add(values);
                }
                return dataTable;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void cargaTabla(DocumentFormat.OpenXml.Spreadsheet.SheetData sheet, System.Data.DataTable tabla)
        {
            try
            {
                foreach (DataRow item in tabla.Rows)
                {


                    Row r = new Row();
                    for (int i = 0; i < item.ItemArray.Length; i++)
                    {
                        Cell c = new Cell()
                        {
                            CellValue = new CellValue(item[i].ToString()),
                            DataType = CellValues.String
                        };
                        r.Append(c);

                    }

                    sheet.Append(r);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}