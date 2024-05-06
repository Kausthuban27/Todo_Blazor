using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Model;
using TodoAPI.ViewModel;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _db;
        private readonly IMapper _map;
        public TodoController(TodoDbContext db, IMapper mapper)
        {
            _db = db;
            _map = mapper;
        }

        [HttpGet("GetUser")]
        public IActionResult GetUserData(string username, string password)
        {
            var user = _db.Users.Where(u => u.Username == username && u.Password == password);
            if (user.Any())
            {
                return Ok("User Do Exist");
            }
            return BadRequest("User Does not exist");
        }

        [HttpPost("AddUser")]
        public IActionResult PostUserData(UserData userData)
        {
            var user = _map.Map<User>(userData);
            if (userData != null)
            {
                var users = _db.Users.Where(u => u.Username == user.Username);
                if (!users.Any())
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return Ok();
                }
                return BadRequest("User Already Exists");
            }
            return BadRequest();
        }

        [HttpGet("GetTasks")]
        public IActionResult GetTasks(string username)
        {
            var tasks = _db.Todos.Where(u => u.Username == username).ToList();
            if(tasks == null || !tasks.Any())
            {
                return BadRequest();
            }
            else
            {
                return Ok(tasks);
            }
        }

        [HttpPost("AddTasks")]
        public IActionResult AddTasks(TodoData todoData)
        {
            var task = _map.Map<Todo>(todoData);
            if(todoData != null)
            {
                _db.Todos.Add(task);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask([FromBody] TodoData updatedTask)
        {
            var task = _db.Todos.FirstOrDefault(t => t.TaskName == updatedTask.TaskName);

            if (task == null)
            {
                return BadRequest("Task not found."); 
            }

            task.IsDone = updatedTask.IsDone; 
            _db.SaveChanges(); 

            return Ok("Task status updated successfully.");
        }

        [HttpDelete("RemoveCompletedTasks")]
        public IActionResult RemoveCompletedTasks(string username)
        {
            var tasksToRemove = _db.Todos.Where(t => t.Username == username && t.IsDone).ToList();

            if (tasksToRemove.Any())
            {
                _db.Todos.RemoveRange(tasksToRemove); 
                _db.SaveChanges(); 
                return Ok("Tasks removed successfully.");
            }
            return BadRequest("No completed tasks found."); 
        }
    }
}
