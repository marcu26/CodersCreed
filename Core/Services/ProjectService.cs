using Core.Database.Entities;
using Core.Dtos.Project;
using Core.Dtos.ProjectUser;
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
    public class ProjectService
    {
        public EfUnitOfWork _unitOfWork { get; set; }

        public ProjectService(EfUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task CreateProjectAsync(CreateProjectDto payload, int managerId)
        {
            var proj = await _unitOfWork._projectRepository.GetProjectByName(payload.Name);

            if (proj != null)
            {
                throw new WrongInputException($"Project with name {payload.Name} already exists.");
            }

            _unitOfWork._projectRepository.Add(new Database.Entities.Project()
            {
                Name = payload.Name
            });

            await _unitOfWork.SaveAsync();

            var freshProject = await _unitOfWork._projectRepository.GetProjectByName(payload.Name);

            var projectUser = new ProjectUser()
            {
                ProjectId = freshProject.Id,
                UserId = managerId,
                isManager = true
            };

            _unitOfWork._projectUserRepository.Add(projectUser);

            await _unitOfWork.SaveAsync();
        }

        public async Task<GetProjectDto> GetProjectById(int projectId)
        {
            var project = await _unitOfWork._projectRepository.GetProjectById(projectId);
            if (project == null)
            {
                throw new WrongInputException($"Project with id {projectId} does not exist");
            }

            var projDto = new GetProjectDto()
            {
                Id = projectId,
                Name = project.Name,
                ManagerName = project.ProjectUsers.Where(pu => pu.isManager).Select(pu => pu.User.Username).FirstOrDefault()
            };

            return projDto;
        }

        public async Task EditProject(int projectId, EditProjectDto payload)
        {
            var proj = await _unitOfWork._projectRepository.GetProjectById(projectId);

            if (proj == null)
            {
                throw new WrongInputException($"Project with id {projectId} does not exist.");
            }

            proj.Name = payload.Name;

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProject(int projectId)
        {
            var proj = await _unitOfWork._projectRepository.GetProjectById(projectId);

            if (proj == null)
            {
                throw new WrongInputException($"Project with id {projectId} does not exist.");
            }

            proj.IsDeleted = true;

            await _unitOfWork.SaveAsync();
        }

        public async Task<PageableResponse> GetProjectsPaginaAsync(PageablePostModelProject request)
        {
            var dto = await _unitOfWork._projectRepository.GetProjectsPageByFiltersAsync(request);

            return new PageableResponse()
            {
                draw = request.draw,
                recordTotal = dto.NumarTotalRanduri,
                recordsFiltered = dto.NumarRanduriFiltrate,
                data = dto.Pagina.Select(p => new GetProjectDto
                {
                   Id = p.Id,
                   Name = p.Name,
                   ManagerName = p.ProjectUsers.Where(pu=>pu.isManager).Select(pu => pu.User.Username).FirstOrDefault()
                }
                )
            };
        }
    }
}
