using Core.Database.Entities;
using Core.Dtos.Courses;
using Core.Dtos.CourseSection;
using Core.UnitOfWork;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CourseSectionService
    {
        public EfUnitOfWork _efUnitOfWork { get; set; }

        public CourseSectionService(EfUnitOfWork efUnitOfWork)
        {
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task CreateCourseSectionAsync(CreateCourseSectionDto payload)
        {
            var exist = await _efUnitOfWork._courseSectionRepository.DoesNameExist(payload.Name);

            if (exist)
                throw new WrongInputException($"Course section with name {payload.Name} allready exists");

            var courseSection = new CourseSection { Name = payload.Name, Link = payload.Link, CourseId = payload.CourseId };

            _efUnitOfWork._courseSectionRepository.Add(courseSection);

            await _efUnitOfWork.SaveAsync();
        }

        public async Task<GetCourseSectionDto> GetCourseSectionByIdAsync(int courseId)
        {
            var courseSection = await _efUnitOfWork._courseSectionRepository.GetByIdAsync(courseId);

            if (courseSection == null)
                throw new WrongInputException($"Course section with id {courseId} does not exist.");

            if (courseSection.IsDeleted)
                throw new WrongInputException($"Course section with id {courseId} does not exist.");

            return new GetCourseSectionDto {  Name = courseSection.Name, CourseId = courseSection.CourseId , Link = courseSection.Link};
        }

        public async Task DeleteCourseSectionAsync(int courseId)
        {
            var course = await _efUnitOfWork._courseSectionRepository.GetByIdAsync(courseId);

            if (course == null)
                throw new WrongInputException($"Course section with id {courseId} does not exist.");

            if (course.IsDeleted)
                throw new WrongInputException($"Course section with id {courseId} does not exist.");

            course.IsDeleted = true;

            await _efUnitOfWork.SaveAsync();
        }

        public async Task UpdateCourseSectionAsync(UpdateCourseSection payload)
        {
            var course = await _efUnitOfWork._courseSectionRepository.GetByIdAsync(payload.Id);

            if (course == null)
                throw new WrongInputException($"Course section with id {payload.Id} does not exist.");

            if (course.IsDeleted)
                throw new WrongInputException($"Course section with id {payload.Id} does not exist.");

            if (payload.Name != null)
            {
                var alreadyExists = await _efUnitOfWork._courseSectionRepository.DoesNameExist(payload.Name);

                if (alreadyExists)
                    throw new WrongInputException($"Course section with name {payload.Name} already exists.");
            }

            course.Name = String.IsNullOrEmpty(payload.Name) ? course.Name : payload.Name;
            course.Link = String.IsNullOrEmpty(payload.Link) ? course.Link : payload.Link;
            course.CourseId = payload.CourseId == null ? course.CourseId : payload.CourseId.Value;

            await _efUnitOfWork.SaveAsync();
        }
    }
}
