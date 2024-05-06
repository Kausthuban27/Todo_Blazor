using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
                return NotFound("No task is found");
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
    }
}
