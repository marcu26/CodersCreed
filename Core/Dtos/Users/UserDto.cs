using Core.Dtos.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Users
{
    public class UserDto
    {
        public UserDto()
        {
            Roles = new List<RoleDto>();
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? Image { get; set; }
        public string Function { get; set; }
        public int Level { get; set; }
        public int Points { get; set; }
        public int Rank { get; set; }
        public List<RoleDto> Roles { get; set; }

    }
}
