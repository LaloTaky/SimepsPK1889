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
    public partial class MosaicoRamo33 : System.Web.UI.Page
    {
        public int iCiclo = DateTime.Now.Year;

        public string sFondo = string.Empty;

        public string sRamo = string.Empty;

        public string sModalidad = string.Empty;

        public string sClave = string.Empty;

        public FondoRamo33View matriz;

        public FondoRamo33View componente;

        protected void Page_Load(object sender, EventArgs e)
        {
            IndicadoresDal simeps = new IndicadoresDal();
            if (base.Request.Params["pCiclo"] != null)
            {
                iCiclo = Convert.ToInt16(base.Request.Params["pCiclo"].ToString());
                if (base.Request.Params["sFondo"] != null)
                {
                    sFondo = base.Request.Params["sFondo"].ToString();
                    if (base.Request.Params["sRamo"] != null)
                    {
                        sRamo = base.Request.Params["sRamo"].ToString();
                    }
                    if (base.Request.Params["sModalidad"] != null)
                    {
                        sModalidad = base.Request.Params["sModalidad"].ToString();
                    }
                    if (base.Request.Params["sClave"] != null)
                    {
                        sClave = base.Request.Params["sClave"].ToString();
                    }
                    FondoRamo33View[] fondos = simeps.ConsultaFondosRamo33PorCiclo(iCiclo);
                    if (fondos != null)
                    {
                        matriz = fondos
                            .FirstOrDefault((FondoRamo33View x) => x.SiglasFondo.Equals(sFondo, StringComparison.InvariantCultureIgnoreCase));

                        if (matriz.MatrizComponentes.Count > 1)
                        {
                            if (!string.IsNullOrEmpty(sRamo) && !string.IsNullOrEmpty(sModalidad) && !string.IsNullOrEmpty(sClave))
                            {
                                componente = matriz.MatrizComponentes
                                    .FirstOrDefault((FondoRamo33View x) => x.Ramo == int.Parse(sRamo) && x.Modalidad.Equals(sModalidad, StringComparison.InvariantCultureIgnoreCase) && x.Clave == int.Parse(sClave));
                            }
                            //LblFondoSiglas.Text = matriz.SiglasFondo;
                            LblFondoNombre.Text = $"{matriz.SiglasFondo} {matriz.NombreFondo}. {iCiclo}";
                            numComponentes.Value = matriz.MatrizComponentes.Count.ToString();
                            rprMosaico.DataSource = matriz.MatrizComponentes;
                            rprMosaico.DataBind();
                        }
                        else
                        {
                            componente = matriz.MatrizComponentes.FirstOrDefault();
                            base.Response.Redirect(matriz.Url);
                        }
                    }
                    else
                    {
                        base.Response.Redirect(string.Format("HomeRamo33.aspx?pCiclo={0}", iCiclo));
                    }
                }
                else
                {

                    base.Response.Redirect(string.Format("HomeRamo33.aspx?pCiclo={0}", iCiclo));
                }
            }
            else
            {
                base.Response.Redirect("HomeRamo33.aspx");
            }
        }
    }
}