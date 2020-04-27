using ElasticPMTServer.Models;
using Elasticsearch.Net;
using Nest;
using System.Collections.Generic;

namespace ElasticPMTServer.Repositories
{
    public class RequirementRepository : IRequirementRepository
    {
        public static ConnectionSettings settings = new ConnectionSettings()
                                                        .DefaultMappingFor<Requirement>(m => m
                                                        .IndexName("requirements") 
                                                        .IdProperty(p => p.Id)
                                                        );

        public static ElasticClient client = new ElasticClient(settings);

        public IndexResponse createRequirement(Requirement requirement)
        {
            if(checkIfIndexExists())
            {
                return client.Index(requirement, i => i
                .Refresh(Elasticsearch.Net.Refresh.True));
            }
            else
            {
                var response = createIndex();
                if(response.IsValid)
                {
                    return client.Index(requirement, i => i
                        .Refresh(Elasticsearch.Net.Refresh.True));
                }
                return null;
            }
        }

        public UpdateResponse<Requirement> updateRequirement(string id, Requirement requirement)
        {
            return client.Update<Requirement>(id, u => u
                    .Doc(requirement)
                    .Refresh(Elasticsearch.Net.Refresh.True)
            );
        }

        public ISearchResponse<Requirement> getRequirements()
        {
            client.Indices.Refresh();
            return client.Search<Requirement>(s => s
               .MatchAll()
            );
        }

        public GetResponse<Requirement> getRequirementById(string id)
        {
            client.Indices.Refresh();
            return client.Get<Requirement>(id);
        }

        public DeleteResponse deleteRequirement(string id)
        {
            return client.Delete<Requirement>(id);
        }

        public bool checkIfIndexExists()
        {
            return client.Indices.Exists("requirements").Exists;
        }

        public CreateIndexResponse createIndex()
        {
            // at the moment I let ES create mapping according to my object, may change later
            return client.Indices.Create("requirements", c => c
                        .Settings(s => s
                            .NumberOfShards(1)
                        ).Map<Requirement>(r => r
                            .AutoMap()
                            .Properties(ps => ps
                                    .Nested<Comment>(n => n
                                        .Name(nn => nn.Comments)
                                    )
                            )
                        )
            );
        }
    }
}
