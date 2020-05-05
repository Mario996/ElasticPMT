using ElasticPMTServer.Models;
using System.Collections.Generic;

namespace ElasticPMTServer.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        Dictionary<string, long?> orderTasksPerCreator();
    }
}
