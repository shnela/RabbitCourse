# HAProxy

[Docker image information.](https://hub.docker.com/_/haproxy/)

## HAProxy docs
[docs](https://cbonte.github.io/haproxy-dconv/)

Run:
```bash
cd ./haproxy
docker build -t my-haproxy .
docker run --net cluster_rabbits -p 5672:5672 -p 8404:8404 -d --name my-running-haproxy --sysctl net.ipv4.ip_unprivileged_port_start=0 my-haproxy
```
