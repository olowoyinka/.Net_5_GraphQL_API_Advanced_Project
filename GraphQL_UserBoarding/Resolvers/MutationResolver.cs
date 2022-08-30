using GraphQL_UserBoarding.InputTypes;
using GraphQL_UserBoarding.Logics;
using GraphQL_UserBoarding.ViewModels;
using HotChocolate;

namespace GraphQL_UserBoarding.Resolvers
{
    public class MutationResolver
    {
        public string Register([Service] IAuthLogic authLogic, RegisterInputType registerInput)
        {
            return authLogic.Register(registerInput);
        }

        public TokenResponseModel Login([Service] IAuthLogic authLogic, LoginInputType loginInput)
        {
            return authLogic.Login(loginInput);
        }

        public TokenResponseModel RenewAccessToken([Service] IAuthLogic authLogic, RenewTokenInputType renewToken)
        {
            return authLogic.RenewAccessToken(renewToken);
        }
    }
}