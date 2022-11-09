# MQTT protocol

## The Quorum Requirement

> As of 3.8, the plugin requires a quorum of cluster nodes to be present. This means two nodes out of three, three out of five and so on.\
> The plugin can also be used on a single node but does not support clusters of two nodes.\
> In case the majority of cluster nodes is down, remaining cluster nodes would not be able to accept new MQTT client connections.

## Enable the plugin
In node:
```bash
rabbitmq-plugins enable rabbitmq_mqtt
```
In our case we've automatically enabled `rabbimt_mqtt` plugin for all nodes in their settings.


## Create user
By default `guest` user can be used only on localhost connections.

```bash
# username and password are both "mqtt-test"
rabbitmqctl add_user mqtt-test mqtt-test
rabbitmqctl set_permissions -p / mqtt-test ".*" ".*" ".*"
rabbitmqctl set_user_tags mqtt-test management
```