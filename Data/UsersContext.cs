using Microsoft.EntityFrameworkCore;
using Educational_platform.Models;

namespace Educational_platform.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Enrollments> Enrollments { get; set; }
        public DbSet<Educational_platform.Models.Pages> Pages { get; set; }  // Fully qualify the Pages class

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite primary key
            modelBuilder.Entity<Enrollments>()
                .HasKey(e => new { e.IdCourses, e.IdUsers });

            // Configure one-to-many relationship between Enrollments and Courses
            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.IdCourses);

            // Configure one-to-many relationship between Enrollments and Users
            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.IdUsers);

            // Configure one-to-many relationship between Courses and Pages
            modelBuilder.Entity<Educational_platform.Models.Pages>()  // Fully qualify the Pages class
                .HasOne(p => p.Course)
                .WithMany(c => c.Pages)
                .HasForeignKey(p => p.IdCourse);
        }
    }
}
