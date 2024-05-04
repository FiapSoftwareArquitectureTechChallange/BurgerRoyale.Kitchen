using BurgerRoyale.Kitchen.Domain.Entities;
using BurgerRoyale.Kitchen.Domain.Repositories;
using BurgerRoyale.Kitchen.Infrastructure.Database.Context;
using MongoDB.Driver;

namespace BurgerRoyale.Kitchen.Infrastructure.Repositories;

public class PreparationRepository(PreparationContext context) : IPreparationRepository
{
    public async Task Add(Order order)
    {
        await context.Orders.InsertOneAsync(order);
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await context.Orders
            .Find(o => true)
            .ToListAsync();
    }

    public async Task Update(Order order)
    {
        var filter = Builders<Order>.Filter.Eq(e => e.Id, order.Id);

        await context.Orders.ReplaceOneAsync(filter, order);
    }
}
