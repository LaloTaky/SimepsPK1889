using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;
//using ModeloDatos.Dal;
//using ModeloDatos;
using System.IO;
using System.Configuration;
using System.Data.SqlTypes;
using SIMEPS.Comun;

namespace SIMEPS.Dal
{
    public class ReportesDal
    {
        private string simepsConn = ConfigurationManager.ConnectionStrings["con_simepsDB"].ConnectionString;
        Logger log = new Logger();
        string fileTable;
        public ReportesDal(string nameFileTable)
        {
            fileTable = nameFileTable;
        }


        /// <summary>
        ///Consulta la ruta raíz de un File Table 
        /// </summary>
        /// <param name="nombreFileTable">Nombre de File Table</param>
        /// <returns>String con ruta raíz de File Table</returns>
        public string consultaRutaBase(string nombreFileTable)
        {
            var res = (object)null;
            using (SqlConnection conexionBD = new SqlConnection(simepsConn))
            {
                try
                {
                    conexionBD.Open();

                    if (conexionBD.State == ConnectionState.Open)
                    {
                        string chainSql = "SELECT FileTableRootPath('" + nombreFileTable + "')";
                        using (SqlCommand cmd = new SqlCommand(chainSql, conexionBD))
                        {
                            res = cmd.ExecuteScalar();
                        }
                    }
                }
                catch (Exception e)
                {
                    log.LogMessageToFile(e.ToString());
                }
                finally
                {
                    if ((conexionBD != null) && (conexionBD.State == ConnectionState.Open))
                    {
                        conexionBD.Close();
                    }
                }
            }
            return Convert.ToString(res);
        }
        //consulta Locator
        /// <summary>
        /// Devuelve el valor del ID del localizador  de ruta para el archivo o directorio especificado
        /// </summary>
        /// <param name="rutaCompleta">Ruta del directorio o archivo </param>
        /// <returns>String con cadena del localizador de ruta </returns>
        public string consultaLocator(string rutaCompleta)
        {
            var res = (object)null;
            using (SqlConnection conexionBD = new SqlConnection(simepsConn))
            {
                try
                {
                    conexionBD.Open();
                    if (conexionBD.State == ConnectionState.Open)
                    {
                        string chainSql = " SELECT GETPATHLOCATOR('" + rutaCompleta + "').ToString() AS PL";
                        using (SqlCommand cmd = new SqlCommand(chainSql, conexionBD))
                        {
                            res = cmd.ExecuteScalar();
                        }
                    }
                }
                catch (Exception e)
                {
                    log.LogMessageToFile(e.ToString());
                }
                finally
                {
                    if ((conexionBD != null) && (conexionBD.State == ConnectionState.Open))
                    {
                        conexionBD.Close();
                    }
                }

            }
            return Convert.ToString(res);
        }
        //GeneraRuta
        /// <summary>
        /// Genera nueva ruta para archivo o directorio
        /// </summary>
        /// <param name="rutaLocator">Ruta padre donde se va a generar el archivo o directorio </param>
        /// <returns>String con ruta del archivo o directorio  </returns>
        public string generaRuta(string rutaLocator)
        {
            var res = (object)null;
            using (SqlConnection conexionBD = new SqlConnection(simepsConn))
            {
                try
                {
                    conexionBD.Open();
                    if (conexionBD.State == ConnectionState.Open)
                    {
                        string chainSql = "SELECT '" + rutaLocator + "' + "
                        + " CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), NEWID()), 1, 6))) + '.' + "
                        + " CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), NEWID()), 7, 6))) + '.' + "
                        + " CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), NEWID()), 13, 4))) + '/' ";
                        using (SqlCommand cmd = new SqlCommand(chainSql, conexionBD))
                        {
                            res = cmd.ExecuteScalar();
                        }
                    }
                }
                catch (Exception e)
                {
                    log.LogMessageToFile(e.ToString());
                }
                finally
                {
                    if ((conexionBD != null) && (conexionBD.State == ConnectionState.Open))
                    {
                        conexionBD.Close();
                    }
                }

            }
            return Convert.ToString(res);
        }
        /// <summary>
        /// Guarda archivo en File Table
        /// </summary>
        /// <param name="sFileStream">Arreglo de bytes del archivo a guardar</param>
        /// <param name="sNombreArchivo">Nombre del archivo</param>
        /// <param name="sPathLocator"> Cadena locator de carpeta padre </param>
        /// <returns>Bool Estado al guardar el archivo</returns>
        public bool guardarArchivo(byte[] sFileStream, string sNombreArchivo, string sPathLocator)
        {
            bool resultado = false;
            using (SqlConnection conexionBD = new SqlConnection(simepsConn))
            {
                try
                {
                    conexionBD.Open();
                    if (conexionBD.State == ConnectionState.Open)
                    {
                        using (SqlTransaction transaccion = conexionBD.BeginTransaction())
                        {
                            string cadSql = " INSERT INTO " + fileTable + " " +
                                            " (file_stream , name  , path_locator , is_archive) OUTPUT INSERTED.[stream_id] " +
                                            " VALUES( @fileStream, @nombreArchivo, '" + sPathLocator + "'" +
                                            "+ CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), NEWID()), 1, 6))) + '.' + " +
                                            "  CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), NEWID()), 7, 6))) + '.' + " +
                                            "  CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), NEWID()), 13, 4))) + '/' " +
                                            " , 1 ) ";
                            using (SqlCommand cmd = new SqlCommand(cadSql, conexionBD, transaccion))
                            {
                                cmd.Parameters.AddWithValue("@fileStream", sFileStream);
                                cmd.Parameters.AddWithValue("@nombreArchivo", sNombreArchivo);
                                cmd.ExecuteNonQuery();
                                resultado = true;
                                transaccion.Commit();
                            }

                            string[] subNombre = sNombreArchivo.Split('_');
                            string tipoReporte = subNombre[0] + " " + subNombre[1];
                            string ciclo = subNombre[2];
                            string ramo = subNombre[3];
                            string modalidad = subNombre[4];
                            string clave = subNombre[5];
                            var id_tipoReporte = (object)null; ;

                            //Se obtiene el ID de el tipo de reporte.
                            string idsql = "SELECT ID_TIPO_REPORTE FROM TC_TIPOS_REPORTE WHERE NOMBRE LIKE '%@pTipoNombre%'";
                            using (SqlCommand cmd1 = new SqlCommand(idsql, conexionBD, transaccion))
                            {
                                cmd1.Parameters.AddWithValue("@pTipoNombre", tipoReporte);
                                id_tipoReporte = cmd1.ExecuteScalar();
                            }

                            ////Se guarda el reporte en su tabla de relacion.
                            //string trsql = "INSERT INTO TR_REPORTES_PROGRAMAS (CICLO, RAMO, MODALIDAD, CLAVE, path_locator, ID_TIPO_REPORTE)" +
                            //                "VALUES( @pCiclo, @pRamo, @pModalidad, @pClave, '" + sPathLocator + "'" +
                            //                "+ CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), NEWID()), 1, 6))) + '.' + " +
                            //                "  CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), NEWID()), 7, 6))) + '.' + " +
                            //                "  CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), NEWID()), 13, 4))) + '/' " +
                            //                "@pTipoReporte";
                            //using (SqlCommand cmd = new SqlCommand(trsql, conexionBD, transaccion))
                            //{
                            //    cmd.Parameters.AddWithValue("@pCiclo", ciclo);
                            //    cmd.Parameters.AddWithValue("@pRamo", ramo);
                            //    cmd.Parameters.AddWithValue("@pModalidad", modalidad);
                            //    cmd.Parameters.AddWithValue("@pClave", clave);
                            //    cmd.Parameters.AddWithValue("@pTipoReporte", id_tipoReporte);
                            //    cmd.ExecuteNonQuery();
                            //    resultado = true;
                            //    transaccion.Commit();
                            //}
                        }
                    }
                }
                catch (Exception e)
                {
                    log.LogMessageToFile(e.ToString());
                    //if (sParametroOpcion.ToLower() == "actualiza" || sParametroOpcion.ToLower() == "genera")
                    //{
                    //    Response.Clear();
                    //    Response.ClearHeaders();
                    //    Response.ClearContent();
                    //    Response.HeaderEncoding = System.Text.Encoding.Default;
                    //    Response.Charset = "ISO-8859-15";
                    //    Response.Status = "500 INTERNAL SERVER ERROR";
                    //    Response.StatusCode = 500;
                    //    Response.StatusDescription = "INTERNAL SERVER ERROR";
                    //    Response.End();
                    //}
                }
                finally
                {
                    if ((conexionBD != null) && (conexionBD.State == ConnectionState.Open))
                    {
                        conexionBD.Close();
                    }
                }
            }
            return resultado;
        }
        /// <summary>
        /// Recupera un archivo de un File Table
        /// </summary>
        /// <param name="nombre">Nombre del Archivo </param>
        /// <returns>Byte[] con el arreglo de bytes del archivo</returns>
        public byte[] recuperarDocumento(string nombre)
        {

            var res = (object)null;
            using (SqlConnection conexionBD = new SqlConnection(simepsConn))
            {
                try
                {
                    conexionBD.Open();

                    string[] nombreVersion = nombre.Split('.');
                    if (nombreVersion[1].ToLower() != "csv")
                    {
                        if (conexionBD.State == ConnectionState.Open)
                        {
                            //string chainSql = "SELECT file_stream from dbo.TD_RESOURCE_STORE where name='" + nombre + "'";
                            string chainSql = "SELECT file_stream from " + fileTable + " where name='" + nombreVersion[0] + ".xls' or name='" + nombreVersion[0] + ".xlsx'";
                            using (SqlCommand cmd = new SqlCommand(chainSql, conexionBD))
                            {
                                res = cmd.ExecuteScalar();
                            }
                        }
                    }
                    else
                    {
                        if (conexionBD.State == ConnectionState.Open)
                        {
                            //string chainSql = "SELECT file_stream from dbo.TD_RESOURCE_STORE where name='" + nombre + "'";
                            string chainSql = "SELECT file_stream from " + fileTable + " where name='" + nombre + "'";
                            using (SqlCommand cmd = new SqlCommand(chainSql, conexionBD))
                            {
                                res = cmd.ExecuteScalar();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    log.LogMessageToFile(e.ToString());
                }
                finally
                {
                    if ((conexionBD != null) && (conexionBD.State == ConnectionState.Open))
                    {
                        conexionBD.Close();
                    }
                }
            }
            return (byte[])res;
        }
        /// <summary>
        /// Guarda un directorio dentro de un File Table
        /// </summary>
        /// <param name="sNombreDirectorio">Nombre de Directorio</param>
        /// <param name="sPathLocator">Cadena locator de Directorio</param>
        /// <returns>Bool estado con el estado al guardar Documento</returns>
        public bool guardarDirectorio(string sNombreDirectorio, string sPathLocator)
        {
            bool resultado = false;
            using (SqlConnection conexionBD = new SqlConnection(simepsConn))
            {
                try
                {
                    conexionBD.Open();
                    if (conexionBD.State == ConnectionState.Open)
                    {
                        using (SqlTransaction transaccion = conexionBD.BeginTransaction())
                        {
                            /*string cadSql = " INSERT INTO [dbo].[TD_RESOURCE_STORE] (name,  path_locator,is_directory,is_archive) " +
                                            " SELECT @campo, @newPath, 1 , 0 ";
                            */
                            string cadSql = " INSERT INTO " + fileTable + " (name,  path_locator,is_directory,is_archive) " +
                                            " SELECT @campo, @newPath, 1 , 0 ";
                            using (SqlCommand cmd = new SqlCommand(cadSql, conexionBD, transaccion))
                            {
                                cmd.Parameters.AddWithValue("@campo", sNombreDirectorio);
                                cmd.Parameters.AddWithValue("@newPath", sPathLocator);
                                cmd.ExecuteNonQuery();
                                resultado = true;
                                transaccion.Commit();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    log.LogMessageToFile(e.ToString());
                }
                finally
                {
                    if ((conexionBD != null) && (conexionBD.State == ConnectionState.Open))
                    {
                        conexionBD.Close();
                    }
                }
            }
            return resultado;
        }

        /// <summary>
        /// Borrara el reporte de filetable junto con el registro en la tabla de relación.
        /// </summary>
        /// <param name="nombreReporte">Nombre del reporte que se va a borrar</param>
        /// <param name="sTipoReporte">Tipo de reporte para borrarlo de la tabla de relacion correspondiente</param>
        public void BorrarReporte(string nombreReporte, string sTipoReporte)
        {
            var no = (object)null;
            using (SqlConnection conexionBD = new SqlConnection(simepsConn))
            {
                try
                {
                    conexionBD.Open();

                    string tabla = "";
                    var stream_id = (object)null;

                    if (conexionBD.State == ConnectionState.Open)
                    {
                        
                        string[] nombreVersion = nombreReporte.Split('.');
                        if (nombreVersion[1].ToLower() != "csv")
                        {
                            string chainSqlPath = "Select stream_id FROM " + fileTable + " where name='" + nombreVersion[0] + ".xls' or name='" + nombreVersion[0] + ".xlsx'";
                            using (SqlCommand cmd = new SqlCommand(chainSqlPath, conexionBD))
                            {
                                stream_id = cmd.ExecuteScalar();
                            }
                        }
                        else
                        {
                            //obtener el stream_id
                            string chainSqlPath = "Select stream_id FROM " + fileTable + " where name='" + nombreReporte + "'";
                            using (SqlCommand cmd = new SqlCommand(chainSqlPath, conexionBD))
                            {
                                stream_id = cmd.ExecuteScalar();
                            }
                        }
                        if (stream_id != null)
                        {


                            //Borra el registro de la tabla de relacion
                            if (sTipoReporte == "1" || sTipoReporte == "2"
                                || sTipoReporte == "3" || sTipoReporte == "4"
                                || sTipoReporte == "6")
                            {
                                tabla = "TR_REPORTES_PROGRAMAS";
                                string chainsqlRelacion = "DELETE FROM " + tabla + " where path_locator = (SELECT path_locator FROM TD_RESOURCE_STORE WHERE stream_id = @pStreamId)";
                                using (SqlCommand cmd = new SqlCommand(chainsqlRelacion, conexionBD))
                                {
                                    cmd.Parameters.AddWithValue("@pStreamId",stream_id);
                                    no = cmd.ExecuteScalar();
                                }
                            }
                            else if (sTipoReporte == "5")
                            {
                                tabla = "TR_REPORTES_INDICADORES";
                                string chainsqlRelacion = "DELETE FROM " + tabla + " where path_locator = (SELECT path_locator FROM TD_RESOURCE_STORE WHERE stream_id = @pStreamId)";
                                using (SqlCommand cmd = new SqlCommand(chainsqlRelacion, conexionBD))
                                {
                                    cmd.Parameters.AddWithValue("@pStreamId", stream_id);
                                    no = cmd.ExecuteScalar();
                                }
                            }
                            else if (sTipoReporte == "11")
                            {
                                tabla = "TR_REPORTES_INDICADORES_SEC";
                                string chainsqlRelacion = "DELETE FROM " + tabla + " where path_locator = (SELECT path_locator FROM TD_RESOURCE_STORE WHERE stream_id = @pStreamId)";
                                using (SqlCommand cmd = new SqlCommand(chainsqlRelacion, conexionBD))
                                {
                                    cmd.Parameters.AddWithValue("@pStreamId", stream_id);
                                    no = cmd.ExecuteScalar();
                                }
                            }
                            else if (sTipoReporte == "10" || sTipoReporte == "12"
                                     || sTipoReporte == "13")
                            {
                                tabla = "TR_REPORTES_PROGRAMAS_SEC";
                                string chainsqlRelacion = "DELETE FROM " + tabla + " where path_locator = (SELECT path_locator FROM TD_RESOURCE_STORE WHERE stream_id = @pStreamId)";
                                using (SqlCommand cmd = new SqlCommand(chainsqlRelacion, conexionBD))
                                {
                                    cmd.Parameters.AddWithValue("@pStreamId", stream_id);
                                    no = cmd.ExecuteScalar();
                                }
                            }
                            else if (sTipoReporte == "7" || sTipoReporte == "8" || sTipoReporte == "9")
                            {
                                tabla = "TR_REPORTES_GENERALES";
                                string chainsqlRelacion = "DELETE FROM " + tabla + " where path_locator = (SELECT path_locator FROM TD_RESOURCE_STORE WHERE stream_id = @pStreamId)";
                                using (SqlCommand cmd = new SqlCommand(chainsqlRelacion, conexionBD))
                                {
                                    cmd.Parameters.AddWithValue("@pStreamId", stream_id);
                                    no = cmd.ExecuteScalar();
                                }
                            }
                            //Borra el registro de la tabla de reportes
                            string chainSql = "DELETE FROM " + fileTable + " where stream_id = @pStreamId";
                            using (SqlCommand cmd = new SqlCommand(chainSql, conexionBD))
                            {
                                cmd.Parameters.AddWithValue("@pStreamId", stream_id);
                                no = cmd.ExecuteScalar();
                            }
                        }
                        else
                        {
                            string[] nombreVersion1 = nombreReporte.Split('.');
                            if (nombreVersion[1].ToLower() != "csv")
                            {
                                string chainSqlPath = "delete from " + fileTable + " where name='" + nombreVersion1[0] + ".xls' or name='" + nombreVersion1[0] + ".xlsx'";
                                using (SqlCommand cmd = new SqlCommand(chainSqlPath, conexionBD))
                                {
                                    no = cmd.ExecuteScalar();
                                }
                            }
                            else
                            {
                                //obtener el path_locator
                                string chainSqlPath = "delete FROM " + fileTable + " where name='" + nombreReporte + "'";
                                using (SqlCommand cmd = new SqlCommand(chainSqlPath, conexionBD))
                                {
                                    no = cmd.ExecuteScalar();
                                }
                            }
                        }


                    }
                }
                catch (Exception e)
                {
                    log.LogMessageToFile(e.ToString());

                    throw new Exception(e.Message, e.InnerException);

                }
                finally
                {
                    if ((conexionBD != null) && (conexionBD.State == ConnectionState.Open))
                    {
                        conexionBD.Close();
                    }
                }

            }
        }

        /// <summary>
        /// Guarda el archivo en filetable y crea la relacion en su tabla correspondiente.
        /// </summary>
        /// <param name="sFileStream">Es el archivo que se desea guardar.</param>
        /// <param name="sNombreArchivo">Nombre que tendra el archivo.</param>
        /// <param name="sPathLocator">Ubicacion del archivo en la base de datos.</param>
        /// <param name="sTipoReporte">Tipo de reporte que se guardara.</param>
        /// <param name="shCicloReporte">Ciclo del reporte en el caso de ser necesario.</param>
        /// <param name="iRamoRepore">Ramo del reporte en el caso de ser necesario.</param>
        /// <param name="sModalidadReporte">Modalidad del reporte en el caso de ser necesario.</param>
        /// <param name="iClaveReporte">Clave del reporte en el caso de ser necesario.</param>
        /// <param name="iIdIndicador">Indicador del reporte en el caso de ser necesario.</param>
        /// <param name="iIdProgramaSectorial">Programa sectorial del reporte en el caso de ser necesario.</param>
        /// <param name="iMatriz">Matriz del reporte en el caso de ser necesario.</param>
        /// <returns>Regresa un boleano en base a si fue exitoso (true) o fallido (false).</returns>
        public bool guardarArchivo(byte[] sFileStream, string sNombreArchivo, string sPathLocator, string sTipoReporte, short? shCicloReporte, int? iRamoRepore, string sModalidadReporte, int? iClaveReporte, int? iIdIndicador, int? iIdProgramaSectorial, int? iMatriz)
        {
            bool resultado = false;
            var stream_id = (object)null;
            int id_tipoReporte = Convert.ToInt32(sTipoReporte);
            using (SqlConnection conexionBD = new SqlConnection(simepsConn))
            {
                try
                {
                    conexionBD.Open();
                    if (conexionBD.State == ConnectionState.Open)
                    {

                        using (SqlTransaction transaccion = conexionBD.BeginTransaction())
                        {
                            //PK1348
                            string query = string.Format("select count(path_locator) from  TD_RESOURCE_STORE where name like '%{0}'", sNombreArchivo);
                            //PK1348

                            //log.LogMessageToFile(string.Format("guardarArchivo::{0}", query));

                            Object count = null;

                            using (SqlCommand cmd = new SqlCommand(query, conexionBD, transaccion))
                            {
                                count = cmd.ExecuteScalar();
                            }

                            if (count.ToString() == "0")
                            {
                                string cadSql = " INSERT INTO " + fileTable + " " +
                                        " (file_stream , name  , path_locator , is_archive) OUTPUT INSERTED.[stream_id] " +
                                        " VALUES( @fileStream, @nombreArchivo, '" + sPathLocator + "'" +
                                        "+ CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), NEWID()), 1, 6))) + '.' + " +
                                        "  CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), NEWID()), 7, 6))) + '.' + " +
                                        "  CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), NEWID()), 13, 4))) + '/' " +
                                        " , 1 ) ";


                                using (SqlCommand cmd = new SqlCommand(cadSql, conexionBD, transaccion))
                                {
                                    cmd.Parameters.AddWithValue("@fileStream", sFileStream);
                                    cmd.Parameters.AddWithValue("@nombreArchivo", sNombreArchivo);
                                    stream_id = cmd.ExecuteScalar();
                                    transaccion.Commit();
                                    resultado = true;
                                }

                                //Se guarda el reporte en su tabla de relacion.
                                if (sTipoReporte == "1" || sTipoReporte == "2"
                                || sTipoReporte == "3" || sTipoReporte == "4"
                                || sTipoReporte == "6")
                                {
                                    string trsql = "INSERT INTO TR_REPORTES_PROGRAMAS (CICLO, RAMO, MODALIDAD, CLAVE, ID_MATRIZ, path_locator, ID_TIPO_REPORTE)" +
                                                "SELECT @pCiclo, @pRamo, @pModalidad, @pClave, @pMatriz, path_locator, @pTipoReporte FROM TD_RESOURCE_STORE WHERE stream_id = @pStreamId";
                                    using (SqlCommand cmd = new SqlCommand(trsql, conexionBD, transaccion))
                                    {
                                        cmd.Parameters.AddWithValue("@pStreamId", stream_id);
                                        cmd.Parameters.AddWithValue("@pCiclo", shCicloReporte);
                                        cmd.Parameters.AddWithValue("@pRamo", iRamoRepore);
                                        cmd.Parameters.AddWithValue("@pModalidad", sModalidadReporte);
                                        cmd.Parameters.AddWithValue("@pClave", iClaveReporte);
                                        cmd.Parameters.AddWithValue("@pMatriz", iMatriz);
                                        cmd.Parameters.AddWithValue("@pTipoReporte", id_tipoReporte);
                                        cmd.ExecuteNonQuery();
                                        resultado = true;
                                    }
                                }
                                else if (sTipoReporte == "5")
                                {
                                    var id_nivel = (object)null;
                                    string tdsql = "SELECT ID_NIVEL FROM TD_INDICADORES WHERE ID_INDICADOR = @pIndicador";
                                    using (SqlCommand cmd = new SqlCommand(tdsql, conexionBD, transaccion))
                                    {
                                        cmd.Parameters.AddWithValue("@pIndicador", iIdIndicador);
                                        id_nivel = cmd.ExecuteScalar();
                                    }

                                    string trsql = "INSERT INTO TR_REPORTES_INDICADORES (ID_INDICADOR, ID_NIVEL, ID_TIPO_REPORTE, path_locator)" +
                                                       "SELECT @pId_indicador, @pId_nivel, @pId_tipo_reporte, path_locator FROM TD_RESOURCE_STORE WHERE stream_id = @pStreamId";
                                    using (SqlCommand cmd = new SqlCommand(trsql, conexionBD, transaccion))
                                    {
                                        cmd.Parameters.AddWithValue("@pStreamId", stream_id);
                                        cmd.Parameters.AddWithValue("@pId_indicador", iIdIndicador);
                                        cmd.Parameters.AddWithValue("@pId_nivel", id_nivel);
                                        cmd.Parameters.AddWithValue("@pId_tipo_reporte", id_tipoReporte);
                                        cmd.ExecuteNonQuery();
                                        resultado = true;
                                    }
                                }
                                else if (sTipoReporte == "11")
                                {
                                    //PK1348
                                    string trsql = "INSERT INTO TR_REPORTES_INDICADORES_SEC (ID_TIPO_REPORTE, ID_INDICADOR, path_locator)" +
                                                  "SELECT @pId_tipo_reporte, @pId_indicador , path_locator FROM TD_RESOURCE_STORE WHERE stream_id = @pStreamId";
                                    //PK1348

                                    using (SqlCommand cmd = new SqlCommand(trsql, conexionBD, transaccion))
                                    {
                                        cmd.Parameters.AddWithValue("@pStreamId", stream_id);
                                        cmd.Parameters.AddWithValue("@pId_tipo_reporte", id_tipoReporte);
                                        cmd.Parameters.AddWithValue("@pId_indicador", iIdIndicador);
                                        cmd.ExecuteNonQuery();
                                        resultado = true;
                                    }
                                }
                                else if (sTipoReporte == "10" || sTipoReporte == "12"
                                     || sTipoReporte == "13")
                                {
                                    string trsql = "INSERT INTO TR_REPORTES_PROGRAMAS_SEC (ID_TIPO_REPORTE, ID_PROG_SECTORIAL, path_locator)" +
                                                   "SELECT @pId_tipo_reporte, @pId_programa_sectorial , path_locator FROM TD_RESOURCE_STORE WHERE stream_id = @pStreamId";
                                    using (SqlCommand cmd = new SqlCommand(trsql, conexionBD, transaccion))
                                    {
                                        cmd.Parameters.AddWithValue("@pStreamId", stream_id);
                                        cmd.Parameters.AddWithValue("@pId_tipo_reporte", id_tipoReporte);
                                        cmd.Parameters.AddWithValue("@pId_programa_sectorial", iIdProgramaSectorial);
                                        cmd.ExecuteNonQuery();
                                        resultado = true;
                                    }
                                }
                                else if (sTipoReporte == "7" || sTipoReporte == "8" || sTipoReporte == "9")
                                {
                                    if (shCicloReporte != null)
                                    {
                                        string trsql = "INSERT INTO TR_REPORTES_GENERALES (ID_TIPO_REPORTE, path_locator, CICLO)" +
                                                       "SELECT @pId_tipo_reporte, path_locator, @pCiclo FROM TD_RESOURCE_STORE WHERE stream_id = @pStreamId";
                                        using (SqlCommand cmd = new SqlCommand(trsql, conexionBD, transaccion))
                                        {
                                            cmd.Parameters.AddWithValue("@pStreamId", stream_id);
                                            cmd.Parameters.AddWithValue("@pId_tipo_reporte", id_tipoReporte);
                                            cmd.Parameters.AddWithValue("@pCiclo", shCicloReporte);
                                            cmd.ExecuteNonQuery();
                                            resultado = true;
                                        }
                                    }
                                    else
                                    {
                                        string trsql = "INSERT INTO TR_REPORTES_GENERALES (ID_TIPO_REPORTE, path_locator, CICLO)" +
                                                       "SELECT @pId_tipo_reporte, path_locator ,null FROM TD_RESOURCE_STORE WHERE stream_id = @pStreamId";
                                        using (SqlCommand cmd = new SqlCommand(trsql, conexionBD, transaccion))
                                        {
                                            cmd.Parameters.AddWithValue("@pStreamId", stream_id);
                                            cmd.Parameters.AddWithValue("@pId_tipo_reporte", id_tipoReporte);
                                            cmd.ExecuteNonQuery();
                                            resultado = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                log.LogMessageToFile(string.Format("guardarArchivo::"));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    log.LogMessageToFile(e.ToString());

                    throw new Exception(e.Message, e.InnerException);
                }
                finally
                {
                    if ((conexionBD != null) && (conexionBD.State == ConnectionState.Open))
                    {
                        conexionBD.Close();
                    }
                }
            }
            return resultado;
        }

        /// <summary>
        /// Genera y guarda el path_locator de el archivo que se va a guardar y llama el metodo de guardado de archivo.
        /// </summary>
        /// <param name="sPath">Ruta completa de directorio</param>
        /// <param name="nombreArchivo">Nombre del archivo</param>
        /// <param name="ubicacionDocumento">Ubicación local del documento</param>
        /// <param name="sTipoReporte">Tipo de reporte que se va a guardar</param>
        /// <param name="shCicloReporte">Ciclo de reporte en el caso de ser necesario.</param>
        /// <param name="iRamoRepore">Ramo del reporte en el caso de ser necesario.</param>
        /// <param name="sModalidadReporte">Modalidad del reporte en el caso de ser necesario.</param>
        /// <param name="iClaveReporte">Clave del reporte en el caso de ser necesario.</param>
        /// <param name="iIdIndicador">Indicador del reporte en el caso de ser necesario.</param>
        /// <param name="iIdProgramaSectorial">Programa sectorial del reporte en el caso de ser necesario.</param>
        /// <param name="iMatriz">Matriz del reporte en el caso de ser necesario.</param>
        /// <returns>Retorna un valor boleano que determina si fue exitoso (true) o fallido (false) el guardado.</returns>
        public bool guardarDocumento(string sPath, string nombreArchivo, string ubicacionDocumento, string sTipoReporte, short? shCicloReporte, int? iRamoRepore, string sModalidadReporte, int? iClaveReporte, int? iIdIndicador, int? iIdProgramaSectorial, int? iMatriz)
        {
            bool resultado = false;
            string sIdRecurso = null;
            byte[] fileStream = File.ReadAllBytes(ubicacionDocumento);
            if (sPath != null && !"".Equals(sPath) && fileStream != null)
            {
                string[] aDirectorios = sPath.Split('\\');
                string sfullPath = null;
                string sNewPath = null;
                string sPathLocator = null;
                if (aDirectorios.Length > 0)
                {
                    //Obtener la ruta completa del File Table
                    sfullPath = consultaRutaBase("TD_RESOURCE_STORE");
                    //Obtener el Path Locator de la raiz
                    sPathLocator = consultaLocator(sfullPath);
                    foreach (var sdirectorio in aDirectorios)
                    {
                        string sPathToVerify = sfullPath + "\\" + sdirectorio;
                        string sFullPathAux = consultaLocator(sPathToVerify);
                        if (sFullPathAux != "")
                        {
                            sfullPath = sPathToVerify;
                            sPathLocator = sFullPathAux;
                        }
                        else
                        {
                            //Obtener en sNewPath y guardar  un nuevo registro con el nombre y el sNewPath  como path locator
                            sNewPath = generaRuta(sPathLocator);
                            //Guardar el nuevo directorio
                            resultado = guardarDirectorio(sdirectorio, sNewPath);
                            //Obtener el pathLocator
                            sPathLocator = sNewPath;
                            sfullPath = sPathToVerify;
                        }
                    }
                }
            }
            string sPathLocatorExpression = null;
            if (sPath == null || "".Equals(sPath))
                sPathLocatorExpression = consultaLocator(consultaRutaBase("TD_RESOURCE_STORE"));  //"FileTableRootPath('TD_RESOURCE_STORE')";
            else
            {
                string rutaRaizLocator = consultaRutaBase("TD_RESOURCE_STORE") + "\\" + sPath;
                sPathLocatorExpression = consultaLocator(rutaRaizLocator);
            }
            bool res = guardarArchivo(fileStream, nombreArchivo, sPathLocatorExpression, sTipoReporte, shCicloReporte, iRamoRepore, sModalidadReporte, iClaveReporte, iIdIndicador, iIdProgramaSectorial, iMatriz);
            return res;
        }

        /// <summary>
        /// Guardado del path y del archivo
        /// </summary>
        /// <param name="sPath">Path compuesto por las carpertas de la ubicacion del archivo.</param>
        /// <param name="nombreArchivo">Nombre del archivo que se va a guardar.</param>
        /// <param name="fileStream">Archivo que se va a guardar.</param>
        /// <param name="sTipoReporte">Tipo de reporte que se va a guardar.</param>
        /// <param name="shCicloReporte">Ciclo del reporte en el caso de ser necesario.</param>
        /// <param name="iRamoRepore">Ramo del reporte en el caso de ser necesario.</param>
        /// <param name="sModalidadReporte">Modalidad del reporte en el caso de ser necesario.</param>
        /// <param name="iClaveReporte">Clave del reporte en el caso de ser necesario.</param>
        /// <param name="iIdIndicador">Indicador del reporte en el caso de ser necesario.</param>
        /// <param name="iIdProgramaSectorial">Programa sectorial del reporte en el caso de ser necesario.</param>
        /// <param name="iMatriz">Matriz del reporte en el caso de ser necesario.</param>
        /// <returns>Retorna un valor boleano que determina si fue exitoso (true) o fallido (false) el guardado.</returns>
        public bool guardarDocumento(string sPath, string nombreArchivo, byte[] fileStream, string sTipoReporte, short? shCicloReporte, int? iRamoRepore, string sModalidadReporte, int? iClaveReporte, int? iIdIndicador, int? iIdProgramaSectorial, int? iMatriz)
        {
            bool resultado = false;
            string sIdRecurso = null;
            //byte[] fileStream = File.ReadAllBytes(ubicacionDocumento);
            if (sPath != null && !"".Equals(sPath) && fileStream != null)
            {
                string[] aDirectorios = sPath.Split('\\');
                string sfullPath = null;
                string sNewPath = null;
                string sPathLocator = null;
                if (aDirectorios.Length > 0)
                {
                    //Obtener la ruta completa del File Table
                    sfullPath = consultaRutaBase("TD_RESOURCE_STORE");
                    //Obtener el Path Locator de la raiz
                    sPathLocator = consultaLocator(sfullPath);
                    foreach (var sdirectorio in aDirectorios)
                    {
                        string sPathToVerify = sfullPath + "\\" + sdirectorio;
                        string sFullPathAux = consultaLocator(sPathToVerify);
                        if (sFullPathAux != "")
                        {
                            sfullPath = sPathToVerify;
                            sPathLocator = sFullPathAux;
                        }
                        else
                        {
                            //Obtener en sNewPath y guardar  un nuevo registro con el nombre y el sNewPath  como path locator
                            sNewPath = generaRuta(sPathLocator);
                            //Guardar el nuevo directorio
                            resultado = guardarDirectorio(sdirectorio, sNewPath);
                            //Obtener el pathLocator
                            sPathLocator = sNewPath;
                            sfullPath = sPathToVerify;
                        }
                    }
                }
            }
            string sPathLocatorExpression = null;
            if (sPath == null || "".Equals(sPath))
                sPathLocatorExpression = consultaLocator(consultaRutaBase("TD_RESOURCE_STORE"));  //"FileTableRootPath('TD_RESOURCE_STORE')";
            else
            {
                string rutaRaizLocator = consultaRutaBase("TD_RESOURCE_STORE") + "\\" + sPath;
                sPathLocatorExpression = consultaLocator(rutaRaizLocator);
            }
            bool res = guardarArchivo(fileStream, nombreArchivo, sPathLocatorExpression, sTipoReporte, shCicloReporte, iRamoRepore, sModalidadReporte, iClaveReporte, iIdIndicador, iIdProgramaSectorial, iMatriz);
            return res;
        }

        /// <summary>
        /// Busca el reporte en la tabla de relacion correspondiente.
        /// </summary>
        /// <param name="nombreArchivo">Nombre del archivo que se va a buscar.</param>
        /// <param name="sTipoReporte">Tipo de reporte que se va a buscar.</param>
        /// <returns>Regresa una variable boleana que determina si el registro en la tabla de relacion existe (true) o si no existe (false)</returns>
        public bool buscarTR(string nombreArchivo, string sTipoReporte, short? shCicloReporte, int? iRamoRepore, string sModalidadReporte, int? iClaveReporte, int? iIdIndicador, int? iIdProgramaSectorial, int? iMatriz)
        {
            bool resultado = false;
            var no = (object)null;

            using (SqlConnection conexionBD = new SqlConnection(simepsConn))
            {
                try
                {
                    conexionBD.Open();
                    if (conexionBD.State == ConnectionState.Open)
                    {

                        using (SqlTransaction transaccion = conexionBD.BeginTransaction())
                        {
                            //Se busca si la relacion existe
                            if (sTipoReporte == "1" || sTipoReporte == "2"
                            || sTipoReporte == "3" || sTipoReporte == "4"
                            || sTipoReporte == "6")
                            {

                                string trsql = "SELECT COUNT(path_locator) FROM TR_REPORTES_PROGRAMAS WHERE CICLO = @pCiclo and ramo = @pRamo and modalidad = @pModalidad and clave = @pClave and id_tipo_reporte = @pIdTipoReporte and id_matriz = @pIdMatriz";
                                using (SqlCommand cmd = new SqlCommand(trsql, conexionBD, transaccion))
                                {
                                    cmd.Parameters.AddWithValue("@pCiclo", shCicloReporte);
                                    cmd.Parameters.AddWithValue("@pRamo", iRamoRepore);
                                    cmd.Parameters.AddWithValue("@pModalidad", sModalidadReporte);
                                    cmd.Parameters.AddWithValue("@pClave", iClaveReporte);
                                    cmd.Parameters.AddWithValue("@pIdTipoReporte", sTipoReporte);
                                    cmd.Parameters.AddWithValue("@pIdMatriz", iMatriz);
                                    no = cmd.ExecuteScalar();
                                    transaccion.Commit();
                                }
                            }
                            else if (sTipoReporte == "5")
                            {
                                var id_nivel = (object)null;
                                string tdsql = "SELECT ID_NIVEL FROM TD_INDICADORES WHERE ID_INDICADOR = @pIndicador";
                                using (SqlCommand cmd = new SqlCommand(tdsql, conexionBD, transaccion))
                                {
                                    cmd.Parameters.AddWithValue("@pIndicador", iIdIndicador);
                                    id_nivel = cmd.ExecuteScalar();
                                }

                                string trsql = "SELECT COUNT(path_locator) FROM TR_REPORTES_INDICADORES WHERE id_indicador= @pIdIndicador and id_tipo_reporte= @pIdTipoReporte and id_nivel = @pIdNivel";
                                using (SqlCommand cmd = new SqlCommand(trsql, conexionBD, transaccion))
                                {
                                    cmd.Parameters.AddWithValue("@pIdIndicador",iIdIndicador);
                                    cmd.Parameters.AddWithValue("@pIdTipoReporte", sTipoReporte);
                                    cmd.Parameters.AddWithValue("@pIdNivel", id_nivel);
                                    no = cmd.ExecuteScalar();
                                    transaccion.Commit();
                                }
                            }
                            else if (sTipoReporte == "11")
                            {
                                string trsql = "SELECT COUNT(path_locator) FROM TR_REPORTES_INDICADORES_SEC WHERE id_indicador = @pIdIndicador and id_tipo_reporte = @pIdTipoReporte";
                                using (SqlCommand cmd = new SqlCommand(trsql, conexionBD, transaccion))
                                {
                                    cmd.Parameters.AddWithValue("@pIdIndicador",iIdIndicador);
                                    cmd.Parameters.AddWithValue("@pIdTipoReporte",sTipoReporte);
                                    no = cmd.ExecuteScalar();
                                    transaccion.Commit();
                                }
                            }
                            else if (sTipoReporte == "10" || sTipoReporte == "12"
                                 || sTipoReporte == "13")
                            {
                                string trsql = "SELECT COUNT(path_locator) FROM TR_REPORTES_PROGRAMAS_SEC WHERE id_prog_sectorial = @pIdProgSectorial and id_tipo_reporte = @pIdTipoReporte";
                                using (SqlCommand cmd = new SqlCommand(trsql, conexionBD, transaccion))
                                {
                                    cmd.Parameters.AddWithValue("@pIdProgSectorial",iIdProgramaSectorial);
                                    cmd.Parameters.AddWithValue("@pIdTipoReporte", sTipoReporte);
                                    no = cmd.ExecuteScalar();
                                    transaccion.Commit();
                                }
                            }
                            else if (sTipoReporte == "7" || sTipoReporte == "8" || sTipoReporte == "9")
                            {
                                string[] nombreVersion = nombreArchivo.Split('.');
                                //PK1348
                                //var path_locator = (object)null;
                                //string tdsql = "SELECT COUNT(path_locator) FROM " + fileTable + " WHERE name = '" + nombreVersion[0] + ".xls' or name = '" + nombreVersion[0] + ".xlsx'";
                                //using (SqlCommand cmd = new SqlCommand(tdsql, conexionBD, transaccion))
                                //{
                                //    path_locator = cmd.ExecuteScalar();
                                //}

                                string trsql = $"SELECT COUNT(path_locator) FROM TR_REPORTES_GENERALES WHERE path_locator in(SELECT path_locator FROM TD_RESOURCE_STORE WHERE name = '{nombreVersion[0]}.xls' or name = '{nombreVersion[0]}.xlsx')";
                                using (SqlCommand cmd = new SqlCommand(trsql, conexionBD, transaccion))
                                {
                                    no = cmd.ExecuteScalar();
                                    transaccion.Commit();
                                }
                                //PK1348
                            }
                            if (no.ToString() != "0")
                            {
                                resultado = true;
                            }
                            else
                            {
                                resultado = false;
                            }
                        }

                    }
                }
                catch (Exception e)
                {
                    log.LogMessageToFile(e.Message);
                    log.LogMessageToFile(e.StackTrace);
                    resultado = false;
                    throw new Exception(e.Message, e.InnerException);
                }
                finally
                {
                    if ((conexionBD != null) && (conexionBD.State == ConnectionState.Open))
                    {
                        conexionBD.Close();
                    }
                }
            }
            return resultado;
        }

        public DateTime FechaCreacionDocumento(string nombre)
        {
            DateTime res = new DateTime();
            using (SqlConnection conexionBD = new SqlConnection(simepsConn))
            {
                try
                {
                    conexionBD.Open();
                    if (conexionBD.State == ConnectionState.Open)
                    {
                        string chainSql = "SELECT creation_time FROM " + fileTable + " WHERE name= '" + nombre + "'";
                        using (SqlCommand cmd = new SqlCommand(chainSql, conexionBD))
                        {
                            var result = cmd.ExecuteScalar();
                            if (result != null)
                                res = Convert.ToDateTime(result.ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    log.LogMessageToFile(e.ToString());
                }
                finally
                {
                    if ((conexionBD != null) && (conexionBD.State == ConnectionState.Open))
                    {
                        conexionBD.Close();
                    }
                }

            }
            return res;
        }

        public Modelo.TipoReporte tipoReporteDal(int id_tipo_reporte)
        {
            Modelo.TipoReporte registro = new Modelo.TipoReporte();
            DataSet data = new DataSet();

            string chainSql = "SELECT ID_TIPO_REPORTE, NOMBRE_ARCHIVO, FORMATO from TC_TIPOS_REPORTE where ID_TIPO_REPORTE='" + id_tipo_reporte + "'";

            string Cadconexion = System.Configuration.ConfigurationManager.ConnectionStrings["con_simepsDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(Cadconexion))
            {
                try
                {
                    using (SqlCommand cm = new SqlCommand(chainSql, connection))
                    {
                        cm.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeOutSQL"]); ;
                        connection.Open();
                        SqlDataAdapter reader = new SqlDataAdapter(cm);

                        reader.Fill(data);
                    }
                }
                catch (Exception e)
                {
                    log.LogMessageToFile(e.ToString());
                    throw new Exception(e.Message, e.InnerException);
                }
                finally
                {
                    if ((connection != null) && (connection.State == ConnectionState.Open))
                    {
                        connection.Close();
                    }
                }
            } string[] partes;
            foreach (DataRow row in data.Tables[0].Rows)
            {
                registro.ID_TIPO_REPORTE = Convert.ToString(row.Field<int>("ID_TIPO_REPORTE"));
                registro.NOMBRE_ARCHIVO = row.Field<string>("NOMBRE_ARCHIVO");
                registro.FORMATO = row.Field<string>("FORMATO");
            }
            return registro;
        }
    }
}