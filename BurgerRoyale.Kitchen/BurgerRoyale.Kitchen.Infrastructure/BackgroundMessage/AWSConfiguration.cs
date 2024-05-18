using BurgerRoyale.Kitchen.Domain.CredentialsConfiguration;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.Kitchen.Infrastructure.BackgroundMessage;

[ExcludeFromCodeCoverage]
public class AWSConfiguration(IConfiguration configuration) : ICredentialConfiguration
{
    public string AccessKey()
    {
        IConfigurationSection awsSection = GetAWSSection();
        return awsSection.GetSection("AccessKey").Value!;
    }

    private IConfigurationSection GetAWSSection()
    {
        return configuration.GetSection("AWS");
    }

    public string Region()
    {
        IConfigurationSection awsSection = GetAWSSection();
        return awsSection.GetSection("Region").Value!;
    }

    public string SecretKey()
    {
        IConfigurationSection awsSection = GetAWSSection();
        return awsSection.GetSection("SecretKey").Value!;
    }

    public string SessionToken()
    {
        IConfigurationSection awsSection = GetAWSSection();
        return awsSection.GetSection("SessionToken").Value!;
    }
}
