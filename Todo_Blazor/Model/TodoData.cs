using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo_Blazor.Model
{
    public partial class TodoData
    {
        public int Id { get; set; }
        public string TaskName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public bool IsDone { get; set; } = false;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
    }
}
