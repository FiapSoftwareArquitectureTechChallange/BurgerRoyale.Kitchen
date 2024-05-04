namespace BurgerRoyale.Kitchen.Domain.DatabaseConfiguration;

public interface IDatabaseConfiguration
{
    string ConnectionURI();

    string DatabaseName();

    string CollectionName();
}
