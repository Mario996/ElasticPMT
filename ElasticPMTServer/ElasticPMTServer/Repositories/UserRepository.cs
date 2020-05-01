using ElasticPMTServer.Models;
using Nest;

namespace ElasticPMTServer.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository() : base("users")
        { }
    }
}
