using DirectoryService.Infrastructure.Postgres;

namespace DirectoryService.Web;

internal static class DInjection
{
    public static IServiceCollection AddProgramDependencies(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .AddInfrastructureDependencies(configuration)
            .AddWebDependencies();

        return serviceCollection;
    }

    private static IServiceCollection AddWebDependencies(
        this IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllers();
        serviceCollection.AddOpenApi();

        return serviceCollection;
    }
}