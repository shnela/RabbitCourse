# Publisher Confirms

As a publisher we want to know whether the message was safely processed by RabbitMQ broker.
E.g. if there was no error and message is lost or if it was succesfully routed.

If we're using `rabbitmq-management` (WEB UI or HTTP API) we're geting response with `routed` key - information if message was succesfully routed or not.

`AMQP-0.9.1` has no option but to effectively track published messages, but `RabbitMQ` has `Publisher Confirms (aka Publisher Acknowledgements)` [extension](https://www.rabbitmq.com/extensions.html).


## Prerequisites
* [HTTP_API.md](./XX_HTTP_API.md)
  * publishing messages returns information whether message was published or not
* `FanoutProducer.cs`
  * we've published messages, but lot of them was not routed unless there was exclusive consumer bound


## Docs
[Publisher Confirms](https://www.rabbitmq.com/confirms.html#publisher-confirms)

`PublisherConfirms` has to be turned on on channel manually.

> To enable confirms, a client sends the confirm.select method. 
> [...] 
> Once the confirm.select method is used on a channel, it is said to be in confirm mode.

Once `PublisherConfrms` is activated every message will be `acked` or `nacked`

> In exceptional cases when the broker is unable to handle messages successfully, instead of a basic.ack, the broker will send a basic.nack. 
> [...] 
> 
> After a channel is put into confirm mode, all subsequently published messages will be confirmed or nack'd once. No guarantees are made as to how soon a message is confirmed. No message will be both confirmed and nack'd.

`basic.nack` will be send only if there's some serious internal `RabbitMQ` borker error.

> basic.nack will only be delivered if an internal error occurs in the Erlang process responsible for a queue.

To monitor if message was `routed` you'll have to publish message as `mandatory`.

> For unroutable messages, the broker will issue a confirm once the exchange verifies a message won't route to any queue (returns an empty list of queues). If the message is also published as mandatory, the basic.return is sent to the client before basic.ack. The same is true for negative acknowledgements (basic.nack).
