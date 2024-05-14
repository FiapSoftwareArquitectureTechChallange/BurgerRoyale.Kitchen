using BoDi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BurgerRoyale.Kitchen.BehaviorTests.Hooks;

[Binding]
public sealed class EnvironmentSetupHooks
{
    [BeforeTestRun]
    public static void BeforeTestRun(IObjectContainer testThreadContainer)
    {
        HttpClient? apiHttpClient;

        var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development");
        });

        apiHttpClient = application.CreateClient();

        var kitchenClient =
            new KitchenClient("", apiHttpClient);

        testThreadContainer.RegisterInstanceAs(kitchenClient);
    }
}