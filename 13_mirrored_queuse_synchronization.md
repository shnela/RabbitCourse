# Unsynchronised Mirrors
[docs](https://www.rabbitmq.com/ha.html#unsynchronised-mirrors)

## Check synchronization
* Create policy for `Q1`
    * `rabbitmqctl set_policy mirroringQ1 "Q1" '{"ha-mode": "exactly", "ha-params": 2}'`
    * it'll keep only one mirror for queue
    * make sure that it's only one policy: `rabbitmqctl list_policies`
* publish some messages (still being on `rmq1`) to `Q1` using default exchange
    * `rabbitmqadmin publish routing_key=Q1 payload=message1`
    * `rabbitmqadmin list queues name messages node`
* Update policy for `Q1`
    * `rabbitmqctl set_policy mirroringQ1 "Q1" '{"ha-mode": "all"}'`
    * Now all nodes will keep copy of the queue
* Check synchronization status:
    * `rabbitmqctl list_queues name slave_pids synchronised_slave_pids`
    * New node doesn't contain new message
    * Sync queue with: `rabbitmqctl sync_queue Q1`
* You can enable auto synchronization with option
    * `ha-sync-mode=automatic`
