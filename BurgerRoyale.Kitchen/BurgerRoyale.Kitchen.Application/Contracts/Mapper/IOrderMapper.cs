using BurgerRoyale.Kitchen.Application.Models;
using BurgerRoyale.Kitchen.Domain.Entities;

namespace BurgerRoyale.Kitchen.Application.Contracts.Mapper;

public interface IOrderMapper
{
    OrderResponse Map(Order order);
}
