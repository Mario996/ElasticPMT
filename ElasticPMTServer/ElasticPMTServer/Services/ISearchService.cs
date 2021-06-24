using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Services
{
    public interface ISearchService
    {
        IEnumerable<string> Autocomplete(string query, int count);
    }
}
