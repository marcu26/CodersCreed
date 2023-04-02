using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Entities
{
    public class User:BaseEntity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ResetPasswordCode { get; set; }
        public string Image { get; set; }
        public string Function { get; set; }
        public int Points { get; set; } = 0;
        public int Experience { get; set; } = 0;
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public List<ProjectUser> ProjectUsers { get; set; }
        public List<UserCategory> UserCategories { get; set; }
        public List<Reward> Rewards { get; set; }
        public List<Badge> Badges { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
