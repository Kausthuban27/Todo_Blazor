using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo_Blazor.SharedService;
using TodoAPI.Interface;
using TodoAPI.Model;
using TodoAPI.ViewModel;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodo _todo;
        private bool isSuccess;
        public TodoController(ITodo todo)
        {
            _todo = todo;
        }

        [HttpGet("GetTasks")]
        public IActionResult GetTasks(string username)
        {
            var todoList = _todo.GetTodoTasks(username);
            if(todoList == null)
            {
                return BadRequest();
            }
            return Ok(todoList);
        }

        [HttpPost("AddTasks")]
        public IActionResult AddTasks(TodoData todoData)
        {
            isSuccess = _todo.AddTodoTasks(todoData);
            if(isSuccess)
            {
                _todo.SaveChanges();
                return CreatedAtAction(nameof(AddTasks), "Success");
            }
            return BadRequest();
        }

        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask(Todo updatedTask)
        {
            isSuccess = _todo.UpdateTodoTasks(updatedTask);
            if(isSuccess)
            {
                _todo.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("RemoveCompletedTasks")]
        public IActionResult RemoveCompletedTasks(string username)
        {
            isSuccess = _todo.DeleteTodoTasks(username);
            if(isSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
