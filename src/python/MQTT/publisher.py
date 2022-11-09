from time import sleep
import paho.mqtt.client as mqtt


client = mqtt.Client(client_id="mqtt-producer", protocol=mqtt.MQTTv311)
client.username_pw_set(username="guest", password="guest")
client.connect(host="127.0.0.1", port=8883)

for i in range(10):
    client.publish("paho/topic", payload=f"test{i}", qos=1)
    sleep(1)

client.disconnect()
