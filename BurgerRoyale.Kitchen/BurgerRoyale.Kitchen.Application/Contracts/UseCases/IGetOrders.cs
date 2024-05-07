using BurgerRoyale.Kitchen.Application.Models;
using BurgerRoyale.Kitchen.Domain.Enums;

namespace BurgerRoyale.Kitchen.Application.Contracts.UseCases;

public interface IGetOrders
{
    Task<IEnumerable<OrderResponse>> GetAllAsync();
    Task<OrderResponse> UpdateOrderAsync(Guid id, OrderStatus? orderStatus);
}
