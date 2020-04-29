using ElasticPMTServer.Models;
using Nest;

namespace ElasticPMTServer.Repositories
{
    public interface IProjectRepository
    {
        IndexResponse createProject(Project project);
        ISearchResponse<Project> getProjects();
        GetResponse<Project> getProjectById(string id);
        UpdateResponse<Project> updateProject(string id, Project project);
        bool checkIfIndexExists();
        CreateIndexResponse createIndex();
        DeleteResponse deleteProject(string id);
    }
}
