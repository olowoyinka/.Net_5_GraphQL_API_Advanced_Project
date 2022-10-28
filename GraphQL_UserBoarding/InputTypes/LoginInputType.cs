using Graph.ArgumentValidator;
using System.ComponentModel.DataAnnotations;

namespace GraphQL_UserBoarding.InputTypes
{
    [Validatable]
    public class LoginInputType
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}