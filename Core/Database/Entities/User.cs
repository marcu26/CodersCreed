using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Entities
{
    public class User:BaseEntity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ResetPasswordCode { get; set; }
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
