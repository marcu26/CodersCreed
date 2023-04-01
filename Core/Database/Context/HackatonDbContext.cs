using Core.Database.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Database.Context
{
    public class HackatonDbContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>  builder.AddConsole() );
        public HackatonDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            optionsBuilder.UseSqlServer(AppConfig.ConnectionStringConfig.MainDatabase);
        }

        
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<TaskToDo> TasksToDo { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Username).HasMaxLength(128);

            });

            modelBuilder.Entity<User>().ToTable("Users").HasKey(u => u.Id);
            modelBuilder.Entity<Role>().ToTable("Roles").HasKey(r => r.Id);
            modelBuilder.Entity<User>().Property(u => u.ResetPasswordCode).IsRequired(false);
            modelBuilder.Entity<UserRole>().ToTable("UserRoles").HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<TaskToDo>().ToTable("TasksToDo").HasKey(t => t.Id);
            modelBuilder.Entity<Course>().ToTable("Courses").HasKey(c => c.Id);
            modelBuilder.Entity<Answer>().ToTable("Answers").HasKey(a => a.Id);
            modelBuilder.Entity<Question>().ToTable("Questions").HasKey(q => q.Id);
            modelBuilder.Entity<Project>().ToTable("Projects").HasKey(p => p.Id);
            modelBuilder.Entity<ProjectUser>().ToTable("ProjectUsers").HasKey(pu => new {pu.ProjectId, pu.UserId});

            modelBuilder.Entity<Question>().HasMany(q => q.Answers)
                .WithOne(a=>a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectUser>()
                .HasOne(p => p.Project)
                .WithMany(p => p.ProjectUsers)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectUser>()
                .HasOne(p => p.User)
                .WithMany(p => p.ProjectUsers)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reward>().ToTable("Rewards").HasKey(r => r.Id);
       
            new DbInitializer(modelBuilder).Seed();
        }
    }
}
