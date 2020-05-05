using Status = ElasticPMTServer.Models.Status;

namespace ElasticPMTServer.Repositories
{
    public class StatusRepository : Repository<Status>, IStatusRepository
    { 
        public StatusRepository() : base("statuses")
        { }
    }
}
