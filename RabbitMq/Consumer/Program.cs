using Consumer;
using RabbitMQ.Client;

var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

//QueueConsumer.Consume(channel, "demo-queue");
//DirectExchangeConsumer.Consume(channel, "demo-direct-exchange", "account.init", "demo-direct-queue");
//TopicExchangeConsumer.Consume(channel, "demo-topic-exchange", "account.*", "demo-topic-queue");

//var header = new Dictionary<string, object> { { "account", "new" } };
//HeaderExchangeConsumer.Consume(channel, "demo-header-exchange", "demo-header-queue", header);

FanoutExchangeConsumer.Consume(channel, "demo-fanout-exchange", "demo-fanout-queue", string.Empty);