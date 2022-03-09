using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Negotiations.Application.Interfaces;
using Negotiations.Application.Settings;
using Negotiations.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Negotiations.Domain.Entities;
using Negotiations.Infrastructure.Identity;

namespace Negotiations.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NegotiationsDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(NegotiationsDbContext).Assembly.FullName)));


            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<INegotiationsDbContext>(provider => provider.GetRequiredService<NegotiationsDbContext>());

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<NegotiationsSeeder>();

            return services;
        }
    }
}