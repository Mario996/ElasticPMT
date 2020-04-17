using ElasticPMTServer.Models;
using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Services
{
    public interface IElasticSearchService
    {
          StringResponse createRequirement(Requirement requirement);
    }
}
