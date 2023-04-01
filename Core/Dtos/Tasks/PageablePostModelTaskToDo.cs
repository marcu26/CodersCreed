using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Tasks
{
    public class PageablePostModelTaskToDo
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public int? projectId { get; set; }
        public int? userId { get; set; }
        public bool beforeDeadline { get; set; }
        public bool afterDeadline { get; set; }
    }
}
