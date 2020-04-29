using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
    }
}
