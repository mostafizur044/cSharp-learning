using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Producer
{
    public class HeaderExchangePublisher
    {
        public static void Publish(IModel channel, string exchange, Dictionary<string, object> header)
        {
            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare(exchange, ExchangeType.Headers, arguments: ttl);

            CreateMultiMessages(channel, exchange, header);
            Console.ReadLine();
        }

        private static void CreateMessage(IModel channel, string exchange, Dictionary<string, object> header, int count)
        {
            var message = new { Name = "Producer", Exchange = "header", Count = count };
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            var properties = channel.CreateBasicProperties();
            properties.Headers = header;
            channel.BasicPublish(exchange, string.Empty, properties, body);
        }
        private static void CreateMultiMessages(IModel channel, string exchange, Dictionary<string, object> header)
        {
            var count = 0;

            while (count < 10)
            {
                CreateMessage(channel, exchange, header, count);
                count++;
                Thread.Sleep(5000);
            }
        }
    }
}
