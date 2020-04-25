using ElasticPMTServer.Models;
using Nest;

namespace ElasticPMTServer.Repositories
{
    public interface ITaskRepository
    {
        IndexResponse createTask(Task task);
        ISearchResponse<Task> getTasks();
        GetResponse<Task> getTaskById(string id);
        UpdateResponse<Task> updateTask(string id, Task task);
        bool checkIfIndexExists();
        CreateIndexResponse createIndex();
        DeleteResponse deleteTask(string id);
    }
}
