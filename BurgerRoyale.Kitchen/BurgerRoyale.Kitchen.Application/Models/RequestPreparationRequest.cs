using BurgerRoyale.Kitchen.Domain.Entities;

namespace BurgerRoyale.Kitchen.Application.Models;

public class RequestPreparationRequest
{
    public Guid OrderId { get; set; }

    public IEnumerable<OrderProduct> OrderProducts { get; set; }

    public Guid UserId { get; set; }
}
