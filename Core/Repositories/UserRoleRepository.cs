using Core.Database.Context;
using Core.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class UserRoleRepository
    {
        public HackatonDbContext _DbContext { get; set; }

        public UserRoleRepository(HackatonDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public void RemoveUserRole(UserRole userRole)
        {
            _DbContext.Set<UserRole>().Remove(userRole);
        }
    }
}
