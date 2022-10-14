using Newtonsoft.Json;
using SIMEPS.Dal;
using SIMEPS.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIMEPS
{
    public partial class IndicadoresR33 : System.Web.UI.Page
    {
        public int iCiclo = DateTime.Now.Year;
        public string sIdMatriz = string.Empty;
        public string sFondo = string.Empty;
        public string sComponente = string.Empty;

        public FondoRamo33 matriz = null;
        public FondoRamo33 componente = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            IndicadoresDal simeps = new IndicadoresDal();

            if (Request.Params["pCiclo"] != null)
            {
                iCiclo = Convert.ToInt16(Request.Params["pCiclo"].ToString());

                sFondo = Request.Params["sFondo"];
                sComponente = Request.Params["sComponente"];
                sIdMatriz = Request.Params["iMatriz"];

                if (Request.Params["iMatriz"] != null)
                {
                    sIdMatriz = Request.Params["iMatriz"];

                    LblFondoNombre.Text = sComponente;
                    LblFondoNombreBD.Text = sComponente;

                    // obtiene el proposito del fondo
                    var matriz = simeps.ConsultaFondoMatrizComponente(int.Parse(sIdMatriz), sFondo, sComponente, iCiclo);

                    if (matriz != null)
                    {
                        if (simeps.ConsultarPrograma(iCiclo, "33", "0", "0", decimal.Parse(sIdMatriz), 0, "0", "b", "1", -1).Any())
                            lblVerMIR.Text = "<a href=\"MIR.aspx?pIdMatriz=" + sIdMatriz + "&pCiclo=" + iCiclo + "&pRamo=33&&t=b\" class=\"btn-mosaico btn-mosaico-general\" style=\"font-size: 12px;font-weight: normal;\">Ver MIR completa</a>";

                        var proposito = matriz
                            .Where(x => x.Proposito == "Propósito")
                            .Select(x => new IndicadorRamo33
                            {
                                Clave = x.Clave,
                                Ramo = x.Ramo,
                                Modalidad = x.Modalidad,
                                Version = x.Version,
                                Proposito = x.Proposito,
                                DescNivel = x.DescNivel,
                                Indicadores = x.Indicadores,
                                CantidadIndicadores = x.CantidadIndicadores,
                                Index = x.Index,
                            }).ToList();

                        spnTitProposito.InnerText = "Propósito (" + proposito.Count + ")";
                        rprObjetivosComp.DataSource = proposito;
                        rprObjetivosComp.DataBind();

                        spnTitComponente.InnerText = "Componente (" + matriz.Where(x => x.Proposito == "Componente").ToList().Count + ")";
                        rprComponentes.DataSource = matriz.Where(x => x.Proposito == "Componente").ToList();
                        rprComponentes.DataBind();
                    }
                    else
                    {
                        // que pasa si no hay info?
                    }
                }
                else
                {
                    Response.Redirect("HomeRamo33.aspx");
                }
            }
            else
                    {
                Response.Redirect("HomeRamo33.aspx");
            }
                    }

        protected void rprObjetivosComp_ItemDataBound(object sender, RepeaterItemEventArgs e)
                    {
            if (e.Item != null && e.Item.DataItem != null)
            {
                IndicadorRamo33 indi = (IndicadorRamo33)e.Item.DataItem;

                foreach (var i in indi.Indicadores)
                    {
                    if (i.DesempenoEstatales.Count > 0)
                    {
                        if (indi.Version.Equals(2))
                        {
                            string jsonMapa = JsonConvert.SerializeObject(i.DesempenoEstatales);
                            arrayjsonDes.Value += jsonMapa + "|";
                        }
                    }

                    if (i.PromediosEstatales.Count > 0)
                    {
                        string jsonMapa2 = JsonConvert.SerializeObject(i.PromediosEstatales);
                        arrayjsonEnt.Value += jsonMapa2 + "|";

                    }
                }
            }
                        }

        protected void rprComponentes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item != null && e.Item.DataItem != null)
            {
                MatrizRamo33 indi = (MatrizRamo33)e.Item.DataItem;

                foreach (var i in indi.Indicadores)
                        {
                    if (i.DesempenoEstatales.Count > 0)
                        {
                        if (indi.Version.Equals(2))
                    {
                            string jsonMapa = JsonConvert.SerializeObject(i.DesempenoEstatales);
                            arrayjsonDes.Value += jsonMapa + "|";
                    }
                }

                    if (i.PromediosEstatales.Count > 0)
                {
                        string jsonMapa2 = JsonConvert.SerializeObject(i.PromediosEstatales);
                        arrayjsonEnt.Value += jsonMapa2 + "|";
                }
            }
            }
        }
    }
}