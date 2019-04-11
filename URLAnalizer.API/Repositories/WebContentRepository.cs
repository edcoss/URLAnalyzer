using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using URLAnalyzer.API.Models;
using URLAnalyzer.Foundation.Models;
using URLAnalyzer.Foundation.Clients;

namespace URLAnalyzer.API.Repositories
{
    public class WebContentRepository : IContentRepository
    {
        private IClientService clientService;

        public WebContentRepository(IClientService clientService)
        {
            this.clientService = clientService;
        }

        /// <summary>
        /// Calls Http client to make a web request to a URL
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public string GetContent(string location)
        {
            var response = clientService.GetResponse(location, RequestFormat.HTMLTXT);
            return response;
        }
    }
}