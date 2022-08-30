using GraphQL_MongoDB.Models;
using HotChocolate;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace GraphQL_MongoDB.Types
{
    public class UserNodeResolver
    {
        public Task<User> ResolveAsync([Service] IMongoCollection<User> collection, Guid id)
        {
            return collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}