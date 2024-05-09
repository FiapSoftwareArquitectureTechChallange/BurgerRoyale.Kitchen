using BurgerRoyale.Kitchen.Application.Contracts.UseCases;
using BurgerRoyale.Kitchen.Application.Models;
using BurgerRoyale.Kitchen.Application.UseCases;
using BurgerRoyale.Kitchen.Domain.Entities;
using BurgerRoyale.Kitchen.Domain.Enums;
using BurgerRoyale.Kitchen.Domain.Repositories;
using Moq;

namespace BurgerRoyale.Kitchen.Application.Tests.UseCases;

internal class RequestPreparationShould
{
    private Mock<IPreparationRepository> preparationRepositoryMock;

    private IRequestPreparation requestPreparation;

    private RequestPreparationRequest request;

    [SetUp]
    public void SetUp()
    {
        preparationRepositoryMock = new Mock<IPreparationRepository>();

        requestPreparation = new RequestPreparation(preparationRepositoryMock.Object);

        var orderProduct = new OrderProduct()
        {
            ProductName = "Burger",
            ProductId = Guid.NewGuid(),
            Quantity = 1,
        };

        var prodList = new List<OrderProduct>()
        {
            orderProduct
        };

        request = new RequestPreparationRequest
        {
            OrderId = Guid.NewGuid(),
            OrderStatus = OrderStatus.Waiting,
            Products = prodList,
        };
    }

    [Test]
    public async Task Request_Preparation()
    {
        #region Arrange(Given)

        var orderId = Guid.NewGuid();

        request.OrderId = orderId;
        var status = OrderStatus.Waiting;
        request.OrderStatus = status;

        #endregion

        #region Act(When)

        RequestPreparationResponse response = await requestPreparation.RequestAsync(request);

        #endregion

        #region Assert(Then)

        Assert.That(response, Is.Not.Null);

        Assert.That(response.IsValid, Is.True);

        Assert.That(response.OrderId, Is.Not.Empty);

        preparationRepositoryMock
            .Verify(repository => repository.Add(It.Is<Order>(
                o =>
                    o.Id == orderId &&
                    o.Status == status)),
            Times.Once);

        #endregion
    }

    [Test]
    public async Task Create_Order_With_Waiting_Status_When_Request_Order()
    {
        #region Arrange(Given)

        #endregion

        #region Act(When)

        await requestPreparation.RequestAsync(request);

        #endregion

        #region Assert(Then)

        preparationRepositoryMock
            .Verify(repository => repository.Add(It.Is<Order>(
                o =>
                    o.Status == OrderStatus.Waiting)),
            Times.Once);

        #endregion
    }
}
