using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Service;
using Tasked = Test.Entities.Models.Task;

namespace Test.WebApi.Controllers
{
    [Authorize]
    public class TaskController : BaseController
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_taskService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_taskService.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Tasked task)
        {
            if(_taskService.Add(task) > 0)
                return Ok(new { status = true, Data = task, Message = "Save Success!" });
            return Ok(new { status = false, Data = (string)null, Message = "Error Success!" });
        }

        [HttpPut]
        public IActionResult Put([FromBody]Tasked task)
        {
            if(_taskService.Modify(task) > 0)
                return Ok(new { status = true, Data = task, Message = "Update Success!" });
            return Ok(new { status = false, Data = (string)null, Message = "Uupdate Error!" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(_taskService.Delete(id) > 0)
                return Ok(new { status = true, Data = (string)null, Message = "Delete Success!" });
            return Ok(new { status = false, Data = (string)null, Message = "Delete Error!" });
        }

        [HttpGet(nameof(GetAssign))]
        public ActionResult GetAssign(int userId)
        {
            return Ok(_taskService.GetTaskAssign(userId));
        }
    }
}