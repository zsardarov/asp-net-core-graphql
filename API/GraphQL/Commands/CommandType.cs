using System.Linq;
using Data;
using Data.Models;
using HotChocolate;
using HotChocolate.Types;

namespace API.GraphQL.Commands
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("Represents the command");
            
            descriptor.Field(c => c.Platform)
                .ResolveWith<Resolver>(r => r.GetPlatform(default!, default))
                .UseDbContext<DataContext>();
        }
        
        private class Resolver
        {
            public Platform GetPlatform(Command command, [ScopedService] DataContext context)
            {
                return context.Platforms.FirstOrDefault(p => p.Id == command.PlatformId);
            }
        }
    }

}