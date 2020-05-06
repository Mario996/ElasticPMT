using ElasticPMTServer.Models;
using Nest;
using System.Collections.Generic;

namespace ElasticPMTServer.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        Dictionary<string, long?> orderTasksPerCreator();
        UpdateResponse<Task> updateComments(string id, List<Comment> comments);
    }
}
