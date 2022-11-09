# [Dead Letter Exchanges](https://www.rabbitmq.com/dlx.html)

> Messages from a queue can be "dead-lettered"; that is, republished to an exchange when any of the following events occur:
> * The message is negatively acknowledged by a consumer using basic.reject or basic.nack with requeue parameter set to false.
> * The message expires due to per-message TTL; or
> * The message is dropped because its queue exceeded a length limit


## Configuration Using a Policy

### Example using `rabbitmqctl` for creating policy
```bash
rabbitmqctl set_policy DLX ".*" '{"dead-letter-exchange":"my-dlx"}' --apply-to queues
```

### Example in C# for one queue only
```C#
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
```

## TODO

### Run `DeadletterConsumer` code.
So you'll have:
* `ex.code.deadletter` exchange
* `q.code.deadletter` bound to with `ex.code.deadletter` `"dropped"` `routingKey`.

The `DeadletterConsumer` will also consume messages and print information about them:
* messages shorter or equal to two characters are `acked`
* messages longer than two characters will be `rejected` wihtout requeueing
  * we expect to see them collected in `q.code.deadletter`

### Create your own queues and test other situations when message can be republished to DLX
* Create `q.manual.with_timeout` queue. Make the `queue` to store every message for less than 10000ms.
  * Use `x-message-ttl` argument when creating a queue
  * We expect that after 10s message published to that queue will be forwarded to DLX
* Create `q.manual.with_limit` queue which will accept up to 3 messages
  * Use `x-max-length` argument when creating a queue
  * We expect that after publishing 4th message, the first one will be forwarded to DLX

Additional notes:
* We're using exchange and queue defined in `DeadletterConsumer` script
* Remember that every queue you've created will need `x-dead-letter-exchange` and `x-dead-letter-routing-key` arguments, unless you defined a policy.
