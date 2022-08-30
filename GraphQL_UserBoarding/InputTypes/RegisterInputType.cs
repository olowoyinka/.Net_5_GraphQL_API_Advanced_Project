using System.ComponentModel.DataAnnotations;

namespace GraphQL_UserBoarding.InputTypes
{
    public class RegisterInputType
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string EmailAddress { get; set; }
        
        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }
    }
}