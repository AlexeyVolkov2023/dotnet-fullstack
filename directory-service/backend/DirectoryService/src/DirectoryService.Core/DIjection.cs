using DirectoryService.Core.Departments;
using DirectoryService.Core.Locations.Create;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Core;

public static class DInjection
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<CreateLocationHandler>();
        serviceCollection.AddScoped<CreateDepartmentHandler>();

        serviceCollection.AddValidatorsFromAssembly(typeof(DInjection).Assembly);

        return serviceCollection;
    }
}