# RabbitMQ Management HTTP API

http://127.0.0.1:8081/api/index.html

Most important points:
* Endpoints require HTTP basic authentication (using the standard RabbitMQ user database)
* As the default virtual host is called "/", this will need to be encoded as "%2F".

## TODO
We're going to publish messages to `ex.code.weather` topic exchange.
The exchange and sending messages will be done by `WeatherProducer` script.

**Run this script**
Set `PropertyGroup.StartupObject` to `WeatherProducer` in `Producer.csproj`.

Using `HTTP API` and `Postman` create several queues and bindings.
* `q.warsaw_precipitation`
* `q.weather_in_poland`
* `q.berlin_weather`

Use:
* `PUT /api/queues/vhost/name` for queue creation
* `PUT /api/bindings/vhost/e/exchange/q/queue` for binding creation
* `POST /api/queues/vhost/name/get` for checking what is inside the queue

> The binding key must also be in the same form. The logic behind the topic exchange is similar to a direct one - a message sent with a particular routing key will be delivered to all the queues that are bound with a matching binding key.
> However there are two important special cases for binding keys:
> * \* (star) can substitute for exactly one word.
> * \# (hash) can substitute for zero or more words.
