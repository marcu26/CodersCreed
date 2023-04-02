using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Quiz> Quizzes { get; set; }
        public List<Category> Categories { get; set; }
        public int Xp { get; set; }
        public List<User> Users { get; set; }
        public List<CourseSection> CourseSections { get; set; }
    }
}
