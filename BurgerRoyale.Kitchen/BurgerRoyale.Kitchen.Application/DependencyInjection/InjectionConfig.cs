using BurgerRoyale.Kitchen.Application.Contracts.UseCases;
using BurgerRoyale.Kitchen.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.Kitchen.Application.DependencyInjection;

public static class InjectionConfig
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        #region UseCases

        services.AddScoped<IRequestPreparation, RequestPreparation>();

        #endregion

        #region Mappers

        

        #endregion

        #region Validators

        

        #endregion
    }
}
