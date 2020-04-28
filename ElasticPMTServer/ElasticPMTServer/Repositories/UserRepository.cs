using ElasticPMTServer.Models;
using Nest;

namespace ElasticPMTServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        public static ConnectionSettings settings = new ConnectionSettings()
                                                       .DefaultMappingFor<User>(m => m
                                                       .IndexName("users")
                                                       .IdProperty(p => p.Id)
                                                       );

        public static ElasticClient client = new ElasticClient(settings);

        public bool checkIfIndexExists()
        {
            return client.Indices.Exists("users").Exists;
        }

        public CreateIndexResponse createIndex()
        {
            return client.Indices.Create("users", c => c
                    .Settings(s => s
                        .NumberOfShards(1))
                        .Map<User>(r => r
                           .AutoMap()
                        )
                   );
        }

        public IndexResponse createUser(User user)
        {
            if (checkIfIndexExists())
            {
                return client.Index(user, i => i
                    .Refresh(Elasticsearch.Net.Refresh.True));
            }
            else
            {
                var response = createIndex();
                if (response.IsValid)
                {
                    return client.Index(user, i => i
                        .Refresh(Elasticsearch.Net.Refresh.True));
                }
                return null;
            }
        }

        public DeleteResponse deleteUser(string id)
        {
            return client.Delete<User>(id);
        }

        public GetResponse<User> getUserById(string id)
        {
            client.Indices.Refresh();
            return client.Get<User>(id);
        }

        public ISearchResponse<User> getUsers()
        {
            client.Indices.Refresh();
            return client.Search<User>(s => s
               .MatchAll()
            );
        }

        public UpdateResponse<User> updateUser(string id, User user)
        {
            return client.Update<User>(id, u => u
                .Doc(user)
                .Refresh(Elasticsearch.Net.Refresh.True)
           );
        }
    }
}
