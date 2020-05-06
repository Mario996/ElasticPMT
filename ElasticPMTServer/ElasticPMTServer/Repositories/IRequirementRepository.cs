using ElasticPMTServer.Models;
using Nest;
using System.Collections.Generic;

namespace ElasticPMTServer.Repositories
{
    public interface IRequirementRepository : IRepository<Requirement>
    {
        UpdateResponse<Requirement> updateComments(string id, List<Comment> comments);
    }
}
