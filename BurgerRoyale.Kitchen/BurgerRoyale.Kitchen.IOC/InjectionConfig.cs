using BurgerRoyale.Kitchen.Application.DependencyInjection;
using BurgerRoyale.Kitchen.BackgroundService.DependencyInjection;
using BurgerRoyale.Kitchen.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.Kitchen.IOC;


[ExcludeFromCodeCoverage]
public static class InjectionConfig
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddApplicationDependencies();

        services.AddInfrastructureDependencies();

        services.AddBackgroundServiceDependencies();
    }
}