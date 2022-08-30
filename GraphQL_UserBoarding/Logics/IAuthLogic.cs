using GraphQL_UserBoarding.InputTypes;
using GraphQL_UserBoarding.ViewModels;

namespace GraphQL_UserBoarding.Logics
{
    public interface IAuthLogic
    {
        string Register(RegisterInputType registerInput);

        TokenResponseModel Login(LoginInputType loginInput);

        TokenResponseModel RenewAccessToken(RenewTokenInputType renewToken);
    }
}