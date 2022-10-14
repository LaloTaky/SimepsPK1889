using SIMEPS.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SIMEPS
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            SiteMap.SiteMapResolve +=
             new SiteMapResolveEventHandler(this.ExpandForumPaths);

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Logger log = new Logger();
            log.LogMessageToFile(Server.GetLastError().Message);
            log.LogMessageToFile(Server.GetLastError().InnerException.StackTrace);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private SiteMapNode ExpandForumPaths(Object sender, SiteMapResolveEventArgs e)
        {
            SiteMapNode currentNode = new SiteMapNode(SiteMap.Provider, "SIteMapSIMEPS", "#", "Pagina");
            if (SiteMap.CurrentNode != null)
            {
                currentNode = SiteMap.CurrentNode.Clone(true);
                SiteMapNode tempNode = currentNode;
                string pageName = HttpContext.Current.Request.CurrentExecutionFilePath.ToString();
                string sPaginaParentNode = "";



                string postID = HttpContext.Current.Request["pIdMatriz"] == null ? "" : HttpContext.Current.Request["pIdMatriz"];
                if (pageName.Contains("ProgramaFin.aspx"))
                {
                    if (HttpContext.Current.Request["pCiclo"] != null)
                    {
                        tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"];
                        tempNode.ParentNode.Title = tempNode.ParentNode.Title + " > " + HttpContext.Current.Request["pCiclo"] + " > " + HttpContext.Current.Request["pSiglas"];
                        tempNode.ParentNode.Description = tempNode.ParentNode.Description + " > " + HttpContext.Current.Request["pCiclo"] + " > " + HttpContext.Current.Request["pSiglas"];
                    }
                }



                if (pageName.Contains("MosaicoFin.aspx"))
                {
                    if (HttpContext.Current.Request["pCiclo"] != null)
                    {
                        tempNode.Url = tempNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"];
                        tempNode.Title = tempNode.Title + " > " + HttpContext.Current.Request["pCiclo"];
                        tempNode.Description = tempNode.Description + " > " + HttpContext.Current.Request["pCiclo"];
                    }
                }
                if (pageName.Contains("FichasMonitoreo.aspx"))
                {
                    if (HttpContext.Current.Request["pCiclo"] != null)
                    {
                        tempNode.Url = tempNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"];
                        tempNode.Title = tempNode.Title + " " + HttpContext.Current.Request["pCiclo"];
                        tempNode.Description = tempNode.Description + " " + HttpContext.Current.Request["pCiclo"];
                    }
                }
                if (pageName.Contains("Programas.aspx"))
                {
                    tempNode.ParentNode.Title = "Módulo SIPS " + HttpContext.Current.Request["ciclo"];
                    tempNode.ParentNode.Description = "Módulo SIPS " + HttpContext.Current.Request["ciclo"];
                    if (HttpContext.Current.Request["t"] != null)
                    {
                        tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["ciclo"] + "&t=" + HttpContext.Current.Request["t"];
                        tempNode.Title = HttpContext.Current.Request["siglas"];

                    }
                    else
                    {
                        tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["ciclo"];
                        tempNode.Title = HttpContext.Current.Request["siglas"];

                    }
                }
                if (pageName.Contains("Programa.aspx"))
                {
                    tempNode.ParentNode.ParentNode.Title = "Módulo SIPS " + HttpContext.Current.Request["pCiclo"];
                    tempNode.ParentNode.ParentNode.Description = "Módulo SIPS " + HttpContext.Current.Request["pCiclo"];
                    tempNode.ParentNode.Title = HttpContext.Current.Request["siglas"];
                    tempNode.ParentNode.Description = HttpContext.Current.Request["siglas"];
                    tempNode.Title = HttpContext.Current.Request["nombre"];
                    if (HttpContext.Current.Request["t"] != null)
                    {
                        tempNode.ParentNode.ParentNode.Url = tempNode.ParentNode.ParentNode.Url + "?ciclo=" + HttpContext.Current.Request["pCiclo"] + "&t=" + HttpContext.Current.Request["t"];
                        tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?ciclo=" + HttpContext.Current.Request["pCiclo"] + "&ramo=" + HttpContext.Current.Request["pRamo"] + "&siglas=" + HttpContext.Current.Request["siglas"] + "&t=" + HttpContext.Current.Request["t"];
                    }
                    else
                    {
                        tempNode.ParentNode.ParentNode.Url = tempNode.ParentNode.ParentNode.Url + "?ciclo=" + HttpContext.Current.Request["pCiclo"];
                        tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?ciclo=" + HttpContext.Current.Request["pCiclo"] + "&ramo=" + HttpContext.Current.Request["pRamo"] + "&siglas=" + HttpContext.Current.Request["siglas"];
                    }
                }
                if (pageName.Contains("MIR.aspx"))
                {
                    if (HttpContext.Current.Request["t"] != null)
                        tempNode.ParentNode.ParentNode.Url = tempNode.ParentNode.ParentNode.Url + "?t=" + HttpContext.Current.Request["t"];

                    tempNode.ParentNode.Title = HttpContext.Current.Request["pCiclo"] + " > " + HttpContext.Current.Request["siglas"];
                    tempNode.ParentNode.Description = HttpContext.Current.Request["pCiclo"] + " > " + HttpContext.Current.Request["siglas"];
                    if (HttpContext.Current.Request["pRamo"] != null)
                    {
                        tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?ciclo=" + HttpContext.Current.Request["pCiclo"] + "&ramo=" + HttpContext.Current.Request["pRamo"] + "&siglas=" + HttpContext.Current.Request["siglas"] + "&t=" + HttpContext.Current.Request["t"];
                    }
                    else
                    {
                        tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?ciclo=" + HttpContext.Current.Request["pCiclo"] + "&ramo=" + HttpContext.Current.Request["ramo"] + "&siglas=" + HttpContext.Current.Request["siglas"] + "&t=" + HttpContext.Current.Request["t"];
                    }

                    tempNode.Url = tempNode.Url + "?ciclo=" + HttpContext.Current.Request["ciclo"] + "&ramo=" + HttpContext.Current.Request["ramo"];
                    tempNode.Title = "MIR-" + HttpContext.Current.Request["nombre"];
                    tempNode.Description = HttpContext.Current.Request["ciclo"] + " > " + HttpContext.Current.Request["siglas"];

                }


                else if (pageName.Contains("DetalleIndicador.aspx"))
                {

                    sPaginaParentNode = HttpContext.Current.Request.CurrentExecutionFilePath.ToString();
                    //1.- Fin
                    //2.- Proposito
                    //3.- Componente
                    //4.- Actividad     
                    String sNivel = "";
                    if (Convert.ToInt32(HttpContext.Current.Request["pNivel"].ToString()) == 1) sNivel = "Fin";
                    if (Convert.ToInt32(HttpContext.Current.Request["pNivel"].ToString()) == 2) sNivel = "Proposito";
                    if (Convert.ToInt32(HttpContext.Current.Request["pNivel"].ToString()) == 3) sNivel = "Componente";
                    if (Convert.ToInt32(HttpContext.Current.Request["pNivel"].ToString()) == 4) sNivel = "Actividad";

                    if (HttpContext.Current.Request["vBusqueda"] != null)
                    {
                        tempNode.ParentNode.ParentNode.Title = HttpContext.Current.Request["vBusqueda"];
                        tempNode.ParentNode.ParentNode.Url = "~/BuscaIndicador.aspx";
                        tempNode.ParentNode.Title = "";
                        tempNode.ParentNode.Title = "";

                        tempNode.Title = "Indicador de: " + sNivel;

                    }
                    else
                    {
                        tempNode.ParentNode.ParentNode.ParentNode.Title = "Módulo SIPS " + HttpContext.Current.Request["pCiclo"];
                        tempNode.ParentNode.ParentNode.Title = HttpContext.Current.Request["siglas"];
                        tempNode.ParentNode.Title = HttpContext.Current.Request["nMatriz"];
                        tempNode.Title = "Indicador de " + sNivel;
                        tempNode.ParentNode.ParentNode.ParentNode.Description = "Módulo SIPS " + HttpContext.Current.Request["pCiclo"];
                        tempNode.ParentNode.ParentNode.Description = HttpContext.Current.Request["siglas"];
                        tempNode.ParentNode.Description = HttpContext.Current.Request["nMatriz"];

                        //Utiliza parametro ramo desde Mosaico SIPS y parametro pRamo desde Busqueda
                        if (HttpContext.Current.Request["pRamo"] != null)
                        {
                            tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?pIdMatriz=" + HttpContext.Current.Request["pIdMatriz"] + "&pCiclo=" + HttpContext.Current.Request["pCiclo"] + "&pRamo=" + HttpContext.Current.Request["pRamo"] + "&siglas=" + HttpContext.Current.Request["siglas"] + "&nombre=" + HttpContext.Current.Request["nMatriz"] + "&t=" + HttpContext.Current.Request["t"];
                        }
                        else
                        {
                            tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?pIdMatriz=" + HttpContext.Current.Request["pIdMatriz"] + "&pCiclo=" + HttpContext.Current.Request["pCiclo"] + "&pRamo=" + HttpContext.Current.Request["ramo"] + "&siglas=" + HttpContext.Current.Request["siglas"] + "&nombre=" + HttpContext.Current.Request["nMatriz"] + "&t=" + HttpContext.Current.Request["t"];
                        }

                        if (HttpContext.Current.Request["t"] != null)
                        {
                            tempNode.ParentNode.ParentNode.ParentNode.Url = tempNode.ParentNode.ParentNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"] + "&t=" + HttpContext.Current.Request["t"];
                            tempNode.ParentNode.ParentNode.Url = tempNode.ParentNode.ParentNode.Url + "?ciclo=" + HttpContext.Current.Request["pCiclo"] + "&ramo=" + HttpContext.Current.Request["pRamo"] + "&siglas=" + HttpContext.Current.Request["siglas"] + "&t=" + HttpContext.Current.Request["t"];
                            if (HttpContext.Current.Request["t"].ToString().ToUpper().Equals("C") || HttpContext.Current.Request["t"].ToString().ToUpper().Equals("B"))
                            {
                                tempNode.ParentNode.Url = "~/MIR.aspx" + "?pIdMatriz=" + HttpContext.Current.Request["pIdMatriz"] + "&pCiclo=" + HttpContext.Current.Request["pCiclo"] + "&pRamo=" + HttpContext.Current.Request["pRamo"] + "&siglas=" + HttpContext.Current.Request["siglas"] + "&nombre=" + HttpContext.Current.Request["nMatriz"] + "&t=" + HttpContext.Current.Request["t"];
                            }
                            else
                            {
                                tempNode.ParentNode.Url = "~/Programa.aspx" + "?pIdMatriz=" + HttpContext.Current.Request["pIdMatriz"] + "&pCiclo=" + HttpContext.Current.Request["pCiclo"] + "&pRamo=" + HttpContext.Current.Request["ramo"] + "&siglas=" + HttpContext.Current.Request["siglas"] + "&nombre=" + HttpContext.Current.Request["nMatriz"] + "&t=" + HttpContext.Current.Request["t"];
                            }

                        }
                        else
                        {
                            tempNode.ParentNode.ParentNode.ParentNode.Url = tempNode.ParentNode.ParentNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"];
                            tempNode.ParentNode.ParentNode.Url = tempNode.ParentNode.ParentNode.Url + "?ciclo=" + HttpContext.Current.Request["pCiclo"] + "&ramo=" + HttpContext.Current.Request["pRamo"] + "&siglas=" + HttpContext.Current.Request["siglas"];
                            tempNode.ParentNode.Url = tempNode.ParentNode.Url;
                        }

                    }
                }
                //Módulo SIPOL pantalla ListaObjetivos.aspx
                else if (pageName.Contains("ListaObjetivos.aspx"))
                {
                    tempNode.ParentNode.Description = "Módulo SIPOL";
                    tempNode.Title = HttpContext.Current.Request["nSectorial"];


                }

                //Módulo SIPOL pantalla ListaObjetivos.aspx
                else if (pageName.Contains("ListaObjetivos4T.aspx"))
                {
                    tempNode.ParentNode.Description = "Módulo SIPOL 19-24";
                    tempNode.Title = HttpContext.Current.Request["nSectorial"];


                }


                else if (pageName.Contains("IndicadorSectorial.aspx"))
                {
                    tempNode.ParentNode.ParentNode.Url = tempNode.ParentNode.ParentNode.Url;
                    tempNode.ParentNode.ParentNode.Description = "SIMEPS";
                    tempNode.ParentNode.Title = "Módulo SIPOL";
                    tempNode.ParentNode.Description = "Módulo SIPOL";
                    // tempNode.ParentNode.Description = HttpContext.Current.Request["nSectorial"];
                    tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?id=" + HttpContext.Current.Request["id"] + "&nSectorial=" + HttpContext.Current.Request["nSectorial"];



                }

                else if (pageName.Contains("IndicadorSectorial19-24.aspx"))
                {
                    tempNode.ParentNode.ParentNode.Url = tempNode.ParentNode.ParentNode.Url;
                    tempNode.ParentNode.ParentNode.Description = "SIMEPS";
                    tempNode.ParentNode.Title = "Módulo SIPOL 19-24";
                    tempNode.ParentNode.Description = "Módulo SIPOL 19-24";
                    // tempNode.ParentNode.Description = HttpContext.Current.Request["nSectorial"];
                    tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?id=" + HttpContext.Current.Request["id"] + "&nSectorial=" + HttpContext.Current.Request["nSectorial"];



                }

                else if (pageName.Contains("DetalleIndicadorFin.aspx"))
                {
                    if (HttpContext.Current.Request["pCiclo"] != null && HttpContext.Current.Request["pSiglas"] != null)
                    {
                        tempNode.ParentNode.ParentNode.Url = tempNode.ParentNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"];
                        tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"] + "&pRamo=" + HttpContext.Current.Request["pRamo"] + "&pSiglas=" + HttpContext.Current.Request["pSiglas"] + (HttpContext.Current.Request["pUnidad"] != null ? "&pUnidad=" + HttpContext.Current.Request["pUnidad"] : "");
                        tempNode.ParentNode.ParentNode.Title = tempNode.ParentNode.ParentNode.Title + " > " + HttpContext.Current.Request["pCiclo"] + " > " + HttpContext.Current.Request["pSiglas"] + (HttpContext.Current.Request["pUnidad"] != null ? "&pUnidad=" + HttpContext.Current.Request["pUnidad"] : "");
                        tempNode.ParentNode.ParentNode.Description = HttpContext.Current.Request["pCiclo"] + " > " + HttpContext.Current.Request["pSiglas"];
                        tempNode.ParentNode.Description = HttpContext.Current.Request["pCiclo"] + " > " + HttpContext.Current.Request["pSiglas"] + (HttpContext.Current.Request["pUnidad"] != null ? "&pUnidad=" + HttpContext.Current.Request["pUnidad"] : "");

                    }

                }
                if (pageName.Contains("BuscaIndicador.aspx"))
                {
                    tempNode.Title = "Buscador Temático";
                }
                if (pageName.Contains("BuscaIndicador.aspx"))
                {
                    tempNode.Title = "Buscador Temático";
                }

                // devnet01 PK1074
                if (pageName.Contains("HomeRamo33.aspx"))
                {
                    if (HttpContext.Current.Request["pCIclo"] != null)
                    {
                        tempNode.Title = "Módulo MIR33" + " " + HttpContext.Current.Request["pCIclo"];
                        tempNode.Description = "Módulo MIR33" + " " + HttpContext.Current.Request["pCIclo"];
                    }
                    else
                    {
                        tempNode.Title = "Módulo MIR33" + " " + DateTime.Now.Year.ToString();
                        tempNode.Description = "Módulo MIR33" + " " + DateTime.Now.Year.ToString();
                    }

                    if (HttpContext.Current.Request["pCiclo"] == null)
                    {
                        tempNode.Url = tempNode.Url + "?pCiclo=" + DateTime.Now.Year.ToString();
                    }
                    else
                    {
                        tempNode.Url = tempNode.Url + "&pCiclo=" + HttpContext.Current.Request["pCiclo"];
                    }
                }

                if (pageName.Contains("MosaicoRamo33.aspx"))
                {
                    tempNode.ParentNode.Title = "Módulo MIR33 " + HttpContext.Current.Request["pCiclo"];
                    tempNode.ParentNode.Description = "Módulo MIR33 " + HttpContext.Current.Request["pCiclo"];
                    tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"];

                    if (HttpContext.Current.Request["sFondo"] != null)
                    {
                        tempNode.Title = HttpContext.Current.Request["sFondo"];
                        tempNode.Description = HttpContext.Current.Request["sFondo"];
                    }

                    tempNode.Url = tempNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"] + "&sFondo=" + HttpContext.Current.Request["sFondo"];
                }

                if (pageName.Contains("IndicadoresR33.aspx"))
                {


                    string sMatris = HttpContext.Current.Request["sMatris"];

                    if (sMatris == "1")
                    {
                        tempNode.ParentNode.ParentNode.Title = "Módulo MIR33 " + HttpContext.Current.Request["pCiclo"];
                        tempNode.ParentNode.ParentNode.Description = "Módulo MIR33 " + HttpContext.Current.Request["pCiclo"];
                        tempNode.ParentNode.ParentNode.Url = tempNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"];

                    tempNode.ParentNode.Title = HttpContext.Current.Request["sFondo"];
                    tempNode.ParentNode.Description = HttpContext.Current.Request["sFondo"];
                        tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"] + "&sFondo=" + HttpContext.Current.Request["sFondo"];

                        if (HttpContext.Current.Request["sComponente"] != null)
                        {
                            tempNode.Title = HttpContext.Current.Request["sComponente"];
                            tempNode.Description = HttpContext.Current.Request["sComponente"];
                        }

                        tempNode.Url = tempNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"];
                    }
                    else
                    {
                        tempNode.ParentNode.ParentNode.Title = "Módulo MIR33 " + HttpContext.Current.Request["pCiclo"];
                        tempNode.ParentNode.ParentNode.Description = "Módulo MIR33 " + HttpContext.Current.Request["pCiclo"];
                        tempNode.ParentNode.ParentNode.Url = tempNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"];

                        tempNode.ParentNode.Title = HttpContext.Current.Request["sFondo"];
                        tempNode.ParentNode.Description = HttpContext.Current.Request["sFondo"];
                    tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"] + "&sFondo=" + HttpContext.Current.Request["sFondo"];

                    if (HttpContext.Current.Request["sComponente"] != null)
                    {
                        tempNode.Title = HttpContext.Current.Request["sComponente"];
                            tempNode.Description = HttpContext.Current.Request["sComponente"];
                        }

                        tempNode.Url = tempNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"];
                    }

                }

                if (pageName.Contains("IndicadorR33.aspx"))
                {
                    tempNode.ParentNode.ParentNode.ParentNode.Title = "Módulo MIR33 " + HttpContext.Current.Request["pCiclo"];
                    tempNode.ParentNode.ParentNode.ParentNode.Description = "Módulo MIR33 " + HttpContext.Current.Request["pCiclo"];
                    tempNode.ParentNode.ParentNode.ParentNode.Url = tempNode.ParentNode.ParentNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"];

                    tempNode.ParentNode.ParentNode.Title = HttpContext.Current.Request["sFondo"];
                    tempNode.ParentNode.ParentNode.Description = HttpContext.Current.Request["sFondo"];
                    tempNode.ParentNode.ParentNode.Url = tempNode.ParentNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"]
                        + "&iMatriz=" + HttpContext.Current.Request["iMatriz"] + "&sFondo=" + HttpContext.Current.Request["sFondo"];

                    tempNode.ParentNode.Title = HttpContext.Current.Request["sComponente"];
                    tempNode.ParentNode.Description = HttpContext.Current.Request["sComponente"];

                    tempNode.ParentNode.Url = tempNode.ParentNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"] + "&sFondo=" + HttpContext.Current.Request["sFondo"]
                        + "&iMatriz=" + HttpContext.Current.Request["iMatriz"]
                        + "&sComponente=" + HttpContext.Current.Request["sComponente"];

                    tempNode.Title = "Ficha Técnica del Indicador de " + HttpContext.Current.Request["sProposito"];
                    tempNode.Description = "Ficha Técnica del Indicador de " + HttpContext.Current.Request["sProposito"];


                    tempNode.Url = tempNode.Url + "?pCiclo=" + HttpContext.Current.Request["pCiclo"];
                }

                // devnet01 PK1074

                if (pageName.Contains("MosaicoSips.aspx"))
                {
                    if (HttpContext.Current.Request["pCIclo"] != null)
                    {
                        tempNode.Title = "Módulo SIPS" + " " + HttpContext.Current.Request["pCIclo"];
                        tempNode.Description = "Módulo SIPS" + " " + HttpContext.Current.Request["pCIclo"];
                    }
                    else
                    {
                        tempNode.Title = "Módulo SIPS";
                        tempNode.Description = "Módulo SIPS";
                    }

                    if (HttpContext.Current.Request["t"] != null)
                    {
                        tempNode.Url = tempNode.Url + "?ciclo=" + HttpContext.Current.Request["pCiclo"] + "&t=" + HttpContext.Current.Request["t"];

                    }
                    else
                    {

                        tempNode.Url = tempNode.Url + "&pCiclo=" + HttpContext.Current.Request["pCiclo"];

                    }
                }
            }

            return currentNode;
        }


    }
}