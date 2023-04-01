using Core.Database.Entities;
using Core.Repositories;
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
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }

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
            modelBuilder.Entity<Quiz>().ToTable("Quizzes").HasKey(q => q.Id);
            modelBuilder.Entity<Project>().ToTable("Projects").HasKey(p => p.Id);
            modelBuilder.Entity<ProjectUser>().ToTable("ProjectUsers").HasKey(pu => new {pu.ProjectId, pu.UserId});
            modelBuilder.Entity<Category>().ToTable("Categories").HasKey(c => c.Id);
            modelBuilder.Entity<UserCategory>().ToTable("UserCategories").HasKey(uc => new { uc.CategoryId, uc.UserId });

            modelBuilder.Entity<Question>().HasMany(q => q.Answers)
                .WithOne(a=>a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Quiz>().HasMany(q => q.Questions)
                .WithOne(qz => qz.Quiz)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>().HasMany(c => c.Quizzes)
                .WithOne(qz => qz.Course)
                .HasForeignKey(qz => qz.CourseId)
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

            modelBuilder.Entity<TaskToDo>()
                .HasOne(t => t.Project)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Projects)
                .WithMany(c => c.Categories);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Courses)
                .WithMany(c => c.Categories);

            modelBuilder.Entity<UserCategory>()
                .HasOne(t => t.Category)
                .WithMany(t => t.UserCategories)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCategory>()
                .HasOne(t => t.User)
                .WithMany(t => t.UserCategories)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reward>().ToTable("Rewards").HasKey(r => r.Id);
            modelBuilder.Entity<Badge>().ToTable("Badges").HasKey(b => b.Id);
            modelBuilder.Entity<User>().Property(u => u.Image).IsRequired(false);
       
            new DbInitializer(modelBuilder).Seed();
        }
    }
}
