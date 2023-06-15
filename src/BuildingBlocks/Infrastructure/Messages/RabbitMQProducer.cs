using Contracts.Common.Interfaces;
using Contracts.Messages;
using EventBus.IntegrationEvents;
using RabbitMQ.Client;
using System.Text;

namespace Infrastructure.Messages;

public class RabbitMQProducer : IMessageProducer
{
    private readonly ISerializeService _serializeService;

    public RabbitMQProducer(ISerializeService serializeService)
    {
        _serializeService = serializeService;
    }

    public void PublishEvent(IntegrationEvent @event, string queue)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        var connection = connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue, exclusive: false);

        var jsonData = _serializeService.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(jsonData);

        channel.BasicPublish(exchange: "", routingKey: queue, body: body);
    }

    public void SendMessage<T>(T message)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        var connection = connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare("orders", exclusive: false);

        var jsonData = _serializeService.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonData);

        channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
    }
}