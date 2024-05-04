namespace BurgerRoyale.Kitchen.Domain.Contracts.Queues;

public interface IMessageQueue
{
    string OrderPreparationRequestQueue();

    string OrderPreparationFeedbackQueue();
}
