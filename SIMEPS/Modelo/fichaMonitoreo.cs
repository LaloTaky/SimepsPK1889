using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Modelo
{
    public class FichaMonitoreo
    {
        public short CICLO { get; set; }
        public int ID_PROG_SECTORIAL { get; set; }
        public string URL_PORTADA { get; set; }
        public string URL_FICHA { get; set; }
        public string COLOR_CICLO { get; set; }
    }
}