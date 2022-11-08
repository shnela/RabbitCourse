using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class DeadletterConsumer
{
    private const string QueueName = "q.code.queue_to_drop_messages";
    private const string DLXExchangeName = "ex.code.deadletter";
    private const string DLXQueueName = "q.code.deadletter";

    public static void Main(string[] args)
    {
        int msgs_per_second = Int32.Parse(args[0]);

        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            ClientProvidedName = "drop_consumer_connection"
        };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            consume(channel, msgs_per_second);
        }
    }

    public static void consume(IModel channel, int msgs_per_second)
    {
        /* declare DLX Exchange */
        channel.ExchangeDeclare(
            exchange: DLXExchangeName,
            type: "direct",
            durable: true
        );
        /* create DLX Queue */
        channel.QueueDeclare(
            queue: DLXQueueName,
            durable: true,
            exclusive: false
        );
        /* bind DLX Queue to Exchange */
        channel.QueueBind(DLXQueueName, DLXExchangeName, routingKey: "dropped");


        /* declare queue to consume messages with DLX Exchange attached */
        IDictionary<string, object> arguments = new Dictionary<string, object>();
        arguments.Add("x-dead-letter-exchange", DLXExchangeName);
        arguments.Add("x-dead-letter-routing-key", "dropped");
        
        var declaredQueue = channel.QueueDeclare(
            queue: QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: arguments
        );

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("[{0}] * Received {1}", DateTime.Now.TimeOfDay, message);
            Thread.Sleep(1000 / msgs_per_second);

            if(body.Length > 2) {
                Console.WriteLine("Will drop");
                channel.BasicReject(ea.DeliveryTag, requeue: false);
            } else {
                Console.WriteLine("Accept");
                channel.BasicAck(ea.DeliveryTag, multiple: false);
            }
        };
        channel.BasicConsume(queue: declaredQueue.QueueName, autoAck: false, consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}
