using Core.Database.Context;
using Core.Database.Entities;
using Core.Handlers;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using Core.Utils.SignalR;
using Infrastructure.Config;
using ISoft.Travel.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<HackatonDbContext>();
            services.AddDbContext<DbContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<RolesRepository>();
            services.AddScoped<UserRoleRepository>();
            services.AddScoped<UsersRepository>();
            services.AddScoped<CoursesRepository>();
            services.AddScoped<AnswersRepository>();
            services.AddScoped<QuestionsRepitory>();
            services.AddScoped<RewardsRepository>();
            services.AddScoped<TaskToDoRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<EfUnitOfWork>();
            services.AddScoped<UserService>();
            services.AddScoped<AuthTokenHandler>();
            services.AddScoped<RoleService>();
            services.AddScoped<EmailService>();
            services.AddScoped<CourseService>();
            services.AddScoped<TaskToDoService>();
            services.AddScoped<RewardService>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (bearer {token})",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    }
                });


            });
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(AppConfig.JwtConfiguration.SecretKey)),
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidAudience = AppConfig.JwtConfiguration.Audience,
                       ValidIssuer = AppConfig.JwtConfiguration.Issuer,
                       ClockSkew = TimeSpan.Zero
                   };
               });
        }

    }
}
