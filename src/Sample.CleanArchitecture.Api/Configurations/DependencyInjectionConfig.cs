using Sample.CleanArchitecture.Application.Interfaces;
using Sample.CleanArchitecture.Domain.Interfaces;
using Sample.CleanArchitecture.Infra.Data.EF.Repositories;
using Sample.CleanArchitecture.Infra.Data.EF;
using System.Diagnostics.CodeAnalysis;

namespace Sample.CleanArchitecture.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static IServiceCollection ConfigureInterfacesDependencie(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        //services.AddScoped<IPortfolioProdutoRepository, PortfolioProdutoRepository>();
        return services;
    }
}
