using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class WeatherProducer
{
    private const string ExchangeName = "ex.code.weather";

    public static void Main(string[] args)
    {
        produce_weather();
    }

    public static void produce_weather()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            ClientProvidedName = "producer_connection"
        };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(
                exchange: ExchangeName,
                type: "topic",
                durable: true,
                autoDelete: false
            );

            var to_publish = new Dictionary<string, string>();
            to_publish.Add("temperature.poland.warsaw", "25");
            to_publish.Add("precipitation.poland.warsaw", "high");
            to_publish.Add("temperature.germany.berlin", "21");
            to_publish.Add("precipitation.germany.berlin", "low");
            to_publish.Add("temperature.germany.munch", "26");
            to_publish.Add("precipitation.germany.munch", "low");
            foreach(var pair in to_publish)
            {
                string routingKey = pair.Key;
                string message = pair.Value;
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(
                    exchange: ExchangeName,
                    routingKey: routingKey,
                    basicProperties: null,
                    body: body,
                    mandatory: true
                );
                Console.WriteLine(" * Sent {0}={1}", routingKey, message);
            }
        }
    }
}
