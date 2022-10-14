using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;

namespace clientSIID
{
   /// <summary>
   /// Consume servicios de SIID
   /// </summary>
   public class callAPI
   {
      private string BaseAddress,
                      User,
                      Password,
                      PathToken;

      public callAPI(string _baseAddress, string _usr, string _pws, string _pathToken)
      {
         BaseAddress = _baseAddress;
         User = _usr;
         Password = _pws;
         PathToken = _pathToken;
      }

      /// <summary>
      /// Peticiones a SIID
      /// </summary>
      /// <param name="uri">Enlace</param>
      /// <returns></returns>
      private async Task<string> call(string uri)
      {
         string statusCode = "BAD",
                 bodyResponse,
                 message;

         HttpResponseMessage result;
         try
         {
            using (var client = new HttpClient())
            {
               //url SIID
               client.BaseAddress = new System.Uri(BaseAddress);
               client.Timeout = TimeSpan.FromSeconds(900);

               //Asignación de token
               string token = tokenUsr.get(PathToken);

               if (token.Contains("Error:")) { bodyResponse = "\"" + token + "\""; }
               else
               {
                  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                  //Se realiza petición
                  result = await client.GetAsync(uri).ConfigureAwait(false);

                        //Se obtiene mensaje de respuesta
                        bodyResponse = await result.Content.ReadAsStringAsync();
                  statusCode = result.StatusCode.ToString();

                  if (!result.IsSuccessStatusCode)
                  {
                     if (result.StatusCode == System.Net.HttpStatusCode.NotAcceptable)
                     {
                        // ACTUALIZACIÓN DE TOKEN
                        bodyResponse = await getNewToken(uri);
                        if (bodyResponse == "")
                        {
                           //Se vuelve a hacer la petición
                           return await call(uri);
                        }
                     }

                     if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                     {
                        bodyResponse = "No encontrado";
                     }
                     else { bodyResponse = "Error: " + ((int)result.StatusCode).ToString() + result.StatusCode.ToString(); }

                     bodyResponse = "\"" + bodyResponse + "\"";
                  }
               }

            }
         }
         catch (Exception ex)
         {
            bodyResponse = String.Format("\" {0} \n {1} \"", ex.Message, ex.InnerException);
         }

         //tokenUsr.write(uri + ", " + DateTime.Now.ToString("dd/mm/yy hh:mm:ss") + ", " + statusCode);

         message = string.Format("{{\"StatusCode\":\"{0}\", \"ResponseBody\": {1} }}", statusCode, bodyResponse);
         return message;
      }

      /// <summary>
      /// Peticiones a SIID
      /// </summary>
      /// <param name="uri">Enlace</param>
      /// <param name="datos">Datos que no se pueden enviar mediante url</param>
      /// <returns></returns>
      private async Task<string> call(string uri,Dictionary<string,string> datos)
      {
         string statusCode = "BAD",
                 bodyResponse,
                 message;

         HttpResponseMessage result;
         try
         {
            using (var client = new HttpClient())
            {
               //url SIID
               client.BaseAddress = new System.Uri(BaseAddress);

               //Asignación de token
               string token = tokenUsr.get(PathToken);

               if (token.Contains("Error:")) { bodyResponse = "\"" + token + "\""; }
               else
               {
                  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                  if(datos.ContainsKey("objetivoSectorial"))client.DefaultRequestHeaders.Add("objetivoSectorial", datos["objetivoSectorial"]);

                  //Se realiza petición
                  result = await client.GetAsync(uri);

                  //Se obtiene mensaje de respuesta
                  bodyResponse = await result.Content.ReadAsStringAsync();
                  statusCode = result.StatusCode.ToString();

                  if (!result.IsSuccessStatusCode)
                  {
                     if (result.StatusCode == System.Net.HttpStatusCode.NotAcceptable)
                     {
                        // ACTUALIZACIÓN DE TOKEN
                        bodyResponse = await getNewToken(uri);
                        if (bodyResponse == "")
                        {
                           //Se vuelve a hacer la petición
                           return await call(uri);
                        }
                     }

                     if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                     {
                        bodyResponse = "No encontrado";
                     }
                     else { bodyResponse = "Error: " + ((int)result.StatusCode).ToString() + result.StatusCode.ToString(); }

                     bodyResponse = "\"" + bodyResponse + "\"";
                  }
               }

            }
         }
         catch (Exception ex)
         {
            bodyResponse = String.Format("\" {0} \n {1} \"", ex.Message, ex.InnerException);
         }

         //tokenUsr.write(uri + ", " + DateTime.Now.ToString("dd/mm/yy hh:mm:ss") + ", " + statusCode);

         message = string.Format("{{\"StatusCode\":\"{0}\", \"ResponseBody\": {1} }}", statusCode, bodyResponse);
         return message;
      }


      private async Task<string> getNewToken(string uri)
      {
         using (var client = new HttpClient())
         {
            //url SIID
            client.BaseAddress = new System.Uri(BaseAddress);

            // Se agrega información de usuario
            client.DefaultRequestHeaders.Add("User", User);
            client.DefaultRequestHeaders.Add("Pass", Password);

            //Solicitud de nuevo token
            HttpResponseMessage result = await client.GetAsync(uri);

            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
               //Lectura de token
               string token = await result.Content.ReadAsStringAsync();

               if (!tokenUsr.update(token, PathToken))
                  return "ERROR: Token no se logró escribir en ArchivoToken";

            }
         }
         return "";
      }

      /// <summary>
      /// Convierte JSON a lista de objetos
      /// </summary>
      /// <param name="uri">URI</param>
      /// <returns></returns>
      protected dynamic jsonData(string uri)
      {
         return JsonConvert.DeserializeObject(call(uri).Result);
      }

      /// <summary>
      /// Convierte JSON a lista de objetos
      /// </summary>
      /// <param name="uri">URI</param>
      /// <param name="datos">Datos que no se pueden enviar mediante url</param>
      /// <returns></returns>
      protected dynamic jsonData(string uri,Dictionary<string,string> datos)
      {
         return JsonConvert.DeserializeObject(call(uri,datos).Result);
      }

   }
}