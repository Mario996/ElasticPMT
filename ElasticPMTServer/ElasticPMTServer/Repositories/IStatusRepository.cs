using Nest;
using Status = ElasticPMTServer.Models.Status;

namespace ElasticPMTServer.Repositories
{
    public interface IStatusRepository
    {
        IndexResponse createStatus(Status status);
        ISearchResponse<Status> getStatuses();
        GetResponse<Status> getStatusById(string id);
        UpdateResponse<Status> updateStatus(string id, Status status);
        bool checkIfIndexExists();
        CreateIndexResponse createIndex();
        DeleteResponse deleteStatus(string id);
    }
}
