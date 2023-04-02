using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.CoursesUsers
{
    public class CreateCourseUserDto
    {
        public int userId { get; set; }
        public int courseId { get; set; }
    }
}
