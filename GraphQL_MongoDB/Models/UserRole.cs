using System;

namespace GraphQL_MongoDB.Models
{
    public class UserRole
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        
        public string Name { get; set; }
    }
}