using HotChocolate.AspNetCore.Authorization;

namespace GraphQL_UserBoarding.Resolvers
{
    public class QueryResolver
    {
        [Authorize]
        //[Authorize(Roles = new[] { "admin", "super-admin" })]
        //[Authorize(Policy = "roles-policy")]
        //[Authorize(Policy = "claim-policy-1")]
        [Authorize(Policy = "claim-policy-2")]
        public string Welcome()
        {
            return "Welcome To Custom Authentication Servies In GraphQL In Pure Code First";
        }
    }
}