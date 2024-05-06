using BurgerRoyale.Kitchen.Domain.Contracts.Queues;
using Microsoft.Extensions.Configuration;

namespace BurgerRoyale.Kitchen.Infrastructure.QueueConfiguration;

public class MessageQueuesConfiguration(IConfiguration configuration) : IMessageQueue
{
    private IConfigurationSection GetQueueSection()
    {
        return configuration.GetSection("MessageQueues");
    }

    public string OrderPreparationRequestQueue()
    {
        IConfigurationSection queueSettings = GetQueueSection();
        return queueSettings.GetSection("OrderPreparationRequestQueue").Value!;
    }

    public string OrderPreparedQueue()
    {
        IConfigurationSection queueSettings = GetQueueSection();
        return queueSettings.GetSection("OrderPreparedQueue").Value!;
    }
}
