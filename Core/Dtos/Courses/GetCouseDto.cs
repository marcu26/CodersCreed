using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Courses
{
    public class GetCouseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public int Xp { get; set; }
        public bool isMandatory { get; set; }
    }
}
