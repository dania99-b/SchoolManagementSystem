using Demo.Controllers;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.context
{
    public class MyContext : DbContext

    {

        public MyContext(DbContextOptions<MyContext> MyContext):base(MyContext) {
            
        
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("MySQLConnection");
                dbContextOptionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
          new Role { id = 1, name = "student" },
          new Role { id = 2, name = "teacher" },
          new Role { id = 3, name = "admin" }

);



            modelBuilder.Entity<Course>()
       .HasOne(c => c.user)
       .WithMany()
       .HasForeignKey(c => c.user_id)
       .OnDelete(DeleteBehavior.Restrict);

            // ⛔ Prevent cascade delete from Enrollment → User
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.user)
                .WithMany()
                .HasForeignKey(e => e.user_id)
                .OnDelete(DeleteBehavior.Restrict);

            // ⛔ Prevent cascade delete from User → Role (just to be safe)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.role_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
      .HasOne(g => g.user)
      .WithMany()
      .HasForeignKey(g => g.student_id)
      .OnDelete(DeleteBehavior.NoAction);

        }



       
      
        public DbSet<Course> Courses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User>Users { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Grade> Grades { get; set; }




    }
}
