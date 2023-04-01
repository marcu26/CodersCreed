﻿using Core.Database.Entities;
using Core.Dtos.Courses;
using Core.Dtos.Roles;
using Core.Dtos.Users;
using Core.UnitOfWork;
using Core.Utils.Pageable;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CourseService
    {
        public EfUnitOfWork _efUnitOfWork { get; set; }

        public CourseService(EfUnitOfWork efUnitOfWork)
        {
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task CreateCourseAsync(CreateCourseDto payload) 
        {
            var exist =await _efUnitOfWork._coursesRepository.DoesNameExist(payload.Name);

            if (exist)
                throw new WrongInputException($"Course with name {payload.Name} allready exists");

            var course = new Course { Name = payload.Name, Description = payload.Description };

            _efUnitOfWork._coursesRepository.Add(course);

            await _efUnitOfWork.SaveAsync();
        }

        public async Task<GetCouseDto> GetCourseByIdAsync(int courseId) 
        {
            var course = await _efUnitOfWork._coursesRepository.GetByIdAsync(courseId);

            if (course == null)
                throw new WrongInputException($"Course with id {courseId} does not exist.");

            if (course.IsDeleted)
                throw new WrongInputException($"Course with id {courseId} does not exist.");

            return new GetCouseDto { Description=course.Description, Name =course.Name, Id=course.Id };
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            var course = await _efUnitOfWork._coursesRepository.GetByIdAsync(courseId);

            if (course == null)
                throw new WrongInputException($"Course with id {courseId} does not exist.");

            if (course.IsDeleted)
                throw new WrongInputException($"Course with id {courseId} does not exist.");

            course.IsDeleted = true;

            await _efUnitOfWork.SaveAsync();
        }

        public async Task UpdateCourseAsync(UpdateCourseDto payload)
        {
            var course = await _efUnitOfWork._coursesRepository.GetByIdAsync(payload.Id);

            if (course == null)
                throw new WrongInputException($"Course with id {payload.Id} does not exist.");

            if (course.IsDeleted)
                throw new WrongInputException($"Course with id {payload.Id} does not exist.");

            if (payload.Name != null)
            {
                var alreadyExists = await _efUnitOfWork._coursesRepository.DoesNameExist(payload.Name);

                if (alreadyExists)
                    throw new WrongInputException($"Course with name {payload.Name} already exists.");
            }

            course.Name = String.IsNullOrEmpty(payload.Name) ? course.Name : payload.Name;
            course.Description = String.IsNullOrEmpty(payload.Description) ? course.Description : payload.Description;

            await _efUnitOfWork.SaveAsync();
        }

        public async Task<PageableResponse> GetPaginaAsync(PageablePostModel request)
        {
            var dto = await _efUnitOfWork
                    ._coursesRepository
                    .GetPaginaAsync(request.start, request.length);

            return new PageableResponse()
            {
                draw = request.draw,
                recordTotal = dto.NumarTotalRanduri,
                recordsFiltered = dto.NumarRanduriFiltrate,
                data = dto.Pagina.Select(e => new GetCouseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description
                })
            };
        }
    }
}
