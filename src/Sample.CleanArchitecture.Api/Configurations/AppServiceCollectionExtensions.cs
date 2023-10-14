using Sample.CleanArchitecture.Application;

namespace Sample.CleanArchitecture.Api.Configurations;

public static class AppServiceCollectionExtensions
{
    public static void ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        //services.AddMediatR(typeof(ApplicationEntryPoint));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(ApplicationEntryPoint)));

        services.AddAppConections(configuration);
        services.ConfigureInterfacesDependencie();
        ////services.AddHeaderPropagation(options => options.Headers.Add("X-Correlation-Id"));
    }
}
