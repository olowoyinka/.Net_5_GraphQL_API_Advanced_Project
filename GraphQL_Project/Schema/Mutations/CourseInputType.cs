using Graph.ArgumentValidator;
using GraphQL_Project.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace GraphQL_Project.Schema.Mutations
{
    [Validatable]
    public class CourseInputType
    {
        [Required]
        public string Name { get; set; }

        public Subject Subject { get; set; }

        [Required]
        public Guid InstructorId { get; set; }
    }
}