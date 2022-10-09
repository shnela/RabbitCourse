using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


class SimpleConsumer
{
    private const string QueueName = "q.code.simple";

    public static void Main(string[] args)
    {
        int msgs_per_second = Int32.Parse(args[0]);
        consume(msgs_per_second);
    }
    
    public static void consume(int msgs_per_second)
    {
        var factory = new ConnectionFactory() {
            HostName = "localhost",
            Port = 5672,
            ClientProvidedName = "consumer_connection"
            };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: QueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false);

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: QueueName,
                                 autoAck: true,
                                 consumer: consumer);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("[{0}] * Received {1}", DateTime.Now.TimeOfDay, message);
                Thread.Sleep(1000 / msgs_per_second);
            };

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
    
    public static void consume_manual_ack(int msgs_per_second)
    {
        var factory = new ConnectionFactory() {
            HostName = "localhost",
            Port = 5672,
            ClientProvidedName = "consumer_connection"
            };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            // https://www.rabbitmq.com/dotnet-api-guide.html#passive-declaration
            channel.QueueDeclarePassive(queue: QueueName);

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: QueueName,
                                 autoAck: false,
                                 consumer: consumer);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("[{0}] * Received {1}", DateTime.Now.TimeOfDay, message);
                Thread.Sleep(1000 / msgs_per_second);
                // channel.BasicAck(ea.DeliveryTag, false); // Uncomment this line, otherwise messages won't be sent
            };

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
    
    public static void consume_manual_ack_with_prefetch_count(int msgs_per_second)
    {
        var factory = new ConnectionFactory() {
            HostName = "localhost",
            Port = 5672,
            ClientProvidedName = "consumer_connection"
            };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclarePassive(queue: QueueName);
            channel.BasicQos(0, prefetchCount: 10, global: true);

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: QueueName,
                                 autoAck: false,
                                 consumer: consumer);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" * Received {0}", message);
                Thread.Sleep(1000 / msgs_per_second);
                channel.BasicAck(ea.DeliveryTag, false);
            };

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
