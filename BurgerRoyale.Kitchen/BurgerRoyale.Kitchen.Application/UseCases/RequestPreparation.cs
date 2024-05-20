using BurgerRoyale.Kitchen.Application.Contracts.UseCases;
using BurgerRoyale.Kitchen.Application.Models;
using BurgerRoyale.Kitchen.Domain.Entities;
using BurgerRoyale.Kitchen.Domain.Repositories;

namespace BurgerRoyale.Kitchen.Application.UseCases;

public class RequestPreparation(IPreparationRepository repository) : IRequestPreparation
{
    public async Task<RequestPreparationResponse> RequestAsync(RequestPreparationRequest request)
    {
        Order order = CreateOrder(request);

        await AddOrder(order);

        return SuccessfulResponse(order);
    }

    private static Order CreateOrder(RequestPreparationRequest request)
    {
        return new Order(request.OrderId, request.OrderProducts, Domain.Enums.OrderStatus.Waiting);
    }

    private async Task AddOrder(Order order)
    {
        await repository.Add(order);
    }

    private static RequestPreparationResponse SuccessfulResponse(Order order)
    {
        return new RequestPreparationResponse
        {
            OrderStatus = order.Status,
            OrderId = order.Id
        };
    }
}


