using Core.Database.Context;
using Core.Database.Entities;
using Core.Dtos.Quiz;
using Core.Dtos.Tasks;
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
    public class QuizzesRepository : BaseRepository<Quiz>
    {
        public HackatonDbContext _DbContext { get; set; }
        public QuizzesRepository(HackatonDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<bool> DoesNameExist(string name) 
        {
            return await _DbContext.Quizzes.AnyAsync(q => q.Name == name && !q.IsDeleted);
        }

        public async Task<Quiz> GetQuizWithPropertiesAsync(int quizId) 
        {
            return await _DbContext.Quizzes
                .Where(q => q.Id == quizId && !q.IsDeleted)
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync();
        }

        public async Task<PageableDto<Quiz>> GetQuizzesPaginaAsync(PageablePostModelQuiz request)
        {
            var dto = new PageableDto<Quiz>();

            var querry = _DbContext
                .Quizzes
                .Where(q => !q.IsDeleted)
                .AsQueryable();

            dto.NumarTotalRanduri = await querry.CountAsync();

            if (request.CourseId != null)
                querry = querry.Where(q => q.CourseId == request.CourseId.Value);

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
