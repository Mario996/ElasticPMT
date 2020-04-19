using ElasticPMTServer.Models;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
        public static ElasticClient client = new ElasticClient();

        public  IndexResponse createRequirement(Requirement requirement)
        {
            if(checkIfIndexExists())
            {
                return client.Index(requirement, i => i.Index("elasticpmt"));
            }
            else
            {
                createIndex();
                return client.Index(requirement, i => i.Index("elasticpmt"));
            }
        }

        public ISearchResponse<Requirement> getRequirements()
        {
            return client.Search<Requirement>(s => s
               .Index("elasticpmt")
               .MatchAll()
            );
        }

        public ISearchResponse<Requirement> getRequirementById(string id)
        {
            return client.Search<Requirement>(s => s
               .Index("elasticpmt")
               .Query(q => q
                  .Ids(c => c
                    .Values(id)
                    )
               )
            );
        }

        public bool checkIfIndexExists()
        {
            return client.Indices.Exists("elasticpmt").Exists;
        }

        public void createIndex()
        {
            client.Indices.Create("elasticpmt", c => c
                        .Settings(s => s
                            .NumberOfShards(1)
                        )
                        .Map(m => m
                            .Properties(p => p
                                .Text(t => t
                                    .Name("requirement_id")
                                    .Name("requirement_version")
                                    .Name("requirement_description")
                                    .Name("requirement_rationale")
                                    .Name("requirement_type")
                                    .Name("requirement_status")
                                )
                            )
                        )
            );
        }
    }
}
