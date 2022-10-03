# Running RMQ with Docker

## What we'll do
* introduction to `docker`
* run one `RabbitMQ` node
* get familiar with `Management UI`
* declare first `queue`
* send and receive first message using `Management UI`
* analyze queue statistics using `Management UI`


## Downloading and Installing RabbitMQ
[Downloading and Installing RabbitMQ ](https://www.rabbitmq.com/download.html)
[Docker image information.](https://hub.docker.com/_/rabbitmq/)

Run:
```
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.10-management
```

## Management Plugin
[docs](https://www.rabbitmq.com/management.html)

Go to http://127.0.0.1:15672

Creadentials: guest/guest

## Tasks

1. Create a queue `q.01_running`
1. Send some messages to it
1. Check if message was published to queue in UI
1. Read messages
