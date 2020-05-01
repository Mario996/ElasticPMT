using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models
{
    public interface IElasticSearchType
    {
         Guid Id { get; set; }
    }
}
