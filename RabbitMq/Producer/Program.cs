using Producer;
using RabbitMQ.Client;

var factory = new ConnectionFactory 
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

//QueueProducer.Publish(channel, "demo-queue");
//DirectExchangePublisher.Publish(channel, "account.init", "demo-direct-exchange");
//TopicExchangePublisher.Publish(channel, "account.update", "demo-topic-exchange");

//var header = new Dictionary<string, object> { { "account", "new" } };
//HeaderExchangePublisher.Publish(channel, "demo-header-exchange", header);

FanoutExchangePublisher.Publish(channel, "demo-fanout-exchange", "account.update");