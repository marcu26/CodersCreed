using Core.Database.Context;
using Core.Database.Entities;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class RolesRepository:BaseRepository<Role>
    {
        public HackatonDbContext _DbContext { get; set; }
        public RolesRepository(HackatonDbContext DbContext):base(DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _DbContext.Roles
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<Role> GetRoleWithUserRolesByIdAsync(int roleId)
        {
            return await _DbContext
                .Roles
                .Include(r => r.UserRoles)
                .SingleOrDefaultAsync(r => r.Id == roleId);
        }

        public async Task<Role> GetRoleWithUserRolesByNameAsync(string roleName)
        {
            return await _DbContext.Roles
                .Include(r => r.UserRoles)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByResetPasswordCodeAsync(string resetPasswordCode)
        {
            return await _DbContext.Users.Where(u => u.ResetPasswordCode == resetPasswordCode).FirstOrDefaultAsync();
        }

        public async Task<Role> GetRoleByTitle(string title)
        {
            return await _DbContext.Roles
                .Where(r=>r.Title == title)
                .Include(r=>r.UserRoles)
                .FirstOrDefaultAsync();
        }
    }
}
