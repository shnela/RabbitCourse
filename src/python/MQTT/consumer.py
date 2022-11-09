import paho.mqtt.subscribe as subscribe
import paho.mqtt.client as mqtt


def on_message_callback(client, userdata, message):
    print(message.payload)


client = mqtt.Client(client_id="mqtt-consumer", protocol=mqtt.MQTTv311)
client.username_pw_set(username="guest", password="guest")
client.connect(host="127.0.0.1", port=8883)
client.on_message = on_message_callback

client.subscribe("paho/+", qos=1)
client.loop_forever()
