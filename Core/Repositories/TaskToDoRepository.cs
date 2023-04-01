using Core.Database.Context;
using Core.Database.Entities;
using Core.Dtos.Tasks;
using Core.Utils.Pageable;
using Infrastructure.Base;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class TaskToDoRepository : BaseRepository<TaskToDo>
    {
        public HackatonDbContext _DbContext { get; set; }
        public TaskToDoRepository(HackatonDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<TaskToDo> GetTaskById(int taskId)
        {
            return await _DbContext.TasksToDo
                .FirstOrDefaultAsync(t => t.Id == taskId && !t.IsDeleted);
        }

        public async Task<PageableDto<TaskToDo>> GetTasksToDoPageByFiltersAsync(PageablePostModelTaskToDo request)
        {
            var dto = new PageableDto<TaskToDo>();

            var querry = _DbContext
                .TasksToDo
                .Where(tm => !tm.IsDeleted)
                .AsQueryable();

            dto.NumarTotalRanduri = await querry.CountAsync();

            if(request.afterDeadline)
            {
                querry = querry.Where(t => t.Deadline < DateTime.Now);
            }

            if (request.beforeDeadline)
            {
                querry = querry.Where(t => t.Deadline > DateTime.Now);
            }

            if(request.projectId != null)
            {
                querry = querry.Where(t => t.ProjectId == request.projectId.Value);
            }

            if (request.userId != null)
            {
                querry = querry.Where(t => t.UserId == request.userId.Value);
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
