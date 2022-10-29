using Graph.ArgumentValidator;
using System.ComponentModel.DataAnnotations;

namespace GraphQL_Project.Schema.Mutations
{
    [Validatable]
    public class InstructorInputType
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        public double Salary { get; set; }
    }
}