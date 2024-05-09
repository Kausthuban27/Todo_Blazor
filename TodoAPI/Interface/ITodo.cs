using TodoAPI.Model;
using TodoAPI.ViewModel;

namespace TodoAPI.Interface
{
    public interface ITodo
    {
        bool AddTodoTasks(TodoData todos);
        IEnumerable<Todo> GetTodoTasks(string username);
        bool UpdateTodoTasks(Todo todos);
        bool DeleteTodoTasks(string username);
        void SaveChanges();
    }
}
