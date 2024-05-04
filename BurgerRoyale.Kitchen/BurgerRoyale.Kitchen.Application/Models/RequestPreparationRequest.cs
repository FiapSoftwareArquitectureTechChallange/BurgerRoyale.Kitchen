using BurgerRoyale.Kitchen.Domain.Entities;
using BurgerRoyale.Kitchen.Domain.Enums;

namespace BurgerRoyale.Kitchen.Application.Models;

public class RequestPreparationRequest
{
    public Guid OrderId { get; set; }

    public IEnumerable<OrderProduct> Products { get; set; }

    public OrderStatus OrderStatus { get; set; }
}
