# Docker troubleshooting

## Stop and remove docker containers

### Particular
```
docker rm -f <docker name>
```

### All
```
docker ps -aq | xargs docker stop | xargs docker rm
```