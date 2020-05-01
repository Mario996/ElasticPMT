﻿using System;
using System.Collections.Generic;

namespace ElasticPMTServer.Models
{
    public class Task : ElasticSearchType
    {
        public string Type { get; set; }
        public string Summary { get; set; }
        public string Priority { get; set; }
        public User Assignee { get; set; }
        public string Environment { get; set; }
        public string Component { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
