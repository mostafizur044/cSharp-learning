using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumer
{
    public class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel, string exchange, string queue, Dictionary<string, object> header)
        {
            channel.ExchangeDeclare(exchange, ExchangeType.Headers);
            channel.QueueDeclare(queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueBind(queue, exchange, string.Empty, header);
            channel.BasicQos(0, 5, false);

            ConsumeMessage(channel, queue);
        }

        private static void ConsumeMessage(IModel channel, string queue)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(queue, true, consumer);
            Console.ReadLine();
        }
    }
}
