using Core.Database.Entities;
using Core.Dtos.Roles;
using Core.UnitOfWork;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class RoleService
    {
        public EfUnitOfWork _efUnitOfWork { get; set; }

        public RoleService(EfUnitOfWork efUnitOfWork)
        {
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<List<RoleDto>> GetRolesDtoAsync()
        {
            var roles = await _efUnitOfWork._rolesRepository.GetRolesAsync();
            if (roles == null)
                throw new WrongInputException($"No roles exist in the database");
            return roles.Select(r => new RoleDto()
            {
                Id = r.Id,
                Title = r.Title
            }).ToList();
        }

        public async Task AddRoleToUserAsync(string roleName, int userId)
        {
            var role = await _efUnitOfWork._rolesRepository.GetRoleByTitle(roleName);
            var user = await _efUnitOfWork._usersRepository.GetUserByIdAsync(userId);
           
           
            if (role == null)
                throw new WrongInputException($"Role with title={roleName} does not exist.");

            if(role.IsDeleted)
                throw new WrongInputException($"Role with title={roleName} does not exist.");

            if (user == null)
                throw new WrongInputException($"User with id={userId} does not exist.");

            if(user.IsDeleted)
                throw new WrongInputException($"User with id={userId} does not exist.");


            var hasRole = user.UserRoles.Any(ur => ur.RoleId == role.Id);

            if (hasRole)
                throw new WrongInputException($"The user already has this role assigned. ");

            role.UserRoles.Add(new UserRole()
            {
                User = user
            });
            await _efUnitOfWork.SaveAsync();

        }
    }
}
