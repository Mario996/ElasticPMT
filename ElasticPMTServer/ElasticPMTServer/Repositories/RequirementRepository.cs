using ElasticPMTServer.Models;
using Elasticsearch.Net;
using Nest;
using System.Collections.Generic;

namespace ElasticPMTServer.Repositories
{
    public class RequirementRepository : Repository<Requirement>, IRequirementRepository
    {
        public RequirementRepository() : base("requirements")
        { }
    }
}
