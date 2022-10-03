# Consumer Acknowledgements
[ Consumer Acknowledgements and Publisher Confirms](https://www.rabbitmq.com/confirms.html#channel-qos-prefetch-throughput)
[ Consumer Prefetch ](https://www.rabbitmq.com/consumer-prefetch.html)

## What we'll do
* ...



## Tasks
**All below tasks are based no code located in:**
* **`cd src/Csharp/Producer`**
* and **`cd src/Csharp/Consumer`**

**Make sure that `Producer` script is not running.**

### Get rid of `Consumer's` `autoAck` attribute
1. Remove `autoAck` parameter from `BasicConsume` 
2. Run script
3. Check statistics
   1. `Consumer` should get all messages, but non of them will be `Acked`
4. Stop script
5. Check statistics
   1. All messages should be marked again as `Ready`
6. Ack every message manually using `channel.BasicAck(ea.DeliveryTag, false);`
7. Start script
8. Check statistics
   1. Finally the rate of `Acked` messages should increase
9. Set global `prefetchCount` on whole `channel`
   1. `channel.BasicQos(0, 10, global: false);`
10. Retart script
11. Check statistics
   1. Number of `unacked` messages should be equal to `10`
   2. There should be plenty of `Ready` messages for other `Consumers`
12. Start additionall two consumers
13. And... check statistics
    1.  We've finally started to catch up with the traffic
