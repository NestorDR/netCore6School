using SchoolWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolWeb.Data
{
    // Visit: https://github.com/entityframeworktutorial/EF6-Code-First-Demo
    public class AppDbContext : DbContext
    {
        // To create constructor type "ctor and Tab twice"
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /*
         * In Entity Framework Core, the DbSet represents the set of entities. 
         * In a database, a group of similar entities is called an Entity Set (DbSet<TEntity> are called entity sets).
         * The DbSet enables to perform CRUD operations on the entity set.
         */
        // Entity sets
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
