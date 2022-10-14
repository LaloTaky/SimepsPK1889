using SIMEPS.Dal;
using SIMEPS.Modelo;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SIMEPS
{
   public partial class Mosaico : System.Web.UI.Page
   {
      public String urlMetodologiaMir = "#", urlReporteHistorico = "", urlReporteTodosIndicadores="", urlDictamenAprobacionIndicadores="";
      public String valorT = "";
      public int iCiclo = DateTime.Now.Year;
      
      protected void Page_Load(object sender, EventArgs e)
      {
         string dCiclo = iCiclo.ToString();
         IndicadoresDal simeps = new IndicadoresDal();
         List<Parametro> parametros = simeps.ConsultarParametro("URL_METODOLOGIA_MIR");
         foreach (Parametro url in parametros)
         {
            urlMetodologiaMir = url.VALOR;
         }
         List<Parametro> rutaHco = simeps.ConsultarParametro("URL_REPORTE_HISTORICO_INDI");
         foreach (Parametro url in rutaHco)
         {
            urlReporteHistorico = url.VALOR;
         }
         List<Parametro> rutaIndicadores = simeps.ConsultarParametro("URL_REPORTE_TODOS_INDI");
         foreach (Parametro url in rutaIndicadores)
         {
            urlReporteTodosIndicadores = url.VALOR;
         }
         if (Request.Params["t"] != null) {
                if (Request.Params["t"].ToUpper().Equals("C"))
                {
                    List<Parametro> rutaDictamenIndicadores = simeps.ConsultarParametro("URL_REPORTE_HISTORICO_APRO_IND");
                    foreach (Parametro url in rutaDictamenIndicadores)
                    {
                        urlDictamenAprobacionIndicadores = url.VALOR;
                    }
                    HdnRutaHistoricoAproInd.Value = urlDictamenAprobacionIndicadores;
                }
         }
         HdnRutaHistorico.Value = urlReporteHistorico;
         HdnRutaTodos.Value = urlReporteTodosIndicadores;
         

            if (Request.Params["pCiclo"] != null) {
                odsMosaicos.SelectParameters["pCiclo"].DefaultValue = Request.Params["pCiclo"].ToString();
                iCiclo = Convert.ToInt16(Request.Params["pCiclo"].ToString());
            }
            else {
                odsMosaicos.SelectParameters["pCiclo"].DefaultValue = (iCiclo).ToString();
            }
      

         if (Request.Params["t"] != null)
         {
            if (Request.Params["t"].ToUpper().Equals("B"))
            {
               //ddlCiclos.Style.Add("display", "none");
               //spnCiclo.Style.Add("display", "none");
               valorT = "&t=" + Request.Params["t"];
               lblTitulo.Text = "Matriz de Indicadores para Resultados";
               lblCuerpo1.Text = "La <a href=\"" + urlMetodologiaMir + "\">Matriz de Indicadores para Resultados</a> (MIR) es una herramienta de planeación que identifica en forma resumida los objetivos de un programa, incorpora los indicadores de resultados y gestión que miden dichos objetivos; especifica los medios para obtener y verificar la información de los indicadores, e incluye los riesgos y contingencias que pueden afectar el desempeño del programa.";
                lblCuerpo2.Text = "De acuerdo con los  <a href='https://www.coneval.org.mx/rw/resource/coneval/eval_mon/1768.pdf'>Lineamientos Generales para la Evaluación de los Programas Federales de la Administración Pública Federal</a>, todos los programas presupuestarios están obligados a tener una MIR. A fin de contribuir a la transparencia y la rendición de cuentas, el CONEVAL pone a disposición para su consulta las MIR de los programas y acciones de desarrollo social desde 2008.";
               lblCuerpo3.Text = "La información referente al contenido de las MIR es responsabilidad de cada programa por lo que para cualquier información adicional, se sugiere solicitarla directamente a la dependencia o programa responsable.";
               dContenido.Visible = false;
               lblMIRSiglas.Visible = false;
               this.divBotones.Style.Add("display", "none");
               this.divBotonesDic.Style.Add("display", "none");
               this.SubtituloEncabezado.Style.Add("display", "none");
            }
            else if (Request.Params["t"].ToUpper().Equals("C"))
            {
               valorT = "&t=" + Request.Params["t"];
               lblTitulo.Text = "PROGRAMAS SOCIALES CON INDICADORES APROBADOS";
               lblCuerpo1.Text = "Desde su creación, el CONEVAL ha apoyado a los programas sociales a construir y mejorar sus indicadores. Algunas de las acciones que ha realizado para ello, son la impartición de cursos-taller de ";
               lblCuerpo1Cursiva.Text = "<i> Metodología de Marco Lógico para la construcción de la Matriz de Indicadores de Resultados y de Construcción de Indicadores de Desempeño</i>";
               lblCuerpo1Continuacion.Text = ", a los servidores públicos a cargo de los programas, así como la realización de asesorías técnicas.";
               lblCuerpo2.Text = "También, ha puesto en marcha el <a href='https://www.coneval.org.mx/coordinacion/Paginas/monitoreo/metodologia/maprob.aspx' >Proceso de Aprobación de Indicadores</a>, mediante el cual se determina si los indicadores contenidos en la <a href=\"" + urlMetodologiaMir + "\">Matriz de Indicadores para Resultados</a> de los programas sociales, cumplen con los criterios mínimos necesarios para medir los objetivos a los cuales están asociados, en un punto determinado en el tiempo.";
               lblCuerpo3.Text = "<b>" + "Consulta los programas sociales que cuentan con indicadores aprobados desde 2012." + "</b\">";
               dContenido.Visible = false;
               lblMIRSiglas.Visible = false;
               this.divBotones.Style.Add("display", "none");
               this.SubtituloEncabezado.Style.Add("display", "none");
            }
            else
            {
               valorT = "&t=" + Request.Params["t"];
               lblTitulo.Text = "SISTEMA DE INDICADORES DE LOS PROGRAMAS Y ACCIONES DE DESARROLLO SOCIAL";
               lblSubtitulo.Text = "¿Qué es un indicador?";
               lblCuerpo1.Text = "Los indicadores son una herramienta utilizada, a partir de varia&#8203;bles cuantitativas o cualitativas, para medir el logro de los ob&#8203;jetivos de los programas de desarrollo social y que representan un referente para el seguimiento de los avances y la evaluación de sus resultados. En México, los indicadores de los programas y acciones de desarrollo social han sido generados con base en la Metodología de Marco Lógico a través de la&nbsp <a href=\"" + urlMetodologiaMir + "\">Matriz de Indicadores para Resultados</a>";
               lblCuerpo2.Text = "El Sistema de Indicadores de los Programas y Acciones de Desarrollo Social expone el seguimiento de los indicadores de dichos programas, los cuales, proveen información oportuna y robusta sobre el alcance de sus objetivos.";

               lblPregunta.Text = "¿Para qué sirve el Sistema de Indicadores de los Programas y Acciones de Desarrollo Social?";
               

               lblRespuesta1.Text = "Para que los tomadores de decisiones y la ciudadanía en general cuenten con información &nbsp;acerca de los indicadores, metas y resultados de los programas y acciones sociales.";
               lblRespuesta2.Text = "Contribuir a la transparencia y rendición de cuentas.";
               lblRespuesta3.Text = "Para mejorar las políticas públicas, con la finalidad de continuar y corregir los programas &nbsp; &nbsp; &nbsp;y acciones de desarrollo&nbsp;social.";


               lblCuerpo4.Text = "En un esfuerzo porque esto suceda, el CONEVAL ha incluido en esta sección información de los indicadores de los niveles de Propósito ";
               lblCuerpo5.Text = "(razón de ser de los programas) y de Componentes ";
               lblCuerpo6.Text = "(bienes o servicios entregados a la población beneficiaria) de cada programa y acción social.";
               lblCuerpo7.Text = "Con la información que se presenta en esta sección, es posible conocer los resultados de los programas y acciones sociales, así como los avances en la solución de problemáticas específicas que buscan resolver.";
               lblCuerpo8.Text = "La información contenida en las MIR es responsabilidad de cada programa por lo que para cualquier información adicional, se sugiere solicitarla directamente a la dependencia o programa responsable.";
               this.divBotonesDic.Style.Add("display", "none");
            }
         }

         else
         {
            Label Label1 = (Label)Page.Master.FindControl("LblTitulo");
            LabeleTituloPrin.Text = "Módulo de indicadores de los programas y acciones de desarrollo social";
            lblSubtitulo.Text = "¿Qué es un indicador?";
            lblCuerpo1.Text = "Los indicadores son una herramienta utilizada, a partir de varia&#8203;bles cuantitativas o cualitativas, para medir el logro de los ob&#8203;jetivos de los programas de desarrollo social y que representan un referente para el seguimiento de los avances y la evaluación de sus resultados. En México, los indicadores de los programas y acciones de desarrollo social han sido generados con base en la Metodología de Marco Lógico a través de la&nbsp <a href=\"" + urlMetodologiaMir + "\">Matriz de Indicadores para Resultados</a>";
            lblCuerpo2.Text = "El Sistema de Indicadores de los Programas y Acciones de Desarrollo Social expone el seguimiento de los indicadores de dichos programas, los cuales, proveen información oportuna y robusta sobre el alcance de sus objetivos.";
            lblPregunta.Text = "¿Para qué sirve el Sistema de Indicadores de los Programas y Acciones de Desarrollo Social?";
            lblRespuesta1.Text = "Para que los tomadores de decisiones y la ciudadanía en general cuenten con información &nbsp;acerca de los indicadores, metas y resultados de los programas y acciones sociales.";
            lblRespuesta2.Text = "Transparencia y rendición de cuentas.";
            lblRespuesta3.Text = "Mejorar las políticas públicas.";
            lblCuerpo4.Text = "En un esfuerzo porque esto suceda, el CONEVAL ha incluido en esta sección información de los indicadores de los niveles de Propósito ";
            lblCuerpo5.Text = "(razón de ser de los programas) y de Componentes ";
            lblCuerpo6.Text = "(bienes o servicios entregados a la población beneficiaria) de cada programa y acción social.";
            lblCuerpo7.Text = "Con la información que se presenta en esta sección, es posible conocer los resultados de los programas y acciones sociales, así como los avances en la solución de problemáticas específicas que buscan resolver.";
            lblCuerpo8.Text = "¡Consulta el Sistema de Indicadores de los Programas y Acciones de Desarrollo Social de México!";
            this.TituloEncabezado.Style.Add("display", "none");
            this.divBotonesDic.Style.Add("display", "none");
            }
      }

      public void BuscarIndicador(object sender, EventArgs e)
      {
         Response.Redirect("BuscaIndicador.aspx?opcionBusqueda=1");
      }

      protected void btnBuscarEspecial_Click(object sender, EventArgs e)
      {
         Response.Redirect("BuscaIndicador.aspx");
      }
    }
}