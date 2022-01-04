#  Running several docker containers at once
[docker-compose docs](hhttps://docs.docker.com/compose/)

[Config File Peer Discovery Overview](https://www.rabbitmq.com/cluster-formation.html#peer-discovery-classic-config)

## Run all three nodes at once
In the directory containing `docker-compose.yml`

```bash
docker-compose up --force-recreate --no-deps
```
