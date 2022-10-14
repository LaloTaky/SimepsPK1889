using Newtonsoft.Json;
using SIMEPS.Comun;
using SIMEPS.Dal;
using SIMEPS.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

using System.ComponentModel;
using System.Web.Services;
using System.Web.Script.Services;

namespace SIMEPS
{
    public partial class MetasNacionales : System.Web.UI.Page
    {
        private static string meta;
        private static int idMetaSeleccionada;
        public string MetaSeleccionada
        {
            get { return meta; }
            set { meta = value; }
        }
        Logger log = new Logger();

        static IndicadoresDal indicador = new IndicadoresDal();
        static List<MetasNac> listaMetas;
        private List<ObjetivosM> listaObjetivosMetas;
        private List<IndicadoresPND> listaIndicadoresObjetivo;
        private List<IndicadoresTrans> listaIndicadoresTrans;

        protected void Page_Load(object sender, EventArgs e)
        {
            Logger log = new Logger();
            if (!this.IsPostBack)
            {
                listaMetas = indicador.ConsultaMetasNacionales();
                this.CargaBotonesMetas();
            }
        }

        private void CargaBotonesMetas()
        {
            string host = HttpContext.Current.Request.Url.Authority;

            BotonMeta1.ImageUrl = this.ResolveUrl("~" + listaMetas[0].PATH_IMAGEN_URL);
            BotonMeta1.AlternateText = listaMetas[0].DESC_META;

            BotonMeta2.ImageUrl = this.ResolveUrl("~" + listaMetas[1].PATH_IMAGEN_URL);
            BotonMeta2.AlternateText = listaMetas[1].DESC_META;

            BotonMeta3.ImageUrl = this.ResolveUrl("~" + listaMetas[2].PATH_IMAGEN_URL);
            BotonMeta3.AlternateText = listaMetas[2].DESC_META;

            BotonMeta4.ImageUrl = this.ResolveUrl("~" + listaMetas[3].PATH_IMAGEN_URL);
            BotonMeta4.AlternateText = listaMetas[3].DESC_META;

            BotonMeta5.ImageUrl = this.ResolveUrl("~" + listaMetas[4].PATH_IMAGEN_URL);
            BotonMeta5.AlternateText = listaMetas[4].DESC_META;

            BotonEstrategiasTransversales.ImageUrl = this.ResolveUrl("~/img/BotonesMetasNacionales/btn_estrategias_transversales.jpg");

        }

        private void CargaObjetivosConIndicadores(int idMeta)
        {
            // Se cargan los Objetivos para las Metas
            try
            {
                listaObjetivosMetas = indicador.ConsultaObjetivosPND(idMeta);
                var l1 = listaObjetivosMetas.OrderBy(n => n.DESC_OBJETIVO).ToList();

                listaIndicadoresObjetivo = indicador.ConsultaIndicadoresObjetivo(idMeta).ToList();
                var l2 = listaIndicadoresObjetivo.GroupBy(x => new { x.NOMBRE, x.UNIDAD_MEDIDA }).Select(x => x.FirstOrDefault()).ToList();

                List<ObjetivosM> lstConIndicadores = new List<ObjetivosM>();
                List<ObjetivosM> lstSinIndicadores = new List<ObjetivosM>();
                List<ObjetivosM> lstFinal = new List<ObjetivosM>();

                foreach (var obj in l1)
                {
                    if (l2.Count(x => x.ID_OBJETIVO_M.Equals(obj.ID_OBJETIVO_M)).Equals(0))
                        lstSinIndicadores.Add(obj);
                    else
                        lstConIndicadores.Add(obj);
                }

                lstFinal.AddRange(lstConIndicadores);
                lstFinal.AddRange(lstSinIndicadores);

                foreach (var objetivo in lstFinal)
                {
                    if (l2.Count(x => x.ID_OBJETIVO_M.Equals(objetivo.ID_OBJETIVO_M)).Equals(0))
                    {
                        l2.Add(new IndicadoresPND()
                        {
                            ID_OBJETIVO_M = objetivo.ID_OBJETIVO_M,
                            NOMBRE = "Sin indicadores asociados",
                            ID_INDICADOR_PND = -1
                        });
                    }
                }

                DataSet ds = new DataSet();
                DataTable dt1 = ToDataTable(lstFinal, "TC_OBJETIVOS_M");
                DataTable dt2 = ToDataTable(l2, "TD_INDICADORES_PND");

                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                ds.Relations.Add("myrelation", ds.Tables["TC_OBJETIVOS_M"].Columns["ID_OBJETIVO_M"], ds.Tables["TD_INDICADORES_PND"].Columns["ID_OBJETIVO_M"]);

                parentRepeater.DataSource = ds.Tables["TC_OBJETIVOS_M"];
                parentRepeater.DataBind();

                Page.DataBind();
            }
            catch (Exception error)
            {
                if (!error.Message.Equals("Excepción de DataSet para Objetivos de Metas con Indicadores."))
                {
                    log.LogMessageToFile("");
                    log.LogMessageToFile(error.Message);
                    log.LogMessageToFile(error.StackTrace);
                }
            }
        }

        private void CargaIndicadoresTransversales()
        {
            // Se cargan los Indicadores Transversales
            try
            {
                listaIndicadoresTrans = indicador.ConsultaIndicadoresTransversales();

                var l1 = listaIndicadoresTrans.GroupBy(x => x.NOMBRE).Select(x => x.FirstOrDefault());

                DataSet ds = new DataSet();
                DataTable dt1 = ToDataTable(l1, "TD_INDICADORES_ESTR_TRANS");

                ds.Tables.Add(dt1);

                RepeaterIndicadoresTrans.DataSource = ds.Tables["TD_INDICADORES_ESTR_TRANS"];
                RepeaterIndicadoresTrans.DataBind();
                Page.DataBind();
            }
            catch (Exception error)
            {
                if (!error.Message.Equals("Excepción de DataSet para Indicadores Transversales."))
                {
                    log.LogMessageToFile("");
                    log.LogMessageToFile(error.Message);
                    log.LogMessageToFile(error.StackTrace);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public DataTable ToDataTable<T>(IEnumerable<T> collection, string nombreTabla)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            object temp;
            DataRow dr;

            dt.TableName = nombreTabla;
            for (int i = 0; i < pia.Length; i++)
            {
                dt.Columns.Add(pia[i].Name, Nullable.GetUnderlyingType(pia[i].PropertyType) ?? pia[i].PropertyType);
                dt.Columns[i].AllowDBNull = true;
            }

            foreach (T item in collection)
            {
                dr = dt.NewRow();
                dr.BeginEdit();

                for (int i = 0; i < pia.Length; i++)
                {
                    temp = pia[i].GetValue(item, null);
                    if (temp == null || (temp.GetType().Name == "Char" && ((char)temp).Equals('\0')))
                    {
                        dr[pia[i].Name] = (object)DBNull.Value;
                    }
                    else
                    {
                        dr[pia[i].Name] = temp;
                    }
                }

                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// Convierte un objeto de tipo lista en una tabla
        /// Fuente:
        /// https://social.msdn.microsoft.com/Forums/vstudio/en-US/6ffcb247-77fb-40b4-bcba-08ba377ab9db/converting-a-list-to-datatable?forum=csharpgeneral
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        

        private DataTable Table<T>(string name, IEnumerable<T> list, PropertyInfo[] pi)
        {
            DataTable table = new DataTable(name);
            foreach (PropertyInfo p in pi)
                table.Columns.Add(p.Name, p.PropertyType);
            return table;
        }

        /// <summary>
        /// Se ejecuta cuando se da un clic en el boton de alguna Meta
        /// El orden de los botones es igual que en la tabla de TC_METAS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MetasClick(object sender, CommandEventArgs e)
        {
            if (IsPostBack)
            {
                //Label1.Text = "Meta:" + DateTime.Now.ToLongTimeString();
                MetaSeleccionada = e.CommandArgument.ToString();
                Response.Redirect("/MetasNacionales.aspx");
            }
        }

        /// <summary>
        /// Descarga el archivo de excel de la url indicada
        /// Este archivo contiene la base de datos de todos los indicadores
        /// </summary>
        protected void DescargaArchivoIndicadores(object sender, CommandEventArgs e)
        {
            string FilePath = "https://www.coneval.org.mx/coordinacion/Documents/monitoreo/PND/Metas_Nacionales_PND20132018.csv";
            Response.Redirect(FilePath);
        }

        protected void BotonEstrategiasTransversalesClick(object sender, CommandEventArgs e)
        {
            panelListIndicadores.Visible = true;
            panelListObjetivos.Visible = false;
            panelDetalleObjetivos.Visible = false;
            panelDetalleIndicadores.Visible = false;
            CargaBotonesMetas();
            tituloTrans.InnerText = "Indicadores";
            tituloObjetivos.InnerText = "";

            cambiarBotonesDifuminados("EstrategiaTrans");
            CargaIndicadoresTransversales();
        }

        protected void BotonMetaClick(object sender, CommandEventArgs e)
        {
            panelListObjetivos.Visible = true;
            panelListIndicadores.Visible = false;
            panelDetalleObjetivos.Visible = false;
            panelDetalleIndicadores.Visible = false;
            //Label1.Text = "Meta:" + DateTime.Now.ToLongTimeString();
            tituloObjetivos.InnerText = "Objetivos";
            tituloTrans.InnerText = "";

            CargaBotonesMetas();

            MetaSeleccionada = e.CommandArgument.ToString();

            switch (MetaSeleccionada)
            {
                case "Meta1":
                    cambiarBotonesDifuminados("Meta1");
                    CargaObjetivosConIndicadores(1);
                    idMetaSeleccionada = listaMetas[0].ID_META;
                    break;
                case "Meta2":
                    cambiarBotonesDifuminados("Meta2");
                    CargaObjetivosConIndicadores(2);
                    idMetaSeleccionada = listaMetas[1].ID_META;
                    break;
                case "Meta3":
                    cambiarBotonesDifuminados("Meta3");
                    CargaObjetivosConIndicadores(3);
                    idMetaSeleccionada = listaMetas[2].ID_META;
                    break;
                case "Meta4":
                    cambiarBotonesDifuminados("Meta4");
                    CargaObjetivosConIndicadores(4);
                    idMetaSeleccionada = listaMetas[3].ID_META;
                    break;
                case "Meta5":
                    cambiarBotonesDifuminados("Meta5");
                    CargaObjetivosConIndicadores(5);
                    idMetaSeleccionada = listaMetas[4].ID_META;
                    break;
            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public static List<DetalleIndicadorMeta> GraficaIndicadoresPND(int iIdObetivoM, string sNombre)
        public static string GraficaIndicadoresPND(int iIdObetivoM, string sNombre, string unidadMedida)
        {
            string lstSerializada = "";
            List<IndicadoresPND> lstIndicadores = new List<IndicadoresPND>();
            List<DetalleIndicadorMeta> lstDetalleIndicador = new List<DetalleIndicadorMeta>();

            lstIndicadores = indicador.ConsultaIndicadoresObjetivo(idMetaSeleccionada);



            lstDetalleIndicador = lstIndicadores
                .Where(i => i.ID_OBJETIVO_M.Equals(iIdObetivoM) && i.NOMBRE.Equals(sNombre) && i.UNIDAD_MEDIDA.Equals(unidadMedida, StringComparison.InvariantCultureIgnoreCase))
                .Select(ind => new DetalleIndicadorMeta
                {
                    CICLO = ind.CICLO,
                    MetaAlcanzada = string.Format("{0:0.00}", ind.VALOR_ALCANZADO)
                })
                                                         .OrderBy(i => i.CICLO).ToList();

            lstSerializada = JsonConvert.SerializeObject(lstDetalleIndicador, Newtonsoft.Json.Formatting.Indented);

            return lstSerializada;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static string GraficaIndicadoresTrans(string sNOMBRE)
        {
            string lstSerializada = "";
            List<IndicadoresTrans> lstIndicadores = new List<IndicadoresTrans>();
            List<DetalleIndicadorMeta> lstDetalleIndicador = new List<DetalleIndicadorMeta>();


            lstIndicadores = indicador.ConsultaIndicadoresTransversales();
            lstDetalleIndicador = lstIndicadores

                .Where(i => i.NOMBRE.Equals(sNOMBRE))
                .Select(ind => new DetalleIndicadorMeta
                {
                    CICLO = ind.CICLO,
                    MetaAlcanzada = string.Format("{0:0.00}", ind.VALOR_ALCANZADO)
                })
                .OrderBy(i => i.CICLO).ToList();

            lstSerializada = JsonConvert.SerializeObject(lstDetalleIndicador, Newtonsoft.Json.Formatting.Indented);

            return lstSerializada;
        }

        protected void ObtenerDetalleIndicador_Click(object sender, EventArgs e)
        {
            var arr = ((System.Web.UI.WebControls.LinkButton)sender).CommandArgument.ToString().Split('|');
            CargaIndicadorTransversalDetalle(Int32.Parse(arr[1]));
            panelListIndicadores.Visible = false;
            panelListObjetivos.Visible = false;
            panelDetalleObjetivos.Visible = false;
            panelDetalleIndicadores.Visible = true;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(GetType(), "Grafica", "fjsGeneraGraficoDetalleIndicadorTrans('" + arr[0] + "','" + arr[1] + "');", true);
        }

        protected void VisualizarIndicadorTrans_Click(object sender, EventArgs e)
        {
            CargaBotonesMetas();
            tituloTrans.InnerText = "Indicadores";
            tituloObjetivos.InnerText = "";

            cambiarBotonesDifuminados("EstrategiaTrans");
            CargaIndicadoresTransversales();

            panelListIndicadores.Visible = true;
            panelListObjetivos.Visible = false;
            panelDetalleObjetivos.Visible = false;
            panelDetalleIndicadores.Visible = false;
        }

        private void CargaIndicadorTransversalDetalle(int ID)
        {
            // Se cargan los Indicadores Transversales
            try
            {
                listaIndicadoresTrans = indicador.ConsultaIndicadoresTransversales();

                var l1 = listaIndicadoresTrans.GroupBy(x => x.NOMBRE).Select(x => x.FirstOrDefault());
                l1 = l1.Where(x => x.ID_INDICADOR_ESTR_TRANS.Equals(ID));
                DataSet ds = new DataSet();
                DataTable dt1 = ToDataTable(l1, "TD_INDICADORES_ESTR_TRANS");

                ds.Tables.Add(dt1);

                PanelRepeaterIndicadoresTrans.DataSource = ds.Tables["TD_INDICADORES_ESTR_TRANS"];
                PanelRepeaterIndicadoresTrans.DataBind();
                Page.DataBind();
            }
            catch (Exception error)
            {
                if (!error.Message.Equals("Excepción de DataSet para Indicadores Transversales."))
                {
                    log.LogMessageToFile("");
                    log.LogMessageToFile(error.Message);
                    log.LogMessageToFile(error.StackTrace);
                }
            }
        }

        protected void ObtenerDetalleObjetivoIndicador_Click(object sender, EventArgs e)
        {
            var arr = ((System.Web.UI.WebControls.LinkButton)sender).CommandArgument.ToString().Split('|');
            
            panelListIndicadores.Visible = false;
            panelListObjetivos.Visible = false;
            panelDetalleObjetivos.Visible = true;
            panelDetalleIndicadores.Visible = false;

            switch (MetaSeleccionada)
            {
                case "Meta1":
                    cambiarBotonesDifuminados("Meta1");
                    CargaObjetivosConIndicadoresDetalle(1, Int32.Parse(arr[0]), Int32.Parse(arr[2]));
                    idMetaSeleccionada = listaMetas[0].ID_META;
                    break;
                case "Meta2":
                    cambiarBotonesDifuminados("Meta2");
                    CargaObjetivosConIndicadoresDetalle(2, Int32.Parse(arr[0]), Int32.Parse(arr[2]));
                    idMetaSeleccionada = listaMetas[1].ID_META;
                    break;
                case "Meta3":
                    cambiarBotonesDifuminados("Meta3");
                    CargaObjetivosConIndicadoresDetalle(3, Int32.Parse(arr[0]), Int32.Parse(arr[2]));
                    idMetaSeleccionada = listaMetas[2].ID_META;
                    break;
                case "Meta4":
                    cambiarBotonesDifuminados("Meta4");
                    CargaObjetivosConIndicadoresDetalle(4, Int32.Parse(arr[0]), Int32.Parse(arr[2]));
                    idMetaSeleccionada = listaMetas[3].ID_META;
                    break;
                case "Meta5":
                    cambiarBotonesDifuminados("Meta5");
                    CargaObjetivosConIndicadoresDetalle(5, Int32.Parse(arr[0]), Int32.Parse(arr[2]));
                    idMetaSeleccionada = listaMetas[4].ID_META;
                    break;
            }

            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(GetType(), "Grafica", "fjsGeneraGraficoDetalleIndicador('" + arr[0] + "','" + arr[1] + "','" + arr[2] + "','" + arr[3] + "');", true);
        }

        private void CargaObjetivosConIndicadoresDetalle(int idMeta, int Id_Objetivo, int Id_Indicador)
        {
            // Se cargan los Objetivos para las Metas
            try
            {
                listaObjetivosMetas = indicador.ConsultaObjetivosPND(idMeta);
                var l1 = listaObjetivosMetas.OrderBy(n => n.DESC_OBJETIVO).ToList();

                listaIndicadoresObjetivo = indicador.ConsultaIndicadoresObjetivo(idMeta);
                var l2 = listaIndicadoresObjetivo.GroupBy(x => new { x.NOMBRE, x.UNIDAD_MEDIDA }).Select(x => x.FirstOrDefault()).ToList();
                l1 = l1.Where(x => x.ID_OBJETIVO_M.Equals(Id_Objetivo)).ToList();
                l2 = l2.Where(x => x.ID_INDICADOR_PND.Equals(Id_Indicador)).ToList();

                foreach (var objetivo in l1)
                {
                    if(l2.Count(x => x.ID_OBJETIVO_M.Equals(objetivo.ID_OBJETIVO_M)).Equals(0))
                    {
                        l2.Add(new IndicadoresPND() {
                            ID_OBJETIVO_M = objetivo.ID_OBJETIVO_M,
                            NOMBRE = "Sin indicadores asociados",
                            ID_INDICADOR_PND = -1
                        });
                    }
                }

                DataSet ds = new DataSet();
                DataTable dt1 = ToDataTable(l1, "TC_OBJETIVOS_M");
                DataTable dt2 = ToDataTable(l2, "TD_INDICADORES_PND");

                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                ds.Relations.Add("myrelation", ds.Tables["TC_OBJETIVOS_M"].Columns["ID_OBJETIVO_M"], ds.Tables["TD_INDICADORES_PND"].Columns["ID_OBJETIVO_M"]);

                PanelRepeaterDetalleObjetivoIndicador.DataSource = ds.Tables["TC_OBJETIVOS_M"];
                PanelRepeaterDetalleObjetivoIndicador.DataBind();

                Page.DataBind();
            }
            catch (Exception error)
            {
                if (!error.Message.Equals("Excepción de DataSet para Objetivos de Metas con Indicadores."))
                {
                    log.LogMessageToFile("");
                    log.LogMessageToFile(error.Message);
                    log.LogMessageToFile(error.StackTrace);
                }
            }
        }

        protected void VisualizarObjetivoIndicador_Click(object sender, EventArgs e)
        {
            panelListObjetivos.Visible = true;
            panelListIndicadores.Visible = false;
            panelDetalleObjetivos.Visible = false;
            panelDetalleIndicadores.Visible = false;
            
            tituloObjetivos.InnerText = "Objetivos";
            tituloTrans.InnerText = "";

            CargaBotonesMetas();

            switch (MetaSeleccionada)
            {
                case "Meta1":
                    cambiarBotonesDifuminados("Meta1");
                    CargaObjetivosConIndicadores(1);
                    idMetaSeleccionada = listaMetas[0].ID_META;
                    break;
                case "Meta2":
                    cambiarBotonesDifuminados("Meta2");
                    CargaObjetivosConIndicadores(2);
                    idMetaSeleccionada = listaMetas[1].ID_META;
                    break;
                case "Meta3":
                    cambiarBotonesDifuminados("Meta3");
                    CargaObjetivosConIndicadores(3);
                    idMetaSeleccionada = listaMetas[2].ID_META;
                    break;
                case "Meta4":
                    cambiarBotonesDifuminados("Meta4");
                    CargaObjetivosConIndicadores(4);
                    idMetaSeleccionada = listaMetas[3].ID_META;
                    break;
                case "Meta5":
                    cambiarBotonesDifuminados("Meta5");
                    CargaObjetivosConIndicadores(5);
                    idMetaSeleccionada = listaMetas[4].ID_META;
                    break;
            }
        }

        
        private void cambiarBotonesDifuminados(string nombreBoton)
        {
            BotonMeta1.ImageUrl = ResolveUrl("~" + listaMetas[0].PATH_IMAGEN_OVER_URL);
            BotonMeta2.ImageUrl = ResolveUrl("~" + listaMetas[1].PATH_IMAGEN_OVER_URL);
            BotonMeta3.ImageUrl = ResolveUrl("~" + listaMetas[2].PATH_IMAGEN_OVER_URL);
            BotonMeta4.ImageUrl = ResolveUrl("~" + listaMetas[3].PATH_IMAGEN_OVER_URL);
            BotonMeta5.ImageUrl = ResolveUrl("~" + listaMetas[4].PATH_IMAGEN_OVER_URL);
            BotonMeta1.AlternateText = listaMetas[0].DESC_META;
            BotonMeta2.AlternateText = listaMetas[1].DESC_META;
            BotonMeta3.AlternateText = listaMetas[2].DESC_META;
            BotonMeta4.AlternateText = listaMetas[3].DESC_META;
            BotonMeta5.AlternateText = listaMetas[4].DESC_META;

            BotonEstrategiasTransversales.ImageUrl = ResolveUrl("~/img/BotonesMetasNacionales/btn_estrategias_transversales_over.jpg");
            BotonEstrategiasTransversales.AlternateText = "Estrategias Transversales";

            if (nombreBoton == "Meta1")
                BotonMeta1.ImageUrl = ResolveUrl("~" + listaMetas[0].PATH_IMAGEN_URL);
            if (nombreBoton == "Meta2")
                BotonMeta2.ImageUrl = ResolveUrl("~" + listaMetas[1].PATH_IMAGEN_URL);
            if (nombreBoton == "Meta3")
                BotonMeta3.ImageUrl = ResolveUrl("~" + listaMetas[2].PATH_IMAGEN_URL);
            if (nombreBoton == "Meta4")
                BotonMeta4.ImageUrl = ResolveUrl("~" + listaMetas[3].PATH_IMAGEN_URL);
            if (nombreBoton == "Meta5")
                BotonMeta5.ImageUrl = ResolveUrl("~" + listaMetas[4].PATH_IMAGEN_URL);
            if (nombreBoton == "EstrategiaTrans")
                BotonEstrategiasTransversales.ImageUrl = this.ResolveUrl("~/img/BotonesMetasNacionales/btn_estrategias_transversales.jpg");
        }
    }


}