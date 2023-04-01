using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Entities
{
    public class TaskToDo : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int Points { get; set; }
        public int Experience { get; set; } = 0;
        public bool IsDone { get; set; } = false;
        public int? UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
