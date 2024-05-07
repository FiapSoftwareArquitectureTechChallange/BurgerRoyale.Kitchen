using BurgerRoyale.Kitchen.Domain.Entities.Base;
using BurgerRoyale.Kitchen.Domain.Enums;
using Flunt.Notifications;

namespace BurgerRoyale.Kitchen.Domain.Entities;

public class Order : Notifiable<Notification>, IEntityBase
{
    public Order(IEnumerable<OrderProduct> orderProducts, OrderStatus orderStatus)
    {
        Id = Guid.NewGuid();
        OrderProducts = orderProducts;
        Status = orderStatus;
    }

    public Order(Guid id, IEnumerable<OrderProduct> orderProducts, OrderStatus orderStatus)
    {
        Id = id;
        OrderProducts = orderProducts;
        Status = orderStatus;
    }

    public IEnumerable<OrderProduct> OrderProducts { get; set; }

    public Guid Id { get; private set; }

    public OrderStatus Status { get; private set; }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
    }
}