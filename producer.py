import pika
import sys
from time import sleep

# command line parameters
MSG_PER_SECOND = 1 / int(sys.argv[1])
QUEUE = sys.argv[2]

# init connection
connection_params = pika.ConnectionParameters(port=5001)
connection = pika.BlockingConnection(parameters=connection_params)
channel = connection.channel()
channel.queue_declare(QUEUE)

i = 0
while True:
    channel.basic_publish(
        exchange='',
        routing_key=QUEUE,
        body=f'Test message {i}'.encode()
    )
    sleep(MSG_PER_SECOND)
    i += 1

connection.close()
