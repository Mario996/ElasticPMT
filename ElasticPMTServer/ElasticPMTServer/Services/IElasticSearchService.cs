using ElasticPMTServer.Models;
using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Services
{
    public interface IElasticSearchService
    {
        IndexResponse createRequirement(Requirement requirement);
        ISearchResponse<Requirement> getRequirements();
        ISearchResponse<Requirement> getRequirementById(string id);
        bool checkIfIndexExists();
        void createIndex();
    }
}
