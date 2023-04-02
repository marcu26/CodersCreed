using Core.Database.Context;
using Core.Database.Entities;
using Core.Utils.Pageable;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class CoursesRepository : BaseRepository<Course>
    {
        public HackatonDbContext _DbContext { get; set; }
        public CoursesRepository(HackatonDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<bool> DoesNameExist(string name)
        {
            return await _DbContext.Courses.AnyAsync(c => c.Name == name && !c.IsDeleted);
        }

        public async Task<PageableDto<Course>> GetPaginaAsync(int offset, int pageSize)
        {
            var dto = new PageableDto<Course>();

            var query = _DbContext.Courses
                .Where(e => !e.IsDeleted)
                .AsQueryable();

            dto.NumarTotalRanduri = await query.CountAsync();
            dto.NumarRanduriFiltrate = await query.CountAsync();

            dto.Pagina = await query
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync();

            return dto;
        }

        public async Task<bool> IsCourseAllreadyHadbyUser(int courseId, int userId)
        {
            return await _DbContext.Courses
                        .Include( c => c.Users)
                        .Where(c => c.Id == userId)
                        .Where(c => c.Users.Any(u => u.Id== userId))
                        .AnyAsync();
        }

    }
}
