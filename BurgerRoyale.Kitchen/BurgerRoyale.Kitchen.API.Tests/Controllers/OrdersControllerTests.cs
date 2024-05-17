using BurgerRoyale.Kitchen.API.Controllers;
using BurgerRoyale.Kitchen.Application.Contracts.UseCases;
using BurgerRoyale.Kitchen.Application.Models;
using BurgerRoyale.Kitchen.Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using NSubstitute.Core.Arguments;
using System.Net;
using Xunit;

namespace BurgerRoyale.Kitchen.API.Tests.Controllers;

public class OrdersControllerTests
{
	private Mock<IGetOrders> _orderService;
	private OrdersController _ordersController;

	[SetUp]
	public void Setup()
	{
		_orderService = new Mock<IGetOrders>();
		_ordersController = new OrdersController(_orderService.Object);
	}

	[TearDown]
	public void Dispose()
	{
		_ordersController.Dispose();
	}

	[Test]
	public async Task GivenGetOrdersRequest_WhenGetOrders_ThenShouldReturnListWithStatusOk()
	{
		// arrange
		_orderService
			.Setup(x => x.GetAllAsync())
			.ReturnsAsync(new List<OrderResponse>());

		// act
		var response = await _ordersController.GetOrders() as ObjectResult;

		// assert
		response.Should().NotBeNull();
		response?.StatusCode.Should().Be((int)HttpStatusCode.OK);
		response?.Value.Should().BeOfType<List<OrderResponse>>();
	}

	[Test]
	public async Task GivenUpdateOrder_WhenUpdateOrder_ThenShouldReturnOrderResponseWithStatusOk()
	{
		// arrange
		var guid = Guid.NewGuid();
		_orderService
			.Setup(x => x.UpdateOrderAsync(guid, OrderStatus.Preparing))
			.ReturnsAsync(new OrderResponse());

		// act
		var response = await _ordersController.UpdateOrderStatus(guid, OrderStatus.Preparing) as ObjectResult;

		// assert
		response.Should().NotBeNull();
		response?.StatusCode.Should().Be((int)HttpStatusCode.OK);
		response?.Value.Should().BeOfType<OrderResponse>();
	}
}

