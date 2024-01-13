using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.DbServices
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
        public DbSet<TaskModel> Tasks { get; set; }
    }
}
