using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.SQS;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.Kitchen.Infrastructure.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class InjectionConfig
{
    public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPreparationRepository, PreparationRepository>();
        services.AddScoped<IMessageService, AWSSQSService>();
        services.AddScoped<IMessageQueue, MessageQueuesConfiguration>();
        services.AddScoped<ICredentialConfiguration, AWSConfiguration>();

        var awsConfiguration = configuration
            .GetSection("AWS");

        services.AddDefaultAWSOptions(new AWSOptions()
        {
            Credentials = new SessionAWSCredentials(
                    awsConfiguration.GetSection("AccessKey").Value,
                    awsConfiguration.GetSection("SecretKey").Value,
                    awsConfiguration.GetSection("SessionToken").Value
                ),

            Region = RegionEndpoint.GetBySystemName(awsConfiguration.GetSection("Region").Value)
        });

        services.AddScoped<IDatabaseConfiguration, MongoDBSettings>();
        services.AddScoped<PreparationContext>();
        services.AddAWSService<IAmazonSQS>(ServiceLifetime.Scoped);
    }
}
