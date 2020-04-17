using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models
{
    public class Requirement
    {
        public string Requirement_id { get; set; }
        public string Requirement_version { get; set; }
        public string Requirement_description { get; set; }
        public string Requirement_rationale { get; set; }
        public string Requirement_type { get; set; }
        public string Requirement_status { get; set; }
    }
}
