using BurgerRoyale.Kitchen.Domain.DatabaseConfiguration;
using BurgerRoyale.Kitchen.Domain.Entities;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.Kitchen.Infrastructure.Database.Context;

[ExcludeFromCodeCoverage]
public class PreparationContext
{
    public readonly IMongoCollection<Order> Orders;

    public PreparationContext(IDatabaseConfiguration databaseSettings)
    {
        MongoClient client = new(databaseSettings.ConnectionURI());

        IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName());

        Orders = database.GetCollection<Order>(databaseSettings.CollectionName());
    }
}
