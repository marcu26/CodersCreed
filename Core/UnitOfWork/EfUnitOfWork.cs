using Core.Database.Context;
using Core.Repositories;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UnitOfWork
{
    public class EfUnitOfWork
    {
        private HackatonDbContext _context;

        public RolesRepository _rolesRepository { get; set; }
        public UsersRepository _usersRepository { get; set; }
        public UserRoleRepository _userRoleRepository { get; set; }

        public CoursesRepository _coursesRepository { get; set; }
 
        public EfUnitOfWork(
            HackatonDbContext context,
            RolesRepository rolesRepository,
            UserRoleRepository userRolesRepository,
            UsersRepository usersRepository,
            CoursesRepository coursesRepository
            )
        {
            _context = context;
            _rolesRepository = rolesRepository;
            _usersRepository = usersRepository;
            _userRoleRepository = userRolesRepository;
            _coursesRepository = coursesRepository;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
