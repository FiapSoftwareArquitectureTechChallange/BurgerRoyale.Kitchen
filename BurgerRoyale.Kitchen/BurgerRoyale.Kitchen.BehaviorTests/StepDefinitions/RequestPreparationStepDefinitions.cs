﻿namespace BurgerRoyale.Kitchen.BehaviorTests.StepDefinitions;

[Binding]
public class RequestPreparationStepDefinitions(ScenarioContext context, KitchenClient client)
{
    [Given(@"An order arrives in the system")]
    public void GivenAnOrderArrivesInTheSystem()
    {
    }

    [When(@"User places an order")]
    public async Task WhenUserPlacesAnOrder()
    {
        var response = await client.Get_ordersAsync();

        context.Set(response);
    }

    [Then(@"Create order in the database")]
    public void ThenCreateOrderInTheDatabase()
    {
    }
}