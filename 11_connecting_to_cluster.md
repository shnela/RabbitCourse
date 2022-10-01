#  Running several docker containers at once
[docker-compose docs](https://docs.docker.com/compose/)

[Config File Peer Discovery Overview](https://www.rabbitmq.com/cluster-formation.html#peer-discovery-classic-config)

## Run all three nodes at once
In the directory containing `docker-compose.yml`

```bash
cd ./cluster
docker-compose up --force-recreate --no-deps
```

> Your app’s network is given a name based on the “project name”, which is based on the name of the directory it lives in. You can override the project name with either the --project-name flag or the COMPOSE_PROJECT_NAME environment variable.

So newly created network will be called `cluster_rabbits`.
You can check it with `docker network ls` command.

## Producing and consuming using cluster
Useful help command: `rabbitmqadmin help subcommands`

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
* consume messages (still being on `rmq1`) from `Q1` and `Q2`
    * `rabbitmqadmin get queue=Q1 ackmode=ack_requeue_false`
    * `rabbitmqadmin get queue=Q2 ackmode=ack_requeue_false`


## Problems with unavialiable nodes
* With the same queues `Q1` and `Q2`
* publish some message (still being on `rmq1`) to `Q2` using default exchange
    * `rabbitmqadmin publish routing_key=Q2 payload=message2`
    * `rabbitmqadmin list queues name messages node`
* turn application on `rmq2` off: `rabbitmqctl -n rabbit@rmq2 stop_app`
* consume messages (still being on `rmq1`) from `Q2`
    * `rabbitmqadmin get queue=Q2 ackmode=ack_requeue_false`
    * you can't consume from unavailable node
* turn application on `rmq2` on: `rabbitmqctl -n rabbit@rmq2 start_app`
