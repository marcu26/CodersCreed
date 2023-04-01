using Core.Database.Context;
using Core.Database.Entities;
using Core.Dtos.Project;
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
    public class ProjectRepository : BaseRepository<Project>
    {
        public HackatonDbContext _DbContext { get; set; }
        public ProjectRepository(HackatonDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<Project> GetProjectByName(string name)
        {
            return await _DbContext.Projects
                .Where(p => p.Name == name && !p.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<Project> GetProjectById(int projectId)
        {
            return await _DbContext.Projects
                .Where(p => p.Id == projectId && !p.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<PageableDto<Project>> GetProjectsPageByFiltersAsync(PageablePostModelProject request)
        {
            var dto = new PageableDto<Project>();

            var querry = _DbContext
                .Projects
                .Where(p => !p.IsDeleted)
                .Include(p=>p.ProjectUsers)
                .AsQueryable();

            dto.NumarTotalRanduri = await querry.CountAsync();

            if (request.Name != null)
            {
                querry = querry.Where(p => p.Name.Contains(request.Name));
            }

            if (request.userId != null) 
            {
                querry = querry.Where(p => p.ProjectUsers.Any(pu => pu.UserId == request.userId.Value));
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
