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
            kdkd();

            return authLogic.Login(loginInput);
        }

        public TokenResponseModel RenewAccessToken([Service] IAuthLogic authLogic, RenewTokenInputType renewToken)
        {
            return authLogic.RenewAccessToken(renewToken);
        }

        private void kdkd()
        {
            var gg = ErrorBuilder.New()
               .SetMessage("This is the jjj message")
               .SetCode("YOURCODE00000123")
               .Build();

            if ("ddd" != "ff")
            {
                throw new GraphQLException(gg);
            }
        }
    }
}