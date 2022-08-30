using GraphQL_MongoDB.Models;
using HotChocolate;
using HotChocolate.Types.Relay;
using System;
using static GraphQL_MongoDB.ViewModels.UserInput;
using static GraphQL_MongoDB.ViewModels.UserPayload;

namespace GraphQL_MongoDB.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IExecutable<User> GetUser();

        IExecutable<User> GetUserById([ID] Guid id);
        
        User CreateUser(CreateUserInput createUserInput);
        
        bool DeleteUser(Guid id);
        
        bool UpdateUser(Guid id, UpdateUserInput updateUserInput);
        
        UserTokenPayload Login(LoginInput loginInput);
        
        UserTokenPayload RenewAccessToken(RenewTokenInput renewTokenInput);
    }
}