using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLAnalyzer.API.Repositories
{
    public interface IContentRepository
    {
        string GetContent(string location);
    }
}
