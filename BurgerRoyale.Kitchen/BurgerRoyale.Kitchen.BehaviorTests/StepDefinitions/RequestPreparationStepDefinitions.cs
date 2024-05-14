namespace BurgerRoyale.Kitchen.BehaviorTests.StepDefinitions;

[Binding]
public class RequestPreparationStepDefinitions(ScenarioContext context, KitchenClient client)
{
	[Given(@"An order arrives in the system")]
	public void AnOrderArrivesInTheSystem()
	{
		var products = new List<OrderProduct>() 
		{
			new OrderProduct()
			{
				ProductId = Guid.NewGuid(),
				ProductName = "Premium Burger",
				Quantity = 10
			}
		};

		RequestPreparationRequest request = new RequestPreparationRequest()
		{
			OrderId = Guid.NewGuid(),
			OrderStatus = OrderStatus._0,
			Products = products
		};
		context["Order"] = request;
	}

	[When(@"User places an order")]
	public async Task WhenUserPlacesAnOrder()
	{
		var prepRequest = context.Get<RequestPreparationRequest>("Order");

		var response = await client.RequestPreparationAsync(prepRequest);

		context.Set(response);
	}

	[Then(@"Create order in the database")]
	public void ThenCreateOrderInTheDatabase()
	{
		var orders = client.Get_ordersAsync();
		orders.Should().NotBeNull();
	}
}