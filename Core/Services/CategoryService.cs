using Core.Database.Entities;
using Core.Dtos.Category;
using Core.Dtos.Tasks;
using Core.UnitOfWork;
using Core.Utils.Pageable;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CategoryService
    {
        public EfUnitOfWork _unitOfWork { get; set; }
        public CategoryService(EfUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto payload)
        {

            _unitOfWork._categoryRepository.Add(new Category()
            {
                Name = payload.Name,
                Experience = payload.Experience
            });

            await _unitOfWork.SaveAsync();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            var category = await _unitOfWork._categoryRepository.GetCategoryById(categoryId);
            if (category == null)
            {
                throw new WrongInputException($"Category with id {categoryId} does not exist");
            }

            return category;
        }

        public async Task EditCategory(int categoryId, EditCategoryDto payload)
        {
            var category = await _unitOfWork._categoryRepository.GetCategoryById(categoryId);
            if (category == null)
            {
                throw new WrongInputException($"Category with id {categoryId} does not exist");
            }

            category.Name = payload.Name;
            category.Experience = payload.Experience;

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCategory(int categoryId)
        {
            var category = await _unitOfWork._categoryRepository.GetCategoryById(categoryId);

            if (category == null)
            {
                throw new WrongInputException($"Category with id {categoryId} does not exist");
            }

            category.IsDeleted = true;

            await _unitOfWork.SaveAsync();
        }

        public async Task AddCategoryToUser(int categoryId, int userId)
        {
            var category = await _unitOfWork._categoryRepository.GetCategoryById(categoryId);

            if (category == null)
            {
                throw new WrongInputException($"Category with id {categoryId} does not exist");
            }

            var userExists = await _unitOfWork._usersRepository.GetUserByIdAsync(userId);
            if (userExists == null)
            {
                throw new WrongInputException($"User with id {userId} does not exist");
            }

            var alreadyExists = await _unitOfWork._userCategoryRepository.CheckIfUserAlreadyHasCategory(userId, categoryId);
            if(alreadyExists != null)
            {
                throw new WrongInputException($"User with id {userId} already has category with id {categoryId} assigned");
            }

            var userCategory = new UserCategory
            {
                UserId = userId,
                CategoryId = categoryId,
                Experience = 0
            };

            _unitOfWork._userCategoryRepository.Add(userCategory);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PageableResponse> GetCategoriesDtoPaginaAsync(PageablePostModelCategory request)
        {
            var dto = await _unitOfWork._categoryRepository.GetCategoriesPageByFiltersAsync(request);

            return new PageableResponse()
            {
                draw = request.draw,
                recordTotal = dto.NumarTotalRanduri,
                recordsFiltered = dto.NumarRanduriFiltrate,
                data = dto.Pagina.Select(t => new CategoryDto()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Experience = t.Experience
                }
                )
            };
        }
    }
}
