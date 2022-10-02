# Docker troubleshooting

## Display running docker containers
```
docker ps
```

### Display all
```
docker ps -a
```

## Stop and remove docker containers

### Particular
```
docker rm -f <docker name>
```

### All
```
docker ps -aq | xargs docker stop | xargs docker rm
```
Or
```
docker rm -f `docker ps -aq`
```
