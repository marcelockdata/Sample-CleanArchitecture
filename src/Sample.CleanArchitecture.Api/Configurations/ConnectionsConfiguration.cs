using Microsoft.EntityFrameworkCore;
using Sample.CleanArchitecture.Infra.Data.EF;
using System.Diagnostics.CodeAnalysis;

namespace Sample.CleanArchitecture.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class ConnectionsConfiguration
{
    public static IServiceCollection AddAppConections(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbConnection(configuration);
        return services;
    }

    private static IServiceCollection AddDbConnection(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        var connectionString = configuration
         .GetConnectionString("DataBase");
        ////.GetConnectionString("ConnectionStrings__connectionstring-portfolio-produtos"); /// executar com migrations localhost
        services.AddDbContext<AppDbContext>(
            options => options.UseNpgsql(
                connectionString));
        return services;
    }
}
