﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}
