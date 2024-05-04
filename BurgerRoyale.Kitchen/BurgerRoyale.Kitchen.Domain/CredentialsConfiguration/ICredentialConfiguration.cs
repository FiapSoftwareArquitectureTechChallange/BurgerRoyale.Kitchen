namespace BurgerRoyale.Kitchen.Domain.CredentialsConfiguration;

public interface ICredentialConfiguration
{
    public string AccessKey();

    string SecretKey();

    string SessionToken();

    string Region();
}
