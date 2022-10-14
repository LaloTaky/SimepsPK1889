using SIMEPS.Comun;
using SIMEPS.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace SIMEPS
{
    public partial class Programa : System.Web.UI.Page
    {

        ReportesDal nuevoReporte = new ReportesDal("TD_RESOURCE_STORE");
        ExcelUtilities comun = new ExcelUtilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["pIdMatriz"] == null) Response.Redirect("Mosaico.aspx");

            if (!IsPostBack)
            {
            }

        }

        //Método para escribir el encabezado de la dependencia y el ciclo
        protected void divHead_Load(object sender, EventArgs e)
        {
            addHeader();
        }

        /// <summary>
        /// Cuenta el total de objetivos de los propositos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeaterObj_LoadP(object sender, EventArgs e)
        {
            string pCount = rprObjetivosProp.Items != null ? rprObjetivosProp.Items.Count.ToString() : "0";
            lblPropositos.Text = "Propósito (" + pCount + ")";
        }

        /// <summary>
        /// Cuenta el total de objetivos de los componentes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeaterObj_Load(object sender, EventArgs e)
        {
            string sCount = rprObjetivosComp.Items != null ? rprObjetivosComp.Items.Count.ToString() : "0";
            lbCountObjetivos.Text = "Componente (" + sCount + ")";
        }


        public void addHeader()
        {
            IndicadoresDal data = new IndicadoresDal();
            decimal anio = (String.IsNullOrEmpty(Request.Params["pCiclo"]) ? 0 : Convert.ToDecimal(Request.Params["pCiclo"].ToString()));
            string ramo = (String.IsNullOrEmpty(Request.Params["pRamo"]) ? "0" : Request.Params["pRamo"].ToString());
            string unidad = (String.IsNullOrEmpty(Request.Params["pUnidad"]) ? "0" : Request.Params["pUnidad"].ToString());
            string palabraClave = "0";
            decimal dMatriz = (String.IsNullOrEmpty(Request.Params["pIdMatriz"]) ? -1 : Convert.ToDecimal(Request.Params["pIdMatriz"].ToString()));
            decimal dIndicador = (String.IsNullOrEmpty(Request.Params["pIdIndicador"]) ? 0 : Convert.ToDecimal(Request.Params["pIdIndicador"].ToString()));
            string sNivel = (String.IsNullOrEmpty(Request.Params["pNivel"]) ? "0" : Request.Params["pNivel"].ToString());
            string sPantalla = (String.IsNullOrEmpty(Request.Params["t"]) ? "0" : Request.Params["t"].ToString());
            string universoSimeps = "-1";
            decimal idIndicadorSectorial = -1;
            List<Modelo.Programa> programas = data.ConsultarPrograma(anio, ramo, unidad, palabraClave, dMatriz, dIndicador, sNivel, sPantalla, universoSimeps, idIndicadorSectorial);

            string coneval = "Consejo Nacional de Evaluación de la Política de Desarrollo Social";
            int ciclosCNVL = 2012;

            foreach (Modelo.Programa progra in programas)
            {
                if (progra.RAMO_DEP == 20 && progra.PP == "P003")
                    LblProgEspecial.InnerText = "true";
                else
                    LblProgEspecial.InnerText = "false";

                Modelo.Programa p1 = new Modelo.Programa();
                List<Modelo.Programa> lstProgramas = new List<Modelo.Programa>();
                lstProgramas = data.ConsultarHistoricoPrograma(dMatriz, sPantalla);

                LblNomPrograma.Text = progra.NOMBRE;
                if (progra.RAMO_DEP == 20 && progra.PP == "P003" && Convert.ToInt32(progra.CICLO) >= ciclosCNVL)
                {
                    LblDependencia.Text = coneval;
                    lblTituloHco.Text = "Ver indicadores";
                    string conguCinc = string.Empty;

                    if (progra.CICLO == 2017)
                    {
                        p1 = lstProgramas.Where(x => x.CICLO >= (short)2018 && x.CICLO <= (short)2026).OrderByDescending(y => y.CICLO).FirstOrDefault();
                        conguCinc = "2018 - 2026";
                    }
                    else if (progra.CICLO >= 2018 && progra.CICLO <= 2026)
                    {
                        p1 = lstProgramas.Where(x => x.CICLO == (short)2017).FirstOrDefault();
                        conguCinc = "2012 - 2017";
                    }
                    //switch (progra.CICLO)
                    //{
                    //    case 2026:
                    //    case 2025:
                    //    case 2024:
                    //    case 2023:
                    //    case 2022:
                    //    case 2021:
                    //    case 2020:
                    //    case 2019:
                    //    case 2018:
                    //        p1 = lstProgramas.Where(x => x.CICLO == (short)2017).FirstOrDefault();
                    //        conguCinc = "2012 - 2017";
                    //        break;
                    //    case 2017:
                    //        p1 = lstProgramas.Where(x => x.CICLO >= (short)2018 && x.CICLO <= (short)2026).OrderByDescending(y => y.CICLO).FirstOrDefault();
                    //        conguCinc = "2018 - 2026";
                    //        break;
                    //    default:
                    //        break;
                    //}

                    string hr = HttpUtility.UrlEncode(Request.Params["nombre"], System.Text.Encoding.UTF8);
                    hr = hr.Replace("+", "%20");
                    btnCicloProg.HRef = p1.LIGA + "&siglas=" + Request.Params["siglas"] + "&nombre=" + hr;
                    btnCicloProg.InnerHtml = "Planeación Institucional<br/>" + conguCinc;
                }
                else
                    LblDependencia.Text = progra.DEPENDENCIA;
                LblCiclo.Text = progra.CICLO.ToString();
                if (String.IsNullOrEmpty(progra.DESC_APROBACION_DICTAMEN))
                {
                    divDictamen.Style["Display"] = "none";
                }
                else
                {
                    LblDictamen.Text = progra.DESC_APROBACION_DICTAMEN;
                }
                LblTasaPermanencia.Text = progra.TASAPERMANENCIA != "N/A" ? Convert.ToDouble(progra.TASAPERMANENCIA).ToString("P1") : progra.TASAPERMANENCIA;
                break;
            }

            int contHistorico = 0;
            int sumHistorico = 0;
            decimal average = Decimal.Zero;
            foreach (Modelo.Programa progra in programas)
            {
                foreach (Modelo.Objetivo obj in progra.OBJETIVOS.Where(o => o.NIVEL == 2 || o.NIVEL == 3))
                {
                    foreach (Modelo.Indicador ind in obj.INDICADORES)
                    {
                        if (ind.HISTORICOS.Count > 0)
                        {
                            contHistorico = contHistorico + 1;
                            sumHistorico = sumHistorico + ind.HISTORICOS.Count;
                        }
                        break;
                    }
                }
            }
            contHistorico = (contHistorico == 0 ? 1 : contHistorico);
            average = sumHistorico / contHistorico;
            LblObservaciones.Text = average.ToString("N1");
        }
        public void descargarArchivo(HttpResponse response, String PathToExcelFile, string nombreArchivo)
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
                response.ContentType = "application/CSV charset=utf-8";
                response.AddHeader("content-disposition", "attachment; filename=\"" + fileName + "\"");
                response.AddHeader("Content-Length", file.Length.ToString());
                response.WriteFile(file.FullName);
                response.End();
            }
            else
            {
                response.Write("This file does not exist.");
            }
        }
    }
}