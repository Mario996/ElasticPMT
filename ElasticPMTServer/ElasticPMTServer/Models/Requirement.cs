using System;
using System.Collections.Generic;

namespace ElasticPMTServer.Models
{
    public class Requirement : ElasticSearchType
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string Rationale { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public User Author { get; set; }
        public Project Project { get; set; }
    }
}
