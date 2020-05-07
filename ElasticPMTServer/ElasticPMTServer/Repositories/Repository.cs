using ElasticPMTServer.Models;
using Nest;
using Task = ElasticPMTServer.Models.Task;

namespace ElasticPMTServer.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : ElasticSearchType
    {
        protected readonly ElasticClient _elasticClient;
        private readonly ConnectionSettings _settings;
        private readonly string _indexName;

        protected Repository(string indexName)
        {
            _settings = new ConnectionSettings()
                                .DefaultMappingFor<TEntity>(m => m
                                .IndexName(indexName)
                                .IdProperty(p => p.Id)
                                );
            _elasticClient = new ElasticClient(_settings);
            _indexName = indexName;
        }

        public void checkIfIndexExists()
        {
           if(_elasticClient.Indices.Exists(_indexName).Exists)
           {
                return;
           }

           switch(_indexName)
           {
                case "requirements":
                    _elasticClient.Indices.Create(_indexName, c => c
                        .Settings(s => s
                            .NumberOfShards(1)
                        ).Map<Requirement>(r => r
                            .AutoMap()
                            .Properties(ps => ps
                                    .Nested<Comment>(n => n
                                        .Name(nn => nn.Comments)
                                        .AutoMap()
                                    )
                            )
                        ));
                    break;
                case "tasks":
                    _elasticClient.Indices.Create(_indexName, c => c
                       .Settings(s => s
                           .NumberOfShards(1)
                       ).Map<Task>(r => r
                           .AutoMap()
                           .Properties(ps => ps
                                   .Nested<Comment>(n => n
                                       .Name(nn => nn.Comments)
                                       .AutoMap()
                                   )
                                   .Nested<Label>(n => n
                                       .Name(nn => nn.Labels)
                                       .AutoMap()
                                   )
                           )
                       ));
                    break;
                default:
                    _elasticClient.Indices.Create(_indexName, c => c
                                                .Settings(s => s
                                                    .NumberOfShards(1))
                                                    .Map<TEntity>(m => m
                                                        .AutoMap()
                                                    )
                                                );
                    break;
           }         
        }

        public IndexResponse create(TEntity document)
        {
            checkIfIndexExists();
            return _elasticClient.Index(document, i => i
                    .Refresh(Elasticsearch.Net.Refresh.True));
        }

        public DeleteResponse delete(string id)
        {
            return _elasticClient.Delete<TEntity>(id);
        }

        public ISearchResponse<TEntity> getAll()
        {
            _elasticClient.Indices.Refresh();
            return _elasticClient.Search<TEntity>(s => s
               .MatchAll()
            );
        }

        public GetResponse<TEntity> getById(string id)
        {
            _elasticClient.Indices.Refresh();
            return _elasticClient.Get<TEntity>(id);
        }

        public UpdateResponse<TEntity> update(string id, TEntity document)
        {
            return _elasticClient.Update<TEntity>(id, u => u
              .Doc(document)
              .Refresh(Elasticsearch.Net.Refresh.True)
            );
        }
    }
}
