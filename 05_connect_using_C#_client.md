# Connect using C# client

## What we'll do
* We'll see how to configure ` .NET/C# RabbitMQ Client Library `
* We'll `publish` and `consume` messages using code written in `C#`

[.NET/C# RabbitMQ Client Library](https://rabbitmq.com/dotnet.html)

## Tasks
**All below tasks are based no code located in:**
* **`cd src/Csharp/Producer`**
* and **`cd src/Csharp/Consumer`**

### Publish messages
1. Open new terminal: `Top menu` &rarr; `Terminal` &rarr; `New terminal`
2. Go to `Producer` catalog: `cd src/Csharp/Producer`
3. View `Producer.cs` file
4. Run code with `dotnet run`

### Consume messages
1. Open new terminal: `Top menu` &rarr; `Terminal` &rarr; `New terminal`
2. Go to `Consumer` catalog: `cd src/Csharp/Consumer`
3. View `Consumer.cs` file
4. Run code with `dotnet run`
   1. **The code will result in error because we're trying to declare wrong type of the queue**
   2. Comment out line responsible for declaring the queue and run the code.
   3. Now it should work, but assuming that the queue existss is a bad practice. Fix declaration of the queue, so it'll match declared queue parameters.
   4. In case of problems:
      1. you can compare file with `Consumer05.cs`
      2. or just run `Consumer05.cs` by changing `<StartupObject>` property in `consumer.csproj`

## More about RabbitMQ.Client
[ .NET/C# Client API Guide ](https://rabbitmq.com/dotnet-api-guide.html)
