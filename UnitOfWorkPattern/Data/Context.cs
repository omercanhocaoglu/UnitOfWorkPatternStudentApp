using Microsoft.EntityFrameworkCore;
using UnitOfWorkPattern.Models;

namespace UnitOfWorkPattern.Data
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options):base(options) 
        {

        }
        public DbSet<Student> Students { get; set; }
    }
}
