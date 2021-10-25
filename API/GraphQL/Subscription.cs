using Data.Models;
using HotChocolate;
using HotChocolate.Types;

namespace API.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public Platform OnPlatformAdded([EventMessage] Platform platform) => platform;
    }
}