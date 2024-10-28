using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoListApi.auth.TodoListApi.auth;
using TodoListApi.Dto_s;
using TodoListApi.Interface;
using TodoListApi.Models;
using TodoListApi.Repository;

namespace TodoListApi.Controllers
{
    [BasicAuth]
    [ApiController]
    [Route("api/[Controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskReposıtory _taskReposıtory;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public TaskController(ITaskReposıtory taskReposıtory, IMapper mapper, IUserRepository userRepository)
        {
            this._taskReposıtory = taskReposıtory;
            this._mapper = mapper;
            this._userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TaskDto>))]
        public ActionResult<List<TaskDto>> GetAllTasks()
        {
            var tasks = _mapper.Map<List<TaskDto>>(_taskReposıtory.GetAllTasks());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(tasks);

        }

        [HttpGet("completed")]
        [ProducesResponseType(200, Type = typeof(List<TaskDto>))]
        public ActionResult<List<TaskDto>> GetCompletedTasks()
        {
            var tasks = _mapper.Map<List<TaskDto>>(_taskReposıtory.GetCompletedTasks());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(tasks);
        }

        [HttpGet("incomplete")]
        [ProducesResponseType(200, Type = typeof(List<TaskDto>))]
        public ActionResult<List<TaskDto>> GetIncompleteTasks()
        {
            var tasks = _mapper.Map<List<TaskDto>>(_taskReposıtory.GetIncompleteTasks());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(tasks);
        }

        [HttpGet("{taskId}")]
        [ProducesResponseType(200, Type = typeof(TaskDto))]
        [ProducesResponseType(404)]
        public ActionResult<TaskDto> GetTaskById(int taskId)
        {
            var task = _mapper.Map<TaskDto>(_taskReposıtory.GetTask(taskId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateTask(CreateTaskDto task)
        {
            if (task == null)
            {
                return BadRequest(ModelState);
            }
            var user = _userRepository.GetUser(2);
            var Mappingtask = _mapper.Map<TodoTask>(task);
            Mappingtask.User = user;
            if (!_taskReposıtory.CreateTask(Mappingtask))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfuly Created");
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTask(UpdateTaskDto task)
        {
            if (task == null)
            {

                return BadRequest(ModelState);

            }
            if (!_taskReposıtory.TaskExist(task.Id))
            {
                return NotFound(ModelState);
            }
            var MappingTask = _mapper.Map<TodoTask>(task);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_taskReposıtory.UpdateTask(MappingTask))
            {
                ModelState.AddModelError("", "Something went wrong while update");
                return StatusCode(500, ModelState);
            }
            return NoContent();
            
        }

        [HttpPut("UpdateTaskStatus")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateTaskStatus(UpdateTaskStatusDto taskStatus)
        {
            if (taskStatus == null)
            {

                return BadRequest(ModelState);

            }
            if (!_taskReposıtory.TaskExist(taskStatus.Id))
            {
                return NotFound(ModelState);
            }
            var status = _taskReposıtory.GetStatus((int)taskStatus.TaskStatus);
            var task = _taskReposıtory.GetTask(taskStatus.Id);
            task.TodoStatus = status;
            if (!_taskReposıtory.UpdateTask(task))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
            }
            return NoContent();

        }

        [HttpDelete("{TaskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTask(int TaskId)
        {
            if (!_taskReposıtory.TaskExist(TaskId))
            {
                return NotFound();
            }
            var task =_taskReposıtory.GetTask(TaskId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_taskReposıtory.DeleteTask(task))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
