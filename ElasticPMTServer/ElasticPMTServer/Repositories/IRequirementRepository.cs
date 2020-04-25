using ElasticPMTServer.Models;
using Nest;

namespace ElasticPMTServer.Repositories
{
    public interface IRequirementRepository
    {
        IndexResponse createRequirement(Requirement requirement);
        ISearchResponse<Requirement> getRequirements();
        GetResponse<Requirement> getRequirementById(string id);
        UpdateResponse<Requirement> updateRequirement(string id, Requirement requirement);
        bool checkIfIndexExists();
        CreateIndexResponse createIndex();
        DeleteResponse deleteRequirement(string id);
    }
}
