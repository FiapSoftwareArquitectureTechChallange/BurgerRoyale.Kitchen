using BurgerRoyale.Kitchen.Domain.Entities;
using BurgerRoyale.Kitchen.Domain.Enums;
using Flunt.Notifications;

namespace BurgerRoyale.Kitchen.Application.Models;

public class OrderResponse
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<OrderProduct> OrderProducts { get; set; }
}
