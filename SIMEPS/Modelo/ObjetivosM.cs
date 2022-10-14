using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Modelo
{
    /// <summary>
    /// Estructura de la tabla TC_OBJETIVO_M
    /// </summary>
    public class ObjetivosM
    {
        /// <summary>
        /// ID del objetivo de cada meta
        /// </summary>
        public int ID_OBJETIVO_M { get; set; }
        /// <summary>
        /// Descripcion del objetivo de cada meta
        /// </summary>
        public string DESC_OBJETIVO { get; set; }
        /// <summary>
        /// ID de la meta del objetivo
        /// </summary>
        public int ID_META { get; set; }
    }
}