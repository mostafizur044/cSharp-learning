using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Producer
{
    public class QueueProducer
    {
        public static void Publish(IModel channel, string queue)
        {
            channel.QueueDeclare(queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            CreateMultiMessages(channel, queue);
            Console.ReadLine();
        }

        private static void CreateMessage(IModel channel, string routingKey, int count)
        {
            var message = new { Name = "Producer", Message = "Hello", Count = count };
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish("", routingKey, null, body);
        }
        private static void CreateMultiMessages(IModel channel, string routingKey)
        {
            var count = 0;

            while(count < 10)
            {
                CreateMessage(channel, routingKey, count);
                count++;
                Thread.Sleep(5000);
            }
        }
    }
}
