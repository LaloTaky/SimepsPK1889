using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMEPS.Comun
{
    public class Logger
    {
        public string GetTempPath()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["LogPath"]; ;
            if (!path.EndsWith("\\")) path += "\\";
            return path;
        }

        public void LogMessageToFile(string msg)
        {
            System.IO.StreamWriter sw = System.IO.File.AppendText(
                GetTempPath() + "DescargaAppLog");
            try
            {
                string logLine = System.String.Format(
                    "{0:G}: {1}.", System.DateTime.Now, msg);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }
    }
}