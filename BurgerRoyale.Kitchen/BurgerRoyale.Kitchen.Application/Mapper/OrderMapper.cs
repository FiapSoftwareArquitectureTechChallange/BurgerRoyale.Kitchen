using BurgerRoyale.Kitchen.Application.Contracts.Mapper;
using BurgerRoyale.Kitchen.Application.Models;
using BurgerRoyale.Kitchen.Domain.Entities;

namespace BurgerRoyale.Kitchen.Application.Mapper;

public class OrderMapper : IOrderMapper
{
    public OrderResponse Map(Order order)
    {
        return new OrderResponse()
        {
            Id = order.Id,
            OrderProducts = order.OrderProducts,
            Status = order.Status,
        };
    }
}
