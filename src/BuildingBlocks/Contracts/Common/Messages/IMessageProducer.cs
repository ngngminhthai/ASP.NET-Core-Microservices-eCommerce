namespace Contracts.Messages;

public interface IMessageProducer
{
    void SendMessage<T>(T message);
}