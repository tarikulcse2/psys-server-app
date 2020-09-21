using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Entities.Models;
using Test.Repository;
using Task = System.Threading.Tasks.Task;

namespace Test.Service
{
    public interface IUserService
    {
        IEnumerable<object> GetAll();
        int Registration(User user);
        User CheckUserLogin(User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        User IUserService.CheckUserLogin(User user)
        {
            return _userRepository.FirstOrDefault(s => s.Email == user.Email && s.Password == user.Password);
        }

        IEnumerable<object> IUserService.GetAll()
        {
            return _userRepository.GetAll().Select(r => new {r.Id, r.FirstName, r.LastName});
        }

        int IUserService.Registration(User user)
        {
            return _userRepository.Add(user);
        }
    }
}