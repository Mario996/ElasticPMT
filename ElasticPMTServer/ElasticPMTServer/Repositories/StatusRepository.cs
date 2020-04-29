using System;
using Nest;
using Status = ElasticPMTServer.Models.Status;

namespace ElasticPMTServer.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        public static ConnectionSettings settings = new ConnectionSettings()
                                                       .DefaultMappingFor<Status>(m => m
                                                       .IndexName("statuses")
                                                       .IdProperty(p => p.Id)
                                                       );

        public static ElasticClient client = new ElasticClient(settings);

        public bool checkIfIndexExists()
        {
            return client.Indices.Exists("statuses").Exists;
        }

        public CreateIndexResponse createIndex()
        {
            return client.Indices.Create("statuses", c => c
                    .Settings(s => s
                        .NumberOfShards(1))
                        .Map<Status>(r => r
                           .AutoMap()
                        )
                   );
        }

        public IndexResponse createStatus(Status status)
        {
            if (checkIfIndexExists())
            {
                return client.Index(status, i => i
                    .Refresh(Elasticsearch.Net.Refresh.True));
            }
            else
            {
                var response = createIndex();
                if (response.IsValid)
                {
                    return client.Index(status, i => i
                        .Refresh(Elasticsearch.Net.Refresh.True));
                }
                return null;
            }
        }

        public DeleteResponse deleteStatus(string id)
        {
            return client.Delete<Status>(id);
        }

        public GetResponse<Status> getStatusById(string id)
        {
            client.Indices.Refresh();
            return client.Get<Status>(id);
        }

        public ISearchResponse<Status> getStatuses()
        {
            client.Indices.Refresh();
            return client.Search<Status>(s => s
               .MatchAll()
            );
        }

        public UpdateResponse<Status> updateStatus(string id, Status status)
        {
            return client.Update<Status>(id, u => u
               .Doc(status)
               .Refresh(Elasticsearch.Net.Refresh.True)
            );
        }
    }
}
