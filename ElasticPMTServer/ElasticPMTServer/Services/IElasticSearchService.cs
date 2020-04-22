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
        GetResponse<Requirement> getRequirementById(string id);
        UpdateResponse<Requirement> updateRequirement(string id, Requirement requirement);
        bool checkIfIndexExists();
        void createIndex();
        DeleteResponse deleteRequirement(string id);
    }
}
