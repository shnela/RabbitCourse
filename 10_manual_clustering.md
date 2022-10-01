#  Cluster Formation and Peer Discovery
[docs](https://www.rabbitmq.com/cluster-formation.html)

## Make sure that there are no running nodes
```bash
docker ps -a # should return empty list if not run following
docker rm -f `docker ps -aq`
```

## Docker containers must communicate with each other
[docs](https://docs.docker.com/network/#network-drivers)
```bash
docker network create rabbits
```

## Run three RQM nodes:
* as part of `rabbits` network
* with rabbitmqadmin UI available on following ports: `8081`, `8082` and `8083`.
* with the same erlang cookie
* with given hostname: `rmq1`, `rmq2`, `rmq3`

```bash
docker run -d --rm --net rabbits -p 8081:15672 -e RABBITMQ_ERLANG_COOKIE=ABCDEFGH --hostname rmq1 --name rmq1 rabbitmq:3.10-management
docker run -d --rm --net rabbits -p 8082:15672 -e RABBITMQ_ERLANG_COOKIE=ABCDEFGH --hostname rmq2 --name rmq2 rabbitmq:3.10-management
docker run -d --rm --net rabbits -p 8083:15672 -e RABBITMQ_ERLANG_COOKIE=ABCDEFGH --hostname rmq3 --name rmq3 rabbitmq:3.10-management
```

### Analyze standalone node status

#### In management UI
* http://127.0.0.1:8081/#/
* http://127.0.0.1:8082/#/
* http://127.0.0.1:8083/#/

#### Manually in node
```bash
docker exec -it rmq1 /bin/bash
rabbitmqctl cluster_status
```

## Make `rmq2` and `rmq3` join `rmq1` as common cluster
[docs](https://www.rabbitmq.com/clustering.html#creating)

On nodes 2 and three:
```bash
rabbitmqctl stop_app
rabbitmqctl reset
rabbitmqctl join_cluster rabbit@rmq1
rabbitmqctl start_app
rabbitmqctl cluster_status
```

### How to remove node from cluster
```bash
rabbitmqctl -n rabbit@rmq3 stop_app
rabbitmqctl forget_cluster_node rabbit@rmq3
```

## TODO
* Remove all exisintg nodes with: `docker rm -f $(docker ps -aq)`
* Create cluster of three nodes: `rmq{1,2,3}
* Create some exchanges and queues in cluster
    * verify that exchanges and queues are available from all nodes
* Create new standalone node `rmq4` and create some exchanges and queues in it.
* Try to add `rmq4` to cluster and check which exchanges / queues survived.
* Kick out `rmq4` from cluster with `rabbitmqctl forget_cluster_node rabbit@rmq4`
    * to forget node from cluster it must be shut down
    * after removing cluster if you try to add it again it won't be able to join since it'll assume that is part of cluster
    * try to re-join `rmq3` node to cluster
  