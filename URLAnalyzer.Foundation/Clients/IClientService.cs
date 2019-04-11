using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLAnalyzer.Foundation.Models;

namespace URLAnalyzer.Foundation.Clients
{
    public interface IClientService
    {
        string GetResponse(string url, RequestFormat format, object body = null);
    }
}
