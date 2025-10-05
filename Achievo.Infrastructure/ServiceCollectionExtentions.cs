using System;
using Achievo.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Achievo.Infrastructure;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddAchievoInfrastructureDbContext(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AchievoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
}
