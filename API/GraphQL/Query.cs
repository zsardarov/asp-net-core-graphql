using System.Linq;
using Data;
using Data.Models;
using HotChocolate;
using HotChocolate.Data;

namespace API.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(DataContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Platform> GetPlatform([ScopedService] DataContext context)
        {
            return context.Platforms;
        }

        [UseDbContext(typeof(DataContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Command> GetCommand([ScopedService] DataContext context)
        {
            return context.Commands;
        }
    }
}