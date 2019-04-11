using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLAnalyzer.Foundation.Constants;
using URLAnalyzer.Foundation.Models;

namespace URLAnalyzer.Foundation.Configuration
{
    public class WebConfigurationSettings : IConfigurationSettings
    {
        private Dictionary<string, string> configuration = new Dictionary<string, string>();

        public WebConfigurationSettings()
        {
            Load();
        }
        public string Get(string settingName)
        {
            string value = string.Empty;
            if (configuration.TryGetValue(settingName, out value))
            {
                return value;
            }
            return string.Empty;
        }

        private void Load()
        {
            configuration.Add(URLAnalyzerSettings.API_URL_ENDPOINT, ConfigurationManager.AppSettings[URLAnalyzerSettings.API_URL_ENDPOINT]);
            configuration.Add(URLAnalyzerSettings.API_URL_REQUEST_PATH, ConfigurationManager.AppSettings[URLAnalyzerSettings.API_URL_REQUEST_PATH]);
            configuration.Add(URLAnalyzerSettings.API_SEARCH_FILTERS, ConfigurationManager.AppSettings[URLAnalyzerSettings.API_SEARCH_FILTERS]);
            configuration.Add(URLAnalyzerSettings.API_REQUEST_TIMEOUT, ConfigurationManager.AppSettings[URLAnalyzerSettings.API_REQUEST_TIMEOUT]);
        }
    }
}
