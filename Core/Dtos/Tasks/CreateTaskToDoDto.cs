using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Tasks
{
    public class CreateTaskToDoDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int Points { get; set; }
        public int Experience { get; set; } = 0;
        public int? UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
