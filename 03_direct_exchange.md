# Using direct exchange and rabbitmqctl

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

1. Create three queues: `img_archiver1`, `img_archiver2` and `img_resize`
1. Create exchange `img`
   1. Add routing_key:`img.archive` to both archiver queues
   1. Add routing_key:`img.resize` to resize queue
1. Send some messages with `img.archive`, `img.resize` and some invalid routing_keys.
1. List some stats with `rabbitmqctl`
   1. `rabbitmqctl list_queues`
   1. `rabbitmqctl list_exchanges`
   1. `rabbitmqctl list_bindings`

### Manage node elements with `rabbitmqadmin`
[rabbitmqadmin docs](https://www.rabbitmq.com/management-cli.html)

1. `rabbitmqctl` alternatives
   1. `rabbitmqadmin list queues`
   1. `rabbitmqadmin list exchanges`
   1. `rabbitmqadmin list bindings`
1. `rabbitmqadmin help subcommands`
1. Declare `new_images` exchange and `new_archive`, `new_resize` queues with `rabbitmqadmin`
