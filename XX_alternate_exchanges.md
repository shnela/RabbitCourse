# [Alternate exchanges](https://www.rabbitmq.com/ae.html)
> It is sometimes desirable to let clients handle messages that an exchange was unable to route (i.e. either because there were no bound queues or no matching bindings). Typical examples of this are
> * detecting when clients accidentally or maliciously publish messages that cannot be routed
> * "or else" routing semantics where some messages are handled specially and the rest by a generic handler
> 
> Alternate Exchange ("AE") is a feature that addresses these use cases.


## Configuration Using a Policy

### Example using `rabbitmqctl` for creating policy
```bash
rabbitmqctl set_policy AE "^my-direct$" '{"alternate-exchange": "ex.alternate-exchange"}' --apply-to exchanges
rabbitmqctl declare_queue
```
