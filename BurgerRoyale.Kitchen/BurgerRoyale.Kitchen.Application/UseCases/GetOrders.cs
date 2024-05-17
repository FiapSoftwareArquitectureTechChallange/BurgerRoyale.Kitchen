using BurgerRoyale.Kitchen.Application.Contracts.Mapper;
using BurgerRoyale.Kitchen.Application.Contracts.UseCases;
using BurgerRoyale.Kitchen.Application.Models;
using BurgerRoyale.Kitchen.Domain.Contracts.IntegrationServices;
using BurgerRoyale.Kitchen.Domain.Contracts.Queues;
using BurgerRoyale.Kitchen.Domain.Enums;
using BurgerRoyale.Kitchen.Domain.Repositories;
using System.Data;

namespace BurgerRoyale.Kitchen.Application.UseCases;

public class GetOrders(IPreparationRepository repository, IOrderMapper mapper, IMessageService messageService, IMessageQueue messageQueue) : IGetOrders
{
	public async Task<IEnumerable<OrderResponse>> GetAllAsync()
	{
		var orders = await repository.GetAll();
		return orders.Select(mapper.Map);
	}

	public async Task<OrderResponse> UpdateOrderAsync(Guid id, OrderStatus? orderStatus)
	{
		var order = await repository.GetById(id);
		if (orderStatus is not null)
		{
			order.UpdateStatus(orderStatus.Value);
			await repository.Update(order);
			if (orderStatus == OrderStatus.Ready)
			{
				await messageService.SendMessageAsync(
					messageQueue.OrderPreparedQueue(),
					new RequestPreparationResponse
					{
						OrderId = order.Id,
						OrderStatus = order.Status,
					}
				);
			}
		}
		return mapper.Map(order);
	}
}
