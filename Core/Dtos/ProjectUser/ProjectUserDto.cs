using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.ProjectUser
{
    public class ProjectUserDto
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string ProjectName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int Points { get; set; } = 0;
        public int Experience { get; set; } = 0;
    }
}
