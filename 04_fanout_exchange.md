# Using direct exchange and rabbitmqctl

[Docker image information.](https://hub.docker.com/_/rabbitmq/)

## Downloading and Installing RabbitMQ
[docs](https://www.rabbitmq.com/download.html)

Run:
```
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.10-management
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
Using `rabbitmqadmin` (`rabbitmqadmin help subcommands`)

1. Create three queues: `broadcast1`, `broadcast2` (`rabbitmqadmin declare queue`)
1. Create fanout exchange `weather` and bind it  to both queues (`rabbitmqadmin declare exchange`, `rabbitmqadmin 
1. Send some messages to `weather` exchagne.
1. Check where they appear.


### publish/consume with `rabbitmqadmin`
[rabbitmqadmin docs](https://www.rabbitmq.com/management-cli.html)

1. `rabbitmqadmin help subcommands`
1. `rabbitmqadmin publish exchange=weather topic="" payload=foo`
1. `rabbitmqadmin get queue=broadcast1`

