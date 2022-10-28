using GraphQL_Project.Models;
using GraphQL_Project.Schema.Queries;
using System;

namespace GraphQL_Project.Schema.Mutations
{
    public class CourseResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Subject Subject { get; set; }

        public Guid InstructorId { get; set; }
    }
}