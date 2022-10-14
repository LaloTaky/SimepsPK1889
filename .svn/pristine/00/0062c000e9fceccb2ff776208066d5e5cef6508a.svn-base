using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIMEPS.Dal;
using SIMEPS.Modelo;

namespace SIMEPS
{
   public partial class Programas : System.Web.UI.Page
   {
      public bool valorT = false;
      public int numeroProgramas = 0;
        public int numeroAprobados = 0;
        public int numeroIndicadores = 0;
        public Double promedioTasaPermanencia = 0;
        public Double TPermanencia = 0;
        public Double promedioMetas = 0;
        public Double pMetas = 0;
        public int EDRDestacado = 0;
        public int EDRModerado = 0;
        public int EDROportunidadMejora = 0;
        public int MIRDestacado = 0;
        public int MIRAdecuado = 0;
        public int MIRModerado = 0;
        public int MIROportunidadMejora = 0;
        public string barraMetas = "";
        public int version = 0;
        public string unidad = "-1";
        public string TC="";
      protected void Page_Load(object sender, EventArgs e)
      {
         if (Request.Params["t"] == null)
         {
            divAB.Visible = true;
            divProgramas.Visible = false;
            TC = " <div  style='font-weight: bold;color: #FFFFFF;word-wrap: break-word;padding-bottom: 10px;font-family: sans-serif;font-size: 12pt;'>Programas</div>";
            headerTC.InnerHtml = TC;
         }
         else if (Request.Params["t"].ToUpper().Equals("C"))
         {
            divProgramas.Visible = true;
            divAB.Visible = false;
                //TableHead.Text = "ProgramaTest";
                TC = "<div  class='HeaderTCC col-2 col-sm-2 col-md-2 text-lef' style='width: 70px;padding-top: 6pt'>Clave</div>";
                TC += "<div class='HeaderTCC col-4 col-sm-4 col-md-4 text-center' style='width: 255px;padding-top: 6pt'>Programa</div>";
                TC += "<div class='HeaderTCC col-2 col-sm-2 col-md-2 text-left' style='width: 85px;'>Proceso de aprobación</div>";
                TC += "<div class='HeaderTCC col-2 col-sm-2 col-md-2 text-left' style='width: 80px;padding-left: 20px;padding-top: 6pt'>Ciclo</div>";
                TC += "<div class='HeaderTCC col-2 col-sm-2 col-md-2 text-center' style='width:80px;padding-top: 6pt'>Dictamen</div>";
                headerTC.InnerHtml = TC;

          }
         else
         {
            divAB.Visible = true;
            divProgramas.Visible = false;
            TC = " <div  style='font-weight: bold;color: #FFFFFF;word-wrap: break-word;padding-bottom: 10px;font-family: sans-serif;font-size: 12pt;'>Programas</div>";
            headerTC.InnerHtml = TC;

            }
            if (Request.Params["unidad"] != null) {
                unidad = Request.Params["unidad"];
            }
            
         if (Request.Params["descarga"] != null)
         {
            string sUrl = Request.Params["url"];
            string sCiclo = Request.Params["ciclo"];
            string sRamo = Request.Params["ramo"];
            string sModalidad = Request.Params["modalidad"];
            string sClave = Request.Params["clave"];
            string sT = Request.Params["t"];
            ValidaDescarga(sUrl, sCiclo, sRamo, sModalidad, sClave, sT);
         }
      }

      protected void gvAB_DataBound(object sender, EventArgs e)
      {
         try
         {
            gvAB.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            LblNumeroProgramas.Text = numeroProgramas.ToString();
                LblNumeroAprobados.Text = numeroAprobados.ToString();
                if (numeroAprobados == 0)
                    LblNumeroAprobados.Text = "N/A*";
                LblNumeroIndicadores.Text = numeroIndicadores.ToString();
                promedioTasaPermanencia = Math.Truncate(TPermanencia * 100) / 100;
                LblPromedioTasaPermamencia.Text = promedioTasaPermanencia.ToString();
                if (promedioTasaPermanencia == 0)
                    LblPromedioTasaPermamencia.Text = "N/A*";
                promedioMetas = Math.Truncate(pMetas * 100) / 100;
                if (promedioMetas <= 48.5)
                    barraMetas = "width:" + promedioMetas + "%; background-color:rgb(255, 0, 0);";
                if (promedioMetas > 48.5 & promedioMetas < 80)
                    barraMetas = "width:" + promedioMetas + "%; background-color:rgb(255, 255, 0);";
                if (promedioMetas >= 80 & promedioMetas < 100)
                    barraMetas = "width:" + promedioMetas + "%; background-color:rgb(146, 208, 80);";
               if (promedioMetas >= 100 & promedioMetas < 120)
                  barraMetas = "width:100%; background-color:rgb(146, 208, 80);";
               if (promedioMetas >= 120 & promedioMetas < 146)
                    barraMetas = "width:100%; background-color:rgb(0, 176, 80);";
                if (promedioMetas >= 146)
                    barraMetas = "width:100%; background-color:rgb(0, 102, 0);";
                if (promedioMetas > 150)
                    barraMetas = "width:100%; background-color:rgb(0, 102, 0);";
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(GetType(), "Grafica", "fjsEnfoqueResultados('" + EDRDestacado + "','" + EDRModerado + "','" + EDROportunidadMejora + "');", true);
                cs.RegisterStartupScript(GetType(), "GraficaMIR", "fjsCalidadMIR('" + MIRDestacado + "','" + MIRAdecuado + "','" + MIRModerado + "','" + MIROportunidadMejora + "');", true);
                
                if ((MIRDestacado == 0) & (MIRAdecuado == 0) & (MIRModerado == 0) & (MIROportunidadMejora == 0))
                    graficaMIR.Visible = false;
                if ((EDRDestacado == 0) & (EDRModerado == 0) & (EDROportunidadMejora == 0))
                    graficaEDR.Visible = false;
                if (version < 2)
                   barraCumplimientoMetas.Visible = false;
                if((graficaMIR.Visible == false) & (graficaEDR.Visible == false) & (barraCumplimientoMetas.Visible == false))
                   tituloDivValoraciones.Visible = false;
         }
         catch (Exception ex)
         {

         }
      }

      protected void odsProgramaAB_Selected(object sender, ObjectDataSourceStatusEventArgs e)

      {
         try
         {
            List<SIMEPS.Modelo.Programa> source = (List<SIMEPS.Modelo.Programa>)e.ReturnValue;
                /**************INICIA CONTADOR INDICADORES**********************/
                IndicadoresDal data = new IndicadoresDal();
                                
                foreach (Modelo.Programa progra in source)
                {
                    List<Contador> contador = data.ConsultarContadorIndicadores(progra.CICLO, progra.RAMO_DEP,unidad , "0");
                    foreach (Modelo.Contador conta in contador)
                    {
                        numeroProgramas = conta.PROGRAMAS;
                        numeroIndicadores = conta.INDICADORES;
                        numeroAprobados = conta.PROGRAMAS_APROBADOS;
                        TPermanencia = conta.PROMEDIO_PERMANENCIA;
                        pMetas = conta.PROMEDIO_METAS;
                        version = conta.VERSION;
         }
                    List<Valoracion> valoracion = data.ConsultarValoraciones(progra.RAMO_DEP, progra.MODALIDAD, progra.CLAVE.ToString());
                    foreach (Modelo.Valoracion valorMir in valoracion.Where(v => v.CICLO == progra.CICLO))
                    {
                        if (valorMir.CALIF_EDR == 0)
                            EDROportunidadMejora = EDROportunidadMejora + 1;
                        if (valorMir.CALIF_EDR == 0.5)
                            EDRModerado = EDRModerado + 1;
                        if (valorMir.CALIF_EDR == 1)
                            EDRDestacado = EDRDestacado + 1;

                        if (valorMir.CALIF_TOT == 1)
                            MIROportunidadMejora = MIROportunidadMejora + 1;
                        if (valorMir.CALIF_TOT == 2)
                            MIRModerado = MIRModerado + 1;
                        if (valorMir.CALIF_TOT == 3)
                            MIRAdecuado = MIRAdecuado + 1;
                        if (valorMir.CALIF_TOT == 4)
                            MIRDestacado = MIRDestacado + 1;
      }
                    ///****************TERMINA CONTADOR INDICADORES********************/
                }
            }
            catch (Exception ex)
            {

            }

        }

      protected void grvProgramaC_DataBound(object sender, EventArgs e)
      {
         LblNumeroProgramas.Text = numeroProgramas.ToString();
            LblNumeroAprobados.Text = numeroAprobados.ToString();
            if (numeroAprobados == 0)
                LblNumeroAprobados.Text = "N/A*";
            LblNumeroIndicadores.Text = numeroIndicadores.ToString();
            promedioTasaPermanencia = Math.Truncate(TPermanencia * 100) / 100;
            LblPromedioTasaPermamencia.Text = promedioTasaPermanencia.ToString();
            if (promedioTasaPermanencia == 0)
                LblPromedioTasaPermamencia.Text = "N/A*";
               promedioMetas = Math.Truncate(pMetas * 100) / 100;
               if (promedioMetas <= 48.5)
                  barraMetas = "width:" + promedioMetas + "%; background-color:rgb(255, 0, 0);";
               if (promedioMetas > 48.5 & promedioMetas < 80)
                  barraMetas = "width:" + promedioMetas + "%; background-color:rgb(255, 255, 0);";
               if (promedioMetas >= 80 & promedioMetas < 100)
                  barraMetas = "width:" + promedioMetas + "%; background-color:rgb(146, 208, 80);";
               if (promedioMetas >= 100 & promedioMetas < 120)
                  barraMetas = "width:100%; background-color:rgb(146, 208, 80);";
               if (promedioMetas >= 120 & promedioMetas < 146)
                  barraMetas = "width:100%; background-color:rgb(0, 176, 80);";
               if (promedioMetas >= 146)
                  barraMetas = "width:100%; background-color:rgb(0, 102, 0);";
               if (promedioMetas > 150)
                  barraMetas = "width:100%; background-color:rgb(0, 102, 0);";
         ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(GetType(), "Grafica", "fjsEnfoqueResultados('" + EDRDestacado + "','" + EDRModerado + "','" + EDROportunidadMejora + "');", true);
            cs.RegisterStartupScript(GetType(), "GraficaMIR", "fjsCalidadMIR('" + MIRDestacado + "','" + MIRAdecuado + "','" + MIRModerado + "','" + MIROportunidadMejora + "',);", true);

            if ((MIRDestacado == 0) & (MIRAdecuado == 0) & (MIRModerado == 0) & (MIROportunidadMejora == 0))
                graficaMIR.Visible = false;
            if ((EDRDestacado == 0) & (EDRModerado == 0) & (EDROportunidadMejora == 0))
                graficaEDR.Visible = false;
            if (version < 2)
               barraCumplimientoMetas.Visible = false;
            if ((graficaMIR.Visible == false) & (graficaEDR.Visible == false) & (barraCumplimientoMetas.Visible == false))
               tituloDivValoraciones.Visible = false;
      }

      protected void odsProgramasC_Selected(object sender, ObjectDataSourceStatusEventArgs e)
      {
         try
         {
            List<SIMEPS.Modelo.Programa> source = (List<SIMEPS.Modelo.Programa>)e.ReturnValue;
                /**************INICIA CONTADOR INDICADORES**********************/
                IndicadoresDal data = new IndicadoresDal();

                foreach (Modelo.Programa progra in source)
                {
                    List<Contador> contador = data.ConsultarContadorIndicadores(progra.CICLO,progra.RAMO_DEP,unidad, "C");
                    foreach (Modelo.Contador conta in contador)
                    {
                        numeroProgramas = conta.PROGRAMAS;
                        numeroIndicadores = conta.INDICADORES;
                        numeroAprobados = conta.PROGRAMAS_APROBADOS;
                        TPermanencia = conta.PROMEDIO_PERMANENCIA;
                        pMetas = conta.PROMEDIO_METAS;
                        version = conta.VERSION;
         }
                    List<Valoracion> valoracion = data.ConsultarValoraciones(progra.RAMO_DEP, progra.MODALIDAD, progra.CLAVE.ToString());
                    foreach (Modelo.Valoracion valorMir in valoracion.Where(v => v.CICLO == progra.CICLO))
                    {
                        if (valorMir.CALIF_EDR == 0)
                            EDROportunidadMejora = EDROportunidadMejora + 1;
                        if (valorMir.CALIF_EDR == 0.5)
                            EDRModerado = EDRModerado + 1;
                        if (valorMir.CALIF_EDR == 1)
                            EDRDestacado = EDRDestacado + 1;

                        if (valorMir.CALIF_TOT == 1)
                            MIROportunidadMejora = MIROportunidadMejora + 1;
                        if (valorMir.CALIF_TOT == 2)
                            MIRModerado = MIRModerado + 1;
                        if (valorMir.CALIF_TOT == 3)
                            MIRAdecuado = MIRAdecuado + 1;
                        if (valorMir.CALIF_TOT == 4)
                            MIRDestacado = MIRDestacado + 1;
                    }
                }
                ///****************TERMINA CONTADOR INDICADORES********************/
            }
         catch (Exception ex) { }
      }


      public void ValidaDescarga(string sUrl, string sCiclo, string sRamo, string sModalidad, string sClave, string sT)
      {
         string UrlZip = ConfigurationManager.AppSettings["urlZip"];
         string UrlDescarga = UrlZip + sCiclo + "/Ficha Evolución MIR " + sCiclo + sRamo + sModalidad + sClave + ".zip";
         string UrlProgramas = sUrl + "?ciclo=" + sCiclo + "&ramo=" + sRamo + "&t=" + sT;
         var request = WebRequest.Create(new Uri(UrlDescarga));

         request.Method = "HEAD";
         ClientScriptManager cs = Page.ClientScript;
         String csname2 = "ButtonClickScript";
         Type cstype = this.GetType();
         try
         {
            WebResponse response = request.GetResponse();
            cs.RegisterClientScriptBlock(cstype, csname2, "<script>window.open('" + UrlDescarga + "'); window.location.href = '" + UrlProgramas + "';  </Script>", false);
            Console.WriteLine("{0} - {1}", response.ContentLength, response.ContentType);
         }
         catch (WebException ex)
         {
            var resp = (HttpWebResponse)ex.Response;

            if (resp.StatusCode == HttpStatusCode.NotFound)
            {
               cs.RegisterClientScriptBlock(cstype, csname2, "<script> alert('Ficha no disponible para el programa'); window.location.href = '" + UrlProgramas + "';</Script>", false);
            }

         }

      }

   }
}