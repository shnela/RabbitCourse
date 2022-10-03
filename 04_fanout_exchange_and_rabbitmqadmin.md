# Using fanout exchange and rabbitmqadmin

## What we'll do
* using `rabbitmqadmin` we'll
  * declare `fanout exchange`
  * declare `queues` and bind them to created `fanout exchange`
  * then we'll send some messages through newly created `fanout exchange`


## Tasks
Using `rabbitmqadmin` (`rabbitmqadmin help subcommands`)

1. Create three queues: `q.broadcast1`, `q.broadcast2` (`rabbitmqadmin declare queue`)
1. Create fanout exchange `ex.weather` and bind it  to both queues (`rabbitmqadmin declare exchange`, `rabbitmqadmin declare binding`)
1. Send some messages to `ex.weather` exchagne.
1. Check where they appear.


### publish/consume with `rabbitmqadmin`
[rabbitmqadmin docs](https://www.rabbitmq.com/management-cli.html)

1. `rabbitmqadmin help subcommands`
1. `rabbitmqadmin publish exchange=ex.weather routing_key="" payload=foo`
1. `rabbitmqadmin get queue=q.broadcast1`
