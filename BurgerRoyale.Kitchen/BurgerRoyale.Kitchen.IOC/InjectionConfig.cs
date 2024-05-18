using BurgerRoyale.Kitchen.Application.DependencyInjection;
using BurgerRoyale.Kitchen.BackgroundService.DependencyInjection;
using BurgerRoyale.Kitchen.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.Kitchen.IOC;


public static class InjectionConfig
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDependencies();

        services.AddInfrastructureDependencies(configuration);

        services.AddBackgroundServiceDependencies();
    }
}