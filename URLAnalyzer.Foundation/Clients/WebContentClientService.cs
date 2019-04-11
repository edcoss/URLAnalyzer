using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using URLAnalyzer.Foundation.Models;

namespace URLAnalyzer.Foundation.Clients
{
    public class WebContentClientService : IClientService
    {

        private string ParseFormat(RequestFormat format)
        {
            switch (format)
            {
                case RequestFormat.HTMLTXT: return "html/text";
                case RequestFormat.JSON: return "application/json";
                case RequestFormat.XML: return "text/xml";
                default: { return "html/text"; }
            }
        }

        /// <summary>
        /// Uses HttpClient class to make a web request and returns its response
        /// </summary>
        /// <param name="url"></param>
        /// <param name="format"></param>
        /// <param name="body"></param>
        /// <returns>The response returned by the client, in a string</returns>
        public string GetResponse(string url, RequestFormat format, object body = null)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("accept", ParseFormat(format));
            string result = string.Empty;
            try
            {
                HttpResponseMessage responseMsg = httpClient.GetAsync(url).Result;
                result = responseMsg.Content.ReadAsStringAsync().Result;                    
            }
            catch
            {
                throw;
            }
            finally
            {
                httpClient.Dispose();
            }
            return result;
        }

       
    }
}
