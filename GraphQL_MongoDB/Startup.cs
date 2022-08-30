using GraphQL_MongoDB.Repositories.Implementations;
using GraphQL_MongoDB.Repositories.Interfaces;
using GraphQL_MongoDB.Types;
using GraphQL_MongoDB.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

namespace GraphQL_MongoDB
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
            // configure strongly typed settings object
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient
                                                                (Configuration.GetConnectionString("MongoDb")));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserRoleRepository, UserRoleRepository>();

            services.AddGraphQLServer()
                    .AddAuthorization()
                    .AddInMemorySubscriptions()
                    .AddQueryType<Query>()
                    .AddMutationType<Mutation>()
                    //.AddSubscriptionType<Subscription>()
                    .AddGlobalObjectIdentification()
                    .AddMongoDbFiltering()
                    .AddMongoDbSorting()
                    .AddMongoDbProjections()
                    .AddMongoDbPagingProviders();

            // CORS
            services.AddCors(option =>
            {
                option.AddPolicy("allowedOrigin", builder => builder.AllowAnyOrigin()
                                                        .AllowAnyMethod()
                                                        .AllowAnyHeader());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            var tokenSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidIssuer = tokenSettings.Issuer,
                                ValidateIssuer = true,
                                ValidAudience = tokenSettings.Audience,
                                ValidateAudience = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret)),
                                ValidateIssuerSigningKey = true,
                            };
                        });

            services.AddAuthorization(options =>
                {
                    options.AddPolicy("roles-policy", policy =>
                    {
                        policy.RequireRole(new string[] { "admin", "super-admin" });
                    });
                    options.AddPolicy("claim-policy-1", policy =>
                    {
                        policy.RequireClaim("LastName");
                    });
                    options.AddPolicy("claim-policy-2", policy =>
                    {
                        policy.RequireClaim("LastName", new string[] { "Bommidi", "Test" });
                    });
                });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseCors("allowedOrigin");
            
            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
