using BurgerRoyale.Kitchen.BackgroundService.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.Kitchen.BackgroundService.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class InjectionConfig
{
	public static void AddBackgroundServiceDependencies(this IServiceCollection services)
	{
		services.AddHostedService<SuccessfulPaymentBackgroundService>();
	}
}
