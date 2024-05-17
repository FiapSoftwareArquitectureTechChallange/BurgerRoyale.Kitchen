using BurgerRoyale.Kitchen.Application.Contracts.Mapper;
using BurgerRoyale.Kitchen.Application.Contracts.UseCases;
using BurgerRoyale.Kitchen.Application.Models;
using BurgerRoyale.Kitchen.Application.UseCases;
using BurgerRoyale.Kitchen.Domain.Contracts.IntegrationServices;
using BurgerRoyale.Kitchen.Domain.Contracts.Queues;
using BurgerRoyale.Kitchen.Domain.Entities;
using BurgerRoyale.Kitchen.Domain.Enums;
using BurgerRoyale.Kitchen.Domain.Repositories;
using Moq;

namespace BurgerRoyale.Kitchen.Application.Tests.UseCases;

internal class GetOrdersShould
{
    private Mock<IPreparationRepository> repositoryMock;

    private Mock<IOrderMapper> mapperMock;

    private Mock<IMessageQueue> messageQueueMock;

    private Mock<IMessageService> messageServiceMock;

    private IGetOrders getOrders;

    [SetUp]
    public void SetUp()
    {
        repositoryMock = new Mock<IPreparationRepository>();

        mapperMock = new Mock<IOrderMapper>();

        messageQueueMock = new Mock<IMessageQueue>();

        messageServiceMock = new Mock<IMessageService>();

        getOrders = new GetOrders(
            repositoryMock.Object,
            mapperMock.Object,
            messageServiceMock.Object,
            messageQueueMock.Object);
    }

    [Test]
    public async Task Get_Orders()
    {
        #region Arrange(Given)

        var orderProduct1 = new OrderProduct()
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test",
            Quantity = 1
        };

        var orderProduct2 = new OrderProduct()
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test2",
            Quantity = 2
        };

        var orderProducts1 = new List<OrderProduct>
        {
            orderProduct1,
            orderProduct2
        };

        var orderProducts2 = new List<OrderProduct>
        {
            orderProduct1,
        };

        var order1 = new Order(Guid.NewGuid(), orderProducts1, OrderStatus.Waiting);

        var order2 = new Order(Guid.NewGuid(), orderProducts2, OrderStatus.Ready);

        repositoryMock
            .Setup(repository => repository.GetAll())
            .ReturnsAsync([order1, order2]);

        mapperMock
            .Setup(mapper => mapper.Map(order1))
            .Returns(new OrderResponse
            {
                Id = order1.Id
            });

        mapperMock
            .Setup(mapper => mapper.Map(order2))
            .Returns(new OrderResponse
            {
                Id = order2.Id
            });

        #endregion

        #region Act(When)

        IEnumerable<OrderResponse> response = await getOrders.GetAllAsync();

        #endregion

        #region Assert(Then)

        Assert.That(response, Is.Not.Null);

        Assert.That(response, Is.Not.Empty);

        Assert.That(response.Count(), Is.EqualTo(2));

        Assert.Multiple(() =>
        {
            Assert.That(response.First().Id, Is.EqualTo(order1.Id));

            Assert.That(response.Last().Id, Is.EqualTo(order2.Id));
        });

        #endregion
    }

    [Test]
    public async Task Update_Order()
    {
         #region Arrange(Given)

        var orderProduct1 = new OrderProduct()
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test",
            Quantity = 1
        };

        var orderProduct2 = new OrderProduct()
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test2",
            Quantity = 2
        };

        var orderProducts = new List<OrderProduct>
        {
            orderProduct1,
            orderProduct2
        };

        var order = new Order(Guid.NewGuid(), orderProducts, OrderStatus.Waiting);

        mapperMock.Setup(o => o.Map(order)).Returns(new OrderResponse()
        {
            Id = order.Id,
        });

        repositoryMock
            .Setup(repository => repository.GetById(order.Id))
            .ReturnsAsync(order);

        messageQueueMock
            .Setup(x => x.OrderPreparedQueue()).Returns("test-queue");

        #endregion

        #region Act(When)

        OrderResponse response = await getOrders.UpdateOrderAsync(order.Id, OrderStatus.Ready);

        #endregion

        #region Assert(Then)

        Assert.That(response, Is.Not.Null);

        Assert.That(order.Status, Is.EqualTo(OrderStatus.Ready));

        messageServiceMock.Verify(m => m.SendMessageAsync(It.IsAny<string>(), It.IsAny<RequestPreparationResponse>()), Times.Once);

        repositoryMock.Verify(x => x.Update(It.IsAny<Order>()), Times.Once);

        #endregion
    }
}
