using ElasticPMTServer.Models;
using Nest;
using System;
using System.Collections.Generic;

namespace ElasticPMTServer.Repositories
{
    public class RequirementRepository : Repository<Requirement>, IRequirementRepository
    {
        public RequirementRepository() : base("requirements")
        { }

        public UpdateResponse<Requirement> updateComments(string id, List<Comment> comments)
        {
            return _elasticClient.Update<Requirement>(id, desc => desc
                                .Doc(new Requirement { Comments = comments, Id = new Guid(id) })
                                .Refresh(Elasticsearch.Net.Refresh.True));
        }
    }
}
