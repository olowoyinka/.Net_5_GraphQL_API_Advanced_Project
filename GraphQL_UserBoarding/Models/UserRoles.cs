using System.ComponentModel.DataAnnotations;

namespace GraphQL_UserBoarding.Models
{
    public class UserRoles
    {
        [Key]
        public int RoleId { get; set; }

        public int UserId { get; set; }
        
        public string Name { get; set; }
    }
}