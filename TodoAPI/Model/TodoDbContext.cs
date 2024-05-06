using Microsoft.EntityFrameworkCore;

namespace TodoAPI.Model
{
    public partial class TodoDbContext : DbContext
    {
        public TodoDbContext()
        {
        }
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=CEI2103\\SQLEXPRESS;database=Todo;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
