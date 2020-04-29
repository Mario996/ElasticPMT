using ElasticPMTServer.Models;
using Nest;

namespace ElasticPMTServer.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        public static ConnectionSettings settings = new ConnectionSettings()
                                                       .DefaultMappingFor<Project>(m => m
                                                       .IndexName("projects")
                                                       .IdProperty(p => p.Id)
                                                       );

        public static ElasticClient client = new ElasticClient(settings);

        public bool checkIfIndexExists()
        {
            return client.Indices.Exists("projects").Exists;
        }

        public CreateIndexResponse createIndex()
        {
            return client.Indices.Create("projects", c => c
                    .Settings(s => s
                        .NumberOfShards(1))
                        .Map<User>(r => r
                           .AutoMap()
                        )
                   );
        }

        public IndexResponse createProject(Project project)
        {
            if (checkIfIndexExists())
            {
                return client.Index(project, i => i
                    .Refresh(Elasticsearch.Net.Refresh.True));
            }
            else
            {
                var response = createIndex();
                if (response.IsValid)
                {
                    return client.Index(project, i => i
                        .Refresh(Elasticsearch.Net.Refresh.True));
                }
                return null;
            }
        }

        public DeleteResponse deleteProject(string id)
        {
            return client.Delete<Project>(id);
        }

        public GetResponse<Project> getProjectById(string id)
        {
            client.Indices.Refresh();
            return client.Get<Project>(id);
        }

        public ISearchResponse<Project> getProjects()
        {
            client.Indices.Refresh();
            return client.Search<Project>(s => s
               .MatchAll()
            );
        }

        public UpdateResponse<Project> updateProject(string id, Project project)
        {
            return client.Update<Project>(id, u => u
               .Doc(project)
               .Refresh(Elasticsearch.Net.Refresh.True)
          );
        }
    }
}
