using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Producer
{
    public class DirectExchangePublisher
    {
        public static void Publish(IModel channel, string routingKey, string exchange = "")
        {
            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare(exchange, ExchangeType.Direct, arguments: ttl);

            CreateMultiMessages(channel, routingKey, exchange);
            Console.ReadLine();
        }

        private static void CreateMessage(IModel channel, string routingKey, string exchange = "", int count = 0)
        {
            var message = new { Name = "Producer", Message = "Hello", Count = count };
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish(exchange, routingKey, null, body);
        }
        private static void CreateMultiMessages(IModel channel, string routingKey, string exchange = "")
        {
            var count = 0;

            while (count < 10)
            {
                CreateMessage(channel, routingKey, exchange, count);
                count++;
                Thread.Sleep(5000);
            }
        }
    }
}
