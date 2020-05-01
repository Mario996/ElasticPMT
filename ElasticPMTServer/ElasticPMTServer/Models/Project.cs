using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models
{
    public class Project : ElasticSearchType
    {
        public string Name { get; set; }
        public string Methodology { get; set; }
        public string Tag { get; set; }
        public User Manager { get; set; }
        public string Version { get; set; }
    }
}
