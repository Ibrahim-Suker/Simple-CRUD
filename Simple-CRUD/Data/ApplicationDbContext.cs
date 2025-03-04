using Microsoft.EntityFrameworkCore;
using Simple_CRUD.Models;

namespace Simple_CRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }
    
    
}
