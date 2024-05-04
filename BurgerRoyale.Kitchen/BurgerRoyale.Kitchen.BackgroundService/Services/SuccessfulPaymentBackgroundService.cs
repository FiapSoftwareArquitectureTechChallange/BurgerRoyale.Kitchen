using BurgerRoyale.Kitchen.Application.Contracts.UseCases;
using BurgerRoyale.Kitchen.Application.Models;
using BurgerRoyale.Kitchen.Domain.Contracts.IntegrationServices;
using BurgerRoyale.Kitchen.Domain.Contracts.Queues;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.Kitchen.BackgroundService.Services;

public class SuccessfulPaymentBackgroundService : KitchenBackgroundService<RequestPreparationRequest>
{
    private readonly IRequestPreparation _requestPrep;
    public SuccessfulPaymentBackgroundService(
        IServiceScopeFactory serviceScopeFactory,
        IServiceProvider serviceProvider) :
        base(serviceScopeFactory, GetQueueName(serviceProvider))
    {
        _requestPrep = _serviceProvider.GetRequiredService<IRequestPreparation>();
    }

    private static string GetQueueName(IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();

        IMessageQueue messageQueue = scope.ServiceProvider.GetRequiredService<IMessageQueue>();
        return messageQueue.OrderPreparationRequestQueue();
    }

    protected override async Task ProcessMessage(RequestPreparationRequest message)
    {
        await _requestPrep.RequestAsync(message);
    }
}
