using ElasticPMTServer.Models;
using Nest;

namespace ElasticPMTServer.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository() : base("projects")
        { }
    }
}
