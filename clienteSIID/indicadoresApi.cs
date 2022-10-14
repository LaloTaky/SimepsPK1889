using System;
using System.Collections.Generic;
using System.Web;

namespace clientSIID
{
    public class indicadoresApi : callAPI
    {
        public indicadoresApi(string _BaseAddress, string usr, string pws, string pathToken)
            : base(_BaseAddress, usr, pws, pathToken) { }

        public dynamic getObjetivoNacional(string pIdMatriz, string pRamo)
        {
            return jsonData("api/V1/mir/catalogos/objetivoNacional/" + pIdMatriz + "/" + pRamo);
        }
        public dynamic getFrecuenciaMEdicion(string pIdMatriz, string pRamo)
        {
            return jsonData("api/V1/mir/catalogos/frecuenciaMedicion/" + pIdMatriz + "/" + pRamo);
        }
        public dynamic getUnidadMedida(string pIdMatriz, string pRamo)
        {
            return jsonData("api/V1/mir/catalogos/unidadMedida/" + pIdMatriz + "/" + pRamo);
        }
        public dynamic getCiclos(string pPantalla)
        {
            return jsonData("api/V1/mir/catalogos/ciclos" + "/" + pPantalla);
        }
        public dynamic getMosaico(string pAnio, string pCamino)
        {
            return jsonData("api/V1/mir/catalogos/instituciones/" + pAnio + "/" + pCamino);
        }

        public dynamic getBuscarIndicador(string pTextoBusqueda, string pDerecho, string pCiclo, string pRamo, string pIdMatriz, string pObjetivo, string pNivelMIR, string pUnidadM, string pFrecuencia, string pSectorial)
        {
            if (pIdMatriz.Equals("-Seleccione-"))
                pIdMatriz = "0";

            if (pCiclo == "-1" || pCiclo.Equals("-Seleccione-"))
                pCiclo = "0";

            if (pRamo == "-1" || pRamo.Equals("-Seleccione-"))
                pRamo = "0";

            if (pObjetivo == "-1" || pObjetivo == "-Seleccione-")
                pObjetivo = "0";

            if (pDerecho == "-1" || pDerecho == "-Seleccione-")
                pDerecho = "0";

            if (pNivelMIR == "-1" || pNivelMIR == "-Seleccione-")
                pNivelMIR = "0";

            if (pUnidadM == "-1" || pUnidadM == "-Seleccione-")
                pUnidadM = "0";

            if (pFrecuencia == "-1" || pFrecuencia == "-Seleccione-")
                pFrecuencia = "0";

            if (pTextoBusqueda == "" || pTextoBusqueda == null)
                pTextoBusqueda = "0";

            if (pSectorial == "-1" || pSectorial == "-Seleccione-")
                pSectorial = "0";

            return jsonData("api/V1/mir/programas/busquedas/" + pTextoBusqueda + "/" + pDerecho + "/" + pCiclo + "/" + pRamo + "/" + pIdMatriz + "/" + pObjetivo + "/" + pNivelMIR + "/" + pUnidadM + "/" + pFrecuencia + "/" + pSectorial);
        }

        public dynamic getProgramas(string pAnio, string pRamo, string pUnidad, string pNombrePrograma, string IdMatriz, string pUniversoPaemir, string idIndicadorSectorial)
        {
            return jsonData("api/V1/mir/programas/" + pAnio + "/" + pRamo + "/" + pUnidad + "/" + pNombrePrograma + "/" + IdMatriz + "/" + pUniversoPaemir + "/" + idIndicadorSectorial);
        }
        public dynamic getDataRptIndicadores(string pAnio, string pRamo, string IdMatriz)
        {
            return jsonData("api/V1/mir/programas/reportCSV/" + pAnio + "/" + IdMatriz + "/" + pRamo);
        }
        public dynamic getDataRptIndicadoresFin(string pAnio, string pRamo, string IdMatriz, string pTipoExtension, string pPantalla)
        {
            return jsonData("api/V1/mir/programas/reportIndicadoresFin/" + pAnio + "/" + IdMatriz + "/" + pRamo + "/" + pTipoExtension + "/" + pPantalla);
        }
        public dynamic getHistoricoProgramas(string IdMatriz)
        {
            return jsonData("api/V1/mir/programas/" + IdMatriz + "/historicoProgramas");
        }
        public dynamic getCatalogoProgramas(string pAnio, string pRamo)
        {
            return jsonData("api/V1/mir/catalogos/programas/" + pAnio + "/" + pRamo);
        }

        public dynamic getPresupuesto(int pRamo, string pModalidad, string pClave)
        {
            return jsonData("api/V1/mir/programas/" + pRamo.ToString() + "/" + pModalidad + "/" + pClave + "/presupuestos");
        }

        public dynamic getParametro(string pParametro)
        {
            return jsonData("api/V1/mir/programas/parametros/" + pParametro);
        }

        public dynamic getValoracion(int pRamo, string pModalidad, string pClave)
        {
            return jsonData("api/V1/mir/programas/valoracion/" + Convert.ToString(pRamo) + "/" + pModalidad + "/" + pClave);
        }

        public dynamic getObjetivos(string pIdMatriz, string pNivel)
        {
            return jsonData("api/V1/mir/objetivos/" + pIdMatriz + "/niveles/" + pNivel);
        }

        public dynamic getHistorico(string pIdIndicador)
        {
            return jsonData("api/V1/mir/indicadores/" + pIdIndicador + "/historico");
        }

        public dynamic getVariables(string pIdIndicador)
        {
            return jsonData("api/V1/mir/indicadores/" + pIdIndicador + "/variables");
        }

        public dynamic getIndicador(string pIdIndicador, string pNivel, string pIdNivel, string pIdMatriz)
        {
            return jsonData("api/V1/mir/indicadores/" + pIdIndicador + "/" + pNivel + "/" + pIdNivel + "/" + pIdMatriz);
        }

        public dynamic getSupuestos(string pIdMatriz, string pNivel)
        {
            return jsonData("api/V1/mir/programas/" + pIdMatriz + "/" + pNivel + "/supuestos");
        }

        public dynamic getTotalIndicador()
        {
            return jsonData("api/V1/mir/indicadores/totalindicadores");
        }

        public dynamic getDerechos()
        {
            return jsonData("api/V1/evaluaciones/catalogos/derechos");
        }

        public dynamic getMetasNacionales()
        {
            return jsonData("api/V1/mir/indicadores/MetasNacionales");
        }

        public dynamic getObjetivosPND(int idMeta)
        {
            return jsonData("api/V1/mir/indicadores/ObjetivosM/" + idMeta);
        }

        public dynamic getIndicadoresObjetivoPND(int idMeta)
        {
            return jsonData("api/V1/mir/indicadores/IndicadoresObjetivo/" + idMeta);
        }

        public dynamic getIndicadoresTrans()
        {
            return jsonData("api/V1/mir/indicadores/IndicadoresTrans/");
        }
        
        public dynamic getProgramasSectoriales(int idProgramaSectorial)
        {
            return jsonData("api/V1/mir/indicadores/programassectoriales/" + idProgramaSectorial);
        }

        public dynamic getProgramasSectoriales4T(int idProgramaSectorial)
        {
            return jsonData("api/V1/mir/indicadores/programassectoriales4T/" + idProgramaSectorial);
        }

        public dynamic getProgSectorialExcel(int idProgramaSectorial)
        {
            return jsonData("api/V1/mir/indicadores/progSectorialExcel/" + idProgramaSectorial);
        }

        public dynamic getProgSectorialExcel4T(int idProgramaSectorial)
        {
            return jsonData("api/V1/mir/indicadores/progSectorialExcel4T/" + idProgramaSectorial);
        }
        public dynamic getSectores()
        {
            return jsonData("api/V1/mir/indicadores/sectores");
        }

        public dynamic getSectores4T()
        {
            return jsonData("api/V1/mir/indicadores/sectores4T");
        }

        public dynamic getContador(int nSector)
        {
            return jsonData("api/V1/mir/indicadores/conteoEstadistica/" + nSector);
        }

        public dynamic getContador4T(int nSector)
        {
            return jsonData("api/V1/mir/indicadores/conteoEstadistica4T/" + nSector);
        }


        public dynamic getIndicadorSectorial(int pIdProgramaSectorial, int pOpcion)
        {
            return jsonData("api/V1/mir/indicadores/indicadorsectorial/" + pIdProgramaSectorial + "/" + pOpcion);
        }

        public dynamic getIndicadorSectorial(int pIdProgramaSectorial, int pOpcion, string pObjetivo)
        {
            Dictionary<string, string> datos = new Dictionary<string, string>();
            datos.Add("objetivoSectorial", HttpUtility.UrlEncode(pObjetivo));
            return jsonData("api/V1/mir/indicadores/indicadorsectorial/" + pIdProgramaSectorial + "/" + pOpcion, datos);
        }


        public dynamic getIndicadorSectorial4T(int pIdProgramaSectorial, int pOpcion)
        {
            return jsonData("api/V1/mir/indicadores/indicadorsectorial4T/" + pIdProgramaSectorial + "/" + pOpcion);
        }

        public dynamic getIndicadorSectorial4T(int pIdProgramaSectorial, int pOpcion, string pObjetivo)
        {
            Dictionary<string, string> datos = new Dictionary<string, string>();
            datos.Add("objetivoSectorial", HttpUtility.UrlEncode(pObjetivo));
            return jsonData("api/V1/mir/indicadores/indicadorsectorial4T/" + pIdProgramaSectorial + "/" + pOpcion, datos);
        }

        public dynamic getDetalleIndicador(int pIdProgramaSectorial, int pOpcion)
        {
            return jsonData("api/V1/mir/indicadores/detalleIndicadores/" + pIdProgramaSectorial + "/" + pOpcion);
        }

        public dynamic getDetalleIndicador4T(int pIdProgramaSectorial, int pOpcion)
        {
            return jsonData("api/V1/mir/indicadores/detalleIndicadores4T/" + pIdProgramaSectorial + "/" + pOpcion);
        }


        public dynamic getCiclosRamo33()
        {
            return jsonData("api/V1/mir/catalogos/ciclosRamo33");
        }

        public dynamic getMetasIndicador(int pIdProgramaSectorial, int pOpcion)
        {
            return jsonData("api/V1/mir/indicadores/metasIndicadores/" + pIdProgramaSectorial + "/" + pOpcion);
        }

        public dynamic getMetasIndicador4T(int pIdProgramaSectorial, int pOpcion)
        {
            return jsonData("api/V1/mir/indicadores/metasIndicadores4T/" + pIdProgramaSectorial + "/" + pOpcion);
        }

        public dynamic getProgramaIndicador(int pIdProgramaSectorial)
        {
            return jsonData("api/V1/mir/programas/" + pIdProgramaSectorial);
        }
        public dynamic getFichasMonitoreo(short iCiclo)
        {
            return jsonData("api/V1/mir/indicadores/fichasMonitoreo/" + iCiclo);
        }
        public dynamic getCiclosFichasMonitoreo()
        {
            return jsonData("api/V1/mir/indicadores/CiclosFichasMonitoreo");
        }
        public dynamic getprogramaFin(int pCiclo, int pRamo, string pUnidad)
        {
            return jsonData("api/V1/mir/indicadores/programaFin/" + pCiclo + "/" + pRamo + "/" + pUnidad);
        }

        public dynamic getContadorIndicadores(int pAnio, int pRamo, string pUnidad, string pPantalla)
        {
            return jsonData("api/V1/mir/programas/" + pAnio + "/" + pRamo + "/" + pUnidad + "/" + pPantalla);
        }
        public dynamic getContadorIndObje(int pIdProgramaSectorial)
        {
            return jsonData("api/V1/mir/indicadores/ContadorIndObj/" + pIdProgramaSectorial);
        }

        public dynamic getContadorIndObje4T(int pIdProgramaSectorial)
        {
            return jsonData("api/V1/mir/indicadores/contadorIndObj4T/" + pIdProgramaSectorial);
        }

        public dynamic getDerechoSocialInd(int pidIndicador)
        {
            return jsonData("api/V1/mir/indicadores/derechoSocialInd/" + pidIndicador);
        }

        public dynamic getDerechoSocialInd4T(int pidIndicador)
        {
            return jsonData("api/V1/mir/indicadores/derechoSocialInd4T/" + pidIndicador);
        }

        public dynamic GetFondosRamo33PorCiclo(int pCiclo)
        {
            return jsonData("api/V1/mir/catalogos/fondosramo33/" + pCiclo);
        }

        public dynamic GetFondoMatrizComponente(int idMatriz)
        {
            return jsonData(string.Format("api/V1/mir/indicadores/componentesPorFondoRamo33/{0}", idMatriz));
        }

        public dynamic GetValoresPorEntidadFondoRamo33(int idIndicador)
        {
            return jsonData(string.Format("api/V1/mir/indicadores/valoresPorEntidadFondoRamo33/{0}", idIndicador));
        }

        public dynamic GetBaseDeDatosFondoRamo33Federal(int Ciclo)
        {
            return jsonData(string.Format("api/V1/mir/indicadores/BaseDeDatosFondoRamo33Federal/{0}", Ciclo));
        }

        public dynamic GetBaseDeDatosFondoRamo33Estatal(int Ciclo)
        {
            return jsonData(string.Format("api/V1/mir/indicadores/BaseDeDatosFondoRamo33Estatal/{0}", Ciclo));
        }

        public dynamic GetBaseDeDatosFondoRamo33Municipal(int Ciclo)
        {
            return jsonData(string.Format("api/V1/mir/indicadores/BaseDeDatosFondoRamo33Municipal/{0}", Ciclo));
        }

        public dynamic GeValorestDesempenoPorEntidadFondoRamo33(int idIndicador)
        {
            return jsonData(string.Format("api/V1/mir/indicadores/valoresDesempenoPorEntidadFondoRamo33/{0}", idIndicador));
        }
    }

}