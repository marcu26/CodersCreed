using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Entities
{
    public class Quiz : BaseEntity
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public List<Question> Questions { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
