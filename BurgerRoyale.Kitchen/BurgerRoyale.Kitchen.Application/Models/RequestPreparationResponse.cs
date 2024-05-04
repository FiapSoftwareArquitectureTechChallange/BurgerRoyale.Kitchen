using BurgerRoyale.Kitchen.Domain.Entities;
using BurgerRoyale.Kitchen.Domain.Enums;
using Flunt.Notifications;

namespace BurgerRoyale.Kitchen.Application.Models;

public class RequestPreparationResponse : Notifiable<Notification>
{
    public Guid OrderId { get; set; }

    public OrderStatus OrderStatus { get; set; }
}
