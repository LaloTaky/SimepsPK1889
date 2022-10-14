﻿using Newtonsoft.Json;
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
    public partial class IndicadorR33 : System.Web.UI.Page
    {
        public int iCiclo = DateTime.Now.Year;
        public string iMatriz = string.Empty;
        public string iIndicador = string.Empty;
        public string sComponente = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            IndicadoresDal simeps = new IndicadoresDal();

            if (Request.Params["iCiclo"] != null)
            {
                iCiclo = Convert.ToInt16(Request.Params["iCiclo"].ToString());

                if (Request.Params["iMatriz"] != null)
                {
                    iMatriz = Request.Params["iMatriz"].ToString();

                    if (Request.Params["idIndicador"] != null)
                        iIndicador = Request.Params["idIndicador"].ToString();

                    if (Request.Params["sComponente"] != null)
                        sComponente = Request.Params["sComponente"].ToString();

                    var indicador = simeps.ConsultarIndicadorRamo33(Int32.Parse(iMatriz), iCiclo, Int32.Parse(iIndicador)); //Actualizar falta matriz

                    if (indicador != null)
                    {
                        Session["oIndicador"] = indicador;
                        List<HistoricoIndicador> VALORES_GRAFICA = new List<HistoricoIndicador>();
                        VALORES_GRAFICA = simeps.ConsultarHistorico(indicador.IdIndicador.Value);
                        Session["oHistorico"] = VALORES_GRAFICA;
                        foreach (var item in VALORES_GRAFICA)
                        {
                            ddlGraficaProp.Items.Add(new ListItem()
                            {
                                Value = item.NO.ToString(),
                                Text = item.GRAFICA
                            }
                        );
                        }
                        
                        LblFondoSiglas.Text = sComponente;

                        lblNombreIndicador.Text = indicador.NombreIndicador;
                        lblDescripcion.Text = indicador.DEFINICION_IND;
                        lblUnidadMedida.Text = indicador.UNIDAD_MEDIDA;
                        lblMetodoCalculo.Text = indicador.METODO_CALCULO_IND;
                        lblFrecuenciaMedicion.Text = indicador.FRECUENCIA_MEDICION;
                        lblMediosVerificacion.Text = indicador.DESC_MEDIOS_VERIFICACION;
                        DescripcionAmb.InnerHtml = indicador.DescCobertura;

                        if (!indicador.CALIF_CLARIDAD.HasValue && !indicador.CALIF_RELEVANCIA.HasValue && !indicador.CALIF_MONITOREABILIDAD.HasValue && !indicador.CALIF_ADECUACION.HasValue)
                        {
                            LeyendaAviso.Attributes.Add("style", "display:block");
                            divSemaforos.Attributes.Add("style", "display:none");
                        }
                        else
                        {
                            if (!indicador.CALIF_CLARIDAD.Value)
                                cajaClaridad.Attributes.Add("style", "background-color:red !important;width: 20px; height: 20px;");

                            if (!indicador.CALIF_RELEVANCIA.Value)
                                cajaRelevancia.Attributes.Add("style", "background-color:red !important;width: 20px; height: 20px;");

                            if (!indicador.CALIF_MONITOREABILIDAD.Value)
                                cajaMonito.Attributes.Add("style", "background-color:red !important;width: 20px; height: 20px;");

                            if (!indicador.CALIF_ADECUACION.Value)
                                cajaAdecuacion.Attributes.Add("style", "background-color:red !important;width: 20px; height: 20px;");
                        }
                    }

                    List<IndicadorMapaR33> PromediosEstatales = simeps.GetValoresPorEntidadFondoRamo33(Int32.Parse(iIndicador));
                    List<ValoDesepenoRamo33> DesempenoEstatales = simeps.GetValoresDesempenoPorEntidadFondoRamo33(Int32.Parse(iIndicador));
                    Session["oPromediosEstatales"] = PromediosEstatales;
                    Session["oDesempenoEstatales"] = DesempenoEstatales;
                    if (DesempenoEstatales.Count > 0)
                    {
                        if (indicador.Version.Equals(2))
                        {
                            string jsonMapa = JsonConvert.SerializeObject(DesempenoEstatales);
                            arrayjsonDes.Value += jsonMapa + "|";
                        }
                    }

                    if (PromediosEstatales.Count > 0)
                    {
                        string jsonMapa2 = JsonConvert.SerializeObject(PromediosEstatales);
                        arrayjsonEnt.Value += jsonMapa2 + "|";
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
        protected void DescargaFichero_Click(object sender, EventArgs e)
        {
            Response.Redirect("DescargaFichaIndicador.aspx/?idIndicador=" + iIndicador);
        }
    }
}