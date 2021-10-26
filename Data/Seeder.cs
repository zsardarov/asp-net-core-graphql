using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;

namespace Data
{
    public class Seeder
    {
        public static async Task Seed(DataContext context)
        {
            if (!context.Commands.Any() && !context.Platforms.Any())
            {
                var rnd = new Random();
                var platforms = new List<Platform>
                {
                    new Platform
                    {
                        Name = "Windows", LicenseKey = rnd.Next(1000000, 999999999).ToString()
                    },
                    new Platform
                    {
                        Name = "Linux", LicenseKey = rnd.Next(1000000, 999999999).ToString()
                    },
                    new Platform
                    {
                        Name = "OS X", LicenseKey = rnd.Next(1000000, 999999999).ToString()
                    }
                };

                var commands = new List<Command>
                {
                    new Command()
                    {
                        Platform = platforms.ElementAt(0), HowTo = "List directory", CommandLine = "dir"
                    },
                    new Command()
                    {
                        Platform = platforms.ElementAt(2), HowTo = "List directory", CommandLine = "ls"
                    },
                    new Command()
                    {
                        Platform = platforms.ElementAt(2), HowTo = "Create directory", CommandLine = "mkdir"
                    },
                };
                
                await context.Platforms.AddRangeAsync(platforms);

                await context.Commands.AddRangeAsync(commands);

                await context.SaveChangesAsync();
            }
        }
    }
}