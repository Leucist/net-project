using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Educational_platform.Models;
using Microsoft.AspNetCore.Identity;

namespace Educational_platform.Data
{
    public class UsersContext : IdentityDbContext<Users>
    {
        public UsersContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Enrollments> Enrollments { get; set; }
        public DbSet<Models.Pages> Pages { get; set; }  // Fully qualify the Pages class

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            /* used for the lazy loading */
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite primary key
            modelBuilder.Entity<Enrollments>()
                .HasKey(e => new { e.CourseId, e.UserId });

            // Configure one-to-many relationship between Enrollments and Courses
            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            // Configure one-to-many relationship between Enrollments and Users
            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Models.Pages>()
                .HasKey(p => new { p.IdCourse, p.IdPage });

            // Configure one-to-many relationship between Courses and Pages
            modelBuilder.Entity<Models.Pages>()  // Fully qualify the Pages class
                .HasOne(p => p.Course)
                .WithMany(c => c.Pages)
                .HasForeignKey(p => p.IdCourse);
        }
    }
}
