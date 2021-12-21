# Using default exchange and rabbitmqctl

[Docker image information.](https://hub.docker.com/_/rabbitmq/)

## Downloading and Installing RabbitMQ
[docs](https://www.rabbitmq.com/download.html)

Run:
```
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.9-management
```

## Management Plugin
[docs](https://www.rabbitmq.com/management.html)

Go to http://127.0.0.1:15672

Creadentials: guest/guest

## Exec commands in RMQ node
```
docker exec -it rabbitmq /bin/bash
```

## Tasks

1. Create two queues: `test1`, `test2`
1. Send some message to any of them with `default exchange`
1. Read messages

### Analyze node status with commandline
[rabbitmqctl docs](https://www.rabbitmq.com/rabbitmqctl.8.html)

1. `rabbitmqctl list_queues`
1. `rabbitmqctl list_exchanges`
1. `rabbitmqctl list_bindings`

