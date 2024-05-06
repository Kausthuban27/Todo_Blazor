using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoAPI.Model
{
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        [ForeignKey("Username")]
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
    }
}
