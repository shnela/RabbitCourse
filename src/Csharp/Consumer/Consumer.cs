﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


class Consumer
{
    private const string QueueName = "q.test1.from_code";

    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: QueueName,
                                 durable: false,
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
                Console.WriteLine(" * Received {0}", message);
            };

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
