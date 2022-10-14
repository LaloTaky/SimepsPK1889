using SIMEPS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SIMEPS.Comun;

namespace SIMEPS
{
    public partial class MIR : System.Web.UI.Page
    {
        public String divEDRclassName = "";
        public String divEDRstyle = "";
        public String divEDRinnerHTML = "";

        public String divTotalclassName = "";
        public String divTotalstyle = "";
        public String divTotalinnerHTML = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params.Get("pIdMatriz") == null)
                Response.Redirect("Mosaico.aspx");

            addHeader();
        }

        protected void gvIndicadores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string pCiclo = Request.QueryString["pCiclo"];
                string pRamo = Request.QueryString["pRamo"];
                string pMatriz = Request.QueryString["pIdMatriz"];
                string pSiglas = Request.QueryString["siglas"];

                if (!String.IsNullOrEmpty(pCiclo) && !String.IsNullOrEmpty(pRamo))
                {
                    HyperLink HyperLink = e.Row.FindControl("hyperlink") as HyperLink;
                    if (HyperLink != null)
                    {
                        string link = HyperLink.NavigateUrl;
                        string t = "";
                        if (Request.QueryString["t"] != null) t = Request.QueryString["t"];
                        HyperLink.NavigateUrl = link + "&pCiclo=" + pCiclo + "&pRamo=" + pRamo + "&pIdMatriz=" + pMatriz + "&siglas=" + pSiglas + "&t=" + t + "";
                    }
                }

            }
        }

        protected void gvObjetivos_DataBound(object sender, EventArgs e)
        {
            GridView gvObjetivos = (GridView)sender;
            int RowSpan = 2;

            for (int i = gvObjetivos.Rows.Count - 1; i >= 0; i--)
            {
                GridViewRow currRow = gvObjetivos.Rows[i];
                GridViewRow prevRow = gvObjetivos.Rows[i];

                if (((Label)currRow.Cells[0].Controls[1]).Text == ((Label)prevRow.Cells[0].Controls[1]).Text)
                {
                    currRow.CssClass = ((Label)prevRow.Cells[0].Controls[1]).Text.Replace('ó', 'o');
                }
            }

            for (int i = gvObjetivos.Rows.Count - 2; i >= 0; i--)
            {
                GridViewRow currRow = gvObjetivos.Rows[i];
                GridViewRow prevRow = gvObjetivos.Rows[i + 1];
                if (((Label)currRow.Cells[0].Controls[1]).Text == ((Label)prevRow.Cells[0].Controls[1]).Text)
                {
                    currRow.Cells[0].RowSpan = RowSpan;
                    prevRow.Cells[0].Visible = false;
                    RowSpan += 1;
                }
                else
                    RowSpan = 2;

                if (((Label)currRow.Cells[0].Controls[1]).Text == "Componente" & currRow.Cells[0].RowSpan == 0)
                {
                    currRow.CssClass = "Componente " + "division";
                }
            }



        }


        public void addHeader()
        {
            try
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
                string universoPaemir = "1";
                decimal idIndicadorSectorial = -1;
                List<Modelo.Programa> programas = data.ConsultarPrograma(anio, ramo, unidad, palabraClave, dMatriz, dIndicador, sNivel, sPantalla, universoPaemir, idIndicadorSectorial);

                foreach (Modelo.Programa progra in programas)
                {
                    LblCiclo.Text = "MIR " + progra.CICLO.ToString();
                    LblNombrePrograma.Text = (String.IsNullOrEmpty(progra.NOMBRE) ? "NA" : progra.NOMBRE);
                    LblDictamen.Text = (String.IsNullOrEmpty(progra.DESC_APROBACION_DICTAMEN) ? "NA" : progra.DESC_APROBACION_DICTAMEN);
                    LblTasaPermanencia.Text = progra.TASAPERMANENCIA != "N/A" ? Convert.ToDouble(progra.TASAPERMANENCIA).ToString("P1") : progra.TASAPERMANENCIA;
                    break;
                }
                int contHistorico = 0;
                int sumHistorico = 0;
                string caltotal = ""; //Número aleatorio que no se incluye en evaluación, para no alterar resultado
                string calEDR = ""; //Número aleatorio que no se incluye en evaluación, para no alterar resultado
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
                    foreach (Modelo.Valoracion val in progra.VALORACIONES.Where(v => v.CICLO == anio))
                    {
                        caltotal = val.CALIF_TOT.ToString();
                        calEDR = val.CALIF_EDR.ToString();
                    }
                }
                /*ClientScript.RegisterStartupScript(GetType(), "Grafica", "fjsMostrarValoraciones('"+caltotal+"','"+calEDR+"'); ", true);
                */


                switch (calEDR)
                {
                    case "0":
                        divEDRclassName = "cajaRojaMIR";
                        LblCalEDR.Text = "";
                        break;
                    case "0.5":
                        divEDRclassName = "cajaAmarillaMIR";
                        LblCalEDR.Text = "";
                        break;
                    case "1":
                        divEDRclassName = "cajaVerdeMIR";
                        LblCalEDR.Text = "";
                        break;
                    case "-1":
                        divEDRclassName = "";
                        divEDRstyle = "font-weight: bold;";
                        divEDRinnerHTML = "NA";
                        LblCalEDR.Text = "NA";
                        break;
                    default:
                        divEDRclassName = "";
                        divEDRstyle = "font-weight: bold;";
                        divEDRinnerHTML = "NA";
                        LblCalEDR.Text = "NA";
                        break;
                }
                switch (caltotal)
                {
                    case "1":
                        divTotalclassName = "cajaRojaMIR";
                        break;
                    case "2":
                        divTotalclassName = "cajaAmarillaMIR";
                        LblCalTot.Text = "";
                        break;
                    case "3":
                        divTotalclassName = "cajaVerdeClaroMIR";
                        LblCalTot.Text = "";
                        break;
                    case "4":
                        divTotalclassName = "cajaVerdeMIR";
                        LblCalTot.Text = "";
                        break;
                    default:
                        divTotalclassName = "";
                        divTotalstyle = "font-weight: bold;";
                        divTotalinnerHTML = "NA";
                        LblCalTot.Text = "NA";
                        break;
                }


                contHistorico = (contHistorico == 0 ? 1 : contHistorico);
                average = sumHistorico / contHistorico;
                LblObservaciones.Text = average.ToString("N1");
            }
            catch (Exception e)
            {
                Logger log = new Logger();
                log.LogMessageToFile(Server.GetLastError().Message);
            }
        }
    }
}