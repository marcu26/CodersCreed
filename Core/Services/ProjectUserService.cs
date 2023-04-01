using Core.Database.Entities;
using Core.Dtos.ProjectUser;
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
    public class ProjectUserService
    {
        public EfUnitOfWork _unitOfWork { get; set; }

        public ProjectUserService(EfUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AssignUserToProject(int projectId, int userId)
        {
            var project = await _unitOfWork._projectRepository.GetProjectById(projectId);
            if (project == null)
            {
                throw new WrongInputException($"Project with id {projectId} does not exist.");
            }

            var user = await _unitOfWork._usersRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new WrongInputException($"User with id {userId} does not exist.");
            }

            var projectUser = new ProjectUser
            {
                ProjectId = projectId,
                UserId = userId,
                isManager = false
            };

            await _unitOfWork._projectUserRepository.AddAsync(projectUser);
            await _unitOfWork.SaveAsync();
        }

        public async Task AssignManagerToProject(int projectId, int userId)
        {
            var project = await _unitOfWork._projectRepository.GetProjectById(projectId);
            if (project == null)
            {
                throw new WrongInputException($"Project with id {projectId} does not exist.");
            }

            var user = await _unitOfWork._usersRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new WrongInputException($"User with id {userId} does not exist.");
            }

            var manager = await _unitOfWork._projectUserRepository.GetProjectManagerAsync(projectId,userId);

            if (manager != null)
            {
                manager.isManager = false;
            }

            var projManager = await _unitOfWork._projectUserRepository.GetProjectUserEntryAsync(projectId,userId);
            if (projManager != null)
            {
                projManager.isManager = true;
            }
            else
            {
                var projectUser = new ProjectUser
                {
                    ProjectId = projectId,
                    UserId = userId,
                    isManager = true
                };

                _unitOfWork._projectUserRepository.Add(projectUser);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<PageableResponse> GetProjectUsersDtoPaginaAsync(PageablePostModelProjectUsers request)
        {
            var dto = await _unitOfWork._projectUserRepository.GetProjectsOfUserPageByFiltersAsync(request);

            return new PageableResponse()
            {
                draw = request.draw,
                recordTotal = dto.NumarTotalRanduri,
                recordsFiltered = dto.NumarRanduriFiltrate,
                data = dto.Pagina.Select(pu => new ProjectUserDto
                {
                    ProjectId = pu.ProjectId,
                    UserId = pu.UserId,
                    ProjectName = pu.Project.Name,
                    Username = pu.User.Username,
                    Email = pu.User.Email,
                    Points = pu.User.Points,
                    Experience = pu.User.Experience
                }
                )
            };
        }
    }
}
