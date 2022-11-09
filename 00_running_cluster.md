# Running cluster

## Start three RMQ nodes as a cluster
In `./cluster` catalog you'll find:
* Config of each node is stored in `./cluster/config/*`
* `docker-compose.yml` file with run commands of every of the three nodes.

### Run in terminal
```bash
# go to ./cluster catalog
cd ./cluster
# run all nodes
docker-compose up --force-recreate --no-deps  
```

### What you'll have
Three nodes (`rmq1`, `rmq2` and `rmq3`) forming cluster:
* They will be listening for `AMQP-0.9.1` protocol on ports:
  * 5001
  * 5002
  * 5003
* They will be listening for `HTTP Management UI` on ports:
  * 8081
  * 8082
  * 8083
* They will be listening for `MQTT` connections on ports:
  * 8881
  * 8882
  * 8883

Separate `rmq_separate` node with:
* 5004 - `AMQP-0.9.1` protocol port
* 8084 - `HTTP Management UI` port
* 8884 - `MQTT` protocol port


## Starting HAProxy node
In `./haproxy` catalog you'll find:
* `haproxy.cfg` config file
* `Dockerfile` whith instructions to build docker image

### Run in terminal
```bash
# go to ./haproxy catalog
cd ./haproxy
# create `my-proxy` image
docker build -t my-haproxy .
# run image
docker run --net cluster_rabbits -p 5672:5672 -p 8404:8404 -d --name my-running-haproxy --sysctl net.ipv4.ip_unprivileged_port_start=0 my-haproxy
```

### What you'll have
`HAProxy` node with:
* HaProxy UI under port `8404`
* Proxy forwarding port `5672` to one of three RMQ nodes
