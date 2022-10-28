using GraphQL_Project.Schema.Queries;
using GraphQL_Project.Schema.Mutations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQL_Project.Schema.Subscriptions;
using GraphQL_Project.Services;
using Microsoft.EntityFrameworkCore;
using GraphQL_Project.Services.Courses;
using GraphQL_Project.Services.Instructors;
using GraphQL_Project.DataLoaders;
using System;
using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using FirebaseAdmin;
using FirebaseAdminAuthentication.DependencyInjection.Models;
using Graph.ArgumentValidator;

namespace GraphQL_Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphQLServer()
                    .AddArgumentValidator()
                    .AddQueryType<Query>()
                    .AddTypeExtension<CourseQuery>()
                    .AddType<CourseType>()
                    .AddType<InstructorType>()
                    .AddMutationType<Mutation>()
                    .AddSubscriptionType<Subscription>()
                    .AddFiltering()
                    .AddSorting()
                    .AddProjections()
                    .AddAuthorization();

            services.AddSingleton(FirebaseApp.Create());

            services.AddFirebaseAuthentication();

            services.AddAuthorization(
                o => o.AddPolicy("IsAdmin",
                    p => p.RequireClaim(FirebaseUserClaimType.EMAIL, "olowo@gmail.com")));

            services.AddInMemorySubscriptions();

            string connectionString = Configuration.GetConnectionString("default");

            services.AddPooledDbContextFactory<SchoolDbContext>(option => option.UseSqlServer(connectionString).LogTo(Console.WriteLine));

            services.AddScoped<CoursesRepository>();
            services.AddScoped<InstructorsRepository>();
            services.AddScoped<InstructorDataLoader>();
            services.AddScoped<UserDataLoader>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}