using Microsoft.EntityFrameworkCore;
using Test.Entities.DBContext;
using Test.Entities.Models;

namespace Test.Repository
{

    public interface IUserRepository : IRepository<User>
    {
    }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TestDBContext context) : base(context)
        {
        }
    }
}