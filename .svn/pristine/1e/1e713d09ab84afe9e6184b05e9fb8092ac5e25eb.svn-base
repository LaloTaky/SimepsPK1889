using MoreLinq;
using SIMEPS.Dal;
using SIMEPS.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIMEPS
{
    /*
     * Parametros
     * txtBuscador => Para inicializar la busqueda normal - No simpre se recibe.
     * opcionBusqueda = 1 => Indica que se utilizará la pantalla para realizar la búsqueda normal.
     * opcionBusqueda = 2 => Indica que se utilizará la pantalla para realizar la búsqueda especializada. 
    */
    public partial class BuscaIndicador : System.Web.UI.Page
    {
        IndicadoresDal Indicador = new Dal.IndicadoresDal();
        List<Busqueda> listaBusqueda = new List<Busqueda>();
        protected void Page_Load(object sender, EventArgs e)
        {
            int tipoBusqueda = Request.Params.Get("opcionBusqueda") == null ? 1 : Convert.ToInt16(Request.Params.Get("opcionBusqueda"));
            string pTexto = Request.Params.Get("txtBuscador") == null ? "" : Request.Params.Get("txtBuscador");
            if (!IsPostBack)
            {
                /*
                if (tipoBusqueda != 2)
                {
                    panelbusqueda.Visible = false;*/
                    panelBusquedaEspecializada.Visible = true;/*
                }
                else
                {
                    panelbusqueda.Visible = true;
                    panelBusquedaEspecializada.Visible = false;
                    texBuscador.Text = pTexto;
                }
                */
            }
            else
            {
                if (String.IsNullOrEmpty(ddlRamo.DataSourceID))
                {
                    ddlRamo.DataSourceID = "odsRamo";
                    ddlRamo.DataTextField = "DESCRIPCION";
                    ddlRamo.DataValueField = "RAMO";
                    ddlPrograma.DataSourceID = "odsPrograma";
                    ddlPrograma.DataTextField = "NOMBRE";
                    ddlPrograma.DataValueField = "ID_MATRIZ";
                    ddlObjetivoN.DataSourceID = "odsObjetivoN";
                    ddlObjetivoN.DataTextField = "DESC_OBJETIVO";
                    ddlObjetivoN.DataValueField = "ID_OBJETIVO_M";
                    ddlProgramaSectorial.DataSourceID = "odsProgramaSectorial";
                    ddlProgramaSectorial.DataTextField = "NOMBRE";
                    ddlProgramaSectorial.DataValueField = "ID_PROG_SECTORIAL";
                    ddlUnidaM.DataSourceID = "odsUnidadM";
                    ddlUnidaM.DataTextField = "UNIDAD_VALUE";
                    ddlUnidaM.DataValueField = "ID";
                    ddlFrecuanciaM.DataSourceID = "odsFrecuanciaM";
                    ddlFrecuanciaM.DataTextField = "FRECUENCIA_MEDICION";
                    ddlFrecuanciaM.DataValueField = "ID";
                    ddlDerechos.DataSourceID = "odsDerechos";
                    ddlDerechos.DataTextField = "DER_DESCRIPCION_I";
                    ddlDerechos.DataValueField = "DER_ID_I";
                }
            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MosaicoSips.aspx");
        }
        protected void sdsBuscador_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDDLs((List<Busqueda>)e.ReturnValue);
                grvPrograma.Visible = false;
            }
        }
        public void CargaDDLs(List<Busqueda> lista)
        {
            Busqueda selecione = new Busqueda
            {
                CICLO_ID = 0,

                CICLO_VALUE = "-Seleccione-",

                RAMO = 0
                                                ,
                NOMBRE_RAMO = "-Seleccione-"
                                                ,
                ID_MATRIZ = 0
                                                ,
                NOMBRE_PROGRAMA = "-Seleccione-"
                                                ,
                ID_OBJETIVO = -1
                                                ,
                NOMBRE_OBJETIVO = "-Seleccione-"
                                                ,
                ID_DERECHO = -1
                                                ,
                NOMBRE_DERECHO = "-Seleccione-"
                                                ,
                ID_PROG_SECTORIAL = -1
                                                ,
                NOMBRE_PRO_SECTORIAL = "-Seleccione-"
                                                ,
                NIVEL = -1
                                                ,
                NOMBRE_NIVEL = "-Seleccione-"
                                                ,
                UNIDAD_MEDIDA = "-Seleccione-"
                                                ,
                FRECUENCIA_MEDICION = "-Seleccione-",

                ID = -1

            };
            lista.Add(selecione);
            var idCiclos = ddlCiclos.SelectedValue;
            ddlCiclos.DataSource = lista.DistinctBy(c => c.CICLO_ID)
                                          .OrderBy(c => c.CICLO_ID).ToList();
            ddlCiclos.DataTextField = "CICLO_VALUE";
            ddlCiclos.DataValueField = "CICLO_ID";
            ddlCiclos.DataBind();
            if (ddlCiclos.Items.FindByValue(idCiclos) != null)
                ddlCiclos.SelectedValue = idCiclos;

            ddlRamo.DataSource = lista.DistinctBy(l => l.RAMO)
                                       .OrderBy(l => l.RAMO).ToList();
            ddlRamo.DataTextField = "NOMBRE_RAMO";
            ddlRamo.DataValueField = "RAMO";
            ddlRamo.DataBind();

            ddlPrograma.DataSource = lista.DistinctBy(p => p.ID_MATRIZ)
                                          .OrderBy(p => p.ID_MATRIZ).ToList();
            ddlPrograma.DataTextField = "NOMBRE_PROGRAMA";
            ddlPrograma.DataValueField = "ID_MATRIZ";
            ddlPrograma.DataBind();

            ddlObjetivoN.DataSource = lista.Where(o => o.NOMBRE_OBJETIVO != "")
                                             .DistinctBy(o => o.ID_OBJETIVO)
                                             .OrderBy(o => o.ID_OBJETIVO).ToList();
            ddlObjetivoN.DataTextField = "NOMBRE_OBJETIVO";
            ddlObjetivoN.DataValueField = "ID_OBJETIVO";
            ddlObjetivoN.DataBind();

            ddlDerechos.DataSource = lista.Where(d => d.NOMBRE_DERECHO != null && d.NOMBRE_DERECHO != "")
                                                   .DistinctBy(d => d.ID_DERECHO)
                                                   .OrderBy(d => d.ID_DERECHO).ToList();
            ddlDerechos.DataTextField = "NOMBRE_DERECHO";
            ddlDerechos.DataValueField = "ID_DERECHO";
            ddlDerechos.DataBind();

            ddlProgramaSectorial.DataSource = lista.Where(s => s.NOMBRE_PRO_SECTORIAL != "")
                                                   .DistinctBy(s => s.ID_PROG_SECTORIAL)
                                                   .OrderBy(s => s.ID_PROG_SECTORIAL).ToList();
            ddlProgramaSectorial.DataTextField = "NOMBRE_PRO_SECTORIAL";
            ddlProgramaSectorial.DataValueField = "ID_PROG_SECTORIAL";
            ddlProgramaSectorial.DataBind();

            ddlNivelM.DataSource = lista.DistinctBy(n => n.NIVEL)
                                        .OrderBy(n => n.NIVEL).ToList();
            ddlNivelM.DataTextField = "NOMBRE_NIVEL";
            ddlNivelM.DataValueField = "NIVEL";
            ddlNivelM.DataBind();

            ddlUnidaM.DataSource = lista.DistinctBy(u => u.UNIDAD_MEDIDA)
                                          .OrderBy(u => u.ID).ToList();
            ddlUnidaM.DataTextField = "UNIDAD_MEDIDA";
            ddlUnidaM.DataValueField = "ID";
            ddlUnidaM.DataBind();

            ddlFrecuanciaM.DataSource = lista.Where(f => f.FRECUENCIA_MEDICION != "")
                                          .DistinctBy(f => f.FRECUENCIA_MEDICION)
                                          .OrderBy(u => u.ID).ToList();
            ddlFrecuanciaM.DataTextField = "FRECUENCIA_MEDICION";
            ddlFrecuanciaM.DataValueField = "ID";
            ddlFrecuanciaM.DataBind();


        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ClearWindow();
        }
        private void ClearWindow()
        {
            ddlCiclos.SelectedIndex = 0;
            ddlRamo.SelectedIndex = 0;
            ddlPrograma.SelectedIndex = 0;
            ddlObjetivoN.SelectedIndex = 0;
            ddlProgramaSectorial.SelectedIndex = 0;
            ddlNivelM.SelectedIndex = 0;
            ddlUnidaM.SelectedIndex = 0;
            ddlFrecuanciaM.SelectedIndex = 0;
            ddlDerechos.SelectedIndex = 0;
            texBuscador.Text = String.Empty;
            grvPrograma.Visible = false;
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            HdPrograma.Value = ddlPrograma.SelectedValue;
            HdnUnidad.Value = ddlUnidaM.SelectedValue;
            HdFrecuencia.Value = ddlFrecuanciaM.SelectedValue;
            HdProgSecto.Value = ddlProgramaSectorial.SelectedValue;
            HdDerecho.Value = ddlDerechos.SelectedValue;
            HdCiclo.Value = ddlCiclos.SelectedValue;
            HdRamo.Value = ddlRamo.SelectedValue;
            HdObjetivo.Value = ddlObjetivoN.SelectedValue;
            HdNivelMIR.Value = ddlNivelM.SelectedValue;
            grvPrograma.Visible = true;
        }

    }
}