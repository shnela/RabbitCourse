# More about RabbitMQ.Client
[.NET/C# Client API Guide](https://rabbitmq.com/dotnet-api-guide.html)

## What we'll do
* We'll update our code to constantly send and consume messages at given rate
* We'll discuss best practices setting up connections and it's lifetime


## Main points

### [Connecting to RabbitMQ](https://rabbitmq.com/dotnet-api-guide.html#connecting)

Check default connection values.

### [Connection and Channel Lifespan](https://rabbitmq.com/dotnet-api-guide.html#connection-and-channel-lifespan)
> Connections are meant to be long-lived. The underlying protocol is designed and optimized for long running connections. That means that opening a new connection per operation, e.g. a message published, is unnecessary and strongly discouraged as it will introduce a lot of network roundtrips and overhead.
> 
> Channels are also meant to be long-lived but since many recoverable protocol errors will result in channel closure, channel lifespan could be shorter than that of its connection. Closing and opening new channels per operation is usually unnecessary but can be appropriate. When in doubt, consider reusing channels first.

#### More information
* [Sharing Channels Between Threads](https://rabbitmq.com/dotnet-api-guide.html#concurrency-channel-sharing)
* [Per-Connection Thread Use](https://rabbitmq.com/dotnet-api-guide.html#concurrency-thread-usage)

#### Simplified TLDR
* `channel` per thread
* `connection` per application


### [Client-Provided Connection Name](https://rabbitmq.com/dotnet-api-guide.html#client-provided-names)

> To make it easier to identify clients in server logs and management UI, AMQP 0-9-1 client connections, including the RabbitMQ .NET client, can provide a custom identifier. If set, the identifier will be mentioned in log entries and management UI. The identifier is known as the client-provided connection name. The name can be used to identify an application or a specific component within an application. The name is optional; however, developers are strongly encouraged to provide one as it would significantly simplify certain operational tasks.

### [Passive Declaration](https://rabbitmq.com/dotnet-api-guide.html#passive-declaration)

> Queues and exchanges can be declared "passively". A passive declare simply checks that the entity with the provided name exists. If it does, the operation is a no-op. For queues successful passive declares will return the same information as non-passive ones, namely the number of consumers and messages in ready state in the queue.

## Tasks
**All below tasks are based no code located in:**
* **`cd src/Csharp/Producer`**
* and **`cd src/Csharp/Consumer`**

**Make sure that `Consumer` and `Producer` scripts are not running.**

### Pimp up `Producer` capacity
Currently producer sends one message and ends execution.
Make it sending messages idefinetly at rate `3/s`.

_Hint: `while` loop and `Thread.Sleep` method may be usefull._

### Update `Producer's` `Client-Provided Connection Name` 
1. First look at current `connections` and `channels` tab in `Management UI`
2. Passs some `ClientProvidedName` to `ConnectionFactory` and restart `Producer`
3. Look again at stats in `Management UI`

### Update consumer
1. Replace `QueueDeclare` with `QueueDeclarePassive`
2. Slow down consumer to consuming `2/s`.
   1. Waiting for 500ms in callback will be enough.
3. Make sure that `Consumer` prints messages at rate `~2/s`
4. Stop `Consumer` process with `^C`.
5. Run it again.
   1. Does it continue consuming messages where it recently finished?
   2. Check how does `queue` statistics look like in `Management UI`

### Gdzie są **TE** wiadomości?
