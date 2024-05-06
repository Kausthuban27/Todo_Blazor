using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoAPI.Model;
using TodoAPI.ViewModel;
namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserDetailsController : ControllerBase
    {
        private readonly TodoDbContext _db;
        private readonly IMapper _map;
        public UserDetailsController(TodoDbContext db, IMapper mapper) 
        {
            _db = db;
            _map = mapper;
        }

        [HttpGet("GetUser")]
        public IActionResult GetUserData(string username, string password)
        {
            var user = _db.Users.Where(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password.Equals(password, StringComparison.OrdinalIgnoreCase));
            if(user != null)
            {
                return Ok("User Do Exist");
            }
            return BadRequest("User Does not exist");
        }

        [HttpPost("AddUser")]
        public IActionResult PostUserData([FromBody] UserData userData)
        {
            var user = _map.Map<User>(userData);
            if(userData != null)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
