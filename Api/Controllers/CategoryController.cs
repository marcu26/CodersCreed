using Core.Dtos.Category;
using Core.Dtos.Tasks;
using Core.Services;
using Infrastructure.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : BaseController
    {
        public CategoryService _categoryService { get; set; }
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDto payload)
        {
            await _categoryService.CreateCategoryAsync(payload);
            return Ok();
        }

        [HttpGet("get-one/{categoryId}")]
        public async Task<ActionResult> GetCategory([FromRoute] int categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);
            return Ok(category);
        }


        [HttpPut("edit/{categoryId}")]
        public async Task<ActionResult> EditCategory([FromRoute] int categoryId, [FromBody] EditCategoryDto payload)
        {
            await _categoryService.EditCategory(categoryId, payload);
            return Ok();
        }


        [HttpPost("get-pagina")]
        public async Task<ActionResult> GetCategoriesPaginaAsync([FromBody] PageablePostModelCategory payload)
        {
            try
            {
                var dto = await _categoryService.GetCategoriesDtoPaginaAsync(payload);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-category-to-user")]

        public async Task<ActionResult> AddCategoryToUser(int categoryId, int userId)
        {   
            await _categoryService.AddCategoryToUser(categoryId, userId);
            return Ok();
           
        }
    }
}
