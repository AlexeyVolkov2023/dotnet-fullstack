using DirectoryService.Core.Departments;
using DirectoryService.Core.Locations;
using DirectoryService.Infrastructure.Postgres.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Infrastructure.Postgres;

public static class DInjection
{
    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddScoped<DirectoryServiceDbContext>(_ =>
            new DirectoryServiceDbContext(configuration.GetConnectionString("DefaultConnection")!));


        serviceCollection.AddScoped<ILocationRepository, EfCoreLocationRepository>();
        serviceCollection.AddScoped<IDepartmentRepository, EfCoreDepartmentRepository>();


        return serviceCollection;
    }
}