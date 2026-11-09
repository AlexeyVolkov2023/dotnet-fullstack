using DirectoryService.Core.Departments;
using DirectoryService.Core.Departments.Communications.AddLocation;
using DirectoryService.Core.Departments.RemoveLocation;
using DirectoryService.Core.Departments.UpdateName;
using DirectoryService.Core.Locations.Create;
using DirectoryService.Core.Locations.Update;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Core;

public static class DInjection
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<CreateLocationHandler>();
        serviceCollection.AddScoped<CreateDepartmentHandler>();
        serviceCollection.AddScoped<UpdateLocationHandler>();
        serviceCollection.AddScoped<UpdateDepartmentNameHandler>();
        serviceCollection.AddScoped<AddLocationHandler>();
        serviceCollection.AddScoped<RemoveLocationHandler>();
        
        serviceCollection.AddValidatorsFromAssembly(typeof(DInjection).Assembly);

        return serviceCollection;
    }
}