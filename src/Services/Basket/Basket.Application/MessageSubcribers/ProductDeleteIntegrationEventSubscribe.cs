using Basket.Application.IntegrationEvents.EventHandling;
using Basket.Application.IntegrationEvents.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Basket.Application.MessageSubcribers
{
    public class ProductDeleteIntegrationEventSubscribe
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public ProductDeleteIntegrationEventSubscribe()
        {
            _connectionFactory = new ConnectionFactory { HostName = "localhost" };
        }

        public void Subscribe()
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("product-deletes", exclusive: false);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += ConsumerOnReceived;

            _channel.BasicConsume(queue: "product-deletes", autoAck: true, consumer: consumer);
        }

        private void ConsumerOnReceived(object sender, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // Deserialize the message to the appropriate event class
            var @event = JsonConvert.DeserializeObject<ProductDeleteIntegrationEvent>(message);

            // Handle the event
            var handler = new ProductDeleteIntegrationEventHandler();
            handler.Handle(@event).Wait(); // Added wait here because Handle is async.

            Console.WriteLine($"Message received: {message}");
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
