using HotChocolate;

namespace GraphQL_Project.Middlewares.UseUser
{
    public class UserAttribute : GlobalStateAttribute
    {
        public UserAttribute() : base(UserMiddleware.USER_CONTEXT_DATA_KEY)
        {
        }
    }
}