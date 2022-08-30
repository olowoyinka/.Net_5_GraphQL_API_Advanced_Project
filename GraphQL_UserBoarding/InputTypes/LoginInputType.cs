using System.ComponentModel.DataAnnotations;

namespace GraphQL_UserBoarding.InputTypes
{
    public class LoginInputType
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}