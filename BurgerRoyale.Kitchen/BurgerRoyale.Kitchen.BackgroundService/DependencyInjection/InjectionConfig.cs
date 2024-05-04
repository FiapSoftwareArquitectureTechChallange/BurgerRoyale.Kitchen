using BurgerRoyale.Kitchen.BackgroundService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.Kitchen.BackgroundService.DependencyInjection
{
    public static class InjectionConfig
    {
        public static void AddBackgroundServiceDependencies(this IServiceCollection services)
        {
            services.AddHostedService<SuccessfulPaymentBackgroundService>();
        }
    }
}
    