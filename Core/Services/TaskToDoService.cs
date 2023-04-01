using Core.Database.Entities;
using Core.Dtos.Tasks;
using Core.Handlers;
using Core.UnitOfWork;
using Core.Utils.Pageable;
using Infrastructure.Exceptions;
using ISoft.Travel.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TaskToDoService
    {
        public EfUnitOfWork _unitOfWork { get; set; }

        public TaskToDoService(EfUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateTaskToDoAsync(CreateTaskToDoDto payload)
        {
            if (payload.UserId != null)
            {
                var userExists = await _unitOfWork._usersRepository.GetUserByIdAsync(payload.UserId.Value);
                if (userExists == null)
                {
                    throw new WrongInputException($"User with id {payload.UserId} does not exist");
                }
            }

            var projectExists = await _unitOfWork._projectRepository.GetProjectById(payload.ProjectId);
            if (projectExists == null)
            {
                throw new WrongInputException($"Project with id {payload.ProjectId} does not exist");
            }

            if(payload.Points < 0)
            {
                throw new WrongInputException("Points should be a positive value");
            }

            if (payload.Experience < 0)
            {
                throw new WrongInputException("Experience should be a positive value");
            }

            if (payload.Deadline < DateTime.Now)
            {
                throw new WrongInputException("Deadline should be in the future. Please try adding another one");
            }

            _unitOfWork._taskToDoRepository.Add(new Database.Entities.TaskToDo()
            {
               Name = payload.Name,
               Description = payload.Description,
               Deadline = payload.Deadline,
               Points = payload.Points,
               Experience = payload.Experience,
               IsDone = false,
               UserId = payload.UserId,
               ProjectId = payload.ProjectId
            });

            await _unitOfWork.SaveAsync();
        }

        public async Task<TaskToDo> GetTaskById(int taskId)
        {
            var task = await _unitOfWork._taskToDoRepository.GetTaskById(taskId);
            if (task == null)
            {
                throw new WrongInputException($"Task with id {taskId} does not exist");
            }

            return task;
        }

        public async Task AssignTaskToUser(int taskId, int userId)
        {
            var task = await _unitOfWork._taskToDoRepository.GetTaskById(taskId);
            if(task == null)
            {
                throw new WrongInputException($"Task with id {taskId} does not exist");
            }

            var userExists = await _unitOfWork._usersRepository.GetUserByIdAsync(userId);
            if (userExists == null)
            {
                throw new WrongInputException($"User with id {userId} does not exist");
            }

            task.UserId = userId;

            await _unitOfWork.SaveAsync();
        }

        public async Task EditTask(int taskId,EditTaskToDoDto payload)
        {
            if (payload.Points < 0)
            {
                throw new WrongInputException("Points should be a positive value");
            }

            if (payload.Experience < 0)
            {
                throw new WrongInputException("Experience should be a positive value");
            }

            if (payload.Deadline < DateTime.Now)
            {
                throw new WrongInputException("Deadline should be in the future. Please try adding another one");
            }

            var task = await _unitOfWork._taskToDoRepository.GetTaskById(taskId);
            if (task == null)
            {
                throw new WrongInputException($"Task with id {taskId} does not exist");
            }

            task.Name = payload.Name;
            task.Description = payload.Description;
            task.Deadline = payload.Deadline;
            task.Points = payload.Points;
            task.Experience = payload.Experience;

            await _unitOfWork.SaveAsync();
        }

        public async Task MarkTask(int taskId)
        {
            var task = await _unitOfWork._taskToDoRepository.GetTaskById(taskId);
            if (task == null)
            {
                throw new WrongInputException($"Task with id {taskId} does not exist");
            }

            task.IsDone = true;

            await _unitOfWork._usersRepository.AddPointsAsync(task.Points, task.UserId.Value);
            await _unitOfWork.SaveAsync();
        }

        public async Task UnmarkTask(int taskId)
        {
            var task = await _unitOfWork._taskToDoRepository.GetTaskById(taskId);
            if (task == null)
            {
                throw new WrongInputException($"Task with id {taskId} does not exist");
            }

            task.IsDone = false;
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteTask(int taskId)
        {
            var task = await _unitOfWork._taskToDoRepository.GetTaskById(taskId);

            if (task == null)
            {
                throw new WrongInputException($"Task with id {taskId} does not exist");
            }

            if (task != null)
            {
                task.IsDeleted = true;
            }
            else
            {
                throw new WrongInputException($"Task with id {taskId} does not exist");
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<PageableResponse> GetTasksToDoDtoPaginaAsync(PageablePostModelTaskToDo request)
        {
            var dto = await _unitOfWork._taskToDoRepository.GetTasksToDoPageByFiltersAsync(request);

            return new PageableResponse()
            {
                draw = request.draw,
                recordTotal = dto.NumarTotalRanduri,
                recordsFiltered = dto.NumarRanduriFiltrate,
                data = dto.Pagina.Select(t => new TaskToDo()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    Deadline = t.Deadline,
                    Points = t.Points,
                    Experience = t.Experience,
                    IsDone = t.IsDone,
                    UserId = t.UserId!=null ? t.UserId : -1,
                    ProjectId = t.ProjectId
                }
                )
            };
        }
    }
}
