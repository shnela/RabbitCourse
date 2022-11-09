# [Shovel plugin](https://rabbitmq.com/shovel.html)

## Requirements
* `rabbitmq_shovel` plugin should be enabled
* `rabbitmq_shovel_management` would be helpfull

We've enabled both in broker config file.

## Example
Assume we have a warehouse with network.
In warehouse there's a RMQ broker with `q.deliveries` queue tracking packages which were shiped from the wharehouse.

We want to process this data in separate broker located in different network.
We also would like the messages to be stored in `q.deliveries`
We may create a `shovel` which will be moving messages from one broker to another and handling eventual network problems for us.


### TODO
You have one rmq cluster and additional `rmq_separate` node.
Craete shovel which will be responsible for forwarding messages from `q.deliveries` on `rmq_separate` to `q.deliveries` on cluster.

Make sure to use encoded `%2F` when you decide to use `/` vhost.
