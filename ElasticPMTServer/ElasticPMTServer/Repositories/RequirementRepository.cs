using ElasticPMTServer.Models;

namespace ElasticPMTServer.Repositories
{
    public class RequirementRepository : Repository<Requirement>, IRequirementRepository
    {
        public RequirementRepository() : base("requirements")
        { }
    }
}
