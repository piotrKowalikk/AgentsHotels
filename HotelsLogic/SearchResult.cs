using System;
using System.Collections.Generic;
using System.Text;

namespace HotelsLogic
{
    public class SearchResult
    {
        public string FromCompany { get; private set; }
        public string Url { get; private set; }
        public SearchResult(string fromCompany, string url)
        {
            FromCompany = fromCompany;
            Url = url;
        }
    }
}
