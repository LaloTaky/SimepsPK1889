using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Modelo
{
    /// <summary>

    /// <summary>
    /// Modelo de la tabla TD_INDICADORES_ESTR_TRANS de mirDB
    /// </summary>
    public class IndicadoresTrans
    {
        /// <summary>
        /// ID del indicador transversal
        /// </summary>
        public int ID_INDICADOR_ESTR_TRANS { get; set; }
        /// <summary>
        /// ID del indicador transversal anterior
        /// </summary>
        public int? ID_INDICADOR_TRANS_ESTR_ANT { get; set; }
        /// <summary>
        /// Ciclo del indicador transversal
        /// </summary>
        public int CICLO { get; set; }
        /// <summary>
        /// Nombre dle indicador transversal
        /// </summary>
        public string NOMBRE { get; set; }
        /// <summary>
        /// Definicion del indicador transversal
        /// </summary>
        public string DEFINICION { get; set; }
        /// <summary>
        /// Unidad de medida del indicador transversal
        /// </summary>
        public string UNIDAD_MEDIDA { get; set; }
        /// <summary>
        /// Calificacion de claridad del indicador transversal
        /// </summary>
        public bool CALIF_CLARIDAD { get; set; }
        /// <summary>
        /// Calificacion de relevancia del indicador transversal
        /// </summary>
        public bool CALIF_RELEVANCIA { get; set; }
        /// <summary>
        /// calificacion de monitoreabilidad del indicador transversal
        /// </summary>
        public bool CALIF_MONITOREABILIDAD { get; set; }
        /// <summary>
        /// calificacion de la pertenencia del indicador transversal
        /// </summary>
        public bool CALIF_PERTINENCIA { get; set; }
        /// <summary>
        /// Valor alcanzado del indicador transversal
        /// </summary>
        public decimal VALOR_ALCANZADO { get; set; }
        /// <summary>
        /// Categoria de la calidad del indicador transversal
        /// </summary>
        public string CATEGORIA_CALIDAD { get; set; }
        /// <summary>
        /// enfoque del indicador transversal
        /// </summary>
        public string ENFOQUE_INDICADOR { get; set; }
    }



}