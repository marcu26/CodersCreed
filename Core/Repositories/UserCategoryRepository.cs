using Core.Database.Context;
using Core.Database.Entities;
using Infrastructure.Base;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class UserCategoryRepository : BaseRepository<UserCategory>
    {
        public HackatonDbContext _DbContext { get; set; }
        public UserCategoryRepository(HackatonDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<UserCategory> CheckIfUserAlreadyHasCategory(int userId, int categoryId)
        {
            return await _DbContext.UserCategories
                .Where(uc => uc.UserId == userId && uc.CategoryId == categoryId && !uc.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task AddExperienceInCategory(int userId, int categoryId, int expAmount)
        {
            var userCategory = await _DbContext.UserCategories.Where(u => u.UserId == userId && u.CategoryId == categoryId && !u.IsDeleted).FirstOrDefaultAsync();

            if (userCategory == null)
            {
                throw new WrongInputException($"User with id {userId} does not have that category asigned  does not exist");
            }

            userCategory.Experience += expAmount;

            await _DbContext.SaveChangesAsync();
            
        }
    }
}
