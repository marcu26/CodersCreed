using Core.Database.Context;
using Core.Database.Entities;
using Core.Dtos.Badges;
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
    public class BadgesRepository : BaseRepository<Badge>
    {
        public HackatonDbContext _DbContext { get; set; }
        public BadgesRepository(HackatonDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<bool> DoesNameExist(string name)
        {
            return await _DbContext.Badges.AnyAsync(b => b.Name == name && !b.IsDeleted);
        }

        public async Task<bool> DoesImageExist(string image)
        {
            return await _DbContext.Badges.AnyAsync(b => b.Image == image);
        }

        public async Task<bool> DoesDescriptionExist(string description)
        {
            return await _DbContext.Badges.AnyAsync(b => b.Description == description);
        }
        public async Task<PageableDto<Badge>> GetPaginaAsync(PageablePostModelBadges request)
        {
            var dto = new PageableDto<Badge>();

            var query = _DbContext.Badges
                .Where(e => !e.IsDeleted)
                .Include(e => e.Users)
                .AsQueryable();

            dto.NumarTotalRanduri = await query.CountAsync();

            if (request.userId != null)
            {
                query = query.Where(e => e.Users.Any(u => u.Id == request.userId.Value));
            }

            dto.NumarRanduriFiltrate = await query.CountAsync();

            dto.Pagina = await query
                .Skip(request.start)
                .Take(request.length)
                .ToListAsync();

            return dto;
        }

    }
}
