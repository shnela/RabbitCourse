import pika
import sys


connection = pika.BlockingConnection()
channel = connection.channel()
channel.queue_declare('q42')

for i in range(int(sys.argv[1])):
    channel.basic_publish(exchange='', routing_key='q42',
                        body=f'Test message {i}'.encode())
connection.close()
