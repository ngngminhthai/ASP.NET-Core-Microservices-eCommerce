﻿using Basket.Application.IntegrationEvents.EventHandling;
using Basket.Application.IntegrationEvents.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Basket.Application.MessageSubcribers
{
    public class ProductPriceChangedIntegrationEventSubcribe
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public ProductPriceChangedIntegrationEventSubcribe()
        {
            _connectionFactory = new ConnectionFactory { HostName = "localhost" };
        }

        public void Subscribe()
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("product-price-changes", exclusive: false);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += ConsumerOnReceived;

            _channel.BasicConsume(queue: "product-price-changes", autoAck: true, consumer: consumer);
        }

        private void ConsumerOnReceived(object sender, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // Deserialize the message to the appropriate event class
            var @event = JsonConvert.DeserializeObject<ProductPriceChangedIntegrationEvent>(message);

            // Handle the event
            var handler = new ProductPriceChangedIntegrationEventHandler();
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
