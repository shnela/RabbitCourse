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


## Proxy Protocol


[Use the Proxy Protocol to Preserve a Client’s IP Address](https://www.haproxy.com/blog/use-the-proxy-protocol-to-preserve-a-clients-ip-address/)

> Some higher-level protocols, such as HTTP, have a solution for this. With HTTP, a proxy can add a Forwarded HTTP header, or the non-standard X-Forwarded-For header, to store the client’s original IP address so that the server can retrieve it, but other protocols lack a similar fix. The Proxy Protocol, which operates beneath at the TCP layer, fills this gap, expanding coverage to any upper layer protocol—SMTP, IMAP, FTP, the Minecraft protocol, proprietary database protocols, etc.—that transmits messages over TCP/IP. The caveat is that both the proxy and the server on the receiving end must support it.

[Support of ProxyProtocol in RMQ](https://www.rabbitmq.com/networking.html#proxy-protocol)

> When proxy protocol is enabled, clients won't be able to connect to RabbitMQ directly unless they themselves support the protocol. Therefore, when this option is enabled, all client connections must go through a proxy that also supports the protocol and is configured to send a Proxy protocol header.

### Update `haproxy.cfg`
```
server s1 rmq1:5672 check send-proxy
server s2 rmq2:5672 check send-proxy
server s3 rmq3:5672 check send-proxy
```

### Update every `rabbitmq.conf`
```
proxy_protocol = true
```
