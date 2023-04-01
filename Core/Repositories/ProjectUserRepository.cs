using Core.Database.Context;
using Core.Database.Entities;
using Core.Dtos.ProjectUser;
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
    public class ProjectUserRepository : BaseRepository<ProjectUser>
    {
        public HackatonDbContext _DbContext { get; set; }
        public ProjectUserRepository(HackatonDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task AddAsync(ProjectUser projectUser)
        {
            await _DbContext.ProjectUsers.AddAsync(projectUser);
        }

        public async Task<ProjectUser> GetProjectManagerAsync(int projectId, int userId)
        {
            return await _DbContext.ProjectUsers
                .FirstOrDefaultAsync(tm => tm.isManager && tm.ProjectId == projectId && tm.UserId == userId && !tm.IsDeleted);
        }

        public async Task<ProjectUser> GetProjectUserEntryAsync(int projectId, int userId)
        {
            return await _DbContext.ProjectUsers
                .FirstOrDefaultAsync(tm => tm.UserId == userId && tm.ProjectId == projectId && !tm.IsDeleted);
        }

        public async Task<ProjectUser> GetProjectUserByIdAsync(int id)
        {
            return await _DbContext.ProjectUsers
                .Include(tm => tm.User)
                .Include(tm => tm.Project)
                .FirstOrDefaultAsync(tm => tm.Id == id && !tm.IsDeleted);
        }

        public async Task<PageableDto<ProjectUser>> GetProjectsOfUserPageByFiltersAsync(PageablePostModelProjectUsers request)
        {
            var dto = new PageableDto<ProjectUser>();

            var querry = _DbContext
                .ProjectUsers
                .Include(tm => tm.User)
                .Include(tm => tm.Project)
                .Where(tm => !tm.IsDeleted)
                .AsQueryable();

            dto.NumarTotalRanduri = await querry.CountAsync();

            if(request.userId != null)
            {
                querry = querry.Where(tm => tm.UserId == request.userId.Value);
            }

            if (request.projectId != null)
            {
                querry = querry.Where(tm => tm.ProjectId == request.projectId.Value);
            }


            dto.NumarRanduriFiltrate = await querry.CountAsync();

            dto.Pagina = await querry
                .OrderByDescending(r => r.Timestamp)
                .Skip(request.start)
                .Take(request.length)
                .ToListAsync();

            return dto;
        }

        
    }
}
