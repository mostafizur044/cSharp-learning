using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Producer
{
    public class FanoutExchangePublisher
    {
        public static void Publish(IModel channel, string exchange, string rouingKey)
        {
            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare(exchange, ExchangeType.Fanout, arguments: ttl);

            CreateMultiMessages(channel, exchange, rouingKey);
            Console.ReadLine();
        }

        private static void CreateMessage(IModel channel, string exchange, string rouingKey, int count)
        {
            var message = new { Name = "Producer", Exchange = "Fanout", Count = count };
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish(exchange, rouingKey, null, body);
        }
        private static void CreateMultiMessages(IModel channel, string exchange, string rouingKey)
        {
            var count = 0;

            while (count < 10)
            {
                CreateMessage(channel, exchange, rouingKey, count);
                count++;
                Thread.Sleep(5000);
            }
        }
    }
}
