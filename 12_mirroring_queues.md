# Classic Queue Mirroring 
[docs mirroring](https://www.rabbitmq.com/ha.html)
[docs policies](https://www.rabbitmq.com/parameters.html#policies)

**Queue mirroring is deprecated method of keeping HA, since RMQ3.8 Quorum queues are available.**

## Create policy for Q1 in admin UI
Name: mirroringQ1
Pattern: Q1
ha-mode: all

Now all **new** messages are forwarded to mirror queue as well.

## Create policy using commandline
```bash
rabbitmqctl set_policy mirroringQ1 "Q1" '{"ha-mode": "exactly", "ha-params": 2}'
```

## Master node promotion

* start cluster with `docker-compose`
* go to any node `docker exec -it rmq1 /bin/bash`
* create queues `Q1` and `Q2` in nodes 1 and 2
    * `rabbitmqadmin declare queue name=Q1 node=rabbit@rmq1`
    * `rabbitmqadmin declare queue name=Q2 node=rabbit@rmq2`
    * `rabbitmqadmin list queues name messages node`
    * `rabbitmqadmin list bindings`
* publish some messages (still being on `rmq1`) to `Q1` and `Q2` using default exchange
    * `rabbitmqadmin publish routing_key=Q1 payload=message1`
    * `rabbitmqadmin publish routing_key=Q2 payload=message2`
    * `rabbitmqadmin list queues name messages node`
* turn application on `rmq2` off: `rabbitmqctl -n rabbit@rmq2 stop_app`
* check queues
    * `rabbitmqadmin list queues name messages node`
    * now `Q2` is moved to other node
* consume messages (still being on `rmq1`) from `Q2`
    * `rabbitmqadmin get queue=Q2 ackmode=ack_requeue_false`
    * you can't consume from unavailable node
* turn application on `rmq2` on: `rabbitmqctl -n rabbit@rmq2 start_app`
