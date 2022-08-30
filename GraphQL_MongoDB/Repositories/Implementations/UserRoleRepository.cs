using GraphQL_MongoDB.Models;
using GraphQL_MongoDB.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace GraphQL_MongoDB.Repositories.Implementations
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IMongoCollection<UserRole> _userRole;

        public UserRoleRepository(IMongoClient client)
        {
            var database = client.GetDatabase("GraphQL");
            var collection = database.GetCollection<UserRole>(nameof(UserRole));
            _userRole = collection;
        }

        public IList<UserRole> GetRoleById(Guid id)
        {
            return _userRole.Find(_ => _.UserId == id).ToList();
        }
    }
}