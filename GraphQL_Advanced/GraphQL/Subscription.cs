using GraphQL_Advanced.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQL_Advanced.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public Platform OnPlatformAdded([EventMessage] Platform platform)
        {
            return platform;
        }
    }
}