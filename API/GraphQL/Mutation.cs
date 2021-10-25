using System;
using System.Threading.Tasks;
using API.GraphQL.Commands;
using API.GraphQL.Platforms;
using Data;
using Data.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace API.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(DataContext))]
        public async Task<Platform> AddPlatform(
            AddPlatformInput input,
            [ScopedService] DataContext context,
            [Service] ITopicEventSender eventSender
        )
        {
            var platform = new Platform {Name = input.Name};

            context.Platforms.Add(platform);
            await context.SaveChangesAsync();
            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform);

            return platform;
        }
        
        [UseDbContext(typeof(DataContext))]
        public async Task<Platform> UpdatePlatform(
            UpdatePlatformInput input,
            [ScopedService] DataContext context
        )
        {
            var platform = await context.Platforms.FirstOrDefaultAsync(p => p.Id == input.Id);

            if (platform == null)
            {
                throw new Exception("Platform not found");
            }

            platform.Name = input.Name;

            context.Platforms.Update(platform);
            await context.SaveChangesAsync();

            return platform;
        }

        [UseDbContext(typeof(DataContext))]
        public async Task<DeletePlatformPayload> DeletePlatform(DeletePlatformInput input, [ScopedService] DataContext context)
        {
            var platform = await context.Platforms.FirstOrDefaultAsync(p => p.Id == input.Id);

            if (platform == null)
            {
                throw new Exception("Platform not found");
            }

            context.Platforms.Remove(platform);

            await context.SaveChangesAsync();

            return new DeletePlatformPayload {Message = "Successfully deleted"};
        }
        
        
        [UseDbContext(typeof(DataContext))]
        public async Task<Command> AddCommand(AddCommandInput input, [ScopedService] DataContext context)
        {
            var command = new Command
            {
                HowTo = input.HowTo, 
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };
        
            context.Commands.Add(command);
            await context.SaveChangesAsync();

            return command;
        }
        
        [UseDbContext(typeof(DataContext))]
        public async Task<DeleteCommandPayload> DeleteCommand(DeleteCommandInput input, [ScopedService] DataContext context)
        {
            var command = await context.Commands.FirstOrDefaultAsync(c => c.Id == input.Id);

            if (command == null)
            {
                throw new Exception("Command not found");
            }
        
            context.Commands.Remove(command);
            await context.SaveChangesAsync();

            return new DeleteCommandPayload {Message = "Successfully Deleted"};
        }
        
        [UseDbContext(typeof(DataContext))]
        public async Task<Command> UpdateCommand(UpdateCommandInput input, [ScopedService] DataContext context)
        {
            var command = await context.Commands.FirstOrDefaultAsync(c => c.Id == input.Id);

            if (command == null)
            {
                throw new Exception("Command not found");
            }

            command.PlatformId = input.PlatformId;
            command.CommandLine = input.CommandLine;
            command.HowTo = input.HowTo;
        
            context.Commands.Update(command);
            await context.SaveChangesAsync();

            return command;
        }
    
    }
}