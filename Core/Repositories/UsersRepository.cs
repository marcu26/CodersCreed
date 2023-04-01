using Core.Database.Context;
using Core.Database.Entities;
using Core.Utils.Pageable;
using Core.Dtos.Authentication;
using Core.Dtos.Roles;
using Core.Dtos.Users;
using Core.Utils;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Core.UnitOfWork;
using Infrastructure.Exceptions;

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

        public async Task<bool> IsUserHavingTheReward(int userId, int rewardId) 
        {
            return await  _DbContext.Users
                .Include(u => u.Rewards)
                .Where(u => u.Id == userId)
                .Where(u=> u.Rewards.Any(r=>r.Id==rewardId))
                .AnyAsync();
        }

        public async Task<User> GetUserByIdAsyncWithProperties(int userId) 
        {
            return await _DbContext.Users.Where(u => u.Id == userId && !u.IsDeleted)
                .Include(u=>u.Rewards).FirstOrDefaultAsync();
        }

        public async Task AddPointsAsync(int points, int userId)
        {
            var user = await GetUserByIdAsync(userId);

            if (user == null)
                throw new WrongInputException($"The user with the id: {userId} does not exist");

            if (points < 0)
                throw new WrongInputException("You can not add a negative number");

            user.Points += points;

            await _DbContext.SaveChangesAsync();
        }

        public async Task AddXpAsync(int userId,int xpPoints)
        {
            var user = await GetUserByIdAsync(userId);

            if(user == null)
                throw new WrongInputException($"The user with the id: {userId} does not exist");

            user.Experience += xpPoints;

            await _DbContext.SaveChangesAsync();
        }
    }
}
