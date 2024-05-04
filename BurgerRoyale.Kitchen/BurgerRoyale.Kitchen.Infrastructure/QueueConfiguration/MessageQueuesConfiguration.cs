using BurgerRoyale.Kitchen.Domain.Contracts.Queues;
using Microsoft.Extensions.Configuration;

namespace BurgerRoyale.Kitchen.Infrastructure.QueueConfiguration;

public class MessageQueuesConfiguration(IConfiguration configuration) : IMessageQueue
{
    public string OrderPreparationFeedbackQueue()
    {
        IConfigurationSection queueSettings = GetQueueSection();
        return queueSettings.GetSection("OrderPaymentFeedbackQueue").Value!;
    }

    private IConfigurationSection GetQueueSection()
    {
        return configuration.GetSection("MessageQueues");
    }

    public string OrderPreparationRequestQueue()
    {
        IConfigurationSection queueSettings = GetQueueSection();
        return queueSettings.GetSection("OrderPaymentRequestQueue").Value!;
    }
}
