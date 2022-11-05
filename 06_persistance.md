# Persistance

## What we'll do
* using `Management UI` we'll
  * declare `durable` and `transient` queues
  * send messages to those queues
  * check what happened after node restart


## How to simulate application failure

### First go to node container
```bash
docker exec -it rabbitmq /bin/bash
```

### In container stop and start RabbitMQ application
```bash
rabbitmqctl stop_app
rabbitmqctl start_app
```

## Tasks

1. Declare new queues:
   1. `q.test_durability.durable1` - durable
   2. `q.test_durability.transient` - transient
2. For every queue send two messages:
   1. One in `Persistent` mode
   2. One in `Non-persistent` mode
3. Check number of messages in each queue
   1. Should be equall to 2
4. Restart RabbitMQ application
5. Check number of messages in each queue
   1. There should be no `q.test_durability.transient` queue
   2. There should be only **1** message in `q.test_durability.durable1`


### Bonus
Repeat above actions in `Producer`.
