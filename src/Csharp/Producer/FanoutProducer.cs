using RabbitMQ.Client;
using System.Text;

class FanoutProducer
{
    private const string ExchangeName = "ex.code.fanout";

    public static void Main(string[] args)
    {
        int msgs_per_second = Int32.Parse(args[0]);
        produce(msgs_per_second);
    }

    public static void produce(int msgs_per_second)
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
                type: "fanout",
                durable: true,
                autoDelete: false
            );

            while (true)
            {
                string message = "Hello World! " + DateTime.Now.TimeOfDay;
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(
                    exchange: ExchangeName,
                    routingKey: "",
                    basicProperties: null,
                    body: body
                );
                Console.WriteLine(" * Sent {0}", message);
                Thread.Sleep(1000 / msgs_per_second);
            }
        }
    }
}
