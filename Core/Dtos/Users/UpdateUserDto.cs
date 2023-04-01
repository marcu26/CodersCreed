using Core.Dtos.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Users
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public List<RolesId> Roles { get; set; }
    }
}
