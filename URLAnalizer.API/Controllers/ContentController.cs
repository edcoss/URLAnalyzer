using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using URLAnalyzer.API.Services;
using URLAnalyzer.Foundation.Configuration;
using URLAnalyzer.Foundation.Constants;

namespace URLAnalyzer.API.Controllers
{
    public class ContentController : ApiController
    {
        IContentService contentService;
        IConfigurationSettings configurationSettings;

        public ContentController(IContentService contentService, IConfigurationSettings configurationSettings)
        {
            this.contentService = contentService;
            this.configurationSettings = configurationSettings;
        }

        [HttpGet]
        [HttpPost]
        [Route("api/content")]
        public IHttpActionResult LoadUrl(string url)
        {
            // TODO: tab char (\t) failing to load from AppSettings ...
            //var wordfilters = configurationSettings.Get(URLAnalyzerSettings.API_SEARCH_FILTERS).ToCharArray();

            var wordfilters = new char[] { ' ', '\n', '\r', '\t', ',', '.', '[', ']', '{', '}', '(', ')', ':', ';', '|', '/', '\\', '<', '>' };
            var model = contentService.GetContentElements(url, wordfilters);
            return Json(model);
        }
    }
}
