using BurgerRoyale.Kitchen.Application.Contracts.Mapper;
using BurgerRoyale.Kitchen.Application.Mapper;
using BurgerRoyale.Kitchen.Application.Models;
using BurgerRoyale.Kitchen.Domain.Entities;
using BurgerRoyale.Kitchen.Domain.Enums;

namespace BurgerRoyale.Kitchen.Application.Tests.Mappers;

internal class OrderMapShould
{
    private IOrderMapper mapper;

    private Order order;

    private List<OrderProduct> products = new List<OrderProduct>();

    [SetUp]
    public void SetUp()
    {
        mapper = new OrderMapper();

        products.Add(new OrderProduct()
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test",
            Quantity = 1,
        });

        order = new Order(Guid.NewGuid(), products, OrderStatus.Waiting);
    }

    [Test]
    public void Map_Id()
    {
        #region Arrange(Given)

        Guid orderId = order.Id;

        #endregion

        #region Act(When)

        OrderResponse orderResponse = mapper.Map(order);

        #endregion

        #region Assert(Then)

        Assert.That(orderResponse, Is.Not.Null);

        Assert.That(orderResponse.Id, Is.EqualTo(orderId));

        #endregion
    }

    [Test]
    public void Map_Status()
    {
        #region Arrange(Given)

        var status = OrderStatus.Waiting;

        order = new Order(Guid.NewGuid(), products, status);

        #endregion

        #region Act(When)

        OrderResponse orderResponse = mapper.Map(order);

        #endregion

        #region Assert(Then)

        Assert.That(orderResponse.Status, Is.EqualTo(status));

        #endregion
    }

    [Test]
    public void Map_Order_Products()
    {
        // Arrange in SetUp
        
        #region Act(When)

        var mappedOrder = mapper.Map(order);

        #endregion

        #region Assert(Then)

        Assert.That(mappedOrder.OrderProducts, Is.EqualTo(products));

        #endregion
    }
}
