using ElasticPMTServer.Models;

namespace ElasticPMTServer.Repositories
{
    public class PriorityRepository : Repository<Priority>, IPriorityRepository
    {
        public PriorityRepository() : base("priorities")
        { }
    }
}
