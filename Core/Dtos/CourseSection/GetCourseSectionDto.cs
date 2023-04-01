using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.CourseSection
{
    public class GetCourseSectionDto
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public int CourseId { get; set; }
    }
}
