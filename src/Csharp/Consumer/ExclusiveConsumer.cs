using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class ExclusiveConsumer
{
    private const string ExchangeName = "ex.code.fanout";

    public static void Main(string[] args)
    {
        int msgs_per_second = Int32.Parse(args[0]);
        consume(msgs_per_second);
    }

    public static void consume(int msgs_per_second)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            ClientProvidedName = "exclusive_consumer_connection"
        };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            var declaredQueue = channel.QueueDeclare(
                queue: "",
                durable: false,
                exclusive: true,
                autoDelete: false
            );

            channel.QueueBind(
                queue: declaredQueue.QueueName,
                exchange: ExchangeName,
                routingKey: ""
            );

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("[{0}] * Received {1}", DateTime.Now.TimeOfDay, message);
                Thread.Sleep(1000 / msgs_per_second);
            };
            channel.BasicConsume(queue: declaredQueue.QueueName, autoAck: true, consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
