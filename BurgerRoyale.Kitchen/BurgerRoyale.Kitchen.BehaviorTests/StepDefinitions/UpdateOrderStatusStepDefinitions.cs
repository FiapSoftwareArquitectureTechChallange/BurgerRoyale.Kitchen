using NUnit.Framework;

namespace BurgerRoyale.Kitchen.BehaviorTests.StepDefinitions;

[Binding]
public class UpdateOrderStatusStepDefinitions(ScenarioContext context, KitchenClient client)
{
	[Given(@"An order exists in the database")]
	public async Task GivenAnOrderExistsInTheDatabase()
	{
		var orders = await client.Get_ordersAsync();
		orders.Should().NotBeNull();
		context["Orders"] = orders;
	}

	[When(@"The Kitchen fetches orders")]
	public void WhenTheKitchenFetchesOrders()
	{
		var orders = context.Get<ICollection<OrderResponse>>("Orders");
		orders.Should().NotBeNull();
		if (orders.Count > 0)
		{
			context["OrderToUpdate"] = orders.First();
		}
	}

	[Then(@"Update order status")]
	public async Task ThenUpdateOrderStatus()
	{
		var order = context.Get<OrderResponse>("OrderToUpdate");
		var updatedOrder = await client.UpdateAsync(order.Id, OrderStatus._1);
		updatedOrder.Should().NotBeNull();
		Assert.AreEqual(updatedOrder.Status, OrderStatus._1);
	}
}
