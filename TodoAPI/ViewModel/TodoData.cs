using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace TodoAPI.ViewModel
{
    public class TodoData
    {
        public string TaskName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public bool IsDone { get; set; } = false;
    }
}
