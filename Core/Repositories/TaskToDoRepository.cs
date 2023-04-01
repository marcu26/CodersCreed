using Core.Database.Context;
using Core.Database.Entities;
using Infrastructure.Base;
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
    }
}
