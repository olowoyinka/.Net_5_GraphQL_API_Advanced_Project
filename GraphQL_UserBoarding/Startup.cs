using GraphQL.Server.Ui.Voyager;
using GraphQL_UserBoarding.Data;
using GraphQL_UserBoarding.Logics;
using GraphQL_UserBoarding.Resolvers;
using GraphQL_UserBoarding.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace GraphQL_UserBoarding
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
            services.Configure<TokenSettings>(Configuration.GetSection("TokenSettings"));

            services.AddScoped<IAuthLogic, AuthLogic>();

            services.AddDbContext<AppDbContext>(options =>
                             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddGraphQLServer()
                    .AddAuthorization()
                    .AddQueryType<QueryResolver>()
                    .AddMutationType<MutationResolver>();

            services.AddAuthentication(o =>
            {
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                var tokenSettings = Configuration.GetSection("TokenSettings").Get<TokenSettings>();

                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = tokenSettings.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key)),
                    ValidAudience = tokenSettings.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

            services.AddAuthorization(options => {
                options.AddPolicy("roles-policy", policy => {
                    policy.RequireRole(new string[] { "admin", "super-admin" });
                });
                options.AddPolicy("claim-policy-1", policy => {
                    policy.RequireClaim("LastName");
                });
                options.AddPolicy("claim-policy-2", policy => {
                    policy.RequireClaim("LastName", new string[] { "Olowofela", "Test" });
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
                Path = "/graphql-voyager"
            });
        }
    }
}