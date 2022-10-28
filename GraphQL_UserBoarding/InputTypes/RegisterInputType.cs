using Graph.ArgumentValidator;
using System.ComponentModel.DataAnnotations;

namespace GraphQL_UserBoarding.InputTypes
{
    [Validatable]
    public class RegisterInputType
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string ConfirmPassword { get; set; }
    }
}