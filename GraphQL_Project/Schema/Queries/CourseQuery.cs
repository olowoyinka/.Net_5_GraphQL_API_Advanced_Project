using GraphQL_Project.DTOs;
using GraphQL_Project.Schema.Filters;
using GraphQL_Project.Schema.Sorters;
using GraphQL_Project.Services;
using GraphQL_Project.Services.Courses;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL_Project.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class CourseQuery
    {
        private readonly CoursesRepository _coursesRepository;

        public CourseQuery(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<IEnumerable<CourseType>> GetCourses()
        {
            IEnumerable<CourseDTO> courseDTOs = await _coursesRepository.GetAll();

            return courseDTOs.Select(s => new CourseType
            {
                Id = s.Id,
                Name = s.Name,
                Subject = s.Subject,
                InstructorId = s.InstructorId,
                CreatorId = s.CreatorId
            });
        }

        [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 2)]
        public async Task<IEnumerable<CourseType>> GetOffsetCourses()
        {
            IEnumerable<CourseDTO> courseDTOs = await _coursesRepository.GetAll();

            return courseDTOs.Select(s => new CourseType
            {
                Id = s.Id,
                Name = s.Name,
                Subject = s.Subject,
                InstructorId = s.InstructorId,
                CreatorId = s.CreatorId
            });
        }

        [UseDbContext(typeof(SchoolDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 2)]
        [UseProjection]
        [UseFiltering(typeof(CourseFilterType))]
        [UseSorting(typeof(CourseSortType))]
        public IQueryable<CourseType> GetPaginatedCourses([ScopedService] SchoolDbContext context)
        {
            return context.Courses.Select(s => new CourseType
            {
                Id = s.Id,
                Name = s.Name,
                Subject = s.Subject,
                InstructorId = s.InstructorId,
                CreatorId = s.CreatorId
            });
        }

        public async Task<CourseType> GetCourseByIdAsync(Guid id)
        {
            CourseDTO courseDTOs = await _coursesRepository.GetById(id);

            return new CourseType()
            {
                Id = courseDTOs.Id,
                Name = courseDTOs.Name,
                Subject = courseDTOs.Subject,
                InstructorId = courseDTOs.InstructorId,
                CreatorId = courseDTOs.CreatorId
            };
        }
    }
}