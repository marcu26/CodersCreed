using Core.Database.Context;
using Core.Database.Entities;
using Core.Dtos.Rewards;
using Core.Utils.Pageable;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class RewardsRepository : BaseRepository<Reward>
    {
        public HackatonDbContext _DbContext { get; set; }
        public RewardsRepository(HackatonDbContext DbContext) : base(DbContext)
        {
            _DbContext= DbContext;
        }

        public async Task<bool> DoesNameExist(string name)
        {
            return await _DbContext.Rewards.AnyAsync(r => r.Name== name && !r.IsDeleted);
        }

        public async Task<PageableDto<Reward>> GetPaginaAsync(PageablePostModelReward requsest)
        {
            var dto = new PageableDto<Reward>();

            var query = _DbContext.Rewards
                .Where(e => !e.IsDeleted)
                .Include(e=>e.Users)
                .AsQueryable();

            dto.NumarTotalRanduri = await query.CountAsync();

            if (requsest.userId != null) 
            {
                query = query.Where(q => q.Users.Any(u => u.Id == requsest.userId.Value));
            }

            dto.NumarRanduriFiltrate = await query.CountAsync();

            dto.Pagina = await query
                .Skip(requsest.start)
                .Take(requsest.length)
                .ToListAsync();

            return dto;
        }
    }
}
