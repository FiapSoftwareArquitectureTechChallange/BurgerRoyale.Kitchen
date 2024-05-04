using BurgerRoyale.Kitchen.Domain.Entities;

namespace BurgerRoyale.Kitchen.Domain.Repositories;

public interface IPreparationRepository
{
    Task Add(Order order);

    Task<IEnumerable<Order>> GetAll();

    Task Update(Order order);
}
