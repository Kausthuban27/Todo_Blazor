using Microsoft.AspNetCore.Mvc;
using TodoAPI.Interface;
using TodoAPI.ViewModel;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public bool isSuccess;
        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpGet("GetUser")]
        public IActionResult GetUser(string username, string password)
        {
            isSuccess = _user.GetUser(username, password);
            if (isSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(UserData user)
        {
            isSuccess = _user.AddUser(user);
            if (isSuccess)
            {
                _user.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
