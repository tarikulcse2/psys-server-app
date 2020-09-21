using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Test.Entities.Models;
using Test.Repository;

namespace Test.Service
{

    public interface ITaskService
    {
        IEnumerable<Task> GetAll();
        IEnumerable<Task> GetTaskAssign(int userId);
        IEnumerable<Task> GetAllByUser(int userId);
        Task GetById(int id);
        int Add(Task task);
        int Modify(Task task);
        int Delete(int id);
    }

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        IEnumerable<Task> ITaskService.GetAll()
        {
            return _taskRepository.GetAll().Include(r => r.User);
        }

        IEnumerable<Task> ITaskService.GetAllByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        Task ITaskService.GetById(int id)
        {
            return _taskRepository.FirstOrDefault(s => s.Id == id);
        }

        int ITaskService.Modify(Task task)
        {
            return _taskRepository.Update(task);
        }

        int ITaskService.Add(Task task)
        {
            return _taskRepository.Add(task);
        }

        int ITaskService.Delete(int id)
        {
            var task = _taskRepository.FirstOrDefault(s => s.Id == id);
            return _taskRepository.Remove(task);
        }

        IEnumerable<Task> ITaskService.GetTaskAssign(int userId)
        {
            return _taskRepository.Where(e => e.AssignUserId == userId).ToList();
        }
    }
}