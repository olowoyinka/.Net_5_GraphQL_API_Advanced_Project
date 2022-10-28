using GraphQL_Project.Services;
using GraphQL_Project.Services.Courses;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL_Project.Schema.Queries
{
    public class Query
    {
        //private readonly Faker<InstructorType> _instructorFaker;
        //private readonly Faker<StudentType> _studentFaker;
        //private readonly Faker<CourseType> _courseFaker;
        private readonly CoursesRepository _coursesRepository;

        public Query(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
            //_instructorFaker = new Faker<InstructorType>()
            //    .RuleFor(s => s.Id, f => Guid.NewGuid())
            //    .RuleFor(c => c.FirstName, f => f.Name.LastName())
            //    .RuleFor(c => c.LastName, f => f.Name.FirstName())
            //    .RuleFor(c => c.Salary, f => f.Random.Double(0, 1000));

            //_studentFaker = new Faker<StudentType>()
            //    .RuleFor(s => s.Id, f => Guid.NewGuid())
            //    .RuleFor(c => c.FirstName, f => f.Name.LastName())
            //    .RuleFor(c => c.LastName, f => f.Name.FirstName())
            //    .RuleFor(c => c.GPA, f => f.Random.Double(1, 4));

            //_courseFaker = new Faker<CourseType>()
            //    .RuleFor(s => s.Id, f => Guid.NewGuid())
            //    .RuleFor(c => c.Name, f => f.Name.JobTitle())
            //    .RuleFor(c => c.Subject, f => f.PickRandom<Subject>())
            //    .RuleFor(c => c.Instructor, f => _instructorFaker.Generate())
            //    .RuleFor(c => c.Students, f => _studentFaker.Generate(3));
        }



        [UseDbContext(typeof(SchoolDbContext))]
        public async Task<IEnumerable<ISearchResultType>> Search(string term, [ScopedService] SchoolDbContext context)
        {
            IEnumerable<CourseType> courses = await context.Courses
                                                            .Where(c => c.Name.Contains(term))
                                                            .Select(c => new CourseType()
                                                            {
                                                                Id = c.Id,
                                                                Name = c.Name,
                                                                Subject = c.Subject,
                                                                InstructorId = c.InstructorId,
                                                                CreatorId = c.CreatorId
                                                            })
                                                            .ToListAsync();

            IEnumerable<InstructorType> instructors = await context.Instructors
                                                                    .Where(i => i.FirstName.Contains(term) || i.LastName.Contains(term))
                                                                    .Select(i => new InstructorType()
                                                                    {
                                                                        Id = i.Id,
                                                                        FirstName = i.FirstName,
                                                                        LastName = i.LastName,
                                                                        Salary = i.Salary,
                                                                    })
                                                                    .ToListAsync();

            return new List<ISearchResultType>()
                .Concat(courses)
                .Concat(instructors);
        }
    }
}