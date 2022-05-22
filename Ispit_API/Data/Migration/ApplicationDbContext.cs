using Ispit_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ispit_API.Data.Migration
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        { 
        }
        public DbSet<ToDoList> ToDoLists { get; set; }
    }
}
