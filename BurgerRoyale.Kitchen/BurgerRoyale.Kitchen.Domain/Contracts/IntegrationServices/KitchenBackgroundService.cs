﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BurgerRoyale.Kitchen.Domain.Contracts.IntegrationServices;

public abstract class KitchenBackgroundService<TMessage> : BackgroundService, IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly string _queueName;
    protected IServiceProvider _serviceProvider;

    protected KitchenBackgroundService(IServiceScopeFactory serviceScopeFactory, string queueName)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _queueName = queueName;

        _serviceProvider = _serviceScopeFactory
            .CreateScope()
            .ServiceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        IMessageService _messageService = _serviceProvider.GetRequiredService<IMessageService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            var messages = await _messageService.ReadMessagesAsync<TMessage>(_queueName, 10);

            if (messages.Any())
            {
                foreach (var msg in messages)
                {
                    await ProcessMessage(msg);
                }
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }

    protected abstract Task ProcessMessage(TMessage message);

}
