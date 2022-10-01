# Running RMQ with Docker

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

## Tasks

1. Create a queue
2. Send some messages to it
3. Read messages
