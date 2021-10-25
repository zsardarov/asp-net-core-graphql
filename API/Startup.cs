using System;
using API.GraphQL;
using API.GraphQL.Commands;
using API.GraphQL.Platforms;
using Data;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<DataContext>(options =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                
                string connStr;
                
                if (env == "Development")
                {
                    connStr = _config.GetConnectionString("DefaultConnection");
                }
                else
                {
                    var db = Environment.GetEnvironmentVariable("DATABASE_NAME");
                    var user = Environment.GetEnvironmentVariable("DATABASE_USER");
                    var pass = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
                    var host = Environment.GetEnvironmentVariable("DATABASE_HOST");
                    var port = Environment.GetEnvironmentVariable("DATABASE_PORT");

                    connStr = $"Server={host},{port};User={user};Password={pass};Database={db};";
                }
                
                options.UseSqlServer(connStr);
            });

            services.AddGraphQLServer()
                .AddErrorFilter<ErrorFilter>()
                .AddSubscriptionType<Subscription>()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddType<PlatformType>()
                .AddType<CommandType>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new VoyagerOptions {GraphQLEndPoint = "/graphql"}, "/graphql-voyager");
        }
    }
}