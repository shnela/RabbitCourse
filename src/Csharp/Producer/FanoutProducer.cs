using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
                    body: body,
                    mandatory: true
                );
                Console.WriteLine(" * Sent {0}", message);
                Thread.Sleep(1000 / msgs_per_second);
            }
        }
    }

    public static void produce_with_confirm(int msgs_per_second)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            ClientProvidedName = "producer_with_confirm_connection"
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
            channel.ConfirmSelect();
            channel.BasicReturn += handler;
            channel.BasicAcks += ack;
            channel.BasicNacks += nack;

            while (true)
            {
                string message = "Hello World! " + DateTime.Now.TimeOfDay;
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(
                    exchange: ExchangeName,
                    routingKey: "",
                    basicProperties: null,
                    body: body,
                    mandatory: true // with this field basic.return response is expected before ack
                );
                Console.WriteLine(" * Sent {0}", message);
                Thread.Sleep(1000 / msgs_per_second);
            }
        }
    }

    private static void handler(object? sender, BasicReturnEventArgs args) {
        /* callback called before ack/nack when message with `mandatory` property is sent */
        Console.WriteLine("handler");
        Console.WriteLine(args.ReplyText);
    }
    private static void ack(object? sender, BasicAckEventArgs args) {
        /* callback for ack'ed messages */
        Console.WriteLine("ack");
    }
    private static void nack(object? sender, BasicNackEventArgs args) {
        /* callback for nack'ed messages - only RMQ internal failures (not for unrouted messages) */
        Console.WriteLine("nack");
    }
}
