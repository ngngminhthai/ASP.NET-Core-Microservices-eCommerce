using EventBus.IntegrationEvents;

namespace Contracts.Messages;

public interface IMessageProducer
{
    void SendMessage<T>(T message);

    void PublishEvent(IntegrationEvent @event);
}