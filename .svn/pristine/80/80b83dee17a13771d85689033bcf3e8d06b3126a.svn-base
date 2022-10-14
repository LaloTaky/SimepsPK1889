// VERSION PK214
using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using SIMEPS.Dal;
using SIMEPS.Modelo;
using SIMEPS.Comun;
using System.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Web;




namespace SIMEPS
{
    public partial class Monitoreo : System.Web.UI.Page
    {
        int iFila = 1;
        int filaGNum = 0;
        int RangoInicial = 0;
        int RangoFinal = 3;
        int ultimaColumna = 1;
        string texto = "Componente ";
        int Consecutivo = 1;
        int ConseComponente = 1;
        string primerAño = "";
        string ultAño = "";
        int FilaP = 0;
        string desc_nivel = "";
        int num = 0;
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

            if (Request.Params["pIdMatriz"] != null)
                sParametroMatriz = Request.Params["pIdMatriz"];

            if (Request.Params["pRamo"] != null)
                sParametroRamo = Request.Params["pRamo"];

            if (Request.Params["pCiclo"] != null)
                sParametroCiclo = Request.Params["pCiclo"];

            if (Request.Params["pIdIndicador"] != null)
                sParametroIndicador = Request.Params["pIdIndicador"];
            else sParametroIndicador = "0";

            if (Request.Params["pNivel"] != null)
                sParametroNivel = Request.Params["pNivel"];

            if (Request.Params["pOpcion"] != null)
                sParametroOpcion = Request.Params["pOpcion"];

            sUniversoSimeps = "-1";

            if (!sParametroMatriz.Equals("") && !sParametroCiclo.Equals("") && !sParametroRamo.Equals(""))
            {
                try
                {
                    nombreTemporalExcel = comun.GetTimestamp(DateTime.Now);
                    comun.eliminarTemporales(Server.MapPath("~/Descargas"));

                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    //xlApp.Visible = true;
                    wb = comun.crearWorkBook(xlApp);

                    decimal dMatriz = Convert.ToDecimal(sParametroMatriz);
                    decimal ciclo = Convert.ToDecimal(sParametroCiclo);
                    String ramo = sParametroRamo;
                    String unidad = "0";
                    String palabraClave = "0";
                    decimal dIndicador = Convert.ToDecimal(sParametroIndicador);
                    var programasPorCicloRamo = indicadores.ConsultarPrograma(ciclo, ramo, "0", "0", Convert.ToDecimal("-1"), Convert.ToDecimal("0"), "0", "A", "1", Convert.ToDecimal("-1")).ToList();
                    SIMEPS.Modelo.Programa programa = indicadores.ConsultarPrograma(ciclo, ramo, unidad, palabraClave, dMatriz, dIndicador, sParametroNivel, null, sUniversoSimeps, -1).FirstOrDefault();
                    SIMEPS.Modelo.TipoReporte tipoReporte = nuevoReporte.tipoReporteDal(Convert.ToInt32(Utils.IdTiposReportes.monitoreo));
                    string nombreArchivo = "";
                    int resBorrar = 0;
                    nombreArchivo = tipoReporte.NOMBRE_ARCHIVO + "_" + programa.CICLO + "_" + programa.RAMO_DEP + "_" + programa.MODALIDAD + "_" + string.Format("{0:000}", programa.CLAVE) + "_" + programa.ID_MATRIZ + "." + tipoReporte.FORMATO;

                    string rutaArchivo = "SIPS\\" + programa.CICLO + "\\" + tipoReporte.NOMBRE_ARCHIVO + "\\" + programa.RAMO_DEP;

                    byte[] archivoRecuperado = null;
                    bool existeTR = false;

                    if (sParametroOpcion.ToLower() != "actualiza" && sParametroOpcion.ToLower() != "genera")
                    {
                        //condición para actualizar los archivos en BD desde la fecha de liberación de nueva regla.
                        DateTime fechaDocumento = nuevoReporte.FechaCreacionDocumento(nombreArchivo);
                        if (fechaDocumento != DateTime.Parse("01/01/0001") && fechaDocumento <= DateTime.Parse("13/04/2020").Date)
                            sParametroOpcion = "actualiza";
                        else if (fechaDocumento != DateTime.Parse("01/01/0001"))
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

                        ws = crearWorkSheet(wb);
                        iFila = 1;
                        //INFORMACION GENERAL DEL PROGRAMA
                        //*****************************************************************
                        int Count = programa.PRESUPUESTOS.Count;
                        bool valor = false;
                        object[] ArrGastoEjercido = new object[Count];
                        object[] ArrGastoOriginal = new object[Count];
                        object[] ArrAño = new object[Count];
                        int i = 0;
                        foreach (Presupuesto presupuestos in programa.PRESUPUESTOS)
                        {
                            if (valor == false)
                            {
                                primerAño = Convert.ToString(presupuestos.CICLO);
                                valor = true;
                            }

                            ultAño = Convert.ToString(presupuestos.CICLO);

                            ArrGastoEjercido[i] = Convert.ToInt32(presupuestos.IMPORTE_EJERCIDO_MDP);
                            ArrGastoOriginal[i] = Convert.ToInt32(presupuestos.IMPORTE_ORIGINAL_MDP);


                            ArrAño[i] = Convert.ToInt32(presupuestos.CICLO);
                            i++;
                        }
                        //Metodo grafica información general
                        double left = (ws.get_Range("F1").Left + 3);
                        double top = (ws.get_Range("F8").Top + 1);
                        int colorS2 = 0;
                        int colorS1 = 0;
                        comun.graficaLineal(ws, ArrGastoEjercido, ArrGastoOriginal, ArrAño, "Evolución del Gasto " + primerAño + "-" + ultAño, left, top, 416, 135, "Gasto ejercido", "Gasto aprobado", "Año", colorS1, colorS2, 6, 12, 9, XlLegendPosition.xlLegendPositionBottom, false, null, true, true, 20, 2, XlMarkerStyle.xlMarkerStyleCircle, false);
                        //Apartado de informacion general
                        agregarInfoGeneralProgram(ws, programa);


                        //INDICADORES DE RESULTADO
                        //*****************************************************************
                        //Datos para grafica de indicadores
                        iFila = 17;
                        filaGNum = 18;
                        int RangoImg = 23;
                        int j = 0;
                        ws.Cells[iFila, "A"] = "Indicadores de Resultados";
                        Range aRange = ws.get_Range("A" + iFila.ToString(), "L" + iFila.ToString());
                        aRange.Merge();
                        estableceEstiloTitulo(aRange, "#1B4367", "#ffffff", 11, false, true, true);

                        //PROPOSITOS
                        var proposito = from p in programa.OBJETIVOS
                                        where p.NIVEL == 2
                                        select p;

                        foreach (var prop in proposito)
                        {
                            foreach (Indicador indicador in prop.INDICADORES)
                            {
                                int TotalPropositos = prop.INDICADORES.Count();
                                int count = indicador.HISTORICOS.Count;
                                i = 0;
                                object[] ArrMetaAlcanzada = new object[count];
                                object[] ArrMetaPlaneada = new object[count];
                                object[] ArrCiclo = new object[count];

                                foreach (HistoricoIndicador historico in indicador.HISTORICOS)
                                {
                                    if (historico.META_ALCANZADA.Equals("-")) historico.META_ALCANZADA = "#N/A";
                                    if (historico.META_PLANEADA.Equals("-")) historico.META_PLANEADA = "#N/A";
                                    //Llena arrays
                                    ArrMetaAlcanzada[i] = historico.META_ALCANZADA;
                                    ArrMetaPlaneada[i] = historico.META_PLANEADA;
                                    ArrCiclo[i] = historico.ANIO;
                                    i++;
                                }
                                if (TotalPropositos > 1)
                                {
                                    if (j > 0)
                                    {
                                        RangoImg = RangoImg + 6;
                                    }
                                }
                                agregarIndicadoresResul(ws, programa, indicador, ArrMetaAlcanzada, ArrMetaPlaneada, ArrCiclo, RangoImg);
                                j++;
                            }
                        }

                        //INDICADORES DE COMPONENTES
                        //*****************************************************************
                        iFila++;
                        ws.Cells[iFila, "A"] = "Entrega de bienes y servicios (Indicadores de Componentes)";
                        aRange = ws.get_Range("A" + iFila.ToString(), "L" + iFila.ToString());
                        aRange.Merge();
                        estableceEstiloTitulo(aRange, "#1B4367", "#ffffff", 11, false, true, true);
                        iFila++;
                        RangoInicial = 0;
                        RangoFinal = 3;
                        ultimaColumna = 1;

                        //COMPONENTES
                        var componente = from c in programa.OBJETIVOS
                                         where c.NIVEL == 3
                                         select c;

                        var TotalComponentes = Convert.ToInt16(componente.LongCount());

                        foreach (var com in componente)
                        {
                            foreach (Indicador indicador in com.INDICADORES)
                            {
                                i = 0;
                                int count = indicador.HISTORICOS.Count;
                                object[] ArrMetaAlcanz = new object[count];
                                object[] ArrMetaPlane = new object[count];
                                object[] ArrCic = new object[count];

                                foreach (HistoricoIndicador historico in indicador.HISTORICOS)
                                {
                                    if (historico.META_ALCANZADA.Equals("-")) historico.META_ALCANZADA = "#N/A";
                                    if (historico.META_PLANEADA.Equals("-")) historico.META_PLANEADA = "#N/A";
                                    //Llena arrays
                                    ArrMetaAlcanz[i] = historico.META_ALCANZADA;
                                    ArrMetaPlane[i] = historico.META_PLANEADA;
                                    ArrCic[i] = historico.ANIO;
                                    i++;
                                }
                                //Apartado de Indicadores componentes
                                agregarIndicadoresCompon(ws, programa, indicador, ArrMetaAlcanz, ArrMetaPlane, ArrCic, TotalComponentes);
                            }
                        }

                        //VALORACIONES
                        //*****************************************************************
                        int v = 0;
                        i = 0;
                        v = programa.VALORACIONES.Count;
                        agregarValoracionesCone(ws);

                        int[] arrAnio = new int[v];
                        int[] arrValDis = new int[v];
                        int[] arrValInd = new int[v];
                        int[] arrValTot = new int[v];
                        foreach (Valoracion valorac in programa.VALORACIONES)
                        {
                            arrAnio[i] = Convert.ToInt16(valorac.CICLO);
                            arrValDis[i] = Convert.ToInt16(valorac.CALIF_DIS);
                            arrValInd[i] = Convert.ToInt16(valorac.CALIF_IND);
                            arrValTot[i] = Convert.ToInt16(valorac.CALIF_TOT);
                            i++;
                        }
                        //Grafica MIR
                        aRange = ws.get_Range("A" + (iFila + 8).ToString(), "D" + iFila.ToString());
                        aRange.Merge();
                        estableceEstiloDatos(aRange, true, -1, false, true, false);

                        aRange.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlLineStyleNone;
                        GraficaValoracion(ws, "A" + iFila, "D" + iFila, "Histórico de Valoración MIR", arrAnio, arrValTot);


                        //Grafica Diseño
                        aRange = ws.get_Range("E" + (iFila + 8).ToString(), "H" + iFila.ToString());
                        aRange.Merge();
                        estableceEstiloDatos(aRange, true, -1, false, true, false);

                        aRange.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlLineStyleNone;
                        GraficaValoracion(ws, "E" + iFila, "H" + iFila, "Histórico de Valoración de Diseño", arrAnio, arrValDis);


                        //Grafica Indicadores
                        aRange = ws.get_Range("I" + (iFila + 8).ToString(), "L" + iFila.ToString());
                        aRange.Merge();
                        estableceEstiloDatos(aRange, true, -1, false, true, false);

                        aRange.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlLineStyleNone;
                        GraficaValoracion(ws, "I" + iFila, "J" + iFila, "Histórico de Valoracion Indicadores", arrAnio, arrValInd);

                        //Iconos valoraciones
                        agregarIconosValoraciones(ws);


                        ((Worksheet)wb.Sheets[1]).Delete();

                        ((Worksheet)wb.Sheets[1]).Activate();

                        String filename = Server.MapPath("~/Descargas") + "\\" + nombreTemporalExcel + ".xlsx";

                        wb.SaveAs(filename);
                        comun.terminarProcesoExcel(xlApp, wb);

                        //, programa.CICLO, programa.RAMO_DEP, programa.MODALIDAD, programa.CLAVE
                        //, short ciclo, int ramo, string modalidad, int clave
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
                        log.LogMessageToFile("Parametros: " + "Matriz=" + sParametroMatriz + " Ciclo=" + sParametroCiclo + " Ramo=" + sParametroRamo + " Indicador=" + sParametroIndicador);
                        log.LogMessageToFile(error.Message);
                        log.LogMessageToFile(error.StackTrace);

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
        /// <param name="Programa">Datos a escribir en el excel</param>
        public void agregarInfoGeneralProgram(Worksheet ws, SIMEPS.Modelo.Programa programa)
        {
            //Encabezado
            iFila++;

            ws.Cells[iFila, "A"] = "Reporte de Monitoreo";
            Range aRange = ws.get_Range("A" + iFila.ToString(), "L" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange, "#FFFFFF", "#1E4C84", 15, false, true, true);

            iFila++;
            iFila++;

            ws.Cells[iFila, "A"] = "INFORMACIÓN GENERAL DEL PROGRAMA";
            aRange = ws.get_Range("A" + iFila.ToString(), "L" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange, "#25AD4F", "#ffffff", 12, true, true, true);

            //Datos generales
            iFila++;
            ws.Cells[iFila, "A"] = "Nombre del programa: " + programa.PP + " " + programa.NOMBRE;
            aRange = ws.get_Range("A" + iFila.ToString(), "E" + (iFila + 1).ToString());
            aRange.Characters[1, 25].Font.Bold = true;
            aRange.Merge();
            estableceEstiloDatos(aRange, false, -1, false, true, false);

            //Aprobacion de indicadores
            ws.Cells[iFila, "F"] = "Aprobación de Indicadores";
            aRange = ws.get_Range("F" + iFila.ToString(), "L" + iFila.ToString());
            estableceEstiloTitulo(aRange, "#1B4367", "#ffffff", 11, false, true, true);
            estableceEstiloDatos(aRange, true, -1, false, true, false);

            iFila++;
            ws.Cells[iFila, "F"] = "Ciclo: " + programa.CICLO;
            aRange = ws.get_Range("F" + iFila.ToString(), "G" + iFila.ToString());
            aRange.Characters[1, 6].Font.Bold = true;
            aRange.Merge();
            estableceEstiloDatos(aRange, true, -1, false, true, false);

            ws.Cells[iFila, "H"] = "Estatus del programa:";
            aRange = ws.get_Range("H" + iFila.ToString(), "I" + iFila.ToString());
            aRange.Merge();
            estableceEstiloDatos(aRange, true, -1, false, true, true);
            aRange.RowHeight = 20;

            switch (programa.DESC_APROBACION_DICTAMEN)
            {

                case "Aprobación directa":

                    aRange = (Microsoft.Office.Interop.Excel.Range)ws.get_Range("J6", "J6");
                    aRange.Select();
                    ws.Shapes.AddPicture(Server.MapPath("~/img") + "\\aprobacion.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, float.Parse(aRange.Left.ToString()) + 2, float.Parse(aRange.Top.ToString()) + 2, 15, 15);

                    break;

                case "Aprobación condicionada":

                    aRange = (Microsoft.Office.Interop.Excel.Range)ws.get_Range("J6", "J6");
                    aRange.Select();
                    ws.Shapes.AddPicture(Server.MapPath("~/img") + "\\condicionada.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, float.Parse(aRange.Left.ToString()) + 2, float.Parse(aRange.Top.ToString()) + 2, 15, 15);
                    break;

                case "Aún no cuenta con criterios mínimos":
                    aRange = (Microsoft.Office.Interop.Excel.Range)ws.get_Range("J6", "J6");
                    aRange.Select();
                    ws.Shapes.AddPicture(Server.MapPath("~/img") + "\\nocuenta.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, float.Parse(aRange.Left.ToString()) + 2, float.Parse(aRange.Top.ToString()) + 2, 15, 15);
                    aRange.RowHeight = 27;
                    break;
            }

            ws.Cells[iFila, "J"] = programa.DESC_APROBACION_DICTAMEN;
            aRange = ws.get_Range("J" + iFila.ToString(), "L" + iFila.ToString());
            aRange.Merge();
            estableceEstiloDatos(aRange, true, -1, false, true, true);
            aRange.InsertIndent(3);

            iFila++;
            ws.Cells[iFila, "A"] = "Dependencia: " + programa.RAMO_DEP + "-" + programa.DEPENDENCIA;
            aRange = ws.get_Range("A" + iFila.ToString(), "E" + (iFila + 1).ToString());
            aRange.Characters[1, 12].Font.Bold = true;
            aRange.Merge();
            estableceEstiloDatos(aRange, false, -1, false, true, false);

            ws.Cells[iFila, "F"] = "Presupuesto del Programa";
            aRange = ws.get_Range("F" + iFila.ToString(), "L" + iFila.ToString());
            estableceEstiloTitulo(aRange, "#1B4367", "#ffffff", 11, false, true, true);

            iFila++;
            iFila++;
            ws.Cells[iFila, "A"] = "Unidad Responsable: " + programa.DESC_UNIDAD;
            aRange = ws.get_Range("A" + iFila.ToString(), "E" + (iFila + 1).ToString());
            aRange.Characters[1, 19].Font.Bold = true;
            aRange.Merge();
            estableceEstiloDatos(aRange, false, -1, false, true, false);

            iFila++; iFila++;
            ws.Cells[iFila, "A"] = "Meta Nacional: " + programa.DESC_META;
            aRange = ws.get_Range("A" + iFila.ToString(), "E" + (iFila + 1).ToString());
            aRange.Characters[1, 14].Font.Bold = true;
            aRange.Merge();
            estableceEstiloDatos(aRange, false, -1, false, true, false);
            aRange.RowHeight = 20;

            iFila++; iFila++;
            ws.Cells[iFila, "A"] = "Programa Sectorial: " + programa.DESC_PROGRAMA_SEC_INST;
            aRange = ws.get_Range("A" + iFila.ToString(), "E" + (iFila + 1).ToString());
            aRange.Characters[1, 19].Font.Bold = true;
            aRange.Merge();
            estableceEstiloDatos(aRange, false, -1, false, true, false);

            iFila++; iFila++;
            ws.Cells[iFila, "A"] = "Objetivo Estratégico de la Dependencia: " + programa.OBJ_EST_DEP_ENT;
            aRange = ws.get_Range("A" + iFila.ToString(), "E" + (iFila + 1).ToString());
            aRange.Characters[1, 39].Font.Bold = true;
            aRange.Merge();
            estableceEstiloDatos(aRange, false, -1, false, true, false);
            aRange.RowHeight = 40;
            iFila++;

            aRange = ws.get_Range("A" + 5, "E" + 16);
            aRange.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlDot;

        }

        /// <summary>
        /// Agrega los datos de indicadores de resultados al excel
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        /// <param name="indicador">Datos de los indicadores de resultados</param>
        public void agregarIndicadoresResul(Worksheet ws, SIMEPS.Modelo.Programa programa, Indicador indicador, object[] MetaAlcanzada, object[] MetaPlaneada, object[] Ciclo, int RangoImg)
        {

            //Propositos
            iFila++;
            ws.Cells[iFila, "A"] = "Objetivo del Programa(Propósito)";
            Range aRange = ws.get_Range("A" + iFila.ToString(), "E" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange, "#B4C6E7", "#000000", 11, false, true, true);
            estableceEstiloDatos(aRange, true, -1, false, true, false);

            //******************GRAFICA*********************************************************************
            string Letra = "F";
            string filaG = Letra + Convert.ToString(filaGNum);

            double left = (ws.get_Range("F1").Left + 3);
            double top = (ws.get_Range(filaG).Top + 1);
            int colorS2 = System.Drawing.Color.FromArgb(248, 211, 88).ToArgb();
            int colorS1 = System.Drawing.Color.FromArgb(88, 211, 248).ToArgb();
            comun.graficaLineal(ws, MetaAlcanzada, MetaPlaneada, Ciclo, "Histórico (Grafica Líneal)", left, top, 414, 90, "Meta alcanzada", "Meta planeada", "Año", colorS1, colorS2, 11, 12, 1, XlLegendPosition.xlLegendPositionRight, false, null, true, true, 3, 2, XlMarkerStyle.xlMarkerStyleAutomatic, false);
            filaGNum = filaGNum + 6;
            //**********************************************************************************************
            iFila++;
            ws.Cells[iFila, "A"] = "Propósito: " + indicador.DESC_NIVEL;
            aRange = ws.get_Range("A" + iFila.ToString(), "E" + iFila.ToString());
            aRange.Characters[1, 10].Font.Bold = true;
            aRange.Merge();
            estableceEstiloDatos(aRange, true, -1, false, true, false);
            estableceEstiloTitulo(aRange, "#E8F2FE", "#000000", 11, false, false, false);
            aRange.RowHeight = 54;


            iFila++;
            ws.Cells[iFila, "A"] = "Nombre del Indicador";
            aRange = ws.get_Range("A" + iFila.ToString(), "E" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange, "#D9D9D9", "#000000", 11, false, true, true);
            estableceEstiloDatos(aRange, true, -1, false, true, false);

            iFila++;
            ws.Cells[iFila, "A"] = "P1. " + indicador.NOMBRE_IND;
            aRange = ws.get_Range("A" + iFila.ToString(), "E" + iFila.ToString());
            aRange.Characters[1, 3].Font.Bold = true;
            aRange.Merge();
            estableceEstiloDatos(aRange, true, -1, false, true, false);
            estableceEstiloTitulo(aRange, "#E8F2FE", "#000000", 11, false, false, false);
            aRange.RowHeight = 40;

            iFila++;
            ws.Cells[iFila, "A"] = "Año";
            aRange = ws.get_Range("A" + iFila.ToString(), "A" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange, "#D9D9D9", "#000000", 11, false, true, true);
            estableceEstiloDatos(aRange, true, -1, false, true, false);
            ws.Cells[iFila, "B"] = "Línea Base";
            aRange = ws.get_Range("B" + iFila.ToString(), "B" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange, "#D9D9D9", "#000000", 11, false, true, true);
            estableceEstiloDatos(aRange, true, -1, false, true, false);
            ws.Cells[iFila, "C"] = "Frecuencia de Medición";
            aRange = ws.get_Range("C" + iFila.ToString(), "D" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange, "#D9D9D9", "#000000", 11, false, true, true);
            estableceEstiloDatos(aRange, true, -1, false, true, false);
            ws.Cells[iFila, "E"] = "Sentido del Indicador";
            aRange = ws.get_Range("E" + iFila.ToString(), "E" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange, "#D9D9D9", "#000000", 11, false, true, true);
            estableceEstiloDatos(aRange, true, -1, false, true, false);

            iFila++;
            ws.Cells[iFila, "C"] = indicador.FRECUENCIA_MEDICION;
            aRange = ws.get_Range("C" + iFila.ToString(), "D" + iFila.ToString());
            aRange.Merge();
            estableceEstiloDatos(aRange, true, -1, false, true, false);
            ws.Cells[iFila, "B"] = indicador.LINEA_BASE;
            aRange = ws.get_Range("B" + iFila.ToString(), "B" + iFila.ToString());
            aRange.Merge();
            estableceEstiloDatos(aRange, true, -1, false, true, false);
            ws.Cells[iFila, "A"] = indicador.CICLO_LINEA_BASE;
            aRange = ws.get_Range("A" + iFila.ToString(), "A" + iFila.ToString());
            aRange.Merge();
            estableceEstiloDatos(aRange, true, -1, false, true, false);

            if (indicador.SENTIDO_INDICADOR.Equals("Ascendente"))
            {
                aRange = (Microsoft.Office.Interop.Excel.Range)ws.get_Range("E" + RangoImg, "E" + RangoImg);
                aRange.Select();
                ws.Shapes.AddPicture(Server.MapPath("~/img") + "\\Ascendente.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, float.Parse(aRange.Left.ToString()) + 20, float.Parse(aRange.Top.ToString()) + 1, 15, 15);
            }
            if (indicador.SENTIDO_INDICADOR.Equals("Descendente"))
            {
                aRange = (Microsoft.Office.Interop.Excel.Range)ws.get_Range("E" + RangoImg, "E" + RangoImg);
                aRange.Select();
                ws.Shapes.AddPicture(Server.MapPath("~/img") + "\\Descendente.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, float.Parse(aRange.Left.ToString()) + 20, float.Parse(aRange.Top.ToString()) + 1, 15, 15);
            }
            aRange = ws.get_Range("E" + iFila.ToString(), "E" + iFila.ToString());
            aRange.Merge();
            estableceEstiloDatos(aRange, true, -1, false, true, false);
            aRange.RowHeight = 17;
        }

        /// <summary>
        /// Agrega los datos de indicadores de componentes al excel
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        /// <param name="indicador">Datos de los indicadores de componentes</param>
        public void agregarIndicadoresCompon(Worksheet ws, SIMEPS.Modelo.Programa programa, Indicador indicador, object[] MetaAlcanzada, object[] MetaPlaneada, object[] Ciclo, int Numero)
        {
            string[] arr = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };

            if (RangoFinal == 11)
            {

                if (FilaP.Equals(0))
                {
                    FilaP = iFila;
                }
                else
                {
                    iFila = FilaP;
                }

                if (desc_nivel == indicador.DESC_NIVEL) { Consecutivo = num; }

                ws.Cells[iFila, ultimaColumna] = texto + Consecutivo;
                Range aRange = ws.get_Range(arr[RangoInicial] + iFila.ToString(), arr[RangoFinal] + iFila.ToString());
                aRange.Merge();
                estableceEstiloTitulo(aRange, "#B4C6E7", "#000000", 11, false, true, true);
                estableceEstiloDatos(aRange, true, -1, false, true, false);

                iFila++;
                ws.Cells[iFila, ultimaColumna] = indicador.DESC_NIVEL;
                aRange = ws.get_Range(arr[RangoInicial] + iFila.ToString(), arr[RangoFinal] + iFila.ToString());
                aRange.Merge();
                estableceEstiloDatos(aRange, true, -1, false, true, false);
                aRange.RowHeight = 49;

                iFila++;
                ws.Cells[iFila, ultimaColumna] = "Nombre del Indicador";
                aRange = ws.get_Range(arr[RangoInicial] + iFila.ToString(), arr[RangoFinal] + iFila.ToString());
                aRange.Merge();
                estableceEstiloTitulo(aRange, "#D9D9D9", "#000000", 11, false, true, true);
                estableceEstiloDatos(aRange, true, -1, false, true, false);

                iFila++;
                ws.Cells[iFila, ultimaColumna] = "C" + ConseComponente + "." + indicador.NOMBRE_IND;
                aRange = ws.get_Range(arr[RangoInicial] + iFila.ToString(), arr[RangoFinal] + iFila.ToString());
                aRange.Characters[1, 3].Font.Bold = true;
                aRange.Merge();
                estableceEstiloDatos(aRange, true, -1, false, true, false);
                aRange.RowHeight = 55;

                desc_nivel = indicador.DESC_NIVEL;
                num = Consecutivo;
                iFila++;

                //************************************************
                //Grafica indicadores de componentes
                aRange = ws.get_Range(arr[RangoInicial] + (iFila + 7).ToString(), arr[RangoFinal] + iFila.ToString());
                aRange.Merge();
                estableceEstiloDatos(aRange, true, -1, false, true, false);

                double left = ws.get_Range(arr[RangoInicial] + iFila).Left + 3;
                double top = ws.get_Range(arr[RangoFinal] + iFila).Top + 1;
                int colorS2 = System.Drawing.Color.FromArgb(248, 211, 88).ToArgb();
                int colorS1 = System.Drawing.Color.FromArgb(88, 211, 248).ToArgb();
                comun.graficaLineal(ws, MetaAlcanzada, MetaPlaneada, Ciclo, "Histórico (Grafica Líneal)", left, top, 236, 118, "Meta alcanzada", "Meta planeada", "Año", colorS1, colorS2, 11, 12, 1, XlLegendPosition.xlLegendPositionRight, false, null, true, true, 3, 2, XlMarkerStyle.xlMarkerStyleAutomatic, false);

                RangoInicial = 0;
                RangoFinal = 3;
                ultimaColumna = 1;
                Consecutivo++;
                ConseComponente++;
                FilaP = 0;
                iFila += 8;
            }
            else
            {
                if (FilaP.Equals(0))
                {
                    FilaP = iFila;
                }
                else
                {
                    iFila = FilaP;
                }

                if (desc_nivel == indicador.DESC_NIVEL) { Consecutivo = num; }

                ws.Cells[iFila, ultimaColumna] = texto + Consecutivo;
                Range aRange = ws.get_Range(arr[RangoInicial] + iFila.ToString(), arr[RangoFinal] + iFila.ToString());
                aRange.Merge();
                estableceEstiloTitulo(aRange, "#B4C6E7", "#000000", 11, false, true, true);
                estableceEstiloDatos(aRange, true, -1, false, true, false);


                iFila++;
                ws.Cells[iFila, ultimaColumna] = indicador.DESC_NIVEL;
                aRange = ws.get_Range(arr[RangoInicial] + iFila.ToString(), arr[RangoFinal] + iFila.ToString());
                aRange.Merge();
                estableceEstiloDatos(aRange, true, -1, false, true, false);
                aRange.RowHeight = 49;

                iFila++;
                ws.Cells[iFila, ultimaColumna] = "Nombre del Indicador";
                aRange = ws.get_Range(arr[RangoInicial] + iFila.ToString(), arr[RangoFinal] + iFila.ToString());
                aRange.Merge();
                estableceEstiloTitulo(aRange, "#D9D9D9", "#000000", 11, false, true, true);
                estableceEstiloDatos(aRange, true, -1, false, true, false);

                iFila++;
                ws.Cells[iFila, ultimaColumna] = "C" + ConseComponente + "." + indicador.NOMBRE_IND;
                aRange = ws.get_Range(arr[RangoInicial] + iFila.ToString(), arr[RangoFinal] + iFila.ToString());
                aRange.Characters[1, 3].Font.Bold = true;
                aRange.Merge();
                estableceEstiloDatos(aRange, true, -1, false, true, false);
                aRange.RowHeight = 55;

                desc_nivel = indicador.DESC_NIVEL;
                num = Consecutivo;
                iFila++;

                //***************************************************
                //Grafica de indicadores de componentes
                aRange = ws.get_Range(arr[RangoInicial] + (iFila + 7).ToString(), arr[RangoFinal] + iFila.ToString());
                aRange.Merge();
                estableceEstiloDatos(aRange, true, -1, false, true, false);

                double left = ws.get_Range(arr[RangoInicial] + iFila).Left + 3;
                double top = ws.get_Range(arr[RangoFinal] + iFila).Top + 1;
                int colorS2 = System.Drawing.Color.FromArgb(248, 211, 88).ToArgb();
                int colorS1 = System.Drawing.Color.FromArgb(88, 211, 248).ToArgb();
                comun.graficaLineal(ws, MetaAlcanzada, MetaPlaneada, Ciclo, "Histórico (Grafica Líneal)", left, top, 236, 118, "Meta alcanzada", "Meta planeada", "Año", colorS1, colorS2, 11, 12, 1, XlLegendPosition.xlLegendPositionRight, false, null, true, true, 3, 2, XlMarkerStyle.xlMarkerStyleAutomatic, false);

                //Agrega 8 filas en caso de que el numero de componentes no sea divisible en 3 debido a que se manejan 3 columnas solamente
                if (Consecutivo == Numero)
                {
                    iFila += 8;
                }

                Consecutivo++;
                ConseComponente++;
                ultimaColumna += 4;
                RangoInicial += 4;
                RangoFinal += 4;
            }
        }

        /// <summary>
        /// Agrega los datos de valoraciones al excel
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        public void agregarValoracionesCone(Worksheet ws)
        {

            ws.Cells[iFila, "A"] = "Valoraciones CONEVAL";
            Range aRange = ws.get_Range("A" + iFila.ToString(), "L" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange, "#25AD4F", "#ffffff", 12, true, true, true);

            iFila++;

            ws.Cells[iFila, "A"] = "Valoración MIR";
            aRange = ws.get_Range("A" + iFila.ToString(), "D" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange, "#E2EFDA", "#000000", 11, false, true, true);
            estableceEstiloDatos(aRange, true, -1, false, true, false);

            ws.Cells[iFila, "E"] = "Valoración de Diseño";
            aRange = ws.get_Range("E" + iFila.ToString(), "H" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange, "#E2EFDA", "#000000", 11, false, true, true);
            estableceEstiloDatos(aRange, true, -1, false, true, false);

            ws.Cells[iFila, "I"] = "Valoración de Indicadores";
            aRange = ws.get_Range("I" + iFila.ToString(), "L" + iFila.ToString());
            aRange.Merge();
            estableceEstiloTitulo(aRange, "#E2EFDA", "#000000", 11, false, true, true);
            estableceEstiloDatos(aRange, true, -1, false, true, false);

            iFila++;

        }

        /// <summary>
        /// Agrega iconos para los diferentes estatus de las graficas de valoraciones al excel
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        public void agregarIconosValoraciones(Worksheet ws)
        {

            //Ultima fila
            iFila = iFila + 9;
            Range aRange = ws.get_Range("A" + iFila.ToString(), "L" + iFila.ToString());


            //ICONOS
            aRange.RowHeight = 23;
            ws.Shapes.AddPicture(Server.MapPath("~/img") + "\\destacado.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, float.Parse(aRange.Left.ToString()) + 130, float.Parse(aRange.Top.ToString()) + 4, 12, 12);
            ws.Cells[iFila, "C"] = "Destacado";
            aRange = ws.get_Range("C" + iFila.ToString(), "D" + iFila.ToString());
            aRange.Merge();
            estableceEstiloDatos(aRange, false, 23, false, true, true);
            aRange.HorizontalAlignment = XlVAlign.xlVAlignCenter;

            ws.Shapes.AddPicture(Server.MapPath("~/img") + "\\adecuado.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, float.Parse(aRange.Left.ToString()) + 130, float.Parse(aRange.Top.ToString()) + 4, 12, 12);
            ws.Cells[iFila, "E"] = "Adecuado";
            aRange = ws.get_Range("E" + iFila.ToString(), "F" + iFila.ToString());
            aRange.Merge();
            estableceEstiloDatos(aRange, false, 23, false, true, true);
            aRange.HorizontalAlignment = XlVAlign.xlVAlignCenter;

            ws.Shapes.AddPicture(Server.MapPath("~/img") + "\\moderado.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, float.Parse(aRange.Left.ToString()) + 130, float.Parse(aRange.Top.ToString()) + 4, 12, 12);
            ws.Cells[iFila, "G"] = "Moderado";
            aRange = ws.get_Range("G" + iFila.ToString(), "H" + iFila.ToString());
            aRange.Merge();
            estableceEstiloDatos(aRange, false, 23, false, true, true);
            aRange.HorizontalAlignment = XlVAlign.xlVAlignCenter;

            ws.Shapes.AddPicture(Server.MapPath("~/img") + "\\mejora.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, float.Parse(aRange.Left.ToString()) + 130, float.Parse(aRange.Top.ToString()) + 4, 12, 12);
            ws.Cells[iFila, "I"] = "Oportunidad de mejora";
            aRange = ws.get_Range("I" + iFila.ToString(), "K" + iFila.ToString());
            aRange.Merge();
            estableceEstiloDatos(aRange, false, 23, false, true, true);
            aRange.HorizontalAlignment = XlVAlign.xlVAlignCenter;

            aRange = ws.get_Range("A" + 4, "L" + iFila);
            aRange.BorderAround2(XlLineStyle.xlContinuous, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, Type.Missing, Type.Missing);
        }

        /// <summary>
        /// Establece los estilos de tipo titulo al rango de celdas proporcionado
        /// </summary>
        /// <param name="aRange">Rango de celdas a establecer estilo</param>
        /// <param name="colorInterior">Color interior de la celda</param>
        /// <param name="colorFont">Color de la fuente</param>
        /// <param name="FontSize">Tamaño de la fuente</param>
        /// <param name="border">Borde de la celda</param>
        public void estableceEstiloTitulo(Range aRange, string colorInterior, string colorFont, int FontSize, bool border, bool bold, bool Center)
        {
            aRange.Merge();
            if (Center) aRange.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            aRange.Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorInterior));
            aRange.Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml(colorFont));
            if (bold) aRange.Cells.Font.Bold = true;
            aRange.Cells.Font.Size = FontSize;
            aRange.VerticalAlignment = XlVAlign.xlVAlignTop;
            if (border) aRange.BorderAround2(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, Type.Missing, Type.Missing);
        }

        /// <summary>
        /// Establece los estilos de tipo titulo al rango de celdas proporcionado
        /// </summary>
        /// <param name="aRange">Rango de celdas a establecer estilo</param>
        public void estableceEstiloDatos(Range aRange, bool conBorde, int rowHeigh, bool combinar, bool wraptext, bool bold)
        {
            if (combinar) aRange.Merge();
            if (wraptext) aRange.Cells.WrapText = true;
            aRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
            if (rowHeigh != -1) aRange.Rows.RowHeight = rowHeigh;
            if (conBorde) aRange.BorderAround2(XlLineStyle.xlDot, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, Type.Missing, Type.Missing);
            if (bold) aRange.Cells.Font.Bold = true;
        }

        /// <summary>
        /// Agrega grafica en forma de Burbuja
        /// </summary>
        /// <param name="ws">WorkSheet</param>
        /// <param name="CellLeft">Posicion Left de la grafica</param>
        /// <param name="CellTop">Posicion Top de la grafica</param>
        /// <param name="titulo">Titulo de la grafica</param>
        /// <param name="arrAnio">Categoria de la grafica</param>
        /// <param name="arrValor">Valores de la grafica</param>
        private void GraficaValoracion(Worksheet ws, string CellLeft, string CellTop, string titulo, int[] arrAnio, int[] arrValor)
        {
            int i = 0;
            int AñoInicial = 0;
            int añoFinal = 0;

            if (arrAnio.LongLength > 0)
            {
                AñoInicial = arrAnio[0];
                añoFinal = arrAnio[arrAnio.LongLength - 1];
            }


            var charts = ws.ChartObjects() as Microsoft.Office.Interop.Excel.ChartObjects;
            var chartObject = charts.Add((float)ws.get_Range(CellLeft).Left + 3, (float)ws.get_Range(CellTop).Top + 1, 236, 130) as Microsoft.Office.Interop.Excel.ChartObject;
            var chart = chartObject.Chart;

            //Titulo
            chart.HasTitle = true;
            String sTitulo = titulo + " " + AñoInicial + "-" + añoFinal;


            chart.ChartTitle.Text = sTitulo;
            chart.ApplyLayout(9, chart.ChartType);
            chart.ChartStyle = 2;

            chart.ChartTitle.Font.Size = 12;
            chart.ChartTitle.Font.Italic = true;

            //Tipo de grafica
            chart.ChartType = XlChartType.xlXYScatter;

            //Series
            SeriesCollection seriesCollection = (SeriesCollection)chart.SeriesCollection(Missing.Value);

            Series series1 = seriesCollection.NewSeries();
            if (arrAnio.LongLength > 0)
            {
                series1.XValues = arrAnio;
            }
            if (arrValor.LongLength > 0)
            {
                series1.Values = arrValor;
            }

            //Colores
            Points pts = (Microsoft.Office.Interop.Excel.Points)series1.Points(Type.Missing);
            foreach (Point pt in pts)
            {
                //VERDE
                //ADECUADO
                if (arrValor[i] == 3)
                {
                    pt.Format.Fill.ForeColor.RGB = System.Drawing.Color.FromArgb(0, 176, 80).ToArgb();
                    pt.Border.Color = System.Drawing.Color.FromArgb(0, 176, 80).ToArgb();

                }

                //VERDE FUERTE
                //DESTACADO
                if (arrValor[i] == 4)
                {
                    pt.Format.Fill.ForeColor.RGB = System.Drawing.Color.FromArgb(39, 118, 0).ToArgb();
                    pt.Border.Color = System.Drawing.Color.FromArgb(39, 118, 0).ToArgb();
                }

                //AMARILLO
                //MODERADO
                if (arrValor[i] == 2)
                {
                    pt.Format.Fill.ForeColor.RGB = System.Drawing.Color.FromArgb(1, 226, 237).ToArgb();
                    pt.Border.Color = System.Drawing.Color.FromArgb(1, 226, 237).ToArgb();
                }

                //ROJO
                //OPORTUNIDAD DE MEJORA
                if (arrValor[i] == 1)
                {
                    pt.Format.Fill.ForeColor.RGB = System.Drawing.Color.FromArgb(0, 0, 206).ToArgb();
                    pt.Border.Color = System.Drawing.Color.FromArgb(0, 0, 206).ToArgb();
                }

                //NULL
                if (arrValor[i] == -1)
                {
                    pt.Format.Fill.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                    pt.Format.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                }
                i++;
            }

            //Categoria X
            Axis xAxis = (Axis)chart.Axes(XlAxisType.xlValue);
            xAxis.TickLabels.Delete();
            xAxis.HasTitle = false;

            Axis xAxisCategory = (Axis)chart.Axes(XlAxisType.xlCategory, XlAxisGroup.xlPrimary);
            xAxisCategory.HasTitle = false;

            //Maximos y minimos valores de categoria
            xAxisCategory.MaximumScale = añoFinal;
            xAxisCategory.MinimumScale = 2008;

            chartObject.Border.LineStyle = null;

            series1.Format.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
            series1.Format.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
            series1.MarkerStyle = XlMarkerStyle.xlMarkerStyleCircle;
            series1.MarkerSize = 19;


            //Eliminar leyenda de serie
            chart.Legend.Delete();
        }
    }
}