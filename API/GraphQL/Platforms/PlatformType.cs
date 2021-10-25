using System.Linq;
using Data;
using Data.Models;
using HotChocolate;
using HotChocolate.Types;

namespace API.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Represents software or service");

            descriptor.Field(d => d.Commands)
                .ResolveWith<Resolver>(p => p.GetCommands(default!, default))
                .UseDbContext<DataContext>();
        }

        private class Resolver
        {
            public IQueryable<Command> GetCommands(Platform platform, [ScopedService] DataContext context)
            {
                return context.Commands.Where(c => c.PlatformId == platform.Id);
            }
        }
    }
}