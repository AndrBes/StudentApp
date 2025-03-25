using Microsoft.EntityFrameworkCore;

namespace StudentData
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {

        }
    }
}
