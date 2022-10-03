# Using direct exchange and rabbitmqadmin

## What we'll do
* using `Management UI` we'll
  * declare `direct exchange`
  * declare `queues` and bind them to created `direct exchange`
  * then we'll send some messages through newly created `direct exchange`
* analyze queue statistics using `rabbitmqctl` 
* then repeat most of above steps using `rabbitmqadmin`
  * sending and receiving messages will still be done in `Management UI`


## Tasks

1. Create three queues: `q.img_archiver1`, `q.img_archiver2` and `q.img_resize`
1. Create exchange `ex.img`
   1. Add routing_key:`img.archive` to both archiver queues
   1. Add routing_key:`img.resize` to resize queue
1. Send some messages with `img.archive`, `img.resize` and some invalid routing_keys.
1. List some stats with `rabbitmqctl`
   1. `rabbitmqctl list_queues`
   1. `rabbitmqctl list_exchanges`
   1. `rabbitmqctl list_bindings`

### Manage node elements with `rabbitmqadmin`
[rabbitmqadmin docs](https://www.rabbitmq.com/management-cli.html)

1. `rabbitmqctl` alternatives
   1. `rabbitmqadmin list queues`
   1. `rabbitmqadmin list exchanges`
   1. `rabbitmqadmin list bindings`
1. `rabbitmqadmin help subcommands`
1. Declare `ex.new_images` exchange and `q.new_archive`, `q.new_resize` queues with `rabbitmqadmin`
