using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace URLAnalyzer.API.Models
{
    public class PageElements
    {
        public IEnumerable<string> ContentImages { get; set; }
        public long ContentWordCount
        {
            get
            {
                return ContentWordCollection != null ? ContentWordCollection.Sum(kv => kv.Value) : 0;
            }
        }
        public IEnumerable<KeyValuePair<string, long>> ContentWordCollection { get; set; }
    }
}