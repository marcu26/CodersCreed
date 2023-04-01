﻿using Core.Database.Context;
using Core.Database.Entities;
using Core.Utils.Pageable;
using Core.Dtos.Authentication;
using Core.Dtos.Roles;
using Core.Dtos.Users;
using Core.Utils;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class UsersRepository:BaseRepository<User>
    {
        public HackatonDbContext _DbContext { get; set; }
        public UsersRepository(HackatonDbContext DbContext):base(DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _DbContext.Users
                .Include(ur => ur.UserRoles)
                .ThenInclude(ur=>ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);
        }


        public async Task<bool> CheckIfUserAlreadyExistsAsync(string email)
        {
            return await _DbContext.Users.Where(e => e.Email == email && !e.IsDeleted).AnyAsync();
        }

        public async Task<PageableDto<User>> GetPaginaAsync(int offset, int pageSize)
        {
            var dto = new PageableDto<User>();

            var query = _DbContext.Users
                .Where(e => !e.IsDeleted)
                .Include(e=>e.UserRoles)
               .AsQueryable();
            dto.NumarTotalRanduri = await query.CountAsync();
            dto.NumarRanduriFiltrate= await query.CountAsync();

            dto.Pagina= await query
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync();

            return dto;
        }

        public async Task<User> GetUserByCredentialsAsync(AuthenticationRequestDto payload)
        {
            return await _DbContext.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Email == payload.Email && u.Password == Base64Encoder.Encode(payload.Password));
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _DbContext.Users
                .Where(e => e.Email == email)
                .Where(e => !e.IsDeleted)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckResetPasswordCodeExistence(string resetPasswordCode)
        {
            return await _DbContext.Users.AnyAsync(u => u.ResetPasswordCode == resetPasswordCode);
        }

        public async Task<User> GetUserByResetPasswordCodeAsync(string resetPasswordCode)
        {
            return await _DbContext.Users.Where(u => u.ResetPasswordCode == resetPasswordCode).FirstOrDefaultAsync();
        }


    }
}
