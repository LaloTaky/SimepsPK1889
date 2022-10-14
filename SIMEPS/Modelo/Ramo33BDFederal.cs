using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Modelo
{
    public class Ramo33BDFederal
    {
        public short? Ciclo { get; set; }
        public string Modalidad { get; set; }
        public int Clave { get; set; }
        public string Fondo { get; set; }
        public int Nivel { get; set; }
        public string Objetivo { get; set; }
        public string Indicador { get; set; }
        public string Definicion { get; set; }
        public string MetodoCalculo { get; set; }
        public string FrecuenciaMedicion { get; set; }
        public string UnidadMedida { get; set; }
        public string SentidoIndicador { get; set; }
        public short? CicloLineaBase { get; set; }
        public decimal? LineaBase { get; set; }
        public decimal? MetaRelPlaneada { get; set; }
        public decimal? MetaAbsPlaneada { get; set; }
        public decimal? MetaRelAlcanzada { get; set; }
        public decimal? MetaAbsAlcanzada { get; set; }
    }
}