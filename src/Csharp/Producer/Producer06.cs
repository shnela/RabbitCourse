using RabbitMQ.Client;
using System.Text;


class Producer06
{
    private const string QueueName = "q.test1.from_code";

    public static void Main()
    {
        var factory = new ConnectionFactory() {
            HostName = "localhost",
            Port = 5672,
            ClientProvidedName = "consumer_connection"
            };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: QueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false);

            string message = "Hello World! " + DateTime.Now;

            while (true) {
                var body = Encoding.UTF8.GetBytes(message + DateTime.Now);
                channel.BasicPublish(exchange: "",
                                    routingKey: QueueName,
                                    basicProperties: null,
                                    body: body);
                Thread.Sleep(333);
            }
        }
    }
}
