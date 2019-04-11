using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLAnalyzer.Foundation.Models;

namespace URLAnalyzer.Foundation.Configuration
{
    public interface IConfigurationSettings
    {
        string Get(string settingName);
    }
}
