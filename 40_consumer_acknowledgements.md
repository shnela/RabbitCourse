# Consumer Acknowledgements

As a consumer we want to inform broker that message was succesfully processed.
In case of an error on cunsumer side, broker should re-send message again to any available consumer.


## Prerequisites
* `SimpleConsumer.cs`
  * we've published messages, but lot of them was not routed unless there was exclusive consumer bound

## Methods of informing the broker about processed message
[Consumer Acknowledgements](https://www.rabbitmq.com/confirms.html#publisher-confirms)

> Depending on the acknowledgement mode used, RabbitMQ can consider a message to be successfully delivered either immediately after it is sent out (written to a TCP socket) or when an explicit ("manual") client acknowledgement is received.

### Automatic acknowledgement mode

> In automatic acknowledgement mode, a message is considered to be successfully delivered immediately after it is sent. This mode trades off higher throughput (as long as the consumers can keep up) for reduced safety of delivery and consumer processing. This mode is often referred to as "fire-and-forget". Unlike with manual acknowledgement model, if consumers's TCP connection or channel is closed before successful delivery, the message sent by the server will be lost. Therefore, automatic message acknowledgement should be considered unsafe and not suitable for all workloads.

#### Configuring in C#
```C#
channel.BasicConsume(queue: QueueName,
                     autoAck: true,
                     consumer: consumer);
```

### Manual acknowledgement mode

> Positive acknowledgements simply instruct RabbitMQ to record a message as delivered and can be discarded. Negative acknowledgements with basic.reject have the same effect. The difference is primarily in the semantics: positive acknowledgements assume a message was successfully processed while their negative counterpart suggests that a delivery wasn't processed but still should be deleted.

* `basic.ack` is used for positive acknowledgements
* `basic.nack` is used for negative acknowledgements
* `basic.reject` is used for negative acknowledgements but has one limitation compared to `basic.nack`


#### Delivery Tag
> Delivery tags are monotonically growing positive integers and are presented as such by client libraries. Client library methods that acknowledge deliveries take a delivery tag as an argument.

#### Acknowledging Multiple Deliveries at Once
> Manual acknowledgements can be batched to reduce network traffic. This is done by setting the multiple field of acknowledgement methods to true. 


#### Negative Acknowledgement and Requeuing of Deliveries
> The methods are generally used to negatively acknowledge a delivery. Such deliveries can be discarded or dead-lettered or requeued by the broker. This behaviour is controlled by the requeue field. When the field is set to true, the broker will requeue the delivery with the specified delivery tag. Alternatively, when this field is set to false, the message will be routed to a Dead Letter Exchange if it is configured, otherwise it will be discarded.

> When a message is requeued, it will be placed to its original position in its queue, if possible. If not (due to concurrent deliveries and acknowledgements from other consumers when multiple consumers share a queue), the message will be requeued to a position closer to queue head.

#### Configuring and confirming in C#
```C#
channel.BasicConsume(queue: QueueName,
                     autoAck: false,
                     consumer: consumer);


/* in handler code */
channel.BasicAck(ea.DeliveryTag, false);  // ack delivery of message with `ea.DeliveryTag`
/* or */
channel.BasicAck(ea.DeliveryTag, true);  // ack delivery of message with `ea.DeliveryTag` and less

/* in case of signaling failure */
hannel.BasicNack(ea.DeliveryTag, false, true); // nack delivery and set message for re-queue again
```

#### Detecing re-queued messages
> Redeliveries will have a special boolean property, redeliver, set to true by RabbitMQ. For first time deliveries it will be set to false.

