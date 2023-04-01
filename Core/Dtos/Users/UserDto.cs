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
        public List<RoleDto> Roles { get; set; }

    }
}
