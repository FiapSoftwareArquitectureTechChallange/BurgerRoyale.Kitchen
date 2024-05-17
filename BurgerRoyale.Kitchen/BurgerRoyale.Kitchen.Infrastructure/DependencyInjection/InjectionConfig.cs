using BurgerRoyale.Kitchen.Domain.Contracts.IntegrationServices;
using BurgerRoyale.Kitchen.Domain.Contracts.Queues;
using BurgerRoyale.Kitchen.Domain.CredentialsConfiguration;
using BurgerRoyale.Kitchen.Domain.DatabaseConfiguration;
using BurgerRoyale.Kitchen.Domain.Repositories;
using BurgerRoyale.Kitchen.Infrastructure.BackgroundMessage;
using BurgerRoyale.Kitchen.Infrastructure.Database.Config;
using BurgerRoyale.Kitchen.Infrastructure.Database.Context;
using BurgerRoyale.Kitchen.Infrastructure.QueueConfiguration;
using BurgerRoyale.Kitchen.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.Kitchen.Infrastructure.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class InjectionConfig
{
    public static void AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IPreparationRepository, PreparationRepository>();
        services.AddScoped<IMessageService, AWSSQSService>();
        services.AddScoped<IMessageQueue, MessageQueuesConfiguration>();
        services.AddScoped<ICredentialConfiguration, AWSConfiguration>();
        services.AddScoped<IDatabaseConfiguration, MongoDBSettings>();
        services.AddScoped<PreparationContext>();
    }
}
