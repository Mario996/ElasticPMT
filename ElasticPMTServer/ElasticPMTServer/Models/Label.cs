using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models
{
    public class Label : ElasticSearchType
    {
        public string Name { get; set; }
    }
}
