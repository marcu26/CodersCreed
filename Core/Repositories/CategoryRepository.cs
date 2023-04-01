using Core.Database.Context;
using Core.Database.Entities;
using Core.Dtos.Category;
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
    public class CategoryRepository : BaseRepository<Category>
    {
        public HackatonDbContext _DbContext { get; set; }
        public CategoryRepository(HackatonDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _DbContext.Categories
                .FirstOrDefaultAsync(t => t.Id == categoryId && !t.IsDeleted);
        }

        public async Task<PageableDto<Category>> GetCategoriesPageByFiltersAsync(PageablePostModelCategory request)
        {
            var dto = new PageableDto<Category>();

            var querry = _DbContext
                .Categories
                .Where(tm => !tm.IsDeleted)
                .AsQueryable();

            dto.NumarTotalRanduri = await querry.CountAsync();

            if (request.Name != null)
            {
                querry = querry.Where(tm => tm.Name.Contains(request.Name));
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
