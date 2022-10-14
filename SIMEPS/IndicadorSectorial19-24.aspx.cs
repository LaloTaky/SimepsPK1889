using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SIMEPS.Modelo;
using System.Drawing;


namespace SIMEPS
{
    public partial class IndicadorSectorial19_24 : System.Web.UI.Page
    {
        //variables stylos lista objeteivos e indicadores
        public int NUMRepDe = 0;
        public int NUMRepFin = 0;
        public string valorDer = "";
        public string color = "";
        object[] arr = new object[5];
        public int j = 0;
        public int i = 0;
        public string notaIndicador = "";

        public bool bPrimerPrograma = true;
        public bool bPrimerObjetivo = true;
        public bool bPrimerIndicador = true;
        Dal.IndicadoresDal indicadorDAL = new Dal.IndicadoresDal();

        protected void Page_Load(object sender, EventArgs e)
        {

            //List<Modelo.IndicadorSectorial> Lista = new List<Modelo.IndicadorSectorial>();
            //if (Lista.Count == 0)
            //    accordion3.Visible = false;

            arr[0] = "#A6A6A6";
            arr[1] = "#A6A6A6";
            arr[2] = "#A6A6A6";
            arr[3] = "#A6A6A6";
            arr[4] = "#A6A6A6";
            notaIndicador = indicadorDAL.ConsultarParametro("NOTA_DETALLE_INDICADOR").First().VALOR.ToString();


        }

        //Obtiene el ODS de los objetivos para poder tener acceso y se llama cuando le dan clic al imgButton del programa
        //obteniendo el id del Programa sectorial dentro del evento CommandEventArgs
        protected void imbProgramaurl_Command(object sender, CommandEventArgs e)
        {
            
            setIdProgramaSec(Convert.ToInt32(e.CommandArgument), odsObjetivosSecL);
            lblNombrePrograma.Text = ((ImageButton)sender).Attributes["NOMBRE"].ToString();
            lblNombrePrograma.CssClass = "lblNombre-ProgramaSec " + "colorPSec" + ((ImageButton)sender).Attributes["IDPROGSEC"].ToString();
            divIndicadorPro.Style["Display"] = "block";
        }

        //Settea el valor del idProgramaSectorial que se obtiene del imgButton y lo utilizará como parametro 
        //para poder realizar la consulta de los datos necesarios. 
        protected void setIdProgramaSec(int idProgramaSec, ObjectDataSource ods)
        {
            ods.SelectParameters["idProgramaSectorial"].DefaultValue = "-1";
            ods.SelectParameters["idProgramaSectorial"].DefaultValue = idProgramaSec.ToString();
            inputIdProgSec.Value = idProgramaSec.ToString();
            ContadorIndObj(idProgramaSec);


        }

        //Obtiene el idProgramaSectorial del primer elemento de la lista del ODS de la lista de Programas Sectoriales
        protected void rpmProgramas_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (bPrimerPrograma && e.Item != null && e.Item.DataItem != null)
            {
                Modelo.ProgramaSectorial programaSec = (Modelo.ProgramaSectorial)e.Item.DataItem;
                divIndicadorPro.Style["Display"] = "none";
                
                bPrimerPrograma = false;
            }
        }

        //Obtiene el ODS de los Indicadores para poder tener acceso y se llama cuando le dan clic al LinkButton del programa
        //obteniendo el idIndicaor dentro del evento CommandEventArgs
        protected void lbtIndicadorSec_Command(object sender, CommandEventArgs e)
        {
            setIdIndicador(Convert.ToInt32(e.CommandArgument), odsDetalleIndicador);
            setIdIndicador(Convert.ToInt32(e.CommandArgument), odsProgramaSectorialVinculado);
            setIdIndicador(Convert.ToInt32(e.CommandArgument), odsDerechosInd);
            setIdIndicador(Convert.ToInt32(e.CommandArgument), odsGraficaHistoricoMetas);
            


        }

        //Settea el valor del idIndicador que se obtiene del linkButton y lo utilizará como parametro 
        //para poder realizar la consulta de los datos necesarios. 
        protected void setIdIndicador(int idIndicador, ObjectDataSource ods)
        {
            ods.SelectParameters["idIndicador"].DefaultValue = "-1";
            ods.SelectParameters["idIndicador"].DefaultValue = idIndicador.ToString();
            inputIdIndicadorSec.Value = idIndicador.ToString();
        }

        //Obtiene el idIndicador del primer elemento de la lista del ODS de la lista deIndicadores Sectoriales
        protected void rprIndicadoresSectoriales_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (bPrimerIndicador && e.Item != null && e.Item.DataItem != null)
            {
                Modelo.IndicadorSectorial indicadorSec = (Modelo.IndicadorSectorial)e.Item.DataItem;
                setIdIndicador(indicadorSec.ID_INDICADOR, odsDetalleIndicador);
                setIdIndicador(indicadorSec.ID_INDICADOR, odsProgramaSectorialVinculado);
                setIdIndicador(indicadorSec.ID_INDICADOR, odsDerechosInd);
                setIdIndicador(indicadorSec.ID_INDICADOR, odsGraficaHistoricoMetas);
                bPrimerIndicador = false;

            }

        }

        /// <summary>
        /// Contador de objetivos e indicadores
        /// </summary>
        /// <param name="idProgramaSectorial">idProgramaSectorial</param>
        protected void ContadorIndObj(int idProgramaSectorial)
        {
            Modelo.ContadorIndObj4T contador = new Modelo.ContadorIndObj4T();
            contador = indicadorDAL.contadorIndicadoresObjetivos4T(idProgramaSectorial);
            if (contador != null)
            {
                lbMetasbienestar.Text = contador.COUNT_META.ToString();
                lbParametros.Text = contador.COUNT_PARAM.ToString();
                lbObjetivosSecL.Text = contador.COUNT_OBJ.ToString();

            }
        }

        protected void rprObjetivosL_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            NUMRepDe = NUMRepDe + 1;
            valorDer = Convert.ToString(NUMRepDe);

            HtmlGenericControl IndPnd = e.Item.FindControl("IndPnd") as HtmlGenericControl;
            if (i > 4)
            {
                i = 0;
                IndPnd.Style.Add("background-color", arr[i].ToString());
                i = i + 1;
            }
            else
            {
                if (arr[i] != null)
                {
                    IndPnd.Style.Add("background-color", arr[i].ToString());
                    i = i + 1;
                }
            }
        }

        protected void DetalleIndicador_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var QsId = Convert.ToString(odsObjetivosSecL.SelectParameters["idProgramaSectorial"].DefaultValue);
                System.Web.UI.HtmlControls.HtmlContainerControl postLPU = (System.Web.UI.HtmlControls.HtmlContainerControl)e.Item.FindControl("DivLPU");
                System.Web.UI.HtmlControls.HtmlContainerControl divNotaGraf = (System.Web.UI.HtmlControls.HtmlContainerControl)e.Item.FindControl("divNotaGraf");
                System.Web.UI.WebControls.Label NotaEd = (System.Web.UI.WebControls.Label)e.Item.FindControl("LblNotaEdu");
                System.Web.UI.HtmlControls.HtmlGenericControl NIndicador = e.Item.FindControl("NIndicador") as HtmlGenericControl;
                string NombreIndicador = NIndicador.InnerText;
                ClientScript.RegisterStartupScript(GetType(), "Grafica", "fjsCargarGraficaIndicadorSectorial4T('ddlHistoricoMetas', 'divGrafIndicadorSectorial',1,'" + NombreIndicador.Trim() + "')", true);
                //if (QsId == "11" && NotaEd.Text == "True")
                //{
                //    divNotaGraf.Visible = true;
                //    postLPU.Visible = false;
                //    ClientScript.RegisterStartupScript(GetType(), "Grafica", "fjsCargarGraficaIndicadorSectorial('ddlHistoricoMetas', 'divGrafIndicadorSectorial',0)", true);
                //}
                //else
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "Grafica", "fjsCargarGraficaIndicadorSectorial('ddlHistoricoMetas', 'divGrafIndicadorSectorial',1)", true);
                //}
            }
        }

        protected void ddlSectores_DataBound(object sender, EventArgs e)
        {
            if (((DropDownList)sender).Items.FindByValue(Request.Params["idSector"]) != null)
            {
                ((DropDownList)sender).Items.FindByValue(Request.Params["idSector"]).Selected = true;
            }
        }
    }
}