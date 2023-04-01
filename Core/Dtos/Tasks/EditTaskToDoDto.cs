using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Tasks
{
    public class EditTaskToDoDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int Points { get; set; }
        public int Experience { get; set; } = 0;
    }
}
