using AutoMapper;
using TodoAPI.Interface;
using TodoAPI.Model;
using TodoAPI.ViewModel;

namespace TodoAPI.Services
{
    public class TodoService : ITodo
    {
        private readonly TodoDbContext _db;
        private readonly IMapper _map;
        public TodoService(TodoDbContext db, IMapper mapper)
        {
            _db = db;
            _map = mapper;
        }

        public bool AddTodoTasks(TodoData todos)
        {
            var task = _map.Map<Todo>(todos);
            if (todos != null)
            {
                _db.Todos.Add(task);
                return true;
            }
            return false;
        }

        public bool DeleteTodoTasks(string username)
        {
            var tasksToRemove = _db.Todos.Where(t => t.Username == username && t.IsDone).ToList();
            if (tasksToRemove.Any())
            {
                _db.Todos.RemoveRange(tasksToRemove);
                return true;
            }
            return false;
        }

        public IEnumerable<Todo> GetTodoTasks(string username)
        {
            var tasks = _db.Todos.Where(u => u.Username == username).ToList();
            if (tasks == null || !tasks.Any())
            {
                return [];
            }
            else
            {
                return tasks;
            }
        }

        public bool UpdateTodoTasks(Todo todos)
        {
            var task = _db.Todos.FirstOrDefault(t => t.TaskName == todos.TaskName);
            if(task == null)
            {
                return false;
            }
            task.IsDone = todos.IsDone;
            return true;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
