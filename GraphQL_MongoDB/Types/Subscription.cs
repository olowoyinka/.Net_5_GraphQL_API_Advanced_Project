using GraphQL_MongoDB.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQL_MongoDB.Types
{
    public class Subscription
    {
        [Topic]
        [Subscribe]
        public User SubscribeUser([EventMessage] User user)
        {
            return user;
        }
    }
}