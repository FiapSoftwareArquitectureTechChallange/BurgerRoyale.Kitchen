using BurgerRoyale.Kitchen.Application.Models;

namespace BurgerRoyale.Kitchen.BehaviorTests.StepDefinitions;

[Binding]
public class RequestPreparationStepDefinitions(ScenarioContext context, KitchenClient client)
{
    [When(@"User places an order")]
    public async Task WhenUserPlacesAnOrder()
    {
        var addPaymentResponse = context.Get<RequestPreparationResponse>();

        Guid orderId = addPaymentResponse.OrderId;

        var response = await client.Get_ordersAsync();

        context.Set(response);
    }
}
