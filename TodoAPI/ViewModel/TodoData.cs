using Microsoft.EntityFrameworkCore;

namespace TodoAPI.ViewModel
{
    public class TodoData
    {
        public string TaskName { get; set; } = null!;
        public string Username { get; set; } = null!;
    }
}
