using ElasticPMTServer.Models;
using Nest;

namespace ElasticPMTServer.Repositories
{
    public interface IUserRepository
    {
        IndexResponse createUser(User user);
        ISearchResponse<User> getUsers();
        GetResponse<User> getUserById(string id);
        UpdateResponse<User> updateUser(string id, User user);
        bool checkIfIndexExists();
        CreateIndexResponse createIndex();
        DeleteResponse deleteUser(string id);
    }
}
