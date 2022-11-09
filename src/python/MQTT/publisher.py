import paho.mqtt.client as mqtt


client = mqtt.Client(protocol=mqtt.MQTTv311)
client.username_pw_set(username="mqtt-test", password="mqtt-test")
ret = client.connect(host="127.0.0.1", port=8882)
print(ret)


client.publish("paho/test/topic", payload="test", qos=1)
