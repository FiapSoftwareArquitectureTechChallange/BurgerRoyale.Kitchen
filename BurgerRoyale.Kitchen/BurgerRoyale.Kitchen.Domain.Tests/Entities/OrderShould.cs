using BurgerRoyale.Kitchen.Domain.Entities;
using BurgerRoyale.Kitchen.Domain.Enums;

namespace BurgerRoyale.Kitchen.Domain.Tests.Entities;

internal class OrderShould
{
    [Test]
    public void Update_Order_Status_When_Update_Method_Is_Called()
    {
        #region Arrange(Given)

        var order = new Order(new List<OrderProduct>(), OrderStatus.Waiting);

        #endregion

        #region Act(When)

        order.UpdateStatus(OrderStatus.Preparing);

        #endregion

        #region Assert(Then)

        Assert.That(order.Status, Is.EqualTo(OrderStatus.Preparing));
        Assert.That(order.IsValid, Is.True);
        Assert.That(order.OrderProducts.Count, Is.Zero);

        #endregion
    }
}
