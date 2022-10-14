using System;
using System.IO;

namespace clientSIID
{
    public class tokenUsr
    {
        public static string get(string pathFile)
        {
            string cadena = "";
            try
            {
                if (File.Exists(pathFile))
                {
                    var lines = File.ReadLines(pathFile, System.Text.Encoding.UTF8);

                    foreach (string i in lines)
                    {
                        cadena += i;
                    }
                }
                else
                { cadena = "Error: ArchivoToken no encontrado."; }
            }
            catch (Exception error)
            {
                cadena = "Error: ArchivoToken -> " + error.Message;
            }

            return cadena;
        }

        public static bool update(string token, string pathFile)
        {
            try
            {
                string[] lines = { token };
                File.WriteAllLines(pathFile, lines, System.Text.Encoding.UTF8);
            }
            catch (Exception error)
            {
                return false;
            }
            return true;
        }
    }
}