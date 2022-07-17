using SchoolWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolWeb.Data
{
    // Visit: https://github.com/entityframeworktutorial/EF6-Code-First-Demo
    public class AppDbContext : IdentityDbContext<User>
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
        public DbSet<TeacherModel> Teachers { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<StudentAddressModel> StudentAddresses { get; set; }
        public DbSet<GradeModel> Grades { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
    }
}
