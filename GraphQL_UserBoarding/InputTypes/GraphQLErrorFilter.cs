using HotChocolate;

namespace GraphQL_UserBoarding.InputTypes
{
    public class GraphQLErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            //var gg = ErrorBuilder.New()
            //               .SetMessage("This is the message")
            //               .SetCode("YOURCODE00000123")
            //               .SetException(ex)
            //               .AddLocation(context.Selection.SyntaxNode)
            //               .SetPath(context.Path)
            //               .Build();

            var gg = ErrorBuilder.New()
                           .SetMessage(error.Message)
                           .RemovePath()
                           .ClearLocations()
                           .RemoveExtension("")
                           .RemoveCode()
                           .Build();

            //return error.WithMessage(error.Exception.Message);
            return gg;
        }
    }
}