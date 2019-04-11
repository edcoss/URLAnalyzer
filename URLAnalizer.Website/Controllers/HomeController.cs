using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using URLAnalyzer.Foundation.Configuration;
using URLAnalyzer.Foundation.Constants;
using URLAnalyzer.Models;

namespace URLAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        IConfigurationSettings configurationSettings; 
        public HomeController()
        {
            configurationSettings = new WebConfigurationSettings();
        }
        public ActionResult Index()
        {
            var model = new APISettingsViewModel()
            {
                ApiUrlEndpoint = configurationSettings.Get(URLAnalyzerSettings.API_URL_ENDPOINT),
                ApiUrlRequestPath = configurationSettings.Get(URLAnalyzerSettings.API_URL_REQUEST_PATH)
            };
            return View(model);
        }
    }
}