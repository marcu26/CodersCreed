using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int Experience { get; set; }
        public List<Project> Projects { get; set; }
        public List<Course> Courses { get; set; }
        public List<UserCategory> UserCategories { get; set; }
    }
}
