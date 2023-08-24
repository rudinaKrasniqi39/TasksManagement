using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TasksManagement.Models;

namespace TasksManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<SubAssignment> SubAssignments { get; set; }
    }
}