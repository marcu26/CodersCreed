using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Project
{
    public class PageablePostModelProject
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string? Name { get; set; }
        public int? userId { get; set; }
    }
}
