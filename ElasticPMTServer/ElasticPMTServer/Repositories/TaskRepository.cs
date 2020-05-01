using ElasticPMTServer.Models;
using Nest;
using Task = ElasticPMTServer.Models.Task;

namespace ElasticPMTServer.Repositories
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository() : base("tasks")
        { }
    }
}
