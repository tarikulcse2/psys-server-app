using Test.Entities.DBContext;
using Test.Entities.Models;

namespace Test.Repository
{

    public interface ITaskRepository : IRepository<Task>
    {
    }

    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(TestDBContext context) : base(context)
        {
        }
    }
}