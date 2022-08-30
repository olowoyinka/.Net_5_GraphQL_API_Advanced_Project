using GraphQL_MongoDB.Models;
using System;
using System.Collections.Generic;

namespace GraphQL_MongoDB.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        IList<UserRole> GetRoleById(Guid id);
    }
}