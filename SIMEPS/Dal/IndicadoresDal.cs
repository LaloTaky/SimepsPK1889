using clientSIID;
using MoreLinq;
using Newtonsoft.Json;
using SIMEPS.Comun;
using SIMEPS.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Hosting;
using System.Xml;


namespace SIMEPS.Dal
{
    public class IndicadoresDal
    {
        private indicadoresApi clientApi;
        private Logger log;

        public IndicadoresDal()
        {
            string baseAddress = ConfigurationManager.AppSettings["urlSIID"];
            string Usr = ConfigurationManager.AppSettings["User"];
            string Psw = ConfigurationManager.AppSettings["Pass"];
            string PathToken = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["pathToken"]);

            clientApi = new indicadoresApi(baseAddress, Usr, Psw, PathToken);
            log = new Logger();
        }

        private string getBadMessage(dynamic obj)
        {
            return string.Format("ClientSIID: \t {0} \t {1}", obj.StatusCode, obj.ResponseBody);
        }

        public List<FrecuanciaMedicion> ConsultarFrecuenciaMedicion(string pIdMatriz, string pRamo)
        {
            List<FrecuanciaMedicion> lFrecuenciaM = new List<FrecuanciaMedicion>();
            List<FrecuanciaMedicion> ListaDuplicados = new List<FrecuanciaMedicion>();
            ListaDuplicados.Add(new FrecuanciaMedicion
            {
                ID = "0",
                FRECUENCIA_MEDICION = "-Seleccione-"
            });
            try
            {
                dynamic frecuanciaM = clientApi.getFrecuenciaMEdicion(pIdMatriz, pRamo);

                if (frecuanciaM.StatusCode == "OK")
                {
                    foreach (var f in frecuanciaM.ResponseBody)
                    {
                        FrecuanciaMedicion cFrecuencia = new FrecuanciaMedicion();
                        cFrecuencia.ID = f.Id;
                        cFrecuencia.FRECUENCIA_MEDICION = f.Frecuencia;

                        ListaDuplicados.Add(cFrecuencia);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(frecuanciaM));
                }
                lFrecuenciaM = ListaDuplicados.DistinctBy(l => l.FRECUENCIA_MEDICION).ToList();
                return lFrecuenciaM;
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new List<FrecuanciaMedicion>();
            }
        }
        public List<UnidadMedida> ConsultarUnidadMEdida(string pIdMatriz, string pRamo)
        {
            List<UnidadMedida> lUnidadM = new List<UnidadMedida>();
            List<UnidadMedida> ListaDuplicados = new List<UnidadMedida>();
            ListaDuplicados.Add(new UnidadMedida
            {
                ID = "0",
                UNIDAD_VALUE = "-Seleccione-"
            });
            try
            {
                dynamic unidadM = clientApi.getUnidadMedida(pIdMatriz, pRamo);

                if (unidadM.StatusCode == "OK")
                {
                    foreach (var u in unidadM.ResponseBody)
                    {
                        UnidadMedida cUnidadM = new UnidadMedida();
                        cUnidadM.ID = u.Id;
                        cUnidadM.UNIDAD_VALUE = u.UnidadValue;

                        ListaDuplicados.Add(cUnidadM);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(unidadM));
                }

                lUnidadM = ListaDuplicados.DistinctBy(l => l.UNIDAD_VALUE).ToList();
                return lUnidadM;
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new List<UnidadMedida>();
            }
        }
        /// <summary>
        /// Obtiene los Objetivos del Plan Nacional de los indicadores
        /// </summary>
        /// <returns>Lista de objetos ObjetivoNacional</returns>
        public List<ObjetivoNacional> ConsultarObjetivoNacional(string pIdMatriz, string pRamo)
        {
            List<ObjetivoNacional> lObjetivoNacional = new List<ObjetivoNacional>();
            lObjetivoNacional.Add(new ObjetivoNacional
            {
                ID_OBJETIVO_M = -1,
                DESC_OBJETIVO = "-Seleccione-",
            });
            try
            {
                dynamic objetivosN = clientApi.getObjetivoNacional(pIdMatriz, pRamo);

                if (objetivosN.StatusCode == "OK")
                {
                    foreach (var o in objetivosN.ResponseBody)
                    {
                        ObjetivoNacional cObjetivo = new ObjetivoNacional();
                        cObjetivo.ID_OBJETIVO_M = o.IdObjetivoM;
                        cObjetivo.DESC_OBJETIVO = o.DescObjetivo;

                        lObjetivoNacional.Add(cObjetivo);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(objetivosN));
                }
                return lObjetivoNacional;
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new List<ObjetivoNacional>();
            }
        }

        /// <summary>
        /// Obtiene los ciclos de los indicadores
        /// </summary>
        /// <returns>Lista de objetos Ciclo</returns>
        public List<Ciclo> ConsultarCiclos(String sPantalla)
        {
            List<Ciclo> lciclos = new List<Ciclo>();
            //lciclos.Add(new Modelo.Ciclo
            //{
            //   CICLO_ID = 0,
            //   CICLO_VALUE = "-Seleccione-"
            //});
            try
            {
                dynamic val = clientApi.getCiclos(sPantalla);

                if (val.StatusCode == "OK")
                {
                    try
                    {
                        foreach (var ele in val.ResponseBody)
                        {
                            Ciclo cCiclo = new Ciclo();
                            cCiclo.CICLO_ID = ele.CicloId;
                            cCiclo.CICLO_VALUE = ele.CicloValue;
                            lciclos.Add(cCiclo);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Trace.TraceError(ex.Message);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(val));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new List<Ciclo>();
            }

            return lciclos;
        }

        /// <summary>
        /// Obtiene los ciclos de los indicadores para el Ramo 33
        /// </summary>
        /// <returns>Lista de objetos Ciclo</returns>
        public List<Ciclo> ConsultarCiclosRamo33()
        {
            List<Ciclo> lciclos = new List<Ciclo>();
            //lciclos.Add(new Modelo.Ciclo
            //{
            //   CICLO_ID = 0,
            //   CICLO_VALUE = "-Seleccione-"
            //});
            try
            {
                dynamic val = clientApi.getCiclosRamo33();

                if (val.StatusCode == "OK")
                {
                    try
                    {
                        foreach (var ele in val.ResponseBody)
                        {
                            Ciclo cCiclo = new Ciclo();
                            cCiclo.CICLO_ID = ele.CicloId;
                            cCiclo.CICLO_VALUE = ele.CicloValue;
                            lciclos.Add(cCiclo);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Trace.TraceError(ex.Message);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(val));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new List<Ciclo>();
            }

            return lciclos;
        }

        /// <summary>
        /// Obtiene la lista de ramos y unidades para construir el mosaico
        /// </summary>
        /// <param name="pCiclo">Ciclo que va a consultar</param>
        /// <param name="pCamino">Camino A, B, C</param>
        /// <returns>Lista de objetos Mosaico</returns>
        public List<SIMEPS.Modelo.Mosaico> ConsultarMosaicos(Decimal pCiclo, string pCamino, Boolean pMosaicoFin)
        {
            List<SIMEPS.Modelo.Mosaico> lmosaicos = new List<SIMEPS.Modelo.Mosaico>();
            try
            {
                dynamic mosaicos = clientApi.getMosaico(pCiclo.ToString(), pCamino);

                if (mosaicos.StatusCode == "OK")
                {
                    foreach (var m in mosaicos.ResponseBody)
                    {
                        if (m.Ramo != "33")
                        {
                            SIMEPS.Modelo.Mosaico mMosaico = new SIMEPS.Modelo.Mosaico();
                            mMosaico.NOM_ARCHIVO = m.NombreArchivo;
                            mMosaico.LVL = m.Nivel;
                            mMosaico.CICLO = m.Ciclo;
                            mMosaico.RAMO = m.Ramo;
                            mMosaico.UNIDAD = m.Unidad;
                            mMosaico.DESCRIPCION = m.Descripcion;
                            mMosaico.DEPENDENCIA = m.Dependencia;

                            if (pMosaicoFin)
                            {
                                if (mMosaico.LVL == 2) mMosaico.LIGA = "ProgramaFin.aspx?pCiclo=" + mMosaico.CICLO + "&pRamo=" + mMosaico.RAMO + "&pUnidad=" + mMosaico.UNIDAD + "&pSiglas=" + m.siglas + "&pDependencia=" + m.Dependencia;
                                else mMosaico.LIGA = "ProgramaFin.aspx?pCiclo=" + mMosaico.CICLO + "&pRamo=" + mMosaico.RAMO + "&pSiglas=" + m.siglas + "&pDependencia=" + m.Dependencia;
                            }
                            else
                            {
                                if (mMosaico.LVL == 2) mMosaico.LIGA = "programas.aspx?ciclo=" + mMosaico.CICLO + "&ramo=" + mMosaico.RAMO + "&unidad=" + mMosaico.UNIDAD + "&siglas=" + m.siglas;
                                else mMosaico.LIGA = "Programas.aspx?ciclo=" + mMosaico.CICLO + "&ramo=" + mMosaico.RAMO + "&siglas=" + m.siglas;
                            }
                            lmosaicos.Add(mMosaico);
                        }
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(mosaicos));
                }

                return lmosaicos;
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new List<SIMEPS.Modelo.Mosaico>();
            }
        }
        public List<SIMEPS.Modelo.Mosaico> ConsultarRamosIndicadores(Decimal pCiclo, string pCamino)
        {
            List<SIMEPS.Modelo.Mosaico> listaDuplicados = new List<SIMEPS.Modelo.Mosaico>();
            List<SIMEPS.Modelo.Mosaico> lRamos = new List<SIMEPS.Modelo.Mosaico>();
            listaDuplicados.Add(new Modelo.Mosaico
            {
                DESCRIPCION = "-Seleccione-",
                RAMO = "0"
            });
            try
            {
                dynamic mosaicos = clientApi.getMosaico(pCiclo.ToString(), pCamino);

                if (mosaicos.StatusCode == "OK")
                {
                    foreach (var m in mosaicos.ResponseBody)
                    {
                        SIMEPS.Modelo.Mosaico mMosaico = new SIMEPS.Modelo.Mosaico();

                        mMosaico.NOM_ARCHIVO = m.NombreArchivo;
                        mMosaico.LVL = m.Nivel;
                        mMosaico.CICLO = m.Ciclo;
                        mMosaico.RAMO = m.Ramo;
                        mMosaico.UNIDAD = m.Unidad;
                        mMosaico.DESCRIPCION = m.Descripcion;
                        if (mMosaico.LVL == 2) mMosaico.LIGA = "programas.aspx?ciclo=" + mMosaico.CICLO + "&ramo=" + mMosaico.RAMO + "&unidad=" + mMosaico.UNIDAD;
                        else mMosaico.LIGA = "Programas.aspx?ciclo=" + mMosaico.CICLO + "&ramo=" + mMosaico.RAMO;

                        listaDuplicados.Add(mMosaico);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(mosaicos));
                }
                lRamos = listaDuplicados.DistinctBy(l => l.DESCRIPCION).ToList();
                return lRamos;
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new List<SIMEPS.Modelo.Mosaico>();
            }
        }

        /// <summary>
        /// Obtiene la informacion de las metas en años anteriores
        /// </summary>
        /// <param name="dIndicador">Id del indicador</param>
        /// <returns>Lista de Objetos de tipo Historico</returns>
        public List<HistoricoIndicador> ConsultarHistorico(decimal dIndicador)
        {
            List<HistoricoIndicador> lHistoricos = new List<HistoricoIndicador>();
            string meta_planeda = "";
            string meta_alcanzada = "";

            try
            {
                dynamic historicos = clientApi.getHistorico(dIndicador.ToString());

                if (historicos.StatusCode == "OK")
                {
                    foreach (var historico in historicos.ResponseBody)
                    {
                        HistoricoIndicador hIndicador = new HistoricoIndicador();
                        hIndicador.NO = historico.Numero;
                        hIndicador.ANIO = historico.Anio;
                        hIndicador.META_PLANEADA = historico.MetaPlaneada;
                        hIndicador.META_ALCANZADA = historico.MetaAlcanzada;
                        hIndicador.RELATIVA = historico.Relativa;
                        hIndicador.ABSOLUTA = historico.Absoluta;
                        hIndicador.META_ABS_PLANEADA = historico.MetaAbsPlaneada == "" ? 0 : historico.MetaAbsPlaneada;
                        hIndicador.META_ABS_ALCANZADA = historico.MetaAbsAlcanzada == "" ? 0 : historico.MetaAbsAlcanzada;
                        hIndicador.META_REL_PLANEADA = historico.MetaRelPlaneada == "" ? 0 : historico.MetaRelPlaneada;
                        hIndicador.META_REL_ALCANZADA = historico.MetaRelAlcanzada == "" ? 0 : historico.MetaRelAlcanzada;
                        hIndicador.INDICADORCOMPLEMENTARIO = historico.IndComplementario;
                        meta_planeda = (!String.IsNullOrEmpty(hIndicador.META_PLANEADA) && hIndicador.META_PLANEADA != "-") ? "\",\"MetaPlaneada\":\"" + hIndicador.META_PLANEADA : "";
                        meta_alcanzada = (!String.IsNullOrEmpty(hIndicador.META_ALCANZADA) && hIndicador.META_ALCANZADA != "-") ? "\",\"MetaAlcanzada\":\"" + hIndicador.META_ALCANZADA : "";
                        hIndicador.GRAFICA = "{\"id\":\"" + dIndicador.ToString() + "\",\"ciclo\":\"" + hIndicador.ANIO + "" + meta_planeda + "" + meta_alcanzada + "\",\"color1\":\"#7E015B\",\"color2\":\"#3C1559\"}";
                        lHistoricos.Add(hIndicador);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(historicos));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new List<HistoricoIndicador>();
            }

            return lHistoricos;
        }

        /// <summary>
        /// Obtiene la lista de variables de indicador
        /// </summary>
        /// <param name="dIndicador">Id de l indicador</param>
        /// <returns>Lista de objetos de tipo Variable</returns>
        public List<VariableIndicador> ConsultarVariables(decimal dIndicador)
        {
            List<VariableIndicador> lVariables = new List<VariableIndicador>();

            try
            {
                dynamic variables = clientApi.getVariables(dIndicador.ToString());

                if (variables.StatusCode == "OK")
                {
                    foreach (var variable in variables.ResponseBody)
                    {
                        VariableIndicador vVariable = new VariableIndicador();
                        vVariable.NOMBRE = variable.Nombre;
                        vVariable.DESC_VARIABLE = variable.Nombre;
                        vVariable.DESC_MEDIO_VERIFICACION = variable.DescMedioVerificacion;
                        lVariables.Add(vVariable);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(variables));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return lVariables;
        }


        /// <summary>
        /// Obtiene la informacion de los programas
        /// </summary>
        /// <param name="anio">Año a consultar</param>
        /// <param name="ramo">Ramo</param>
        /// <param name="unidad">Unidad</param>
        /// <param name="palabraClave">Texto a buscar</param>
        /// <param name="dMatriz">Id de la Matriz</param>
        /// <param name="dIndicador">Id del indicador</param>
        /// <param name="sNivel">Nivel del indicador:1.-Fin, 2.-Propósito, 3.-Componete, 4.-Actividad</param>
        /// <param name="universoPaemir">Indica si se debe aplicar el filtro de universo paemir</param>
        /// <returns></returns>
        public List<SIMEPS.Modelo.Programa> ConsultarPrograma(decimal anio, string ramo, string unidad, string palabraClave, decimal dMatriz, decimal dIndicador, String sNivel, String sPantalla, string universoPaemir, decimal idIndicadorSectorial)
        {
            Utils u = new Utils();
            List<SIMEPS.Modelo.Programa> lProgramas = new List<SIMEPS.Modelo.Programa>();
            try
            {
                dynamic programas = clientApi.getProgramas(anio.ToString(), ramo, unidad, palabraClave, dMatriz.ToString(), universoPaemir, idIndicadorSectorial.ToString());

                if (programas.StatusCode == "OK")
                {
                    foreach (var p in programas.ResponseBody)
                    {
                        SIMEPS.Modelo.Programa pPrograma = new SIMEPS.Modelo.Programa();
                        pPrograma.PP = p.Pp;
                        pPrograma.NOMBRE = p.Nombre;
                        pPrograma.ID_MATRIZ = p.IdMatriz;
                        pPrograma.CICLO = p.Ciclo;
                        pPrograma.SIGLAS_UNIDAD = p.SiglasUnidad;
                        pPrograma.SIGLAS_DEP = p.SiglasDep;
                        pPrograma.NOMBRE_MATRIZ = p.NombreMatriz;
                        pPrograma.MODALIDAD = p.Modalidad;
                        pPrograma.CLAVE = p.Clave;
                        pPrograma.DESC_UNIDAD = p.DescUnidad;
                        pPrograma.DESC_META = p.DescMeta;
                        pPrograma.DESC_APROBACION_DICTAMEN = p.DescAprobacionDictamen;
                        pPrograma.DESC_PROGRAMA_SEC_INST = p.DescProgramaSecInst;
                        pPrograma.ID_NIVEL_APROBACION = p.IdNivelAprobacion;
                        pPrograma.OBJETIVO_NACIONAL = p.ObjetivoNacional;
                        pPrograma.OBJETIVO_ESTRATEGICO = p.ObjetivoEstrategico;
                        pPrograma.DEPENDENCIA = p.Dependencia;
                        pPrograma.RAMO_DEP = p.RamoDep;
                        pPrograma.OBJ_EST_DEP_ENT = p.ObjEstDepEnt;
                        pPrograma.CICLO_APROBACION = p.CicloAprobacion;
                        pPrograma.PROCESO_APROBACION = p.ProcesoAprobacion;
                        pPrograma.TASAPERMANENCIA = p.TasaPermanencia;
                        pPrograma.LIGA = u.ObtenerNavegacion(sPantalla) + "?pIdMatriz=" + p.IdMatriz + "&pCiclo=" + p.Ciclo + "&pRamo=" + p.RamoDep + "&siglas=" + p.SiglasUnidad + "&nombre=" + p.Nombre;
                        string Clave = p.Clave;
                        string Modalidad = p.Modalidad;
                        if (dMatriz != -1 && dMatriz != 0)
                        {
                            pPrograma.OBJETIVOS = ConsultarObjetivos(dMatriz, dIndicador, sNivel);
                            pPrograma.PRESUPUESTOS = ConsultarPresupuesto(Convert.ToInt32(ramo), Modalidad, Clave);
                            pPrograma.VALORACIONES = ConsultarValoraciones(Convert.ToInt32(ramo), Modalidad, Clave);
                        }
                        if (sPantalla != null && sPantalla.ToUpper() == "C")
                        {
                            if (p.Modalidad == "S" || p.Modalidad == "U")
                                lProgramas.Add(pPrograma);
                        }
                        else if (sPantalla != null && sPantalla.ToUpper() == "C")
                        {
                            if (pPrograma.DESC_APROBACION_DICTAMEN != null)
                                lProgramas.Add(pPrograma);
                        }
                        else
                            lProgramas.Add(pPrograma);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(programas));
                }

                return lProgramas.ToList();

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new List<SIMEPS.Modelo.Programa>();
            }
        }
        /// <summary>
        /// Obtiene la informacion de los historicos de los programas (Grafica inicio)
        /// </summary>
        /// <param name="dMatriz">Id de la Matriz</param>
        /// <returns></returns>
        public List<SIMEPS.Modelo.Programa> ConsultarHistoricoPrograma(decimal dMatriz, String sPantalla)
        {
            Utils u = new Utils();
            List<SIMEPS.Modelo.Programa> lProgramas = new List<SIMEPS.Modelo.Programa>();
            try
            {
                dynamic programas = clientApi.getHistoricoProgramas(dMatriz.ToString());

                if (programas.StatusCode == "OK")
                {
                    foreach (var p in programas.ResponseBody)
                    {
                        SIMEPS.Modelo.Programa pPrograma = new SIMEPS.Modelo.Programa();
                        pPrograma.ID_MATRIZ = p.IdMatriz;
                        pPrograma.CICLO = p.Ciclo;
                        pPrograma.RAMO_DEP = p.RamoDep;
                        pPrograma.LIGA = u.ObtenerNavegacion(sPantalla) + "?pIdMatriz=" + p.IdMatriz + "&pCiclo=" + p.Ciclo + "&pRamo=" + p.RamoDep + "&t=" + sPantalla;
                        lProgramas.Add(pPrograma);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(programas));
                }

                return lProgramas.ToList();

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new List<SIMEPS.Modelo.Programa>();
            }
        }
        public List<SIMEPS.Modelo.Programa> ConsultarProgramasIndicadores(string anio, string ramo)
        {
            List<SIMEPS.Modelo.Programa> lProgramas = new List<SIMEPS.Modelo.Programa>();
            lProgramas.Add(new Modelo.Programa
            {
                ID_MATRIZ = 0,
                NOMBRE = "-Seleccione-"
            });
            try
            {
                dynamic programas = clientApi.getCatalogoProgramas(anio, ramo);

                if (programas.StatusCode == "OK")
                {
                    foreach (var p in programas.ResponseBody)
                    {
                        SIMEPS.Modelo.Programa pPrograma = new SIMEPS.Modelo.Programa();
                        pPrograma.NOMBRE = p.Nombre;
                        pPrograma.ID_MATRIZ = p.IdMatriz;
                        lProgramas.Add(pPrograma);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(programas));
                }

                return lProgramas.ToList();

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new List<SIMEPS.Modelo.Programa>();
            }
        }

        private List<Objetivo> getObjetivos(decimal idMatriz, String sNivel)
        {
            List<Objetivo> lObjetivos = new List<Objetivo>();

            try
            {
                dynamic objs = clientApi.getObjetivos(idMatriz.ToString(), sNivel);

                if (objs.StatusCode == "OK")
                {
                    foreach (var o in objs.ResponseBody)
                    {
                        Objetivo oObjetivo = new Objetivo();
                        oObjetivo.ID = o.IdNivel;
                        oObjetivo.DESC_NIVEL = o.DescObjetivo;
                        oObjetivo.ID_NIVEL = o.IdObjetivo;
                        oObjetivo.ID_PARENT = o.IdParent != "" ? o.IdParent : 0;
                        oObjetivo.NIVEL = Convert.ToInt16(sNivel);

                        lObjetivos.Add(oObjetivo);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(objs));
                }

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return lObjetivos;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idMatriz"></param>
        /// <param name="iNivel"></param>
        /// <returns></returns>
        public List<Supuesto> ConsultarSupuesto(decimal idMatriz, int iNivel)
        {
            decimal? dMatriz = (decimal?)idMatriz;
            List<Supuesto> lSupuestos = new List<Supuesto>();
            try
            {
                dynamic supuestos = clientApi.getSupuestos(dMatriz.ToString(), iNivel.ToString());

                if (supuestos.StatusCode == "OK")
                {
                    foreach (var sSupuesto in supuestos.ResponseBody)
                    {
                        Supuesto supuesto = new Supuesto();
                        supuesto.DESC_SUPUESTO = sSupuesto.DescSupuesto;
                        lSupuestos.Add(supuesto);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(supuestos));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return lSupuestos;
        }

        /// <summary>
        /// obtiene lista de programafin
        /// </summary>
        /// <param name="idMatriz"></param>
        /// <param name="iNivel"></param>
        /// <returns></returns>
        public List<Modelo.ProgramaFin> getProgramaFinal(int pCliclo, int pRamo, string pUnidad)
        {
            List<Modelo.ProgramaFin> lstProgFin = new List<Modelo.ProgramaFin>();
            try
            {
                dynamic pfinal = clientApi.getprogramaFin(pCliclo, pRamo, pUnidad);
                if (pfinal.StatusCode == "OK")
                {
                    foreach (var p in pfinal.ResponseBody)
                    {
                        Modelo.ProgramaFin progfinal = new Modelo.ProgramaFin();
                        progfinal.ID_MATRIZ = p.IdMatriz;
                        progfinal.NOMBRE = p.Nombre;
                        progfinal.ID_INDICADOR = p.IdIndicador;
                        progfinal.NOMBRE_INDICADOR = p.NombreIndicador;
                        progfinal.DEPENDENCIA = p.Dependencia;
                        progfinal.DESC_NIVEL = p.Desc_Nivel;
                        progfinal.NIVEL = p.Nivel;
                        progfinal.UNIDAD = p.Unidad;
                        lstProgFin.Add(progfinal);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(pfinal));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return lstProgFin;
        }

        /// <summary>
        /// Obtiene los programas y sus indicadores
        /// </summary>
        /// <param name="iCiclo"> ciclo de filtro(2016 o 2015)</param>
        /// <returns></returns>
        public List<Modelo.ProgramaFin> ConsultarProgramasFin(int pCiclo, int pRamo, string pUnidad)
        {
            List<Modelo.ProgramaFin> lstProgramaFin = getProgramaFinal(pCiclo, pRamo, pUnidad);
            /*Hace un distinct de los programas*/
            List<Modelo.ProgramaFin> lstProgramas = lstProgramaFin.GroupBy(i => i.ID_MATRIZ).Select(p => p.First()).ToList();
            foreach (Modelo.ProgramaFin prgFin in lstProgramas)
            {
                var indicadores = lstProgramaFin.Where(i => i.ID_MATRIZ == prgFin.ID_MATRIZ).GroupBy(i => i.ID_INDICADOR).Select(p => p.First()).ToList();
                List<Modelo.Indicador> indi = new List<Indicador>();
                foreach (var indicador in indicadores)
                {
                    indi.Add(new Indicador { ID_INDICADOR = indicador.ID_INDICADOR, NOMBRE_IND = indicador.NOMBRE_INDICADOR, LIGA = "DetalleIndicadorFin.aspx?pIdIndicador=" + indicador.ID_INDICADOR + "&pNivel=" + prgFin.NIVEL + "&pIdMatriz=" + prgFin.ID_MATRIZ });
                }
                prgFin.INDICADORES = indi;
            }
            return lstProgramas;
        }



        /// <summary>
        /// Obtiene los objetivos del programa
        /// </summary>
        /// <param name="idMatriz">Id de Matriz</param>
        /// <param name="dIndicador">Id del Indicador</param>
        /// <param name="sNivel">Nivel del indicador:1.-Fin, 2.-Propósito, 3.-Componete, 4.-Actividad</param>
        /// <returns></returns>
        public List<Objetivo> ConsultarObjetivos(decimal idMatriz, decimal dIndicador, String sNivel)
        {
            List<Objetivo> lObjetivos = new List<Objetivo>();
            Dictionary<int, int> componentes = new Dictionary<int, int>();
            int contComponentes = 0;
            int contActividades = 0;
            int parentAnterior = 0;
            int parentNuevo = 0;

            try
            {
                for (int i = 1; i <= 4; i++)
                {
                    if (!sNivel.Equals("") && !sNivel.Equals("0"))
                        i = Convert.ToInt32(sNivel);

                    var objetivos = getObjetivos(idMatriz, i.ToString()).OrderBy(x => x.ID_NIVEL);

                    if (i == 4)
                        objetivos = objetivos.OrderBy(o => o.ID_PARENT);
                    else
                        objetivos = objetivos.OrderBy(o => o.ID_NIVEL);

                    foreach (Objetivo oObjetivo in objetivos)
                    {
                        int parent;
                        switch (oObjetivo.NIVEL)
                        {
                            case 1:
                                oObjetivo.NIVEL_TEXTO = "Fin";
                                break;
                            case 2:
                                oObjetivo.NIVEL_TEXTO = "Propósito";
                                break;
                            case 3:
                                contComponentes++;
                                oObjetivo.NIVEL_TEXTO = "Componente";
                                oObjetivo.NUMERACION = contComponentes;
                                componentes.Add(oObjetivo.ID_NIVEL, contComponentes);
                                break;
                            case 4:
                                parentNuevo = oObjetivo.ID_PARENT;
                                if (parentNuevo == parentAnterior) contActividades++;
                                else contActividades = 1;
                                if (componentes.ContainsKey(oObjetivo.ID_PARENT))
                                {
                                    parent = componentes[oObjetivo.ID_PARENT];
                                    oObjetivo.NUMERACION = parent + Decimal.Divide(contActividades, 10);
                                }
                                oObjetivo.NIVEL_TEXTO = "Actividad";
                                parentAnterior = parentNuevo;
                                break;
                        }

                        if (dIndicador != -1)
                            oObjetivo.INDICADORES = ConsultarIndicador(idMatriz, i, oObjetivo.ID_NIVEL, dIndicador);
                        oObjetivo.SUPUESTOS = ConsultarSupuesto(idMatriz, oObjetivo.NIVEL);

                        lObjetivos.Add(oObjetivo);
                    }

                    if (!sNivel.Equals("") && !sNivel.Equals("0")) break;
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return lObjetivos.OrderBy(o => o.NIVEL).ThenBy(n => n.NUMERACION).ToList();
        }

        /// <summary>
        /// Obtiene la información de  detalle del indicador
        /// </summary>
        /// <param name="idMatriz">Id de Matriz</param>
        /// <param name="nivel">Nivel del indicador:1.-Fin, 2.-Propósito, 3.-Componete, 4.-Actividad</param>
        /// <param name="idNivel">Id del nivel</param>
        /// <param name="dIndicador">Id de indicador</param>
        /// <returns></returns>
        public List<Indicador> ConsultarIndicador(decimal idMatriz, int nivel, decimal idNivel, decimal dIndicador)
        {
            List<Indicador> lIndicadores = new List<Indicador>();

            try
            {
                dynamic listaIndicadoresDetalle = clientApi.getIndicador(dIndicador.ToString(), nivel.ToString(), idNivel.ToString(), idMatriz.ToString());

                if (listaIndicadoresDetalle.StatusCode == "OK")
                {
                    foreach (var ind in listaIndicadoresDetalle.ResponseBody)
                    {
                        Indicador iIndicador = new Indicador();
                        iIndicador.ID = ind.Id;
                        iIndicador.NOMBRE_IND = ind.NombreIndicador;
                        iIndicador.ID_NIVEL = ind.IdNivel;
                        iIndicador.ID_INDICADOR = ind.IdIndicador;
                        iIndicador.NOMBRE_MATRIZ = ind.NombreMatriz;
                        iIndicador.LIGA = "DetalleIndicador.aspx?pIdIndicador=" + ind.IdIndicador + "&pNivel=" + nivel + "&nMatriz=" + ind.NombreMatriz;
                        iIndicador.DESC_NIVEL = ind.DescNivel;
                        iIndicador.DEFINICION_IND = ind.DefIndicador;
                        iIndicador.METODO_CALCULO_IND = ind.MetCalculoInd;
                        iIndicador.FRECUENCIA_MEDICION = ind.FrecuenciaMedicion;
                        iIndicador.UNIDAD_MEDIDA = ind.UnidadMedida;
                        iIndicador.TIPO_RELATIVO = ind.TipoRelativo;
                        iIndicador.SENTIDO_INDICADOR = ind.SentidoIndicador;
                        iIndicador.DESC_DIMENSION = ind.DescDimension;
                        iIndicador.CICLO_LINEA_BASE = ind.CicloLineaBase;
                        iIndicador.META_ABS_PLANEADA = ind.MetaAbsPlaneada;
                        iIndicador.META_ABS_ALCANZADA = ind.MetaAbsAlcanzada;
                        iIndicador.META_REL_PLANEADA = ind.MetaRelPlaneada;
                        iIndicador.META_REL_ALCANZADA = ind.MetaRelAlcanzada;
                        iIndicador.LINEA_BASE = ind.LineaBase;
                        iIndicador.Claridad = ind.Claridad;
                        iIndicador.Relevancia = ind.Relevancia;
                        iIndicador.Adecuacion = ind.Adecuacion;
                        iIndicador.Monitoreabilidad = ind.Monitoreabilidad;
                        iIndicador.VARIABLES = ConsultarVariables((decimal)ind.IdIndicador);
                        iIndicador.HISTORICOS = ConsultarHistorico((decimal)ind.IdIndicador);

                        iIndicador.CALIFICACION_PROMEDIO = CalculaCalificacionPromedio((string)ind.Claridad, (string)ind.Relevancia, (string)ind.Adecuacion, (string)ind.Monitoreabilidad);
                        foreach (var historico in iIndicador.HISTORICOS)
                        {
                            if (historico.INDICADORCOMPLEMENTARIO)
                            {
                                iIndicador.TieneIndicadorComple = true;
                                break;
                            }
                        }
                        lIndicadores.Add(iIndicador);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(listaIndicadoresDetalle));
                }
            }
            catch (Exception error)
            {

                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return lIndicadores;
        }
        /// <summary>
        /// Obtiene el promedio de las calificaciones
        /// </summary>
        /// <param name="claridad">Calificación de claridad</param>
        /// <param name="relevancia">Calificación de relevancia</param>
        /// <param name="adecuacion">Calificación de adecuación</param>
        /// <param name="monitoreabilidad">Calificación de monitoreabilidad</param>
        /// <returns></returns>
        private string CalculaCalificacionPromedio(string claridad, string relevancia, string adecuacion, string monitoreabilidad)
        {
            string clase = "divLeyenda";
            if (claridad == null && relevancia == null && adecuacion == null && monitoreabilidad == null)
            {
                clase = "divLeyenda";
            }
            else
            {
                int c = Convert.ToInt16(claridad), r = Convert.ToInt16(relevancia), a = Convert.ToInt16(adecuacion), m = Convert.ToInt16(monitoreabilidad);
                double avg = ((r * 4) + c + a + m);
                //decimal avg = (total / 4) * 100;

                if ((avg >= 0) && (avg <= 2))
                    clase = "cajaRoja";
                else if (avg == 3)
                    clase = "cajaAmarilla";
                else if ((avg >= 4) && (avg <= 6))
                    clase = "cajaVerdeClaro";
                else if (avg == 7)
                    clase = "cajaVerde";
                else
                    clase = "divLeyenda";
            }
            return clase;
        }

        /// <summary>
        /// Realiza una busqueda de indicadores que coincidan con el texto a buscar
        /// </summary>
        /// <param name="textoBusqueda">Texto a buscar</param>
        /// <returns></returns>
        public List<Busqueda> BuscarIndicadores(String textoBusqueda, int derecho, int ciclo, int ramo, string idMatriz, int objetivo, int nivelMIR, string unidadM, string frecuencia, string sectorial)
        {
            List<Busqueda> lBusqueda = new List<Busqueda>();

            try
            {
                dynamic Busquedaindicadores = clientApi.getBuscarIndicador(textoBusqueda, derecho.ToString(), ciclo.ToString(), ramo.ToString(), idMatriz, objetivo.ToString(), nivelMIR.ToString(), unidadM, frecuencia.ToString(), sectorial);
                if (Busquedaindicadores.StatusCode == "OK")
                {
                    foreach (var busqueda in Busquedaindicadores.ResponseBody)
                    {
                        Busqueda resultado = new Busqueda();

                        resultado.NOMBRE = busqueda.NombreInd;
                        resultado.DEFINICION_IND = busqueda.Definicion;
                        resultado.CICLO_ID = busqueda.Ciclo;
                        resultado.CICLO_VALUE = busqueda.Ciclo;
                        resultado.ID_INDICADOR = busqueda.IdIndicador;
                        resultado.NIVEL = busqueda.Nivel;
                        resultado.NOMBRE_NIVEL = busqueda.NombreNivel;
                        resultado.RAMO = busqueda.Ramo;
                        resultado.NOMBRE_RAMO = busqueda.NombreRamo;
                        resultado.LIGA = "DetalleIndicador.aspx?pIdIndicador=" + busqueda.IdIndicador + "&pNivel=" + busqueda.Nivel + "&pIdNivel=" + busqueda.IdNivel + "&pIdMatriz=" + busqueda.IdMatriz + "&pCiclo=" + busqueda.Ciclo + "&pRamo=" + busqueda.Ramo;
                        resultado.ID_MATRIZ = busqueda.IdMatriz;
                        resultado.NOMBRE_PROGRAMA = busqueda.NombrePrograma;
                        resultado.ID_OBJETIVO = busqueda.IdObjetivo;
                        resultado.NOMBRE_OBJETIVO = busqueda.NombreObjetivo;
                        resultado.ID_PROG_SECTORIAL = busqueda.IdSectorial;
                        resultado.NOMBRE_PRO_SECTORIAL = busqueda.NombreSectorial;
                        resultado.UNIDAD_MEDIDA = busqueda.UnidadMedida;
                        resultado.FRECUENCIA_MEDICION = busqueda.FrecuenciaMedicion;
                        resultado.ID_DERECHO = busqueda.IdDerecho;
                        resultado.NOMBRE_DERECHO = busqueda.NombreDerecho;
                        lBusqueda.Add(resultado);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(Busquedaindicadores));
                }
            }
            catch (Exception error)
            {

                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return lBusqueda;
        }

        public List<Parametro> ConsultarParametro(String sNombreParametro)
        {
            List<Parametro> lParametro = new List<Parametro>();
            try
            {

                dynamic parametros = clientApi.getParametro(sNombreParametro);
                if (parametros.StatusCode == "OK")
                {
                    foreach (var parametro in parametros.ResponseBody)
                    {
                        Parametro pParametro = new Parametro();
                        pParametro.VALOR = parametro.Valor;
                        lParametro.Add(pParametro);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(parametros));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return lParametro;
        }

        /// <summary>
        /// Obtiene el presupuesto por año ejercido y original del programa
        /// </summary>
        /// <param name="ramo">Ramo</param>
        /// <param name="modalidad">Modalidad</param>
        /// <param name="Clave">Clave</param>
        /// <returns></returns>
        public List<Presupuesto> ConsultarPresupuesto(int ramo, string modalidad, string clave)
        {
            List<Presupuesto> lPresupuesto = new List<Presupuesto>();
            try
            {
                dynamic programaPresupuesto = clientApi.getPresupuesto(ramo, modalidad, clave);
                if (programaPresupuesto.StatusCode == "OK")
                {
                    foreach (var presupuesto in programaPresupuesto.ResponseBody)
                    {
                        Presupuesto pPresupuesto = new Presupuesto();
                        pPresupuesto.CICLO = presupuesto.Ciclo;
                        pPresupuesto.IMPORTE_EJERCIDO_MDP = presupuesto.ImporteEjercidoMdp;
                        pPresupuesto.IMPORTE_ORIGINAL_MDP = presupuesto.ImporteOriginalMdp;

                        lPresupuesto.Add(pPresupuesto);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(programaPresupuesto));
                }
            }
            catch (Exception error)
            {

                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new List<Presupuesto>();
            }
            return lPresupuesto;
        }

        /// <summary>
        /// Obtiene el catálogo de derechos sociales desde la BD de inventarios
        /// </summary>
        /// <returns>Listado de derechos sociales</returns>
        public List<Derecho> ConsultarDerechos()
        {
            List<Derecho> lDerechos = new List<Derecho>();
            lDerechos.Add(new Derecho
            {
                DER_ID_I = -1,
                DER_DESCRIPCION_I = "-Seleccione-"
            });
            try
            {
                var derechos = clientApi.getDerechos();
                foreach (var derecho in derechos.ResponseBody)
                {
                    if (derecho.DerIdI != -1 && derecho.DerIdI != 0 && derecho.DerIdI != 10)
                    {
                        Derecho dDerecho = new Derecho();

                        dDerecho.DER_ID_I = derecho.DerIdI;
                        dDerecho.DER_DESCRIPCION_I = derecho.DerDescripcionI;

                        lDerechos.Add(dDerecho);
                    }
                }
            }

            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return lDerechos.OrderBy(d => d.DER_ID_I).ToList();
        }

        public List<MetasNac> ConsultaMetasNacionales()
        {
            List<MetasNac> LMetasNacionales = new List<MetasNac>();
            try
            {
                var resultado = clientApi.getMetasNacionales();

                // MetasNac MetasNacionales = new JavaScriptSerializer().Deserialize<MetasNac>(resultado);

                foreach (var meta in resultado.ResponseBody)
                {
                    if (meta.DerIdI != -1 && meta.DerIdI != 0 && meta.DerIdI != 10)
                    {
                        MetasNac dmeta = new MetasNac
                        {
                            ID_META = meta.ID_META,
                            DESC_META = meta.DESC_META,
                            PATH_IMAGEN_URL = meta.PATH_IMAGEN_URL,
                            PATH_IMAGEN_OVER_URL = meta.PATH_IMAGEN_OVER_URL
                        };
                        LMetasNacionales.Add(dmeta);
                    }
                }
            }

            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return LMetasNacionales;
        }

        public List<ObjetivosM> ConsultaObjetivosPND(int idMeta)
        {
            List<ObjetivosM> listalObjetivosM = new List<ObjetivosM>();
            try
            {
                var resultado = clientApi.getObjetivosPND(idMeta);

                foreach (var objetivoM in resultado.ResponseBody)
                {
                    if (objetivoM.DerIdI != -1 && objetivoM.DerIdI != 0 && objetivoM.DerIdI != 10)
                    {
                        ObjetivosM dobjetivoM = new ObjetivosM
                        {
                            ID_OBJETIVO_M = objetivoM.ID_OBJETIVO_M,
                            DESC_OBJETIVO = objetivoM.DESC_OBJETIVO,
                            ID_META = objetivoM.ID_META,
                        };
                        listalObjetivosM.Add(dobjetivoM);
                    }
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return listalObjetivosM;
        }

        public List<IndicadoresPND> ConsultaIndicadoresObjetivo(int idMeta)
        {
            List<IndicadoresPND> listaIndicadoresM = new List<IndicadoresPND>();
            try
            {
                var resultado = clientApi.getIndicadoresObjetivoPND(idMeta);
                foreach (var objetivoM in resultado.ResponseBody)
                {
                    if (objetivoM.DerIdI != -1 && objetivoM.DerIdI != 0 && objetivoM.DerIdI != 10)
                    {
                        IndicadoresPND dobjetivoM = new IndicadoresPND
                        {
                            ID_OBJETIVO_M = objetivoM.ID_OBJETIVO_M,
                            ID_INDICADOR_PND = objetivoM.ID_INDICADOR_PND,
                            ID_INDICADOR_ANT = objetivoM.ID_INDICADOR_ANT,
                            NOMBRE = objetivoM.NOMBRE,
                            DEFINICION = objetivoM.DEFINICION,
                            UNIDAD_MEDIDA = objetivoM.UNIDAD_MEDIDA,
                            VALOR_ALCANZADO = objetivoM.VALOR_ALCANZADO,
                            CALIF_CLARIDAD = objetivoM.CALIF_CLARIDAD,
                            CALIF_RELEVANCIA = objetivoM.CALIF_RELEVANCIA,
                            CALIF_MONITOREABILIDAD = objetivoM.CALIF_MONITOREABILIDAD,
                            CALIF_PERTINENCIA = objetivoM.CALIF_PERTINENCIA,
                            CICLO = objetivoM.CICLO,
                            CATEGORIA_CALIDAD = objetivoM.CATEGORIA_CALIDAD,
                            ENFOQUE_INDICADOR = objetivoM.ENFOQUE_INDICADOR,
                            COMENTARIO = objetivoM.COMENTARIO
                        };
                        listaIndicadoresM.Add(dobjetivoM);
                    }
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return listaIndicadoresM;
        }

        public List<IndicadoresTrans> ConsultaIndicadoresTransversales()
        {
            List<IndicadoresTrans> listalIndicadoresTransversales = new List<IndicadoresTrans>();
            try
            {
                var resultado = clientApi.getIndicadoresTrans();

                foreach (var indicadorTrans in resultado.ResponseBody)
                {
                    if (indicadorTrans.DerIdI != -1 && indicadorTrans.DerIdI != 0 && indicadorTrans.DerIdI != 10)
                    {
                        IndicadoresTrans dindicadorTrans = new IndicadoresTrans
                        {
                            ID_INDICADOR_ESTR_TRANS = indicadorTrans.ID_INDICADOR_ESTR_TRANS,

                            ID_INDICADOR_TRANS_ESTR_ANT = indicadorTrans.ID_INDICADOR_TRANS_ESTR_ANT,
                            CICLO = indicadorTrans.CICLO,
                            NOMBRE = indicadorTrans.NOMBRE,
                            DEFINICION = indicadorTrans.DEFINICION,
                            UNIDAD_MEDIDA = indicadorTrans.UNIDAD_MEDIDA,
                            CALIF_CLARIDAD = indicadorTrans.CALIF_CLARIDAD,
                            CALIF_RELEVANCIA = indicadorTrans.CALIF_RELEVANCIA,
                            CALIF_MONITOREABILIDAD = indicadorTrans.CALIF_MONITOREABILIDAD,
                            CALIF_PERTINENCIA = indicadorTrans.CALIF_PERTINENCIA,
                            VALOR_ALCANZADO = indicadorTrans.VALOR_ALCANZADO,
                            CATEGORIA_CALIDAD = indicadorTrans.CATEGORIA_CALIDAD,
                            ENFOQUE_INDICADOR = indicadorTrans.ENFOQUE_INDICADOR
                        };
                        listalIndicadoresTransversales.Add(dindicadorTrans);
                    }
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return listalIndicadoresTransversales;
        }


        /// <summary>
        /// Obtiene las valoraciones del programa
        /// </summary>
        /// <param name="clave">Clave</param>
        /// <param name="ramo">Ramo</param>
        /// <param name="modalidad">Modalidad</param>
        /// <returns></returns>
        public List<Valoracion> ConsultarValoraciones(int ramo, string modalidad, string clave)
        {
            List<Valoracion> lValoraciones = new List<Valoracion>();
            try
            {
                dynamic ValoracionPrograma = clientApi.getValoracion(ramo, modalidad, clave);
                if (ValoracionPrograma.StatusCode == "OK")
                {
                    foreach (var valoracion in ValoracionPrograma.ResponseBody)
                    {
                        Valoracion vValoracion = new Valoracion();
                        vValoracion.CICLO = valoracion.Ciclo;
                        vValoracion.CALIF_DIS = valoracion.CalifDis;
                        vValoracion.CALIF_IND = valoracion.CalifInd;
                        vValoracion.CALIF_TOT = valoracion.CalifTot;
                        vValoracion.CALIF_EDR = valoracion.CalifEdr;

                        lValoraciones.Add(vValoracion);
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(ValoracionPrograma));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new List<Valoracion>();
            }
            return lValoraciones;
        }


        /// <summary>
        /// Obtiene el conteo de los indicadores de Resultados, Servicio, Gestión asi como del total
        /// </summary>
        /// <returns></returns>
        public List<TotalIndicador> ConsultarTodosIndicadores()
        {
            List<TotalIndicador> lTotales = new List<TotalIndicador>();

            try
            {
                dynamic val = clientApi.getTotalIndicador();
                if (val.StatusCode == "OK")
                {

                    foreach (var ele in val.ResponseBody)
                    {
                        TotalIndicador iIndicador = new TotalIndicador();
                        iIndicador.CICLO = ele.Ciclo;
                        iIndicador.TotalIndicadores = ele.TotalIndicadores;
                        iIndicador.TotalClasificados = ele.TotalClasificados;

                        lTotales.Add(iIndicador);
                    }
                }
                else
                {

                    log.LogMessageToFile(getBadMessage(val));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return lTotales;
        }

        /// <summary>
        /// Consulta todos los programas sectoriales
        /// Se envía un true para quee obtenga los valores por sector y false para programas
        /// el parametro id se dejo con el mismo nombre para no afectar al sistema																					 
        /// </summary>
        /// <param name="idProgramaSectorial">Id de programa sectorial</param>
        /// <returns></returns>
        public List<ProgramaSectorial> ConsultarProgramaSectoriales(int idProgramaSectorial)
        {
            return ConsultarProgramaSectoriales(idProgramaSectorial, true);
        }

        public List<ProgramaSectorial> ConsultarProgramaSectoriales4T(int idProgramaSectorial)
        {
            return ConsultarProgramaSectoriales4T(idProgramaSectorial, true);
        }

        /// <summary>
        /// Consulta todos los programas sectoriales
        /// </summary>
        /// <param name="idProgramaSectorial">Id de programa sectorial</param>
        /// <returns></returns>
        public List<ProgramaSectorial> ConsultarProgramaSectoriales(int idProgramaSectorial, bool bProgramSec)
        {
            List<ProgramaSectorial> lProgramSectorial = new List<ProgramaSectorial>();
            //-2 Significa que viene de la pantalla de buscador temático especializado
            if (idProgramaSectorial == -2)
            {
                lProgramSectorial.Add(new Modelo.ProgramaSectorial
                {
                    ID_PROG_SECTORIAL = 0,
                    NOMBRE = "-Seleccione-"
                });

            }
            try
            {
                dynamic val;
                if (bProgramSec)
                {
                    val = clientApi.getProgramasSectoriales(idProgramaSectorial);
                }
                else
                {
                    val = clientApi.getProgSectorialExcel(idProgramaSectorial);
                }
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        ProgramaSectorial ProgramSectorial = new ProgramaSectorial();
                        ProgramSectorial.NOMBRE = ele.Nombre;
                        ProgramSectorial.URL_ICONO = ele.Url_icono;
                        ProgramSectorial.ID_PROG_SECTORIAL = ele.Id_prog_sectorial;
                        ProgramSectorial.ID_SECTOR = ele.sector;
                        ProgramSectorial.LIGA = "ListaObjetivos.aspx?id=" + ele.Id_prog_sectorial + "&nSectorial=" + ele.Nombre;
                        ProgramSectorial.EVALUACIONDESC = ele.EvaluacionDesc;
                        ProgramSectorial.EVALUACIONLLIGA = ele.EvaluacionlLiga;
                        ProgramSectorial.EVALUACIONIMAGEN = ele.EvaluacionImagen;
                        ProgramSectorial.NOMBRESECTOR = ele.NombreSector;

                        if (idProgramaSectorial != -1 && idProgramaSectorial == -2)
                        {
                            if (ProgramSectorial.ID_PROG_SECTORIAL != idProgramaSectorial)
                            {
                                lProgramSectorial.Add(ProgramSectorial);
                            }
                        }
                        else
                        {
                            if (ProgramSectorial.URL_ICONO != null)
                            {
                                lProgramSectorial.Add(ProgramSectorial);
                            }
                        }

                        ProgramSectorial.CONSECUTIVO = lProgramSectorial.Count;
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(val));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return lProgramSectorial;
        }

        /// <summary>
        /// Consulta todos los programas sectoriales
        /// </summary>
        /// <param name="idProgramaSectorial">Id de programa sectorial</param>
        /// <returns></returns>
        public List<ProgramaSectorial> ConsultarProgramaSectoriales4T(int idProgramaSectorial, bool bProgramSec)
        {
            List<ProgramaSectorial> lProgramSectorial = new List<ProgramaSectorial>();
            //-2 Significa que viene de la pantalla de buscador temático especializado
            if (idProgramaSectorial == -2)
            {
                lProgramSectorial.Add(new Modelo.ProgramaSectorial
                {
                    ID_PROG_SECTORIAL = 0,
                    NOMBRE = "-Seleccione-"
                });

            }
            try
            {
                dynamic val;
                if (bProgramSec)
                {
                    val = clientApi.getProgramasSectoriales4T(idProgramaSectorial);
                }
                else
                {
                    val = clientApi.getProgSectorialExcel4T(idProgramaSectorial);
                }
                if (val.StatusCode == "OK")
                {

                    foreach (var ele in val.ResponseBody)
                    {
                        ProgramaSectorial ProgramSectorial = new ProgramaSectorial();
                        ProgramSectorial.NOMBRE = ele.Nombre;
                        ProgramSectorial.URL_ICONO = ele.Url_icono;
                        ProgramSectorial.ID_PROG_SECTORIAL = ele.Id_prog_sectorial;
                        ProgramSectorial.ID_SECTOR = ele.sector;
                        ProgramSectorial.LIGA = "ListaObjetivos.aspx?id=" + ele.Id_prog_sectorial + "&nSectorial=" + ele.Nombre;
                        ProgramSectorial.EVALUACIONDESC = ele.EvaluacionDesc;
                        ProgramSectorial.EVALUACIONLLIGA = ele.EvaluacionlLiga;
                        ProgramSectorial.EVALUACIONIMAGEN = ele.EvaluacionImagen;
                        ProgramSectorial.NOMBRESECTOR = ele.NombreSector;
                        


                        if (idProgramaSectorial != -1 && idProgramaSectorial == -2)
                        {
                            if (ProgramSectorial.ID_PROG_SECTORIAL != idProgramaSectorial)
                            {
                                lProgramSectorial.Add(ProgramSectorial);
                            }
                        }
                        else
                        {
                            if (ProgramSectorial.URL_ICONO != null)
                            {
                                lProgramSectorial.Add(ProgramSectorial);
                            }
                        }

                        ProgramSectorial.CONSECUTIVO = lProgramSectorial.Count;
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(val));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return lProgramSectorial;
        }

        /// <summary>
        /// Consulta todos los programas sectoriales
        /// </summary>
        /// <param name="idSector">Id de sector</param>
        /// <returns></returns>
        public List<Sector> ConsultarSectores(int idSector)
        {
            List<Sector> lSectores = new List<Sector>();
            Sector sectorOtros = new Sector();
            try
            {
                dynamic val = clientApi.getSectores();
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Sector sector = new Sector();
                        sector.ID_SECTOR = ele.Id_sector;
                        sector.NOMBRE = ele.Nombre;
                        sector.ICONO = ele.Icono;

                        if (idSector != -1 && idSector != -2)
                        {
                            if (sector.ID_SECTOR == idSector)
                            {
                                lSectores.Add(sector);
                            }
                        }
                        else
                        {
                            if (sector.ICONO != null)
                            {
                                lSectores.Add(sector);
                            }
                        }

                        sector.CONSECUTIVO = lSectores.Count;
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(val));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            if (lSectores.Count > 1)
            {
                int iOtros = lSectores.FindIndex((x => x.NOMBRE.ToUpper() == "OTROS"));
                var OtrosItem = lSectores[iOtros];
                lSectores.RemoveAt(iOtros);
                lSectores.Add(OtrosItem);
            }

            return lSectores;
        }

        public List<Sector> ConsultarSectores4T(int idSector)
        {
            List<Sector> lSectores = new List<Sector>();
            Sector sectorOtros = new Sector();
            try
            {
                dynamic val = clientApi.getSectores4T();
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Sector sector = new Sector();
                        sector.ID_SECTOR = ele.Id_sector;
                        sector.NOMBRE = ele.Nombre;
                        sector.ICONO = ele.Icono;

                        if (idSector != -1 && idSector != -2)
                        {
                            if (sector.ID_SECTOR == idSector)
                            {
                                lSectores.Add(sector);
                            }
                        }
                        else
                        {
                            if (sector.ICONO != null)
                            {
                                lSectores.Add(sector);
                            }
                        }

                        sector.CONSECUTIVO = lSectores.Count;
                    }
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(val));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            if (lSectores.Count > 1)
            {
                int iOtros = lSectores.FindIndex((x => x.NOMBRE.ToUpper() == "OTROS"));
                var OtrosItem = lSectores[iOtros];
                lSectores.RemoveAt(iOtros);
                lSectores.Add(OtrosItem);
            }

            return lSectores;
        }

        /// <summary>
        /// Consulta todos los programas sectoriales
        /// </summary>
        /// <param name="idSector">Id de sector</param>
        /// <returns></returns>
        public List<EstadisticasBasicas> ConsultaEstadisticaBasica(int nSector)
        {
            List<EstadisticasBasicas> lstConteoEstadisticas = new List<EstadisticasBasicas>();

            try
            {

                dynamic val = clientApi.getContador(nSector);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        EstadisticasBasicas contador = new EstadisticasBasicas();

                        contador.TIPO = ele.Tipo;
                        contador.CONTEO = ele.Conteo;
                        lstConteoEstadisticas.Add(contador);
                    }


                }
                else
                {
                    log.LogMessageToFile(getBadMessage(val));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return lstConteoEstadisticas;
        }

        public List<EstadisticasBasicas> ConsultaEstadisticaBasica4T(int nSector)
        {
            List<EstadisticasBasicas> lstConteoEstadisticas4T = new List<EstadisticasBasicas>();

            try
            {

                dynamic val = clientApi.getContador4T(nSector);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        EstadisticasBasicas contador = new EstadisticasBasicas();

                        contador.TIPO = ele.Tipo;
                        contador.CONTEO = ele.Conteo;
                        lstConteoEstadisticas4T.Add(contador);

                       
                    }


                }
                else
                {
                    log.LogMessageToFile(getBadMessage(val));
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return lstConteoEstadisticas4T;
        }

        /// <summary>			 
        /// Consulta todos los objetivos sectoriales
        /// </summary>
        /// <param name="idProgramaSectorial">Id de programa al que pertenecen los objetivos a consultar</param>
        /// <returns>Lista de ObjetivoSectorial</returns>
        public List<Modelo.ObjetivoSectorial> ConsultarObjetivosSectoriales(int idProgramaSectorial)
        {
            List<Modelo.ObjetivoSectorial> Lista = new List<Modelo.ObjetivoSectorial>();

            try
            {
                dynamic val = clientApi.getIndicadorSectorial(idProgramaSectorial, 1);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Modelo.ObjetivoSectorial pObjetivoS = new Modelo.ObjetivoSectorial();
                        pObjetivoS.NUM_OBJETIVO = ele.NumObjetivo;
                        pObjetivoS.OBJETIVO = ele.Objetivo;
                        pObjetivoS.ID_PROGRAMA_SEC = ele.IdProgramaSec;



                        Lista.Add(pObjetivoS);
                    }
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return Lista;
        }

        public List<Modelo.ObjetivoSectorial> ConsultarObjetivosSectoriales4T(int idProgramaSectorial)
        {
            List<Modelo.ObjetivoSectorial> Lista = new List<Modelo.ObjetivoSectorial>();

            try
            {
                dynamic val = clientApi.getIndicadorSectorial4T(idProgramaSectorial, 1);
                if (val.StatusCode == "OK")
                {
                    

                    foreach (var ele in val.ResponseBody)
                    {
                        Modelo.ObjetivoSectorial pObjetivoS = new Modelo.ObjetivoSectorial();
                        pObjetivoS.NUM_OBJETIVO = ele.NumObjetivo;
                        pObjetivoS.OBJETIVO = ele.Objetivo;
                        pObjetivoS.ID_PROGRAMA_SEC = ele.IdProgramaSec;
                        pObjetivoS.TIPO_INDICADOR = ele.tipoindicador;

                        

                        Lista.Add(pObjetivoS);
                    }
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return Lista;
        }

        public List<Modelo.IndicadorSectorial> ConsultarIndicadoresSectoriales(int idProgramaSectorial, int opcion, string descObjetivo, int numObjetivo)
        {
            List<Modelo.IndicadorSectorial> Lista = new List<Modelo.IndicadorSectorial>();

            try
            {
                dynamic val = clientApi.getIndicadorSectorial(idProgramaSectorial, opcion, descObjetivo);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Modelo.IndicadorSectorial pIndicadorS = new Modelo.IndicadorSectorial();
                        pIndicadorS.NUM_OBJETIVO = numObjetivo;
                        pIndicadorS.NUM_INDICADOR = ele.NumIndicador;
                        pIndicadorS.ID_INDICADOR = ele.IdIndicador;
                        pIndicadorS.INDICADOR = ele.Indicador;

                        pIndicadorS.LIGA = "IndicadorSectorial.aspx?id=" + idProgramaSectorial + "&idIndicador=" + ele.IdIndicador;
                       

                        Lista.Add(pIndicadorS);
                    }
                }

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return Lista;
        }

        public List<Modelo.IndicadorSectorial> ConsultarIndicadoresSectoriales4T(int idProgramaSectorial, int opcion, string descObjetivo, int numObjetivo)
        {
            List<Modelo.IndicadorSectorial> Lista = new List<Modelo.IndicadorSectorial>();

            try
            {
                dynamic val = clientApi.getIndicadorSectorial4T(idProgramaSectorial, opcion, descObjetivo);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Modelo.IndicadorSectorial pIndicadorS = new Modelo.IndicadorSectorial();
                        pIndicadorS.NUM_OBJETIVO = numObjetivo;
                        pIndicadorS.NUM_INDICADOR = ele.NumIndicador;
                        pIndicadorS.ID_INDICADOR = ele.IdIndicador;
                        pIndicadorS.INDICADOR = ele.Indicador;
                        pIndicadorS.TIPO_INDICADOR = ele.tipoindicador;

                        System.Diagnostics.Debug.WriteLine(pIndicadorS.NUM_OBJETIVO);

                        pIndicadorS.LIGA = "IndicadorSectorial19-24.aspx?id=" + idProgramaSectorial + "&idIndicador=" + ele.IdIndicador;

                        Lista.Add(pIndicadorS);
                    }
                }

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return Lista;
        }

        public List<Modelo.IndicadorSectorial> ConsultarIndicadorFin(int id, int opcion)
        {
            List<Modelo.IndicadorSectorial> Lista = new List<Modelo.IndicadorSectorial>();

            try
            {
                dynamic val = clientApi.getIndicadorSectorial(id, opcion, "");
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Modelo.IndicadorSectorial pIndicadorS = new Modelo.IndicadorSectorial();

                        pIndicadorS.ID_INDICADOR = ele.IdIndicador;
                        pIndicadorS.INDICADOR = ele.Nombre;
                        pIndicadorS.ID_NIVEL = ele.IdNivel;
                        pIndicadorS.NIVEL = ele.Nivel;
                        pIndicadorS.NUM_INDICADOR = (Lista.Count) + 1;
                        pIndicadorS.ID_MATRIZ = ele.IdMatriz;
                        pIndicadorS.CICLO = ele.Ciclo;
                        pIndicadorS.RAMO = ele.Ramo;
                        pIndicadorS.LIGA = "DetalleIndicador.aspx?pIdIndicador=" + pIndicadorS.ID_INDICADOR + "&pNivel=" + pIndicadorS.NIVEL + "&pIdMatriz=" + pIndicadorS.ID_MATRIZ + "&pCiclo=" + pIndicadorS.CICLO + "&pRamo=" + pIndicadorS.RAMO + "&pUniP=0";

                        Lista.Add(pIndicadorS);
                    }
                }

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return Lista;
        }

        //getDetalleIndicador
        public List<Modelo.IndicadorSectorial> ConsultarDetalleIndicador(int idIndicador, int opcion)
        {
            List<Modelo.IndicadorSectorial> Lista = new List<Modelo.IndicadorSectorial>();

            try
            {
                dynamic val = clientApi.getDetalleIndicador(idIndicador, opcion);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Modelo.IndicadorSectorial i = new Modelo.IndicadorSectorial();
                        i.OBJETIVO = ele.Objetivo;
                        i.NOMBRE = ele.Nombre;
                        i.DESCRIPCION = ele.Descripcion;
                        i.METODO = ele.Metodo;
                        i.VALOR_LB = ele.ValorLB;
                        i.PERIODICIDAD = ele.Periodicidad;
                        i.UDM = ele.Udm;
                        i.META = ele.Meta;
                        i.FUENTE = ele.Fuente;
                        i.TIPO = ele.Tipo;
                        i.Nombre_Ramo = ele.NombreRamo;
                        i.CAMBIO_TEXTO = ele.Cambio_Texto;
                        i.META_ALCANZADA = ele.Meta_Alcanzada;
                        i.PORCENTAJE_AVANCE = ele.Porcentaje_Avance;
                        i.LB_COLOR = ele.Lb_Color;
                        i.PORCENTAJE_COLOR = ele.Porcentaje_Avance_Color;
                        i.MALCANZADA_COLOR = ele.Meta_Alcanzada_Color;
                        i.META_COLOR = ele.Meta_Color;
                        i.CLARIDAD = ele.Claridad;
                        i.RELEVANCIA = ele.Relevancia;
                        i.MONITOREABILIDAD = ele.Monitoreabilidad;
                        i.PERTINENCIA = ele.Pertinencia;
                        i.MAX_META_ALCANZADA = ele.Max_MetaAlcanzada;
                        i.MIN_META_ALCANZADA = ele.Min_MetaAlcanzada;
                        i.AVG_META_ALCANZADA = ele.Avg_MetaAlcanzada;
                        i.MAX_META_PLANEADA = ele.Max_MetaPlaneada;
                        i.MIN_META_PLANEADA = ele.Min_MetaPlaneada;
                        i.AVG_META_PLANEADA = ele.Avg_MetaPlaneada;
                        i.COMENTARIO = ele.Comentario;
                        Lista.Add(i);
                    }
                }

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return Lista;
        }


        //getDetalleIndicador
        public List<Modelo.IndicadorSectorial> ConsultarDetalleIndicador4T(int idIndicador, int opcion)
        {
            List<Modelo.IndicadorSectorial> Lista = new List<Modelo.IndicadorSectorial>();

            try
            {
                dynamic val = clientApi.getDetalleIndicador4T(idIndicador, opcion);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Modelo.IndicadorSectorial i = new Modelo.IndicadorSectorial();
                        i.OBJETIVO = ele.Objetivo;
                        i.NOMBRE = ele.Nombre;
                        i.DESCRIPCION = ele.Descripcion;
                        i.METODO = ele.Metodo;
                        i.VALOR_LB = ele.ValorLB;
                        i.PERIODICIDAD = ele.Periodicidad;
                        i.UDM = ele.Udm;
                        i.META = ele.Meta;
                        i.FUENTE = ele.Fuente;
                        i.TIPO = ele.Tipo;
                        i.Nombre_Ramo = ele.NombreRamo;
                        i.CAMBIO_TEXTO = ele.Cambio_Texto;
                        i.META_ALCANZADA = ele.Meta_Alcanzada;
                        i.PORCENTAJE_AVANCE = ele.Porcentaje_Avance;
                        i.LB_COLOR = ele.Lb_Color;
                        i.PORCENTAJE_COLOR = ele.Porcentaje_Avance_Color;
                        i.MALCANZADA_COLOR = ele.Meta_Alcanzada_Color;
                        i.META_COLOR = ele.Meta_Color;
                        i.CLARIDAD = ele.Claridad;
                        i.RELEVANCIA = ele.Relevancia;
                        i.MONITOREABILIDAD = ele.Monitoreabilidad;
                        i.PERTINENCIA = ele.Pertinencia;
                        i.MAX_META_ALCANZADA = ele.Max_MetaAlcanzada;
                        i.MIN_META_ALCANZADA = ele.Min_MetaAlcanzada;
                        i.AVG_META_ALCANZADA = ele.Avg_MetaAlcanzada;
                        i.MAX_META_PLANEADA = ele.Max_MetaPlaneada;
                        i.MIN_META_PLANEADA = ele.Min_MetaPlaneada;
                        i.AVG_META_PLANEADA = ele.Avg_MetaPlaneada;
                        i.TIPO_INDICADOR_GRAFICA = ele.Tipoindicador;
                        i.NIVEL_DESAGREGACION = ele.Desagregacion;
                        i.TENDENCIA = ele.Tendencia;
                        i.ENFOQUE_RES = ele.Enfoque_Res;
                        i.ENFOQUE_INDICADOR = ele.Enfoque_Indicador;
                        i.ACUM_PER = ele.Acum_Per;
                        i.DOF_LB_DESCRIPCION = ele.Doflbdesc;
                        i.DOF_META_DESCRIPCION = ele.Dofmetadesc;
                        i.ADECUACION = ele.Adecuacion;
                        if (ele.Meta == 0 || ele.Meta == null)
                        {
                            i.METATEXT = "ND";
                        }
                        else
                        {
                            i.METATEXT = ele.Meta;
                        }
                        //i.COMENTARIO = ele.Comentario;
                        Lista.Add(i);
                    }
                }

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return Lista;
        }

        public List<Modelo.Meta> ConsultarMetasIndicador(int idIndicador, int opcion)
        {
            List<Modelo.Meta> Lista = new List<Modelo.Meta>();

            try
            {
                dynamic val = clientApi.getMetasIndicador(idIndicador, opcion);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Modelo.Meta meta = new Modelo.Meta();
                        meta.CICLO = ele.Ciclo;
                        meta.MI = ele.Mi;
                        meta.VALOR = ele.Valor;
                        meta.VALORLB = ele.ValorLB;
                        meta.META = ele.Meta;
                        meta.METASHISTORICO = ele.MetasHistorico;
                        Lista.Add(meta);
                    }
                }

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return Lista;
        }

        public List<Modelo.Meta> ConsultarMetasIndicador4T(int idIndicador, int opcion)
        {
            List<Modelo.Meta> Lista = new List<Modelo.Meta>();

            try
            {
                dynamic val = clientApi.getMetasIndicador4T(idIndicador, opcion);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Modelo.Meta meta = new Modelo.Meta();
                        meta.CICLO = ele.Ciclo;
                        meta.MI = ele.Mi;
                        meta.VALOR = ele.Valor;
                        meta.VALORLB = ele.ValorLB;
                        meta.META = ele.Meta;
                        meta.METASHISTORICO = ele.MetasHistorico;
                        Lista.Add(meta);
                    }
                }

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return Lista;
        }

        public List<Modelo.ProgramaIndicadorMIR> ConcultarProgramaIndicador(int idIndicador)
        {
            List<Modelo.ProgramaIndicadorMIR> Lista = new List<Modelo.ProgramaIndicadorMIR>();
            Utils u = new Utils();
            try
            {
                dynamic val = clientApi.getProgramaIndicador(idIndicador);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Modelo.ProgramaIndicadorMIR i = new Modelo.ProgramaIndicadorMIR();
                        i.PP = ele.PP;
                        i.RAMO = ele.Ramo;
                        i.MATRIZ = ele.Id_Matriz;
                        i.NOMBRE = ele.Nombre;
                        i.CICLO = ele.Ciclo;
                        i.LIGA = "MIR.aspx?pIdMatriz=" + ele.Id_Matriz + "&pPrograma=" + ele.Nombre + "&pCiclo=" + ele.Ciclo + "&pRamo=" + ele.Ramo;
                        Lista.Add(i);
                    }
                }

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return Lista;
        }
        /// <summary>
        /// Obtiene la informacion para el reporte de indicadores 
        /// </summary>
        /// <param name="anio">Año a consultar</param>
        /// <param name="ramo">Ramo</param>
        /// <param name="dMatriz">Id de la Matriz</param>
        /// <param name="pTipoExtension">Tipo de extensión de descarga para indicadores de FIN</param>
        /// <param name="pPantalla">Pantalla donde se ubica el reporte, "FIN" o "PROGRAMA"</param>
        /// <returns></returns>
        public DataSet ConsultaReporteCSV(decimal anio, string ramo, decimal dMatriz, int pTipoExtension, string pPantalla)
        {
            dynamic data = null;
            Utils u = new Utils();
            List<SIMEPS.Modelo.Programa> lProgramas = new List<SIMEPS.Modelo.Programa>();
            try
            {
                if (pTipoExtension != -1 && !String.IsNullOrEmpty(pPantalla))
                    data = clientApi.getDataRptIndicadoresFin(anio.ToString(), ramo, dMatriz.ToString(), pTipoExtension.ToString(), pPantalla);
                else
                    data = clientApi.getDataRptIndicadores(anio.ToString(), ramo, dMatriz.ToString());
                DataSet datos = new DataSet();
                if (data.StatusCode == "OK")
                {
                    var json = data.ResponseBody;
                    XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + json + "}}");
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(xml.InnerXml);
                    XmlReader xmlReader = new XmlNodeReader(xml);
                    datos.ReadXml(xmlReader);
                }
                else
                {
                    log.LogMessageToFile(getBadMessage(data));
                }

                return datos;

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
                return new DataSet();
            }
        }

        /// <summary>
        /// Obtiene el conteo de los objetivos e indicadores por programa
        /// </summary>      

        /// <returns></returns>
        public ContadorIndObj contadorIndicadoresObjetivos(int idProgSectorial)
        {
            ContadorIndObj objContador = new ContadorIndObj();

            try
            {
                dynamic val = clientApi.getContadorIndObje(idProgSectorial);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        objContador.COUNT_IND = ele.NumIndicador;
                        objContador.COUNT_OBJ = ele.NumObjetivos;

                    }
                }

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }


            return objContador;

        }

        /// <summary>
        /// Obtiene el conteo de los objetivos e indicadores por programa
        /// </summary>      

        /// <returns></returns>
        public ContadorIndObj4T contadorIndicadoresObjetivos4T(int idProgSectorial)
        {
            ContadorIndObj4T objContador = new ContadorIndObj4T();

            try
            {
                dynamic val = clientApi.getContadorIndObje4T(idProgSectorial);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        objContador.COUNT_META = ele.NumMetas;
                        objContador.COUNT_PARAM = ele.NumParam;
                        objContador.COUNT_OBJ = ele.NumObjetivos;

                    }
                }

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }


            return objContador;

        }
        /// <summary>
        /// Obtiene Las Fichas de Monitoreo filtradas por año
        /// </summary>
        /// <param name="iCiclo"> ciclo de filtro(2016 o 2015)</param>
        /// <returns></returns>
        public List<Modelo.FichaMonitoreo> ConsultarFichasMonitoreo(short iCiclo)
        {
            List<Modelo.FichaMonitoreo> Lista = new List<Modelo.FichaMonitoreo>();

            try
            {
                dynamic val = clientApi.getFichasMonitoreo(iCiclo);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Modelo.FichaMonitoreo ficha = new Modelo.FichaMonitoreo();
                        ficha.CICLO = ele.CICLO;
                        ficha.ID_PROG_SECTORIAL = ele.ID_PROG_SECTORIAL;
                        ficha.URL_PORTADA = ele.URL_PORTADA;
                        ficha.URL_FICHA = ele.URL_FICHA;
                        Lista.Add(ficha);
                    }
                }

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return Lista;
        }
        /// <summary>
        /// Obtiene Las Fichas de Monitoreo filtradas por año
        /// </summary>
        /// <param name="iCiclo"> ciclo de filtro(2016 o 2015)</param>
        /// <returns></returns>
        public List<Modelo.FichaMonitoreo> ConsultarCiclosFichasMonitoreo()
        {
            List<Modelo.FichaMonitoreo> Lista = new List<Modelo.FichaMonitoreo>();

            try
            {
                dynamic val = clientApi.getCiclosFichasMonitoreo();
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Modelo.FichaMonitoreo ficha = new Modelo.FichaMonitoreo();
                        ficha.CICLO = ele.CICLO;
                        ficha.COLOR_CICLO = ele.COLOR_CICLO;


                        Lista.Add(ficha);
                    }
                }

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return Lista;
        }

        /// <summary>
        /// Método para consultar contador de indicadores
        /// </summary>
        /// <param name="pAnio"></param>
        /// <param name="pRamo"></param>
        /// <param name="pPantalla"></param>
        /// <returns></returns>
        public List<Modelo.Contador> ConsultarContadorIndicadores(int pAnio, int pRamo, string pUnidad, string pPantalla)
        {
            List<Modelo.Contador> Lista = new List<Modelo.Contador>();
            Utils u = new Utils();
            try
            {
                dynamic val = clientApi.getContadorIndicadores(pAnio, pRamo, pUnidad, pPantalla);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Modelo.Contador i = new Modelo.Contador();
                        i.PROGRAMAS = ele.Programas;
                        i.INDICADORES = ele.Indicadores;
                        i.PROGRAMAS_APROBADOS = ele.ProgramasAprobados;
                        i.PROMEDIO_PERMANENCIA = ele.PromedioPermanencia;
                        i.PROMEDIO_METAS = ele.PromedioMetas;
                        i.VERSION = ele.Version;
                        Lista.Add(i);
                    }
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return Lista;
        }
        /// <summary>
        /// Consulta Derechos sociales Indicador *Checar Funcionalidad Correcta, solo ejemplo*
        /// </summary>
        /// <param name="idSector">Id de sector</param>


        /// <returns></returns>
        public List<DerechoSocialInd> ConsultaDerechoSocialInd(int idIndicador)
        {
            List<DerechoSocialInd> lstDerechoSocialInd = new List<DerechoSocialInd>();

            try
            {
                dynamic val = clientApi.getDerechoSocialInd(idIndicador);


                foreach (var ele in val.ResponseBody)
                {
                    DerechoSocialInd derSoc = new DerechoSocialInd();
                    derSoc.DER_ID = ele.IdDercho;
                    derSoc.DER_DESCRIPCION = ele.NombreDerechoSocial;
                    lstDerechoSocialInd.Add(derSoc);

                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return lstDerechoSocialInd;
        }

        public List<DerechoSocialInd> ConsultaDerechoSocialInd4T(int idIndicador)
        {
            List<DerechoSocialInd> lstDerechoSocialInd = new List<DerechoSocialInd>();

            try
            {
                dynamic val = clientApi.getDerechoSocialInd4T(idIndicador);


                foreach (var ele in val.ResponseBody)
                {
                    DerechoSocialInd derSoc = new DerechoSocialInd();
                    derSoc.DER_DESCRIPCION = ele.NombreDerechoSocial;
                    lstDerechoSocialInd.Add(derSoc);

                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return lstDerechoSocialInd;
        }

        public List<IndicadorMapaR33> GetValoresPorEntidadFondoRamo33(int idIndicador)
        {
            List<IndicadorMapaR33> lista = new List<IndicadorMapaR33>();
            try
            {
                dynamic val = clientApi.GetValoresPorEntidadFondoRamo33(idIndicador);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        IndicadorMapaR33 item = new IndicadorMapaR33
                        {
                            IdEstado = ele.IdEstado,
                            NombreEstado = ele.NombreEstado,
                            MetaRel = ele.MetaRel,
                            IdIndicador = idIndicador,
                        };

                        lista.Add(item);
                    }
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return lista;
        }


        public List<Ramo33BDFederal> GetBaseDeDatosFondosIndicadoresRamo33Federal(int Ciclo)
        {
            List<Ramo33BDFederal> lista = new List<Ramo33BDFederal>();
            try
            {
                dynamic val = clientApi.GetBaseDeDatosFondoRamo33Federal(Ciclo);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Ramo33BDFederal item = new Ramo33BDFederal
                        {
                            Ciclo = ele.Ciclo,
                            Modalidad = ele.Modalidad,
                            Clave = ele.Clave,
                            Fondo = ele.Fondo,
                            Nivel = ele.Nivel,
                            Objetivo= ele.Objetivo,
                            Indicador= ele.Indicador,
                            Definicion = ele.Definicion,
                            MetodoCalculo = ele.MetodoCalculo,
                            FrecuenciaMedicion = ele.FrecuenciaMedicion,
                            UnidadMedida = ele.UnidadMedida,
                            SentidoIndicador = ele.SentidoIndicador,
                            CicloLineaBase= ele.CicloLineaBase,
                            LineaBase = ele.LineaBase,
                            MetaRelPlaneada = ele.MetaRelPlaneada,
                            MetaAbsPlaneada = ele.MetaAbsPlaneada,
                            MetaRelAlcanzada = ele.MetaRelAlcanzada,
                            MetaAbsAlcanzada = ele.MetaAbsAlcanzada


                        };

                        lista.Add(item);
                    }
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return lista;
        }


        public List<Ramo33BDEstatal> GetBaseDeDatosFondosIndicadoresRamo33Estatal(int Ciclo)
        {
            List<Ramo33BDEstatal> lista = new List<Ramo33BDEstatal>();
            try
            {
                dynamic val = clientApi.GetBaseDeDatosFondoRamo33Estatal(Ciclo);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Ramo33BDEstatal item = new Ramo33BDEstatal
                        {
                            Ciclo = ele.Ciclo,
                            Modalidad = ele.Modalidad,
                            Clave = ele.Clave,
                            Fondo = ele.Fondo,
                            Estado = ele.Estado,
                            Nivel = ele.Nivel,
                            Objetivo = ele.Objetivo,
                            Indicador = ele.Indicador,
                            Definicion = ele.Definicion,
                            MetodoCalculo = ele.MetodoCalculo,
                            FrecuenciaMedicion = ele.FrecuenciaMedicion,
                            UnidadMedida = ele.UnidadMedida,
                            SentidoIndicador = ele.SentidoIndicador,
                            CicloLineaBase = ele.CicloLineaBase,
                            LineaBase = ele.LineaBase,
                            MetaRelPlaneada = ele.MetaRelPlaneada,
                            MetaAbsPlaneada = ele.MetaAbsPlaneada,
                            MetaRelAlcanzada = ele.MetaRelAlcanzada,
                            MetaAbsAlcanzada = ele.MetaAbsAlcanzada


                        };

                        lista.Add(item);
                    }
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return lista;
        }

        public List<Ramo33BDMunicipal> GetBaseDeDatosFondosIndicadoresRamo33Municipal(int Ciclo)
        {
            List<Ramo33BDMunicipal> lista = new List<Ramo33BDMunicipal>();
            try
            {
                dynamic val = clientApi.GetBaseDeDatosFondoRamo33Municipal(Ciclo);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        Ramo33BDMunicipal item = new Ramo33BDMunicipal
                        {
                            Ciclo = ele.Ciclo,
                            Modalidad = ele.Modalidad,
                            Clave = ele.Clave,
                            Fondo = ele.Fondo,
                            Estado = ele.Estado,
                            Municipio=ele.Municipio,
                            Nivel = ele.Nivel,
                            Objetivo = ele.Objetivo,
                            Indicador = ele.Indicador,
                            Definicion = ele.Definicion,
                            MetodoCalculo = ele.MetodoCalculo,
                            FrecuenciaMedicion = ele.FrecuenciaMedicion,
                            UnidadMedida = ele.UnidadMedida,
                            SentidoIndicador = ele.SentidoIndicador,
                            CicloLineaBase = ele.CicloLineaBase,
                            LineaBase = ele.LineaBase,
                            MetaRelPlaneada = ele.MetaRelPlaneada,
                            MetaAbsPlaneada = ele.MetaAbsPlaneada,
                            MetaRelAlcanzada = ele.MetaRelAlcanzada,
                            MetaAbsAlcanzada = ele.MetaAbsAlcanzada


                        };

                        lista.Add(item);
                    }
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }
            return lista;
        }




        public List<ValoDesepenoRamo33> GetValoresDesempenoPorEntidadFondoRamo33(int idIndicador)
        {
            List<ValoDesepenoRamo33> lista = new List<ValoDesepenoRamo33>();

            try
            {
                dynamic val = clientApi.GeValorestDesempenoPorEntidadFondoRamo33(idIndicador);
                if (val.StatusCode == "OK")
                {
                    foreach (var ele in val.ResponseBody)
                    {
                        ValoDesepenoRamo33 item = new ValoDesepenoRamo33
                        {
                            IdEstado = ele.IdEstado,
                            NombreEstado = ele.NombreEstado,
                            PromedioMetas = ele.PromedioMetas,
                            IdIndicador = idIndicador
                        };

                        lista.Add(item);
                    }
                }
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return lista;
        }

        //PK1309
        public List<MatrizRamo33> ConsultaFondoMatrizComponente(int idMatriz, string sFondo, string sComponente, int iCiclo)
        {
            List<MatrizRamo33> matriz = null;

            try
            {
                dynamic val = clientApi.GetFondoMatrizComponente(idMatriz);

                Newtonsoft.Json.Linq.JArray items = val.ResponseBody;


                matriz = items.ToObject<FondoMatrizComponente[]>()
                    .GroupBy(x => new { x.Clave, x.Ramo, x.Modalidad, x.Version, x.Proposito, x.DescNivel })
                    .Select(x => new MatrizRamo33
                    {
                        //IdMatriz = x.Key.IdMatriz,
                        Clave = x.Key.Clave,
                        Ramo = x.Key.Ramo,
                        Modalidad = x.Key.Modalidad,
                        Version = x.Key.Version,
                        Proposito = x.Key.Proposito,
                        DescNivel = x.Key.DescNivel,
                        CantidadIndicadores = x.Count(),
                        Index = 0,

                        Indicadores = x.Select(z => new ComponenteRamo33
                        {
                            IdIndicador = z.IdIndicador,
                            Ciclo = z.Ciclo,
                            NombreIndicador = z.NombreIndicador,
                            DescCobertura = z.DescCobertura,
                            IdMatriz = z.IdMatriz,
                            Meta_Abs_Alcanzada = z.Meta_Abs_Alcanzada,
                            Meta_Abs_Planeada = z.Meta_Abs_Planeada,
                            Meta_Rel_Alcanzada = z.Meta_Rel_Alcanzada,
                            Meta_Rel_Planeada = z.Meta_Rel_Planeada,
                            Metodo_Calculo_Ind = z.Metodo_Calculo_Ind,
                            Modalidad = z.Modalidad,
                            Unidad_Medida = z.Unidad_Medida,
                            Version = z.Version,
                            Index = 0,
                            Url = $"IndicadorR33.aspx?iCiclo={z.Ciclo}&idIndicador={z.IdIndicador}&iMatriz={idMatriz}&sFondo={sFondo}&sComponente={sComponente}&pCiclo={iCiclo}&sProposito={x.Key.Proposito}"
                        }).ToList()
                    })
                    .ToList();

                int index = 0;
                matriz.ForEach(com =>
                {
                    com.Index = index;
                    com.Indicadores.ForEach(item =>
                    {
                        item.Index = com.Index;
                        // promedios estatatales
                        //
                        item.PromediosEstatales = GetValoresPorEntidadFondoRamo33(item.IdIndicador.Value);

                        // Valores de desempeño
                        item.DesempenoEstatales = GetValoresDesempenoPorEntidadFondoRamo33(item.IdIndicador.Value);

                        var lstHistorico = ConsultarHistorico(item.IdIndicador.Value);
                        item.VALORES_GRAFICA.AddRange(lstHistorico);
                    });
                    index++;

                });
            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return matriz;
        }
        //PK1309

        public FondoRamo33View[] ConsultaFondosRamo33PorCiclo(int pCiclo)
        {
            FondoRamo33View[] fondos = null;
            try
            {
                dynamic val = clientApi.GetFondosRamo33PorCiclo(pCiclo);

                Newtonsoft.Json.Linq.JArray items = val.ResponseBody;

                var dummy = items.ToObject<FondoRamo33[]>();


                fondos = dummy.GroupBy(x => new { x.SiglasFondo, x.NombreFondo })
                    .Select(x => new FondoRamo33View
                    {
                        SiglasFondo = x.Key.SiglasFondo,
                        NombreFondo = x.Key.NombreFondo,
                        Icono = string.IsNullOrEmpty(x.Key.SiglasFondo) ? string.Empty : string.Format("https://www.coneval.org.mx/SiteCollectionImages/SIMEPS/LogosR33/btn_{0}.jpg", Uri.EscapeUriString(x.Key.SiglasFondo)),
                        MatrizComponentes = x.Select(y => new FondoRamo33View
                        {
                            IdMatriz = y.Matriz,
                            Ciclo = y.Ciclo,
                            Clave = y.Clave,
                            Matriz = y.Matriz,
                            Modalidad = y.Modalidad,
                            NombreFondo = y.NombreFondo,
                            Ramo = y.Ramo,
                            SiglasComponente = !string.IsNullOrEmpty(y.SiglasComponente) ? y.SiglasComponente : y.SiglasFondo,
                            SiglasFondo = y.SiglasFondo,
                            Icono = string.IsNullOrEmpty(y.SiglasComponente) ? string.Empty :
                            string.Format("https://www.coneval.org.mx/SiteCollectionImages/SIMEPS/LogosR33/{0:D2}-{1:D3}.jpg",
                            y.Ramo, y.Clave),
                            Url = string.Format("IndicadoresR33.aspx?pCiclo={0}&iMatriz={1}&sComponente={2}&sFondo={3}&sMatris=1",
                            pCiclo, y.Matriz, y.SiglasComponente, y.SiglasFondo)
                        }).ToList(),

                    })
                    .OrderBy(x => x.SiglasFondo)
                    .ToArray();

                foreach (var fondo in fondos)
                {
                    if (fondo.MatrizComponentes.Count == 1)
                    {
                        var item = fondo.MatrizComponentes.FirstOrDefault();
                        fondo.SiglasComponente = item.SiglasComponente;
                        fondo.Ramo = item.Ramo;
                        fondo.Modalidad = item.Modalidad;
                        fondo.Clave = item.Clave;
                        fondo.Url = string.Format("IndicadoresR33.aspx?pCiclo={0}&iMatriz={1}&sComponente={2}&sFondo={3}&sMatris=0",
                            pCiclo, item.IdMatriz,
                           !string.IsNullOrEmpty(fondo.SiglasComponente) ? fondo.SiglasComponente : fondo.SiglasFondo, fondo.SiglasFondo);
                    }
                    else
                    {
                        fondo.Url = string.Format("MosaicoRamo33.aspx?sFondo={0}&pCIclo={1}", fondo.SiglasFondo, pCiclo);
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogMessageToFile(ex.Message);
                log.LogMessageToFile(ex.StackTrace);
            }
            return fondos;
        }

        public DetalleIndicadorRamo33 ConsultarIndicadorRamo33(int iMatriz, int iCiclo, int iIndicador)
        {
            DetalleIndicadorRamo33 indicador = null;

            try
            {
                dynamic val = clientApi.GetFondoMatrizComponente(iMatriz);

                Newtonsoft.Json.Linq.JArray items = val.ResponseBody;

                var componentes = items.ToObject<DetalleIndicadorRamo33[]>()

                    .GroupBy(x => new { x.IdIndicador, x.CICLO, x.UNIDAD_MEDIDA, x.Version, x.DescCobertura, x.NombreIndicador, x.DEFINICION_IND, x.METODO_CALCULO_IND, x.FRECUENCIA_MEDICION, x.CALIF_ADECUACION, x.CALIF_CLARIDAD, x.CALIF_MONITOREABILIDAD, x.CALIF_RELEVANCIA, x.META_ABS_ALCANZADA, x.META_ABS_PLANEADA, x.META_REL_ALCANZADA, x.META_REL_PLANEADA,x.DESC_MEDIOS_VERIFICACION })
                    .Select(x => new DetalleIndicadorRamo33
                    {
                        IdIndicador = x.Key.IdIndicador,
                        CICLO = x.Key.CICLO,
                        DescCobertura = x.Key.DescCobertura,
                        Version = x.Key.Version,
                        UNIDAD_MEDIDA = x.Key.UNIDAD_MEDIDA,
                        NombreIndicador = x.Key.NombreIndicador,
                        DEFINICION_IND = x.Key.DEFINICION_IND,
                        METODO_CALCULO_IND = x.Key.METODO_CALCULO_IND,
                        FRECUENCIA_MEDICION = x.Key.FRECUENCIA_MEDICION,
                        CALIF_ADECUACION = x.Key.CALIF_ADECUACION,
                        CALIF_CLARIDAD = x.Key.CALIF_CLARIDAD,
                        CALIF_MONITOREABILIDAD = x.Key.CALIF_MONITOREABILIDAD,
                        CALIF_RELEVANCIA = x.Key.CALIF_RELEVANCIA,
                        META_ABS_ALCANZADA = x.Key.META_ABS_ALCANZADA,
                        META_ABS_PLANEADA = x.Key.META_ABS_PLANEADA,
                        META_REL_ALCANZADA = x.Key.META_REL_ALCANZADA,
                        META_REL_PLANEADA = x.Key.META_REL_PLANEADA,
                        DESC_MEDIOS_VERIFICACION = x.Key.DESC_MEDIOS_VERIFICACION
                    })
                    .ToList();

                indicador = componentes.FirstOrDefault(x => x.IdIndicador == iIndicador && x.CICLO == iCiclo);

            }
            catch (Exception error)
            {
                log.LogMessageToFile(error.Message);
                log.LogMessageToFile(error.StackTrace);
            }

            return indicador;
        }

    }
}