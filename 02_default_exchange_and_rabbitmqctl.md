# Using default exchange and rabbitmqctl

## What we'll do
* declare `queue`
* send and receive messages using `Management UI` beeing aware of `default exchange`
* analyze queue statistics using `rabbitmqctl`


## Tasks

### In UI (RabbitMQ Management)
1. Create two queues: `q.default1`, `q.default2`
1. Send some message to any of them with `default exchange`
1. Read messages

### Analyze node status with commandline
[rabbitmqctl docs](https://www.rabbitmq.com/rabbitmqctl.8.html)

1. `rabbitmqctl list_queues`
1. `rabbitmqctl list_exchanges`
1. `rabbitmqctl list_bindings`
