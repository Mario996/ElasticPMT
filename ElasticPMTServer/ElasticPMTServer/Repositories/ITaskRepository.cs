using ElasticPMTServer.Models;
using Nest;
using System.Collections.Generic;

namespace ElasticPMTServer.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        Dictionary<string, long?> orderTasksPerCreator();
        Dictionary<string, long?> orderRequirementsPerTasksCreated();
        UpdateResponse<Task> updateComments(string id, List<Comment> comments);
    }
}
