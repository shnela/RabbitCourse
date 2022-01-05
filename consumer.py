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

for method_frame, properties, body in channel.consume(QUEUE):
    # Display the message parts and acknowledge the message
    print(method_frame, properties, body)
    channel.basic_ack(method_frame.delivery_tag)

    sleep(MSG_PER_SECOND)

connection.close()
