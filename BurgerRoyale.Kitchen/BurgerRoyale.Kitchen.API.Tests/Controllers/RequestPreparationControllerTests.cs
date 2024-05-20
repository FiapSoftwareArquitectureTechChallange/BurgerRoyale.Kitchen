using BurgerRoyale.Kitchen.API.Controllers;
using BurgerRoyale.Kitchen.Application.Contracts.UseCases;
using BurgerRoyale.Kitchen.Application.Models;
using BurgerRoyale.Kitchen.Domain.Entities;
using BurgerRoyale.Kitchen.Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace BurgerRoyale.Kitchen.API.Tests.Controllers;

public class RequestPreparationControllerTests
{
    private Mock<IRequestPreparation> _requestPreparation;
    private RequestPreparationController _controller;

    [SetUp]
    public void Setup()
    {
        _requestPreparation = new Mock<IRequestPreparation>();
        _controller = new RequestPreparationController(_requestPreparation.Object);
    }

    [Test]
    public async Task GivenGetOrdersRequest_WhenGetOrders_ThenShouldReturnListWithStatusOk()
    {
        // arrange
        var guid = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var rpq = new RequestPreparationRequest()
        {
            OrderId = guid,
            OrderProducts = new List<OrderProduct>(),
            UserId = userId,
        };

        _requestPreparation
            .Setup(x => x.RequestAsync(rpq))
            .ReturnsAsync(new RequestPreparationResponse());

        // act
        var response = await _controller.RequestPayment(rpq) as ObjectResult;

        // assert
        response.Should().NotBeNull();
        response?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        response?.Value.Should().BeOfType<RequestPreparationResponse>();
    }
}
