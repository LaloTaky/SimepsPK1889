using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Reporting.WebForms;
using SIMEPS.Dal;

namespace SIMEPS.Comun
{
    public class Utils
    {
        public enum IdTiposReportes : int {monitoreo = 1, fichaIndicadores = 2, baseIndicadores = 3, baseIndicadoresCSV = 4,
                                           detalleFichaIndicador = 5, mir = 6, baseIndicadoresFinCSV = 7, baseIndicadoresFin = 8,
                                           baseDeDatosDelPND = 9, fichasTecnicasIndicadores = 10, fichaTecnicaIndicador = 11,
                                           baseDeDatos = 12, baseDeDatosCSV = 13};

        /// <summary>
        /// Obtiene la pantalla a la cual se redirige dependiendo del parametro de navegación
        /// </summary>
        /// <returns></returns>
        public String ObtenerNavegacion(String sNavegacion)
        {
            String sPantalla = "";
            if (sNavegacion != null)
            {
                if (sNavegacion.ToLower().Equals("a"))
                {
                    sPantalla = "Programa.aspx";
                }
                else if (sNavegacion.ToLower().Equals("b") || sNavegacion.ToLower().Equals("c"))
                {
                    sPantalla = "MIR.aspx";
                }
                else
                {
                    sPantalla = "Programa.aspx";
                }
            }
            else
            {
                sPantalla = "Programa.aspx";
            }
            return sPantalla;
        }

        /// <summary>
        /// Descarga el archivo desde el ReportServer
        /// </summary>
        /// <param name="Report">Control ReportViewer</param>
        /// <param name="Formato">Formato del archivo a exportar ejemplo: "Excel"</param>
        /// <param name="extension">Extension del archivo a exportar ejemplo: xls</param>
        /// <param name="fileName">Nombre del archivo a exportar</param>
        public void DescargaArchivoRS(HttpResponse response, ReportViewer Report, string Formato, string extension, string fileName, string sPath, string nombreArchivo, string sTipoReporte, short? shCicloReporte, int? iRamoRepore, string sModalidadReporte, int? iClaveReporte, string sOpcion, int? iIdIndicador, int? iIdProgramaSectorial, int? iIdMatriz)
        {
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Microsoft.Reporting.WebForms.Warning[] warnings;
            ReportesDal nuevoReporte = new ReportesDal("TD_RESOURCE_STORE");
            byte[] Content = Report.ServerReport.Render(Formato, null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            nuevoReporte.guardarDocumento(sPath, nombreArchivo, Content, sTipoReporte, shCicloReporte, iRamoRepore, sModalidadReporte, iClaveReporte, iIdIndicador, iIdProgramaSectorial, iIdMatriz);
            if (sOpcion.ToLower() != "actualiza" && sOpcion.ToLower() != "genera")
            {
                response.Clear();
                response.Cookies["fileDownloadToken"].Value = "true";
                response.HeaderEncoding = System.Text.Encoding.Default;
                response.ContentType = "application/" + extension;
                response.AddHeader("Content-disposition", "attachment; filename= " + nombreArchivo);
                response.BinaryWrite(Content);
                response.End();
            }
        }
    }
}
